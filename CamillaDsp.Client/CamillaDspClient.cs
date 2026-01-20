using CamillaDsp.Client.Models;
using System.Net.WebSockets;
using System.Text.Json;

namespace CamillaDsp.Client
{
    public class CamillaDspClient(string url) : WebSocketClient(url)
    {
        protected async Task<T?> Get<T>(GetMethods method)
        {
            var methodString = method.ToString();

            await SendCommandAsync($"\"{methodString}\"");
            var result = await ReceiveStringResultAsync();
            if (result != null)
            {
                var json = JsonDocument.Parse(result);
                if (json.RootElement.TryGetProperty(methodString, out var element))
                {
                    var dspResult = element.Deserialize<DSPResult<T?>>();
                    if (dspResult?.Result != DSPResultStatus.Ok)
                    {
                        throw new CamillaDspException(methodString, "Error");
                    }

                    return dspResult.Value;
                }
            }

            return default;
        }

        protected async Task Set<T>(SetMethods method, T value)
        {
            var methodString = method.ToString();
            var data = new Dictionary<string, object?> { { methodString, value } };
            await SendCommandAsync(data);
            
            var result = await ReceiveStringResultAsync();
            if (result != null)
            {
                var json = JsonDocument.Parse(result);
                if (json.RootElement.TryGetProperty(methodString, out var element))
                {
                    var dspResult = element.Deserialize<DSPResult<T?>>();
                    if (dspResult?.Result != DSPResultStatus.Ok)
                    {
                        throw new CamillaDspException(methodString, "Error");
                    }
                }
            }
        }

        public async Task<string?> GetVersionAsync()
        {
            return await Get<string>(GetMethods.GetVersion);
        }

        public async Task<int?> GetUpdateIntervalAsync()
        {
            return await Get<int>(GetMethods.GetUpdateInterval);
        }

        public async Task SetUpdateIntervalAsync(int interval)
        {
            await Set<int>(SetMethods.SetUpdateInterval, interval);
        }

        public async Task<State?> GetState()
        {
            return await Get<State>(GetMethods.GetState);
        }

        public async Task<StopReason?> GetStopReason()
        {
            return await Get<StopReason>(GetMethods.GetStopReason);
        }

        public async Task<int?> GetCaptureRate()
        {
            return await Get<int>(GetMethods.GetCaptureRate);
        }

        public async Task<string[][]?> GetSupportedDeviceTypes()
        {
            return await Get<string[][]>(GetMethods.GetSupportedDeviceTypes);
        }
    }
}
