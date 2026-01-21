using CamillaDsp.Client.Models.Config;
using System;
using System.Collections.Generic;
using System.Text;

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
                Type = FilterParameterTypes.Peaking,
                Frequency = frequency,
                Gain = gain,
                Q = q,
                Slope = slope,
            }
        };
    }
}
