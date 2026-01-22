using CamillaDsp.Client.Models.Config.AudioDevices;

namespace CamillaDsp.Client.Factories
{
    public static class DeviceFactory
    {
        public static AudioDevice CreateSignal(SignalTypes type, int frequency, float level) =>
            new()
            {
                Type = DeviceTypes.SignalGenerator,
                Signal = new()
                {
                    Type = type,
                    Frequency = frequency,
                    Level = level,
                }
            };
    }
}
