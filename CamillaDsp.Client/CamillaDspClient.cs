using System;
using System.Text.Json;
using System.Threading.Tasks;
using CamillaDsp.Client.Base;
using CamillaDsp.Client.Models;
using CamillaDsp.Client.Models.Config;

namespace CamillaDsp.Client
{
    /// <summary>
    /// https://github.com/HEnquist/camilladsp/blob/master/websocket.md
    /// </summary>
    public class CamillaDspClient : WebSocketClient
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="url">eg. ws://192.168.1.123:1234.</param>
        /// <param name="connect">If true, connects synchronously.</param>
        public CamillaDspClient(string url, bool connect = true) : base(url)
        {
            if (connect)
            {
                // Connect sync
                Connect()
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        /// <summary>
        /// Handle request without parameters
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        protected async Task<string?> HandleRequest(Enum method) =>
            await SendAsync($"\"{method}\"");

        /// <summary>
        /// Handle request with parameters
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        protected async Task<string?> HandleRequest<T>(Enum method, T value) =>
            await SendAsync(method.ToMethodObject(value));

        /// <summary>
        /// Handles the json response
        /// </summary>
        /// <typeparam name="T">Type of method value</typeparam>
        /// <param name="method">Method.</param>
        /// <param name="response">Response as a string.</param>
        /// <returns></returns>
        /// <exception cref="CamillaDspException"></exception>
        private static T? HandleResponse<T>(Enum method, string? response)
        {
            // Null check
            if (response == null || string.IsNullOrWhiteSpace(response))
            {
                return default;
            }

            // Parse json
            var json = JsonDocument.Parse(response);

            // Get method name
            var methodName = method.ToString();

            // Check for method name property
            if (json.RootElement.TryGetProperty(methodName, out JsonElement element))
            {
                // Deserialize the value of the method property 
                Result<T>? result = element.Deserialize<Result<T>>();
                if (result != null && result.Status != null)
                {
                    // Success
                    if (result.Status == Result.Ok)
                    {
                        // Return the received value
                        return result.Value;
                    }

                    // Error 
                    string message = $"{result.Status}";
                    if (result.Value != null)
                    {
                        // Append context of value if not null
                        message += $": {result.Value}";
                    }

                    // Throw exception
                    throw new CamillaDspException(methodName, message);
                }
            }

            // Check for invalid property
            else if (json.RootElement.TryGetProperty(C.Json.Invalid, out element))
            {
                // Deserialize error message
                var error = element.Deserialize<Error>();
                if (error != null)
                {
                    // Use returned error message in exception
                    throw new CamillaDspException(methodName, error.Message);
                }
            }

            return default;
        }

        /// <summary>
        /// Sends method <paramref name="method"/>.
        /// Returns the repsonse as a nullable of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of the return value.</typeparam>
        /// <param name="method"><see cref="Methods"/> to send.</param>
        /// <returns>nullable of type <typeparamref name="T"/>.</returns>
        protected async Task<T?> Send<T>(Enum method) => HandleResponse<T>
        (
            method, await HandleRequest(method)
        );

        /// <summary>
        /// Sends method <paramref name="method"/> with a parameter of 
        /// type <typeparamref name="T"/>.
        /// Returns the response as a nullable of type <typeparamref name="U"/>.
        /// </summary>
        /// <typeparam name="T">Type of the parameter.</typeparam>
        /// <typeparam name="U">Type of the return value.</typeparam>
        /// <param name="method"><see cref="Methods"/> to send.</param>
        /// <param name="value">Parameter value.</param>
        /// <returns></returns>
        protected async Task<U?> Send<T, U>(Enum method, T value) => HandleResponse<U>
        (
            method, await HandleRequest(method, value)
        );

        /// <summary>
        /// Sends method <paramref name="method"/> with a parameter of 
        /// type <typeparamref name="T"/>.
        /// Does not return a value.
        /// </summary>
        /// <typeparam name="T">Type of the parameter.</typeparam>
        /// <param name="method"><see cref="Methods"/>cto send.</param>
        /// <param name="value">Parameter value.</param>
        /// <returns></returns>
        protected async Task Send<T>(Enum method, T value) => HandleResponse<T>
        (
            method, await HandleRequest(method, value)
        );
       
        /// <summary>
        /// Read the CamillaDSP version. 
        /// </summary>
        /// <returns>Returns the version as a string, like 1.2.3.</returns>
        public async Task<string?> GetVersionAsync() => await Send<string>(Methods.GetVersion);

        /// <summary>
        /// Get the update interval in ms for capture rate and signal range. 
        /// </summary>
        public async Task<int?> GetUpdateIntervalAsync() => await Send<int>(Methods.GetUpdateInterval);

        /// <summary>
        /// Set the update interval in ms for capture rate and signal range.
        /// </summary>
        /// <param name="interval">Update interval in ms.</param>
        /// <returns></returns>
        public async Task SetUpdateIntervalAsync(int interval) => await Send(Methods.SetUpdateInterval, interval);

        /// <summary>
        /// Get the current state of the processing as a <see cref="State"/>.
        /// </summary>
        /// <returns><see cref="State"/></returns>
        public async Task<State?> GetState() => await Send<State>(Methods.GetState);

        /// <summary>
        /// Get the last reason why CamillaDSP stopped the processing.
        /// </summary>
        /// <returns><see cref="StopReason"/></returns>
        public async Task<StopReason?> GetStopReason() => await Send<StopReason>(Methods.GetStopReason);

        /// <summary>
        /// Get the measured sample rate of the capture device. 
        /// </summary>
        /// <returns>Sample rate</returns>
        public async Task<int?> GetCaptureRate() => await Send<int>(Methods.GetCaptureRate);

        /// <summary>
        /// Get the range of values in the last chunk. A value of 2.0 means full level (signal swings from -1.0 to +1.0) 
        /// </summary>
        /// <returns></returns>
        public async Task<float?> GetSignalRange() => await Send<float>(Methods.GetSignalRange);

        /// <summary>
        /// Get the adjustment factor applied to the asynchronous resampler. 
        /// </summary>
        /// <returns></returns>
        public async Task<float?> GetRateAdjust() => await Send<float>(Methods.GetRateAdjust);

        /// <summary>
        /// Get the current buffer level of the playback device when rate adjust is enabled, 
        /// returns zero otherwise. 
        /// </summary>
        /// <returns></returns>
        public async Task<int?> GetBufferLevel() => await Send<int>(Methods.GetBufferLevel);

        /// <summary>
        /// Get the number of clipped samples since the config was loaded. 
        /// </summary>
        /// <returns></returns>
        public async Task<int?> GetClippedSamples() => await Send<int>(Methods.GetClippedSamples);

        /// <summary>
        /// Reset the clipped samples counter to zero.
        /// </summary>
        /// <returns></returns>
        public async Task ResetClippedSamples() => await Send<object>(Methods.ResetClippedSamples);

        /// <summary>
        /// Get the current pipeline processing capacity utilization in percent.
        /// </summary>
        /// <returns></returns>
        public async Task<float?> GetProcessingLoad() => await Send<float>(Methods.GetProcessingLoad);

        /// <summary>
        /// Get the current state file path, returns null if no state file is used.
        /// </summary>
        /// <returns></returns>
        public async Task<string?> GetStateFilePath() => await Send<string>(Methods.GetStateFilePath);

        /// <summary>
        /// Check if all changes have been saved to the state file.
        /// </summary>
        /// <returns></returns>
        public async Task<bool?> GetStateFileUpdated() => await Send<bool>(Methods.GetStateFileUpdated);

        /// <summary>
        /// Read which playback and capture device types are supported. 
        /// </summary>
        /// <returns>
        /// Return a list containing two lists of strings (for playback and capture), 
        /// like [['File', 'Stdout', 'Alsa'], ['File', 'Stdin', 'Alsa']].
        /// </returns>
        public async Task<string[][]?> GetSupportedDeviceTypes() => await Send<string[][]>(Methods.GetSupportedDeviceTypes);

        /// <summary>
        /// Get the peak value in the last chunk on the capture side.
        /// </summary>
        /// <returns></returns>
        public async Task<float[]?> GetCaptureSignalPeak() => await Send<float[]>(Methods.GetCaptureSignalPeak);

        /// <summary>
        /// Get the RMS value in the last chunk on the capture side.
        /// </summary>
        /// <returns></returns>
        public async Task<float[]?> GetCaptureSignalRms() => await Send<float[]>(Methods.GetCaptureSignalRms);

        /// <summary>
        /// Get the peak value in the last chunk on the playback side.
        /// </summary>
        /// <returns></returns>
        public async Task<float[]?> GetPlaybackSignalPeak() => await Send<float[]>(Methods.GetPlaybackSignalPeak);

        /// <summary>
        /// Get the RMS value in the last chunk on the playback side.
        /// </summary>
        /// <returns></returns>
        public async Task<float[]?> GetPlaybackSignalRms() => await Send<float[]>(Methods.GetPlaybackSignalRms);

        /// <summary>
        /// Get the peak value measured during a specified time interval. 
        /// Takes a time in seconds (n.nn), and returns the values measured during the last n.nn seconds.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public async Task<float[]?> GetCaptureSignalPeakSince(float seconds) => 
            await Send<float, float[]>(Methods.GetCaptureSignalPeakSince, seconds);

        /// <summary>
        /// Get the RMS value measured during a specified time interval. 
        /// Takes a time in seconds (n.nn), and returns the values measured during the last n.nn seconds.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public async Task<float[]?> GetCaptureSignalRmsSince(float seconds) => 
            await Send<float, float[]>(Methods.GetCaptureSignalRmsSince, seconds);

        /// <summary>
        /// Get the peak value measured during a specified time interval. 
        /// Takes a time in seconds (n.nn), and returns the values measured during the last n.nn seconds.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public async Task<float[]?> GetPlaybackSignalPeakSince(float seconds) =>
            await Send<float, float[]>(Methods.GetPlaybackSignalPeakSince, seconds);

        /// <summary>
        /// Get RMS value measured during a specified time interval. 
        /// Takes a time in seconds (n.nn), and returns the values measured during the last n.nn seconds.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public async Task<float[]?> GetPlaybackSignalRmsSince(float seconds) => 
            await Send<float, float[]>(Methods.GetPlaybackSignalRmsSince, seconds);

        /// <summary>
        /// Get the peak value measured since the last call to the same command from the same client. 
        /// The first time a client calls this command it returns the values measured since the client connected. 
        /// If the command is repeated very quickly, it may happen that there is no new data. 
        /// The response is then an empty vector.
        /// </summary>
        /// <returns></returns>
        public async Task<float[]?> GetCaptureSignalPeakSinceLast() => 
            await Send<float[]>(Methods.GetCaptureSignalPeakSinceLast);

        /// <summary>
        /// Get the RMS value measured since the last call to the same command from the same client. 
        /// The first time a client calls this command it returns the values measured since the client connected. 
        /// If the command is repeated very quickly, it may happen that there is no new data. 
        /// The response is then an empty vector.
        /// </summary>
        /// <returns></returns>
        public async Task<float[]?> GetCaptureSignalRmsSinceLast() => 
            await Send<float[]>(Methods.GetCaptureSignalRmsSinceLast);

        /// <summary>
        /// Get the peak value measured since the last call to the same command from the same client. 
        /// The first time a client calls this command it returns the values measured since the client connected. 
        /// If the command is repeated very quickly, it may happen that there is no new data. 
        /// The response is then an empty vector.
        /// </summary>
        /// <returns></returns>
        public async Task<float[]?> GetPlaybackSignalPeakSinceLast() => 
            await Send<float[]>(Methods.GetPlaybackSignalPeakSinceLast);

        /// <summary>
        /// Get the RMS value measured since the last call to the same command from the same client. 
        /// The first time a client calls this command it returns the values measured since the client connected. 
        /// If the command is repeated very quickly, it may happen that there is no new data. 
        /// The response is then an empty vector.
        /// </summary>
        /// <returns></returns>
        public async Task<float[]?> GetPlaybackSignalRmsSinceLast() => 
            await Send<float[]>(Methods.GetPlaybackSignalRmsSinceLast);

        /// <summary>
        /// Combined commands for reading several levels with a single request. 
        /// These commands provide the same data as calling all the four commands in each of the groups above. 
        /// The values are returned as a json object with keys playback_peak, playback_rms, capture_peak and capture_rms.
        /// </summary>
        /// <returns></returns>
        public async Task<Signals?> GetSignalLevels() => 
            await Send<Signals>(Methods.GetSignalLevels);

        /// <summary>
        /// Combined commands for reading several levels with a single request. 
        /// These commands provide the same data as calling all the four commands in each of the groups above. 
        /// The values are returned as a json object with keys playback_peak, playback_rms, capture_peak and capture_rms.
        /// </summary>
        /// <returns></returns>
        public async Task<Signals?> GetSignalLevelsSince(float seconds) => 
            await Send<float, Signals>(Methods.GetSignalLevelsSince, seconds);

        /// <summary>
        /// Combined commands for reading several levels with a single request. 
        /// These commands provide the same data as calling all the four commands in each of the groups above. 
        /// The values are returned as a json object with keys playback_peak, playback_rms, capture_peak and capture_rms.
        /// </summary>
        /// <returns></returns>
        public async Task<Signals?> GetSignalLevelsSinceLast() => 
            await Send<Signals>(Methods.GetSignalLevelsSinceLast);

        /// <summary>
        /// Get the peak since start.
        /// </summary>
        /// <returns></returns>
        public async Task<SignalPeaks?> GetSignalPeaksSinceStart() => 
            await Send<SignalPeaks>(Methods.GetSignalPeaksSinceStart);

        /// <summary>
        /// Reset the peak values. Note that this resets the peak for all clients.
        /// </summary>
        /// <returns></returns>
        public async Task ResetSignalPeaksSinceStart() => 
            await Send<object>(Methods.ResetSignalPeaksSinceStart);

        /// <summary>
        /// Get the current volume setting in dB.
        /// </summary>
        /// <returns></returns>
        public async Task<float?> GetVolume() => 
            await Send<float>(Methods.GetVolume);

        /// <summary>
        /// Set the volume control to the given value in dB. Clamped to the range -150 to +50 dB.
        /// </summary>
        /// <param name="volumeDb"></param>
        /// <returns></returns>
        public async Task SetVolume(float volumeDb) => 
            await Send(Methods.SetVolume, volumeDb);

        /// <summary>
        /// Change the volume setting by the given number of dB, positive or negative. 
        /// The resulting volume is clamped to the range -150 to +50 dB. 
        /// The allowed range can be reduced by providing two more values, for minimum and maximum.
        /// </summary>
        /// <param name="deltaDb"></param>
        /// <returns></returns>
        public async Task AdjustVolume(float deltaDb) => 
            await Send(Methods.AdjustVolume, new[] { deltaDb });

        /// <summary>
        /// Change the volume setting by the given number of dB, positive or negative. 
        /// The resulting volume is clamped to the range -150 to +50 dB. 
        /// The allowed range can be reduced by providing two more values, for minimum and maximum.
        /// </summary>
        /// <param name="deltaDb">dB</param>
        /// <param name="min">Minimum</param>
        /// <param name="max">Maximum</param>
        /// <returns></returns>
        public async Task AdjustVolume(float deltaDb, float min, float max) =>
            await Send(Methods.AdjustVolume, new[] { deltaDb, min, max });

        /// <summary>
        /// Get the current mute setting.
        /// </summary>
        /// <returns></returns>
        public async Task<bool?> GetMute() => await Send<bool>(Methods.GetMute);

        /// <summary>
        /// Set muting to the given value.
        /// </summary>
        /// <param name="mute"></param>
        /// <returns></returns>
        public async Task SetMute(bool mute) => await Send(Methods.SetMute, mute);

        /// <summary>
        /// Toggle muting.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ToggleMute() => await Send<bool>(Methods.ToggleMute);

        /// <summary>
        /// Get the current volume setting in dB.
        /// </summary>
        /// <param name="faderIndex"></param>
        /// <returns></returns>
        public async Task<float[]?> GetFaderVolume(int faderIndex) => 
            await Send<int, float[]>(Methods.GetFaderVolume, faderIndex);

        /// <summary>
        /// Get a list of available capture devices.
        /// </summary>
        /// <param name="backend"></param>
        /// <returns></returns>
        public async Task<string[][]?> GetAvailableCaptureDevices(Backend backend) => 
            await Send<Backend, string[][]>(Methods.GetAvailableCaptureDevices, backend);

        /// <summary>
        /// Get a list of available playback devices.
        /// </summary>
        /// <param name="backend"></param>
        /// <returns></returns>
        public async Task<string[][]?> GetAvailablePlaybackDevices(Backend backend) => 
            await Send<Backend, string[][]>(Methods.GetAvailablePlaybackDevices, backend);

        /// <summary>
        /// Set the volume control to the given value in dB. Clamped to the range -150 to +50 dB.
        /// </summary>
        /// <param name="faderIndex"></param>
        /// <param name="volumeDb"></param>
        /// <returns></returns>
        public async Task SetFaderVolume(float faderIndex, float volumeDb) => 
            await Send(Methods.SetFaderVolume, new float[] {faderIndex, volumeDb});

        /// <summary>
        /// Special command for setting the volume when a Loudness filter is being combined with an external volume control 
        /// (without a Volume filter). 
        /// Clamped to the range -150 to +50 dB.
        /// </summary>
        /// <param name="faderIndex"></param>
        /// <param name="volumeDb"></param>
        /// <returns></returns>
        public async Task SetFaderExternalVolume(float faderIndex, float volumeDb) => 
            await Send(Methods.SetFaderExternalVolume, new float[] {faderIndex, volumeDb });

        /// <summary>
        /// Change the volume setting by the given number of dB, positive or negative. 
        /// The resulting volume is clamped to the range -150 to +50 dB. 
        /// The allowed range can be reduced by providing two more values, for minimum and maximum.
        /// </summary>
        /// <param name="faderIndex"></param>
        /// <param name="deltaDb"></param>
        /// <returns></returns>
        public async Task AdjustFaderVolume(int faderIndex, float deltaDb) => 
            await Send(Methods.AdjustFaderVolume, new object[] { faderIndex, deltaDb });

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
            await Send(Methods.AdjustFaderVolume, new object[] { faderIndex, deltaDb });
        }

        /// <summary>
        /// Get the current mute setting.
        /// </summary>
        /// <param name="faderIndex"></param>
        /// <returns></returns>
        public async Task<bool?> GetFaderMute(int faderIndex) => 
            await Send<int, bool>(Methods.GetFaderMute, faderIndex);

        /// <summary>
        /// Set muting to the given value.
        /// </summary>
        /// <param name="faderIndex"></param>
        /// <param name="mute"></param>
        /// <returns></returns>
        public async Task SetFaderMute(int faderIndex, bool mute) => 
            await Send(Methods.SetFaderMute, new object[] { faderIndex, mute });

        /// <summary>
        /// Toggle muting.
        /// </summary>
        /// <param name="faderIndex"></param>
        /// <returns></returns>
        public async Task<bool?> ToggleFaderMute(int faderIndex) => 
            (await Send<int, object[]>(Methods.ToggleFaderMute, faderIndex))?[1] as bool?;

        /// <summary>
        /// Read all faders.
        /// </summary>
        /// <returns></returns>
        public async Task<Fader[]?> GetFaders() => await Send<Fader[]>(Methods.GetFaders);

        /// <summary>
        /// Read the current configuration as yaml. 
        /// </summary>
        /// <returns></returns>
        public async Task<string?> GetConfig() => await Send<string>(Methods.GetConfig);

        /// <summary>
        /// Read the current configuration as json.
        /// </summary>
        /// <returns></returns>
        public async Task<string?> GetConfigJson() => await Send<string>(Methods.GetConfigJson);

        /// <summary>
        /// Read the current configuration as json.
        /// </summary>
        /// <returns></returns>
        public async Task<DspConfig?> GetConfigObject() 
        {
            var result = await GetConfigJson();
            return result != null ? Serializer.Deserialize<DspConfig>(result.Trim('\"')) : null;
        }

        /// <summary>
        /// Read the title from the current configuration. 
        /// </summary>
        /// <returns></returns>
        public async Task<string?> GetConfigTitle() => await Send<string>(Methods.GetConfigTitle);

        /// <summary>
        /// Read the description from the current configuration. 
        /// </summary>
        /// <returns></returns>
        public async Task<string?> GetConfigDescription() => await Send<string>(Methods.GetConfigDescription);

        /// <summary>
        /// Get name and path of current config file. 
        /// </summary>
        /// <returns></returns>
        public async Task<string?> GetConfigFilePath() => await Send<string>(Methods.GetConfigFilePath);

        /// <summary>
        /// Read the previous configuration as yaml.
        /// </summary>
        /// <returns></returns>
        public async Task<string?> GetPreviousConfig() => await Send<string>(Methods.GetPreviousConfig);

        /// <summary>
        /// Reload current config file (same as SIGHUP).
        /// </summary>
        /// <returns></returns>
        public async Task Reload() => await Send<object>(Methods.Reload);

        /// <summary>
        /// Stop processing and wait for a new config to be uploaded either with 
        /// <see cref="SetConfig"></see> or with <see cref="SetConfigFilePath"/>+<see cref="Reload"/>.
        /// </summary>
        /// <returns></returns>
        public async Task Stop() => await Send<object>(Methods.Stop);

        /// <summary>
        /// Stop processing and exit.
        /// </summary>
        /// <returns></returns>
        public async Task Exit() => await Send<object>(Methods.Exit);

        /// <summary>
        /// Provide a new config as a yaml string. Applied directly.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task SetConfig(string config) => await Send(Methods.SetConfig, config);

        /// <summary>
        ///  Provide a new config as a JSON string. Applied directly.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task SetConfigJson(string config) => await Send(Methods.SetConfigJson, config);

        /// <summary>
        ///  Provide a new config as a JSON string. Applied directly.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task SetConfigObject(DspConfig config)
        {
            await SetConfigJson(config.ToString());
        }
        
        /// <summary>
        /// Change config file name given as a string, not applied until <see cref="Reload"/> is called.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task SetConfigFilePath(string path) => await Send(Methods.SetConfigFilePath, path);

        /// <summary>
        /// Read the provided config (as a yaml string) and check it for yaml syntax errors. 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task ReadConfig(string config) => await Send<string, object>(Methods.ReadConfig, config);

        /// <summary>
        /// Same as ReadConfig but reads the config from the file at the given path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task ReadConfigFile(string path) => await Send<string, object>(Methods.ReadConfigFile, path);

        /// <summary>
        /// Same as ReadConfig but performs more extensive checks to ensure the configuration can be applied.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task ValidateConfig(string config) => await Send<string, object>(Methods.ValidateConfig, config);
    }
}
