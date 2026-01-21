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
        /// <param name="channels">Number of channels, must match the number of channels of the pipeline where the compressor is inserted.</param>
        /// <param name="attack">Time constant in seconds for attack, how fast the compressor reacts to an increase of the loudness.</param>
        /// <param name="release">Time constant in seconds for release, how fast the compressor scales back the compression when the loudness decreases.</param>
        /// <param name="threshold">The loudness threshold in dB where compression sets in.</param>
        /// <param name="factor">The compression factor, giving the amount of compression over the threshold. 
        /// A factor of 4 means a sound that is 4 dB over the threshold will be attenuated to 1 dB over the threshold.</param>
        /// <param name="makeupGain">Amount of fixed gain in dB to apply after compression. Optional, defaults to 0 dB.</param>
        /// <param name="clipLimit">The level in dB to clip at. Providing a value enables clipping of the signal after compression. 
        /// Leave out or set to null to disable clipping.</param>
        /// <param name="softClip">Enable soft clipping. Set to false to use hard clipping. 
        /// This setting is ignored when clipping is disabled. Note that soft clipping introduces some harmonic distortion to the signal. 
        /// This setting is ignored if enable_clip = false. Optional, defaults to false.</param>
        /// <param name="monitorChannels">A list of channels used when estimating the loudness. Optional, defaults to all channels.</param>
        /// <param name="processChannels">A list of channels to be compressed. Optional, defaults to all channels.</param>
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
        /// <param name="channels">Number of channels, must match the number of channels of the pipeline where the compressor is inserted.</param>
        /// <param name="attack">Time constant in seconds for attack, how fast the gate reacts to an increase of the loudness.</param>
        /// <param name="release">Time constant in seconds for release, how fast the gate reacts when the loudness decreases.</param>
        /// <param name="threshold">The loudness threshold in dB where gate "opens".</param>
        /// <param name="attenuation">The amount of attenuation in dB to apply when the gate is "closed".</param>
        /// <param name="monitorChannels">A list of channels used when estimating the loudness. Optional, defaults to all channels.</param>
        /// <param name="processChannels">A list of channels to be gated. Optional, defaults to all channels.</param>
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
