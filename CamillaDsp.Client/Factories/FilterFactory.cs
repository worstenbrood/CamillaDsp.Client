using System;
using System.Text;
using System.Collections.Generic;
using CamillaDsp.Client.Models.Config.Filters;

namespace CamillaDsp.Client.Factories
{
    public class FilterFactory
    {
        /// <summary>
        /// Create an high pass filter
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
        /// Create a low pass filter
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
        /// Create a free filter
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
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
        /// Create a peaking filter
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
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
        /// Create a high shelf filter
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
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
        /// Create a high shelf filter
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
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
        /// Create a high shelf FO filter
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
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
        /// Create a high shelf FO filter
        /// </summary>
        /// <param name="frequency">Frequency</param>
        /// <param name="q">Q</param>
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
        /// Create a delay filter
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
        /// Create a peaking filter
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
        /// Create a all pass fo filter
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
        /// Create a butterworth high pass filter
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
        /// Create a Linkwitz-Riley high pass filter
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
        /// Create a Linkwitz-Riley low pass filter
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
        /// Create a tilt filter
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
        /// Create a FivePointPeq filter
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
