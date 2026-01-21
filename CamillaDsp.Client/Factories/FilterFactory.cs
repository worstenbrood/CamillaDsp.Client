using CamillaDsp.Client.Models.Config.Filters;

namespace CamillaDsp.Client.Factories
{
    public class FilterFactory
    {
        /// <summary>
        /// Second order high/lowpass filters (12dB/oct)
        /// Defined by cutoff frequency <paramref name="frequency"/> and Q-value <paramref name="q"/>.
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
        /// <returns></returns>
        public static Filter CreateHighPassFilter(int frequency, float q) => new ()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.Highpass,
                Frequency = frequency,
                Q = q,
            }
        };

        /// <summary>
        /// Second order high/lowpass filters (12dB/oct)
        /// Defined by cutoff frequency <paramref name="frequency"/> and Q-value <paramref name="q"/>.
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
        /// <returns></returns>
        public static Filter CreateLowPassFilter(int frequency, float q) => new ()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.LowPass,
                Frequency = frequency,
                Q = q,
            }
        };

        /// <summary>
        /// Given by normalized coefficients <paramref name="a1"/>, <paramref name="a2"/>, <paramref name="b0"/>, <paramref name="b1"/>, <paramref name="b2"/>.
        /// </summary>
        /// <returns></returns>
        public static Filter CreateFreeFilter(float a1, float a2, float b0, float b1, float b2) => new ()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.Free,
                A1 = a1,
                A2 = a2,
                B0 = b0,
                B1 = b1,
                B2 = b2,
            }
        };

        /// <summary>
        /// A parametric peaking filter with selectable gain <paramref name="gain"/> at a given frequency <paramref name="frequency"/> 
        /// with a bandwidth given either by the Q-value <paramref name="q"/> or <paramref name="bandwidth"/> in octaves bandwidth. 
        /// Note that bandwidth and Q-value are inversely related, a small bandwidth corresponds 
        /// to a large Q-value etc. Use positive gain values to boost, and negative values to 
        /// attenuate.
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="gain"></param>
        /// <param name="q">Q</param>
        /// <param name="bandwidth"></param>
        /// <returns></returns>
        public static Filter CreatePeakingFilter(int frequency, float gain, float? q = null, float? bandwidth = null) => new()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.Peaking,
                Frequency = frequency,
                Gain = gain,
                Q = q,
                Bandwidth = bandwidth,
            }
        };

        /// <summary>
        /// High / Low uniformly affects the high / low frequencies respectively while leaving 
        /// the low / high part unaffected. 
        /// In between there is a slope of variable steepness.
        /// </summary>
        /// <param name="frequency">Center frequency of the sloping section.</param>
        /// <param name="gain">The gain of the filter.</param>
        /// <param name="slope">The steepness in dB/octave. Values up to around +-12 are usable.</param>
        /// <param name="q">The Q-value and can be used instead of <paramref name="slope"/> to define 
        /// the steepness of the filter. 
        /// Only one of <paramref name="q"/> and <paramref name="slope"/> can be given.</param>
        /// <returns></returns>
        public static Filter CreateHighShelfFilter(int frequency, float gain, float? q = null, int? slope = null) => new()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.Highshelf,
                Frequency = frequency,
                Gain = gain,
                Q = q,
                Slope = slope,
            }
        };

        /// <summary>
        /// High / Low uniformly affects the high / low frequencies respectively while leaving 
        /// the low / high part unaffected. 
        /// In between there is a slope of variable steepness.
        /// </summary>
        /// <param name="frequency">Center frequency of the sloping section.</param>
        /// <param name="gain">The gain of the filter.</param>
        /// <param name="slope">The steepness in dB/octave. Values up to around +-12 are usable.</param>
        /// <param name="q">The Q-value and can be used instead of <paramref name="slope"/> to define 
        /// the steepness of the filter. 
        /// Only one of <paramref name="q"/> and <paramref name="slope"/> can be given.</param>
        /// <returns></returns>
        public static Filter CreateLowShelfFilter(int frequency, float gain, float? q = null, int? slope = null) => new()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.LowShelf,
                Frequency = frequency,
                Gain = gain,
                Q = q,
                Slope = slope,
            }
        };

        /// <summary>
        /// First order (6dB/oct) versions of the shelving functions.
        /// </summary>
        /// <param name="frequency">Center frequency of the sloping section./param>
        /// <param name="gain">The gain of the filter.</param>
        /// <returns></returns>
        public static Filter CreateHighShelfFOFilter(int frequency, float gain) => new()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.HighshelfFO,
                Frequency = frequency,
                Gain = gain,
            }
        };

        /// <summary>
        /// First order (6dB/oct) versions of the shelving functions.
        /// </summary>
        /// <param name="frequency">Center frequency of the sloping section./param>
        /// <param name="gain">The gain of the filter.</param>
        /// <returns></returns>
        public static Filter CreateLowShelfFOFilter(int frequency, float gain) => new()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.LowshelfFO,
                Frequency = frequency,
                Gain = gain,
            }
        };

        /// <summary>
        /// The delay filter provides a delay in milliseconds, millimetres or samples. 
        /// The unit can be <see cref="Units.ms"/>, <see cref="Units.mm"/> or <see cref="Units.samples"/>, and if left out it defaults to <see cref="Units.ms"/>. 
        /// When giving the delay in millimetres, the speed of sound of is assumed 
        /// to be 343 m/s (dry air at 20 degrees Celsius).
        /// If the <paramref name="subsample"/> parameter is set to true, then it will use use an IIR filter 
        /// to achieve subsample delay precision.
        /// If set to false, the value will instead be rounded to the nearest number of full 
        /// samples.
        /// This is a little faster and should be used if subsample precision is not required.
        /// The <paramref name="delay"/> value must be positive or zero.
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="unit"></param>
        /// <param name="subsample"></param>
        /// <returns></returns>
        public static Filter CreateDelayFilter(float delay, Units unit = Units.ms, bool subsample = false) => new()
        {
            Type = FilterTypes.Delay,
            Parameters = new FilterParameters
            {
                Delay = delay,
                Unit = unit,
                Subsample = subsample,
            }
        };

        /// <summary>
        /// Create a notch filter
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
        /// <returns></returns>
        public static Filter CreateNotchFilter(int frequency, float? q = null, float? bandwidth = null) => new()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.Notch,
                Frequency = frequency,
                Q = q,
                Bandwidth = bandwidth,
            }
        };

        /// <summary>
        /// Create a band pass filter
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
        /// <returns></returns>
        public static Filter CreateBandPassFilter(int frequency, float? q = null, float? bandwidth = null) => new()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.Bandpass,
                Frequency = frequency,
                Q = q,
                Bandwidth = bandwidth,
            }
        };

        /// <summary>
        /// A parametric peaking filter with selectable gain gain at a given frequency freq with 
        /// a bandwidth given either by the Q-value q or bandwidth in octaves bandwidth. 
        /// Note that bandwidth and Q-value are inversely related, a small bandwidth corresponds 
        /// to a large Q-value etc. 
        /// Use positive gain values to boost, and negative values to attenuate.
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
        /// <returns></returns>
        public static Filter CreateAllPassFilter(int frequency, float? q = null, float? bandwidth = null) => new()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.Allpass,
                Frequency = frequency,
                Q = q,
                Bandwidth = bandwidth,
            }
        };

        /// <summary>
        /// Defined by frequency, freq and filter order.
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
        /// <returns></returns>
        public static Filter CreateAllPassFOFilter(int frequency) => new()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.AllpassFO,
                Frequency = frequency,
            }
        };

        /// <summary>
        /// Defined by frequency, freq and filter order.
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
        /// <returns></returns>
        public static Filter CreateButterworthHighPassFilter(int frequency, int order) => new()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.ButterworthHighpass,
                Frequency = frequency,
                Order = order
            }
        };

        /// <summary>
        /// Create a butterworth low pass filter
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
        /// <returns></returns>
        public static Filter CreateButterworthLowPassFilter(int frequency, int order) => new()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.ButterworthLowpass,
                Frequency = frequency,
                Order = order
            }
        };

        /// <summary>
        /// Defined by frequency, freq and filter order.
        /// Note, the order must be even.
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
        /// <returns></returns>
        public static Filter CreateLinkwitzRileyHighPassFilter(int frequency, int order) => new()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.LinkwitzRileyHighpass,
                Frequency = frequency,
                Order = order
            }
        };

        /// <summary>
        /// Defined by frequency, freq and filter order.
        /// Note, the order must be even.
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
        /// <returns></returns>
        public static Filter CreateLinkwitzRileyLowPassFilter(int frequency, int order) => new()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.LinkwitzRileyLowpass,
                Frequency = frequency,
                Order = order
            }
        };

        /// <summary>
        /// he "Tilt" filter applies a tilt across the entire audible spectrum. 
        /// It takes a single parameter gain. A positive value gives a positive tilt, 
        /// that boosts the high end of the spectrum and attenuates the low. 
        /// A negative value gives the opposite result.
        /// The gain value is the difference in gain between the highest and lowest frequencies.
        /// It's applied symmetrically, so a value of +10 dB will result in 5 dB of boost at high 
        /// frequencies, and 5 dB of attenuation at low frequencies. 
        /// In between the gain changes linearly, with a midpoint at about 600 Hz.
        /// The gain value is limited to +- 100 dB.
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
        /// <returns></returns>
        public static Filter CreateTiltFilter(float gain) => new()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.Tilt,
                Gain = gain
            }
        };

        /// <summary>
        /// This filter combo is mainly meant to be created by guis. 
        /// It defines a 5-point (or band) parametric equalizer by combining a Lowshelf, a
        /// Highshelf and three Peaking filters.
        /// Each individual filter is defined by frequency, gain and q.
        /// The parameter names are:
        ///
        /// Lowshelf: fls, gls, qls
        /// Peaking 1: fp1, gp1, qp1
        /// Peaking 2: fp2, gp2, qp2
        /// Peaking 3: fp3, gp3, qp3
        /// Highshelf: fhs, ghs, qhs
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
        /// <returns></returns>
        public static Filter CreateFivePointPeqFilter(float fls, float gls, float qls, float fp1, float gp1, float qp1,
           float fp2, float gp2, float qp2, float fp3, float gp3, float qp3, float fhs, float ghs, float qhs) => new()
        {
            Type = FilterTypes.Biquad,
            Parameters = new FilterParameters
            {
                Type = FilterParameterTypes.FivePointPeq,
                Fls = fls,
                Gls = gls,
                Qls = qls,
                Fp1 = fp1,
                Gp1 = gp1,
                Qp1 = qp1,
                Fp2 = fp2,
                Gp2 = gp2,
                Qp2 = qp2,
                Fp3 = fp3,
                Gp3 = gp3,
                Qp3 = qp3,
                Fhs = fhs,
                Ghs = ghs,
                Qhs = qhs
            }
        };
    }
}
