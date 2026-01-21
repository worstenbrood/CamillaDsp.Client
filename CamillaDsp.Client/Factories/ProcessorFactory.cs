using System;
using System.Text;
using System.Collections.Generic;
using CamillaDsp.Client.Models.Config;
using CamillaDsp.Client.Models.Config.Processors;

namespace CamillaDsp.Client.Factories
{
    public static class ProcessorFactory
    {
        /// <summary>
        /// Create a compressor processor
        /// </summary>
        /// <param name="channels"></param>
        /// <param name="attack"></param>
        /// <param name="release"></param>
        /// <param name="threshold"></param>
        /// <param name="factor"></param>
        /// <param name="makeupGain"></param>
        /// <param name="clipLimit"></param>
        /// <param name="softClip"></param>
        /// <param name="monitorChannels"></param>
        /// <param name="processChannels"></param>
        /// <returns></returns>
        public static Processor CreateCompressor(int channels, float attack, float release, float threshold, float factor,
            float? makeupGain = null, float? clipLimit = null, bool? softClip = null, List<int>? monitorChannels = null,
            List<int>? processChannels = null)
        {
            return new Processor
            {
                Type = ProcessorTypes.Compressor,
                Description = "Compressor",
                Parameters = new ProcessorParameters
                {
                    Channels = channels,
                    Attack = attack,
                    Release = release,
                    Threshold = threshold,
                    Factor = factor,
                    MakeupGain = makeupGain,
                    ClipLimit = clipLimit,
                    SoftClip = softClip,
                    MonitorChannels = monitorChannels,
                    ProcessChannels = processChannels
                }
            };
        }

        /// <summary>
        /// Create a noise gate processor
        /// </summary>
        /// <param name="channels"></param>
        /// <param name="attack"></param>
        /// <param name="release"></param>
        /// <param name="threshold"></param>
        /// <param name="attenuation"></param>
        /// <param name="monitorChannels"></param>
        /// <param name="processChannels"></param>
        /// <returns></returns>
        public static Processor CreateNoiseGate(int channels, float attack, float release, float threshold, float attenuation,
            List<int>? monitorChannels = null, List<int>? processChannels = null)
        {
            return new Processor
            {
                Type = ProcessorTypes.NoiseGate,
                Description = "Noise Gate",
                Parameters = new ProcessorParameters
                {
                    Channels = channels,
                    Attack = attack,
                    Release = release,
                    Threshold = threshold,
                    Attenuation = attenuation,
                    MonitorChannels = monitorChannels,
                    ProcessChannels = processChannels
                }
            };
        }
    }
}
