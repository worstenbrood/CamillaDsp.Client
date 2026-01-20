using CamillaDsp.Client.Models;
using System.Net.WebSockets;
using System.Text.Json;

namespace CamillaDsp.Client
{
    /// <summary>
    /// https://github.com/HEnquist/camilladsp/blob/master/websocket.md
    /// </summary>
    /// <param name="url"></param>
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
                        throw new CamillaDspException(method, "Error");
                    }

                    return dspResult.Value;
                }
            }

            return default;
        }

        protected async Task<U?> Get<T, U>(GetMethods method, T value)
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
                    var dspResult = element.Deserialize<DSPResult<U?>>();
                    if (dspResult?.Result != DSPResultStatus.Ok)
                    {
                        throw new CamillaDspException(method, "Error");
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
                        throw new CamillaDspException(method, "Error");
                    }
                }
            }
        }

        /// <summary>
        /// Read the CamillaDSP version. 
        /// </summary>
        /// <returns>Returns the version as a string, like 1.2.3.</returns>
        public async Task<string?> GetVersionAsync() => await Get<string>(GetMethods.GetVersion);

        /// <summary>
        /// Get the update interval in ms for capture rate and signal range. 
        /// </summary>
        /// <returns>
        /// Return a list containing two lists of strings (for playback and capture), 
        /// like [['File', 'Stdout', 'Alsa'], ['File', 'Stdin', 'Alsa']].
        /// </returns>
        public async Task<int?> GetUpdateIntervalAsync() => await Get<int>(GetMethods.GetUpdateInterval);

        /// <summary>
        /// Set the update interval in ms for capture rate and signal range.
        /// </summary>
        /// <param name="interval">Update interval in ms.</param>
        /// <returns></returns>
        public async Task SetUpdateIntervalAsync(int interval) => await Set<int>(SetMethods.SetUpdateInterval, interval);

        /// <summary>
        /// Get the current state of the processing as a <see cref="State"/>.
        /// </summary>
        /// <returns><see cref="State"/></returns>
        public async Task<State?> GetState() => await Get<State>(GetMethods.GetState);

        /// <summary>
        /// Get the last reason why CamillaDSP stopped the processing.
        /// </summary>
        /// <returns><see cref="StopReason"/></returns>
        public async Task<StopReason?> GetStopReason() => await Get<StopReason>(GetMethods.GetStopReason);

        /// <summary>
        /// Get the measured sample rate of the capture device. 
        /// </summary>
        /// <returns>Sample rate</returns>
        public async Task<int?> GetCaptureRate() => await Get<int>(GetMethods.GetCaptureRate);

        /// <summary>
        /// Get the range of values in the last chunk. A value of 2.0 means full level (signal swings from -1.0 to +1.0) 
        /// </summary>
        /// <returns></returns>
        public async Task<float?> GetSignalRange() => await Get<float>(GetMethods.GetSignalRange);

        /// <summary>
        /// Get the adjustment factor applied to the asynchronous resampler. 
        /// </summary>
        /// <returns></returns>
        public async Task<float?> GetRateAdjust() => await Get<float>(GetMethods.GetRateAdjust);

        /// <summary>
        /// Get the current buffer level of the playback device when rate adjust is enabled, 
        /// returns zero otherwise. 
        /// </summary>
        /// <returns></returns>
        public async Task<int?> GetBufferLevel() => await Get<int>(GetMethods.GetBufferLevel);

        /// <summary>
        /// Get the number of clipped samples since the config was loaded. 
        /// </summary>
        /// <returns></returns>
        public async Task<int?> GetClippedSamples() => await Get<int>(GetMethods.GetClippedSamples);

        /// <summary>
        /// Reset the clipped samples counter to zero.
        /// </summary>
        /// <returns></returns>
        public async Task ResetClippedSamples() => await Get<object>(GetMethods.ResetClippedSamples);

        /// <summary>
        /// Get the current pipeline processing capacity utilization in percent.
        /// </summary>
        /// <returns></returns>
        public async Task<float?> GetProcessingLoad() => await Get<float>(GetMethods.GetProcessingLoad);

        /// <summary>
        /// Get the current state file path, returns null if no state file is used.
        /// </summary>
        /// <returns></returns>
        public async Task<string?> GetStateFilePath() => await Get<string>(GetMethods.GetStateFilePath);

        /// <summary>
        /// Check if all changes have been saved to the state file.
        /// </summary>
        /// <returns></returns>
        public async Task<bool?> GetStateFileUpdated() => await Get<bool>(GetMethods.GetStateFileUpdated);

        /// <summary>
        /// Read which playback and capture device types are supported. 
        /// </summary>
        /// <returns>
        /// Return a list containing two lists of strings (for playback and capture), 
        /// like [['File', 'Stdout', 'Alsa'], ['File', 'Stdin', 'Alsa']].
        /// </returns>
        public async Task<string[][]?> GetSupportedDeviceTypes() => await Get<string[][]>(GetMethods.GetSupportedDeviceTypes);

        /// <summary>
        /// Get the peak value in the last chunk on the capture side.
        /// </summary>
        /// <returns></returns>
        public async Task<float[]?> GetCaptureSignalPeak() => await Get<float[]>(GetMethods.GetCaptureSignalPeak);

        /// <summary>
        /// Get the RMS value in the last chunk on the capture side.
        /// </summary>
        /// <returns></returns>
        public async Task<float[]?> GetCaptureSignalRms() => await Get<float[]>(GetMethods.GetCaptureSignalRms);

        /// <summary>
        /// Get the peak value in the last chunk on the playback side.
        /// </summary>
        /// <returns></returns>
        public async Task<float[]?> GetPlaybackSignalPeak() => await Get<float[]>(GetMethods.GetPlaybackSignalPeak);

        /// <summary>
        /// Get the RMS value in the last chunk on the playback side.
        /// </summary>
        /// <returns></returns>
        public async Task<float[]?> GetPlaybackSignalRms() => await Get<float[]>(GetMethods.GetPlaybackSignalRms);

        /// <summary>
        /// Get the peak value measured during a specified time interval. 
        /// Takes a time in seconds (n.nn), and returns the values measured during the last n.nn seconds.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public async Task<float[]?> GetCaptureSignalPeakSince(float seconds) => 
            await Get<float, float[]>(GetMethods.GetCaptureSignalPeakSince, seconds);

        /// <summary>
        /// Get the RMS value measured during a specified time interval. 
        /// Takes a time in seconds (n.nn), and returns the values measured during the last n.nn seconds.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public async Task<float[]?> GetCaptureSignalRmsSince(float seconds) => 
            await Get<float, float[]>(GetMethods.GetCaptureSignalRmsSince, seconds);

        /// <summary>
        /// Get the peak value measured during a specified time interval. 
        /// Takes a time in seconds (n.nn), and returns the values measured during the last n.nn seconds.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public async Task<float[]?> GetPlaybackSignalPeakSince(float seconds) =>
            await Get<float, float[]>(GetMethods.GetPlaybackSignalPeakSince, seconds);

        /// <summary>
        /// Get RMS value measured during a specified time interval. 
        /// Takes a time in seconds (n.nn), and returns the values measured during the last n.nn seconds.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public async Task<float[]?> GetPlaybackSignalRmsSince(float seconds) => 
            await Get<float, float[]>(GetMethods.GetPlaybackSignalRmsSince, seconds);
    }
}
