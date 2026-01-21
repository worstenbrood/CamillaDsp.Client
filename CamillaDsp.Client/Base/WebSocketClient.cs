using System;
using System.Text;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;

namespace CamillaDsp.Client.Base
{
    public abstract class WebSocketClient(string url) : IDisposable
    {
        private readonly Uri _uri = new(url);
        private readonly ClientWebSocket _webSocket = new();
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        protected CancellationToken CancellationToken => _cancellationTokenSource.Token;
        protected readonly SemaphoreSlim Semaphore = new(1, 1);

        public async Task<WebSocketState> Connect()
        {
            if (_webSocket.State != WebSocketState.Open)
            {
                WebSocketState previousState = _webSocket.State;
                await _webSocket.ConnectAsync(_uri, CancellationToken);
                return previousState;
            }

            return WebSocketState.Open;
        }

        protected async Task SendStringCommandAsync(string message)
        {
            byte[] sendBuffer = Encoding.UTF8.GetBytes(message);
            var sendSegment = new ArraySegment<byte>(sendBuffer);
            
            await Connect();
            await _webSocket.SendAsync(sendSegment, WebSocketMessageType.Text, true, CancellationToken);
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
                result = await _webSocket.ReceiveAsync(segment, CancellationToken);
                switch(result.MessageType)
                {
                    case WebSocketMessageType.Close:
                        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, 
                            C.WebSockets.Closing, CancellationToken);
                        break;
                    case WebSocketMessageType.Text:
                        sb.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
                        break;
                }
            }
            while (!result.EndOfMessage && result.MessageType != WebSocketMessageType.Close);

            // Return string
            if (result.MessageType == WebSocketMessageType.Text)
            {
                return sb.ToString();
            }

            return null;
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


        public async Task<string?> SendAsync(string message)
        {
            // Lock
            Semaphore.Wait(CancellationToken);
            try
            {
                await SendCommandAsync(message);
                return await ReceiveStringResultAsync();
            }
            finally
            {
                // Release lock
                Semaphore.Release();
            }
        }

        public async Task<T?> SendAsync<T>(string message)
        {
            // Lock
            Semaphore.Wait(CancellationToken);
            try
            {
                await SendCommandAsync(message);
                return await ReceiveResultAsync<T>();
            }
            finally
            {
                // Release lock
                Semaphore.Release();
            }
        }

        public async Task<string?> SendAsync<T>(T message)
        {
            // Lock
            Semaphore.Wait(CancellationToken);
            try
            {
                await SendCommandAsync(message);
                return await ReceiveStringResultAsync();
            }
            finally
            {
                // Release lock
                Semaphore.Release();
            }
        }

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
            _webSocket?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
