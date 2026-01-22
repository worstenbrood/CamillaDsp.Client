using System;
using System.Collections.Generic;

namespace CamillaDsp.Client
{
    public enum Methods
    {
        // Get methods
        GetVersion,
        GetUpdateInterval,
        GetState,
        GetStopReason,
        GetCaptureRate,
        GetSignalRange,
        GetRateAdjust,
        GetBufferLevel,
        GetClippedSamples,
        ResetClippedSamples,
        GetProcessingLoad,
        GetStateFilePath,
        GetStateFileUpdated,
        GetSupportedDeviceTypes,
        GetCaptureSignalPeak,
        GetCaptureSignalRms,
        GetPlaybackSignalPeak,
        GetPlaybackSignalRms,
        GetCaptureSignalPeakSince,
        GetCaptureSignalRmsSince,
        GetPlaybackSignalPeakSince,
        GetPlaybackSignalRmsSince,
        GetCaptureSignalPeakSinceLast,
        GetCaptureSignalRmsSinceLast,
        GetPlaybackSignalPeakSinceLast,
        GetPlaybackSignalRmsSinceLast,
        GetSignalLevels,
        GetSignalLevelsSince,
        GetSignalLevelsSinceLast,
        GetSignalPeaksSinceStart,
        ResetSignalPeaksSinceStart,
        GetVolume,
        GetMute,
        ToggleMute,
        GetFaderVolume,
        GetAvailableCaptureDevices,
        GetAvailablePlaybackDevices,
        GetFaderMute,
        ToggleFaderMute,
        GetFaders,
        GetConfig,
        GetConfigJson,
        GetConfigTitle, 
        GetConfigDescription,
        GetConfigFilePath,
        GetPreviousConfig,
        Reload,
        Stop,
        Exit,
        ReadConfig,
        ReadConfigFile,
        ValidateConfig,
        
        // Set methods
        SetUpdateInterval,
        SetVolume,
        AdjustVolume,
        SetMute,
        SetFaderVolume,
        SetFaderExternalVolume,
        AdjustFaderVolume,
        SetFaderMute,
        SetConfigFilePath,
        SetConfig,
        SetConfigJson
    }

    public static class MethodExtensions
    {
        /// <summary>
        /// Convert <paramref name="method"/> and <paramref name="value"/> into 
        /// a method object eg. { 'method': value }".
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="value"/></typeparam>
        /// <param name="method">Method <see cref="Enum"/> value.</param>
        /// <param name="value">Parameter value.</param>
        /// <returns></returns>
        public static Dictionary<string, object?> ToMethodObject<T>(this Enum method, T value) =>
            new() { { method.ToString(), value } };
    }
}
