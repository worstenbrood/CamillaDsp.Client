using System;
using System.IO;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CamillaDsp.Client.Core
{
    public abstract class WebSocketClient(string url) : IDisposable
    {
        private readonly Uri _uri = new(url);
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        protected CancellationToken CancellationToken => _cancellationTokenSource.Token;
        protected readonly SemaphoreSlim Semaphore = new(1, 1);
        protected readonly ClientWebSocket WebSocket = new();

        public async Task<WebSocketState> Connect()
        {
            if (WebSocket.State != WebSocketState.Open)
            {
                WebSocketState previousState = WebSocket.State;
                await WebSocket.ConnectAsync(_uri, CancellationToken);
                return previousState;
            }

            return WebSocketState.Open;
        }

        protected async Task SendStringCommandAsync(string message)
        {
            byte[] sendBuffer = Encoding.UTF8.GetBytes(message);
            var sendSegment = new ArraySegment<byte>(sendBuffer);
            
            await Connect();
            await WebSocket.SendAsync(sendSegment, WebSocketMessageType.Text, true, CancellationToken);
        }

        protected async Task SendCommandAsync<T>(T message)
        {
            string data = ModelTypes<T>.TypeCode switch
            {
                TypeCode.Object => Serializer.Serialize(message),
                _ => (string?)Convert.ChangeType(message, TypeCode.String)
            } ?? throw new ArgumentException("Message cannot be null", nameof(message));
            await SendStringCommandAsync(data);
        }

        protected async Task<string?> ReceiveStringResultAsync(int bufferSize = 4096)
        {
            var buffer = new byte[bufferSize];
            var segment = new ArraySegment<byte>(buffer);
            var sb = new StringBuilder();
            WebSocketReceiveResult result;

            do
            {
                result = await WebSocket.ReceiveAsync(segment, CancellationToken);
                switch (result.MessageType)
                {
                    case WebSocketMessageType.Close:
                        await WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, 
                            C.WebSockets.Closing, CancellationToken);
                        throw new IOException($"Connection closed. {result.CloseStatus} ({result.CloseStatusDescription})");

                    case WebSocketMessageType.Text:
                        sb.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
                        break;

                    case WebSocketMessageType.Binary:
                        throw new NotImplementedException("Received binary data. Expected text.");
                }
            }
            while (!result.EndOfMessage && result.MessageType != WebSocketMessageType.Close);

            return result.MessageType switch
            {
                WebSocketMessageType.Text => sb.ToString(),
                _ => throw new NotImplementedException(result.MessageType.ToString())
            };
        }

        protected async Task<T?> ReceiveResultAsync<T>(int bufferSize = 4096)
        {
            string? result = await ReceiveStringResultAsync(bufferSize);
            if (result != null)
            {
                return ModelTypes<T>.TypeCode switch
                {
                    TypeCode.Object => Serializer.Deserialize<T>(result),
                    _ => (T)Convert.ChangeType(result, ModelTypes<T>.TypeCode),
                };
            }

            return default;
        }

        private T Locked<T>(Func<T> func)
        {
            Semaphore.Wait(CancellationToken);
            try
            {
                return func();
            }
            finally
            {
                // Release lock
                Semaphore.Release();
            }
        }

        public async Task<string?> SendAsync(string message) =>
            await Locked(async () =>
            {
                await SendCommandAsync(message);
                return await ReceiveStringResultAsync();
            });


        public async Task<T?> SendAsync<T>(string message) =>
            await Locked(async () =>
            {
                await SendCommandAsync(message);
                return await ReceiveResultAsync<T>();
            });

        public async Task<string?> SendAsync<T>(T message) =>
            await Locked(async () =>
            {
                await SendCommandAsync(message);
                return await ReceiveStringResultAsync();
            });
            
        public async Task<U?> SendAsync<T,U>(T message)
        {
            // Lock
            Semaphore.Wait(CancellationToken);
            try
            {
                await SendCommandAsync(message);
                return await ReceiveResultAsync<U>();
            }
            finally
            {
                // Release lock
                Semaphore.Release();
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            WebSocket?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
