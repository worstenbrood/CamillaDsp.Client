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

        public static AudioDevice CreateRawFile(string filename, Formats format, int channels = 2,
             int? extraSamples = null, int? skipBytes = null,int?  readBytes = null)
        {
            return new()
            {
                Type = DeviceTypes.RawFile,
                Filename = filename,
                Format = format,
                Channels = channels,
                ExtraSamples = extraSamples,
                SkipBytes = skipBytes,
                ReadBytes = readBytes
            };
        }

        public static AudioDevice CreateFile(string filename, Formats format, int channels = 2,
             bool? waveHeader = null)
        {
            return new()
            {
                Type = DeviceTypes.File,
                Filename = filename,
                Format = format,
                Channels = channels,
                WaveHeader = waveHeader
            };
        }

        public static AudioDevice CreateWavFile(string filename, Formats? format = null, int channels = 2)
        {
            return new()
            {
                Type = DeviceTypes.WavFile,
                Filename = filename,
                Format = format,
                Channels = channels
            };
        }
    }
}
