using CamillaDsp.Client.Models;
using CamillaDsp.Client.Models.Config;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace CamillaDsp.Client
{
    /// <summary>
    /// https://github.com/HEnquist/camilladsp/blob/master/websocket.md
    /// </summary>
    public class CamillaDspClient : WebSocketClient
    {
        /// <param name="url"></param>
        public CamillaDspClient(string url, bool connect = true) : base(url)
        {
            if (connect)
            {
                // Connect sync
                Connect().ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        private static T? HandleResult<T>(string methodString, string? result)
        {
            // Null check
            if (result == null)
            {
                return default;
            }

            var json = JsonDocument.Parse(result);

            // We received an error
            if (json.RootElement.TryGetProperty("Invalid", out JsonElement element))
            {
                var error = element.Deserialize<Error>();
                if (error != null)
                {
                    throw new CamillaDspException(methodString, error.Message);
                }
            }
            // We received a reply
            else if (json.RootElement.TryGetProperty(methodString, out element))
            {
                var dspResult = element.Deserialize<DSPResult<T?>>();
                if (dspResult?.Result != DSPResultStatus.Ok)
                {
                    throw new CamillaDspException(methodString, "Error");
                }

                return dspResult.Value;
            }

            return default;
        }

        protected async Task<T?> Get<T>(GetMethods method)
        {
            var methodString = method.ToString();
            string? result = await SendAsync($"\"{methodString}\"");
            return HandleResult<T>(methodString, result);
        }

        protected async Task<U?> Get<T, U>(GetMethods method, T value)
        {
            var methodString = method.ToString();
            var data = new Dictionary<string, object?> { { methodString, value } };
            string? result = await SendAsync(data);
            return HandleResult<U>(methodString, result);
        }

        protected async Task Set<T>(SetMethods method, T value)
        {
            var methodString = method.ToString();
            var data = new Dictionary<string, object?> { { methodString, value } };
            string? result = await SendAsync(data);
            HandleResult<T>(methodString, result);
        }

        protected async Task<U?> Set<T, U>(SetMethods method, T value)
        {
            var methodString = method.ToString();
            var data = new Dictionary<string, object?> { { methodString, value } };
            string? result = await SendAsync(data);
            return HandleResult<U>(methodString, result);
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
        public async Task SetUpdateIntervalAsync(int interval) => await Set(SetMethods.SetUpdateInterval, interval);

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

        /// <summary>
        /// Get the peak value measured since the last call to the same command from the same client. 
        /// The first time a client calls this command it returns the values measured since the client connected. 
        /// If the command is repeated very quickly, it may happen that there is no new data. 
        /// The response is then an empty vector.
        /// </summary>
        /// <returns></returns>
        public async Task<float[]?> GetCaptureSignalPeakSinceLast() => 
            await Get<float[]>(GetMethods.GetCaptureSignalPeakSinceLast);

        /// <summary>
        /// Get the RMS value measured since the last call to the same command from the same client. 
        /// The first time a client calls this command it returns the values measured since the client connected. 
        /// If the command is repeated very quickly, it may happen that there is no new data. 
        /// The response is then an empty vector.
        /// </summary>
        /// <returns></returns>
        public async Task<float[]?> GetCaptureSignalRmsSinceLast() => 
            await Get<float[]>(GetMethods.GetCaptureSignalRmsSinceLast);

        /// <summary>
        /// Get the peak value measured since the last call to the same command from the same client. 
        /// The first time a client calls this command it returns the values measured since the client connected. 
        /// If the command is repeated very quickly, it may happen that there is no new data. 
        /// The response is then an empty vector.
        /// </summary>
        /// <returns></returns>
        public async Task<float[]?> GetPlaybackSignalPeakSinceLast() => 
            await Get<float[]>(GetMethods.GetPlaybackSignalPeakSinceLast);

        /// <summary>
        /// Get the RMS value measured since the last call to the same command from the same client. 
        /// The first time a client calls this command it returns the values measured since the client connected. 
        /// If the command is repeated very quickly, it may happen that there is no new data. 
        /// The response is then an empty vector.
        /// </summary>
        /// <returns></returns>
        public async Task<float[]?> GetPlaybackSignalRmsSinceLast() => 
            await Get<float[]>(GetMethods.GetPlaybackSignalRmsSinceLast);

        /// <summary>
        /// Combined commands for reading several levels with a single request. 
        /// These commands provide the same data as calling all the four commands in each of the groups above. 
        /// The values are returned as a json object with keys playback_peak, playback_rms, capture_peak and capture_rms.
        /// </summary>
        /// <returns></returns>
        public async Task<Signals?> GetSignalLevels() => 
            await Get<Signals>(GetMethods.GetSignalLevels);

        /// <summary>
        /// Combined commands for reading several levels with a single request. 
        /// These commands provide the same data as calling all the four commands in each of the groups above. 
        /// The values are returned as a json object with keys playback_peak, playback_rms, capture_peak and capture_rms.
        /// </summary>
        /// <returns></returns>
        public async Task<Signals?> GetSignalLevelsSince(float seconds) => 
            await Get<float, Signals>(GetMethods.GetSignalLevelsSince, seconds);

        /// <summary>
        /// Combined commands for reading several levels with a single request. 
        /// These commands provide the same data as calling all the four commands in each of the groups above. 
        /// The values are returned as a json object with keys playback_peak, playback_rms, capture_peak and capture_rms.
        /// </summary>
        /// <returns></returns>
        public async Task<Signals?> GetSignalLevelsSinceLast() => 
            await Get<Signals>(GetMethods.GetSignalLevelsSinceLast);

        /// <summary>
        /// Get the peak since start.
        /// </summary>
        /// <returns></returns>
        public async Task<SignalPeaks?> GetSignalPeaksSinceStart() => 
            await Get<SignalPeaks>(GetMethods.GetSignalPeaksSinceStart);

        /// <summary>
        /// Reset the peak values. Note that this resets the peak for all clients.
        /// </summary>
        /// <returns></returns>
        public async Task ResetSignalPeaksSinceStart() => 
            await Get<object>(GetMethods.ResetSignalPeaksSinceStart);

        /// <summary>
        /// Get the current volume setting in dB.
        /// </summary>
        /// <returns></returns>
        public async Task<float?> GetVolume() => 
            await Get<float>(GetMethods.GetVolume);

        /// <summary>
        /// Set the volume control to the given value in dB. Clamped to the range -150 to +50 dB.
        /// </summary>
        /// <param name="volumeDb"></param>
        /// <returns></returns>
        public async Task SetVolume(float volumeDb) => 
            await Set(SetMethods.SetVolume, volumeDb);

        /// <summary>
        /// Change the volume setting by the given number of dB, positive or negative. 
        /// The resulting volume is clamped to the range -150 to +50 dB. 
        /// The allowed range can be reduced by providing two more values, for minimum and maximum.
        /// </summary>
        /// <param name="deltaDb"></param>
        /// <returns></returns>
        public async Task AdjustVolume(float[] deltaDb) => 
            await Set(SetMethods.AdjustVolume, deltaDb);

        /// <summary>
        /// Get the current mute setting.
        /// </summary>
        /// <returns></returns>
        public async Task<bool?> GetMute() => await Get<bool>(GetMethods.GetMute);

        /// <summary>
        /// Set muting to the given value.
        /// </summary>
        /// <param name="mute"></param>
        /// <returns></returns>
        public async Task SetMute(bool mute) => await Set(SetMethods.SetMute, mute);

        /// <summary>
        /// Toggle muting.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ToggleMute() => await Get<bool>(GetMethods.ToggleMute);

        /// <summary>
        /// Get the current volume setting in dB.
        /// </summary>
        /// <param name="faderIndex"></param>
        /// <returns></returns>
        public async Task<float[]?> GetFaderVolume(int faderIndex) => 
            await Get<int, float[]>(GetMethods.GetFaderVolume, faderIndex);

        /// <summary>
        /// Get a list of available capture devices.
        /// </summary>
        /// <param name="backend"></param>
        /// <returns></returns>
        public async Task<string[][]?> GetAvailableCaptureDevices(Backend backend) => 
            await Get<Backend, string[][]>(GetMethods.GetAvailableCaptureDevices, backend);

        /// <summary>
        /// Get a list of available playback devices.
        /// </summary>
        /// <param name="backend"></param>
        /// <returns></returns>
        public async Task<string[][]?> GetAvailablePlaybackDevices(Backend backend) => 
            await Get<Backend, string[][]>(GetMethods.GetAvailablePlaybackDevices, backend);

        /// <summary>
        /// Set the volume control to the given value in dB. Clamped to the range -150 to +50 dB.
        /// </summary>
        /// <param name="faderIndex"></param>
        /// <param name="volumeDb"></param>
        /// <returns></returns>
        public async Task SetFaderVolume(float faderIndex, float volumeDb) => 
            await Set(SetMethods.SetFaderVolume, new float[] {faderIndex, volumeDb});

        /// <summary>
        /// Special command for setting the volume when a Loudness filter is being combined with an external volume control 
        /// (without a Volume filter). 
        /// Clamped to the range -150 to +50 dB.
        /// </summary>
        /// <param name="faderIndex"></param>
        /// <param name="volumeDb"></param>
        /// <returns></returns>
        public async Task SetFaderExternalVolume(float faderIndex, float volumeDb) => 
            await Set(SetMethods.SetFaderExternalVolume, new float[] {faderIndex, volumeDb });

        /// <summary>
        /// Change the volume setting by the given number of dB, positive or negative. 
        /// The resulting volume is clamped to the range -150 to +50 dB. 
        /// The allowed range can be reduced by providing two more values, for minimum and maximum.
        /// </summary>
        /// <param name="faderIndex"></param>
        /// <param name="deltaDb"></param>
        /// <returns></returns>
        public async Task AdjustFaderVolume(int faderIndex, float deltaDb) => 
            await Set(SetMethods.AdjustFaderVolume, new object[] { faderIndex, deltaDb });

        /// <summary>
        /// Change the volume setting by the given number of dB, positive or negative. 
        /// The resulting volume is clamped to the range -150 to +50 dB. 
        /// The allowed range can be reduced by providing two more values, for minimum and maximum.
        /// </summary>
        /// <param name="faderIndex"></param>
        /// <param name="deltaDb"></param>
        /// <returns></returns>
        public async Task AdjustFaderVolume(int faderIndex, float[] deltaDb)
        {
            await Set(SetMethods.AdjustFaderVolume, new object[] { faderIndex, deltaDb });
        }

        /// <summary>
        /// Get the current mute setting.
        /// </summary>
        /// <param name="faderIndex"></param>
        /// <returns></returns>
        public async Task<bool?> GetFaderMute(int faderIndex) => 
            await Get<int, bool>(GetMethods.GetFaderMute, faderIndex);

        /// <summary>
        /// Set muting to the given value.
        /// </summary>
        /// <param name="faderIndex"></param>
        /// <param name="mute"></param>
        /// <returns></returns>
        public async Task SetFaderMute(int faderIndex, bool mute) => 
            await Set(SetMethods.SetFaderMute, new object[] { faderIndex, mute });

        /// <summary>
        /// Toggle muting.
        /// </summary>
        /// <param name="faderIndex"></param>
        /// <returns></returns>
        public async Task<bool?> ToggleFaderMute(int faderIndex) => 
            (await Get<int, object[]>(GetMethods.ToggleFaderMute, faderIndex))?[1] as bool?;

        /// <summary>
        /// Read all faders.
        /// </summary>
        /// <returns></returns>
        public async Task<Fader[]?> GetFaders() => await Get<Fader[]>(GetMethods.GetFaders);

        /// <summary>
        /// Read the current configuration as yaml. 
        /// </summary>
        /// <returns></returns>
        public async Task<string?> GetConfig() => await Get<string>(GetMethods.GetConfig);

        /// <summary>
        /// Read the current configuration as json.
        /// </summary>
        /// <returns></returns>
        public async Task<string?> GetConfigJson() => await Get<string>(GetMethods.GetConfigJson);

        /// <summary>
        /// Read the current configuration as json.
        /// </summary>
        /// <returns></returns>
        public async Task<DspConfig?> GetConfigObject() 
        {
            var result = await GetConfigJson();
            if (result == null)
            {
                return null;
            }
            return JsonSerializer.Deserialize<DspConfig>(result.Trim('\"'), JsonSerializerOptions);
        }

        /// <summary>
        /// Read the title from the current configuration. 
        /// </summary>
        /// <returns></returns>
        public async Task<string?> GetConfigTitle() => await Get<string>(GetMethods.GetConfigTitle);

        /// <summary>
        /// Read the description from the current configuration. 
        /// </summary>
        /// <returns></returns>
        public async Task<string?> GetConfigDescription() => await Get<string>(GetMethods.GetConfigDescription);

        /// <summary>
        /// Get name and path of current config file. 
        /// </summary>
        /// <returns></returns>
        public async Task<string?> GetConfigFilePath() => await Get<string>(GetMethods.GetConfigFilePath);

        /// <summary>
        /// Read the previous configuration as yaml.
        /// </summary>
        /// <returns></returns>
        public async Task<string?> GetPreviousConfig() => await Get<string>(GetMethods.GetPreviousConfig);

        /// <summary>
        /// Reload current config file (same as SIGHUP).
        /// </summary>
        /// <returns></returns>
        public async Task Reload() => await Get<object>(GetMethods.Reload);

        /// <summary>
        /// Stop processing and wait for a new config to be uploaded either with 
        /// <see cref="SetConfig"></see> or with <see cref="SetConfigFilePath"/>+<see cref="Reload"/>.
        /// </summary>
        /// <returns></returns>
        public async Task Stop() => await Get<object>(GetMethods.Stop);

        /// <summary>
        /// Stop processing and exit.
        /// </summary>
        /// <returns></returns>
        public async Task Exit() => await Get<object>(GetMethods.Exit);

        /// <summary>
        /// Provide a new config as a yaml string. Applied directly.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task SetConfig(string config) => await Set(SetMethods.SetConfig, config);

        /// <summary>
        ///  Provide a new config as a JSON string. Applied directly.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task SetConfigJson(string config) => await Set(SetMethods.SetConfigJson, config);

        /// <summary>
        ///  Provide a new config as a JSON string. Applied directly.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task SetConfigObject(DspConfig config)
        {
            var json = JsonSerializer.Serialize(config, JsonSerializerOptions);
            await Set(SetMethods.SetConfigJson, json);
        }
        
        /// <summary>
        /// Change config file name given as a string, not applied until <see cref="Reload"/> is called.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task SetConfigFilePath(string path) => await Set(SetMethods.SetConfigFilePath, path);

        /// <summary>
        /// Read the provided config (as a yaml string) and check it for yaml syntax errors. 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task ReadConfig(string config) => await Get<string, object>(GetMethods.ReadConfig, config);

        /// <summary>
        /// Same as ReadConfig but reads the config from the file at the given path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task ReadConfigFile(string path) => await Get<string, object>(GetMethods.ReadConfigFile, path);

        /// <summary>
        /// Same as ReadConfig but performs more extensive checks to ensure the configuration can be applied.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task ValidateConfig(string config) => await Get<string, object>(GetMethods.ValidateConfig, config);
    }
}
