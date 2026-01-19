using CamillaDsp.Client.Models;
using System.Net.WebSockets;
using System.Text.Json;

namespace CamillaDsp.Client
{
    public class CamillaDspClient(string url) : WebSocketClient(url)
    {
        public async Task<string?> GetVersionAsync()
        {
            var result = (await SendAsync<Models.Version>($"\"{Methods.GetVersion}\""));
            if (result?.GetVersion?.Result == CDSPResultStatus.Ok)
            { 
                return result.GetVersion.Value;
            }

            throw new CamillaDspException(Methods.GetVersion, "Error");
        }

        public async Task<int?> GetUpdateIntervalAsync()
        {
            var result = (await SendAsync<Models.UpdateInterval>($"\"{Methods.GetUpdateInterval}\""));
            if (result?.GetUpdateInterval?.Result == CDSPResultStatus.Ok)
            {
                return result.GetUpdateInterval.Value;
            }

            throw new CamillaDspException(Methods.GetUpdateInterval, "Error");
        }
    }
}
