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
    public abstract class WebSocketClient : IDisposable
    {
        private readonly Uri _uri;
        private readonly ClientWebSocket _webSocket = new();
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        public WebSocketClient(string url)
        {
            _uri = new(url);
        }

        private async Task Connect()
        {
            if (_webSocket.State != WebSocketState.Open)
            {
                await _webSocket.ConnectAsync(_uri, _cancellationTokenSource.Token);
            }
        }

        private async Task SendCommandAsync(string message)
        {
            var sendBuffer = Encoding.UTF8.GetBytes(message);
            var sendSegment = new ArraySegment<byte>(sendBuffer);
            
            await Connect();
            await _webSocket.SendAsync(sendSegment, WebSocketMessageType.Text, true, _cancellationTokenSource.Token);
        }

        private async Task SendCommandAsync<T>(T message)
        {
            var data = ModelTypes<T>.TypeCode switch
            {
                TypeCode.Object => JsonSerializer.Serialize(message, _jsonSerializerOptions),
                _ => Convert.ChangeType(message, TypeCode.String)
            };

            await SendCommandAsync(data);
        }

        private async Task<string?> ReceiveResultAsync(int bufferSize = 4096)
        {
            var buffer = new byte[bufferSize];
            var segment = new ArraySegment<byte>(buffer);
            var sb = new StringBuilder();
            WebSocketReceiveResult result;

            do
            {
                result = await _webSocket.ReceiveAsync(segment, _cancellationTokenSource.Token);
                switch(result.MessageType)
                {
                    case WebSocketMessageType.Close:
                        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", _cancellationTokenSource.Token);
                        break;
                    case WebSocketMessageType.Text:
                        sb.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
                        break;
                }
            }
            while (!result.EndOfMessage);

            if (!result.CloseStatus.HasValue && result.MessageType == WebSocketMessageType.Text)
            {
                return sb.ToString();
            }

            return null;
        }
        
        private async Task<T?> ReceiveResultAsync<T>(int bufferSize = 4096)
        {
            var result = await ReceiveResultAsync(bufferSize);
            if (result != null)
            {
                return ModelTypes<T>.TypeCode switch
                {
                    TypeCode.Object => JsonSerializer.Deserialize<T>(result, _jsonSerializerOptions),
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
