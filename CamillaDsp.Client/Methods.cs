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
        GetSupportedDeviceTypes
    }

    public enum SetMethods
    {
        SetUpdateInterval
    }
}
