using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamillaDsp.Client
{
    public enum GetMethods
    {
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
        GetAvailablePlaybackDevices
    }

    public enum SetMethods
    {
        SetUpdateInterval,
        SetVolume,
        AdjustVolume,
        SetMute,
        SetFaderVolume
    }
}
