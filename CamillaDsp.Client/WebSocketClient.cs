using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CamillaDsp.Client.Models;

namespace CamillaDsp.Client
{
    public abstract class WebSocketClient(string url) : IDisposable
    {
        private readonly Uri _uri = new(url);
        private readonly ClientWebSocket _webSocket = new();
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        protected CancellationToken CancellationToken => _cancellationTokenSource.Token;
        
        protected readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        public async Task<WebSocketState> Connect()
        {
            if (_webSocket.State != WebSocketState.Open)
            {
                var previousState = _webSocket.State;
                await _webSocket.ConnectAsync(_uri, CancellationToken);
                return previousState;
            }

            return WebSocketState.Open;
        }

        protected async Task SendStringCommandAsync(string message)
        {
            var sendBuffer = Encoding.UTF8.GetBytes(message);
            var sendSegment = new ArraySegment<byte>(sendBuffer);
            
            await Connect();
            await _webSocket.SendAsync(sendSegment, WebSocketMessageType.Text, true, CancellationToken);
        }

        protected async Task SendCommandAsync<T>(T message)
        {
            var data = ModelTypes<T>.TypeCode switch
            {
                TypeCode.Object => JsonSerializer.Serialize(message, JsonSerializerOptions),
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
                        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken);
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
            var result = await ReceiveStringResultAsync(bufferSize);
            if (result != null)
            {
                return ModelTypes<T>.TypeCode switch
                {
                    TypeCode.Object => JsonSerializer.Deserialize<T>(result, JsonSerializerOptions),
                    _ => (T)Convert.ChangeType(result, ModelTypes<T>.TypeCode),
                };
            }

            return default;
        }

        public async Task<T?> SendAsync<T>(string message)
        {
            await SendCommandAsync(message);
            return await ReceiveResultAsync<T>();
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _webSocket?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
