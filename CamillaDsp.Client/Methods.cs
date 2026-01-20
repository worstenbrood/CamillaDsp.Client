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
        GetPlaybackSignalRmsSince
    }

    public enum SetMethods
    {
        SetUpdateInterval,
        
    }
}
