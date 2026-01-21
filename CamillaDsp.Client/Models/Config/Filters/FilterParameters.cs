using CamillaDsp.Client.Models.Config.Mixers;
using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config.Filters
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Faders
    {
        Main,
        Aux1,
        Aux2,
        Aux3,
        Aux4
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Units
    {
        ms,     // Milliseconds
        mm,     // Millimeters
        samples // Samples
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FilterParameterTypes
    {
        Raw,
        Wav,
        Values,
        Free,
        Highpass,
        LowPass,
        Peaking,
        Highshelf,
        LowShelf,
        LinkwitzRileyHighpass,
        LinkwitzRileyLowpass,
        HighshelfFO,
        LowshelfFO,
        Notch,
        GeneralNotch,
        Bandpass,
        Allpass,
        AllpassFO,
        LinkwitzTransform,
        ButterworthHighpass,
        ButterworthLowpass,
        Tilt,
        FivePointPeq,
        GraphicEqualizer,
        Dither,
        Limiter,
        Difference
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RawFormats
    {
        TEXT,
        S16LE, // signed 16-bit little-endian integers
        S24LE, // signed 24-bit little-endian integers stored
               // as 32 bits (with the data in the low 24)
        S24LE3, // signed 24-bit little-endian integers stored as 24 bits
        S32LE, // signed 32-bit little-endian integers
        FLOAT32LE, // 32-bit little endian float
        FLOAT64LE, //64-bit little endian float
    }

    public class FilterParameters
    {
        [JsonPropertyName("type")]
        public FilterParameterTypes? Type { get; set; }

        // Gain
        [JsonPropertyName("gain")]
        public double? Gain { get; set; }

        [JsonPropertyName("inverted")]
        public bool? Inverted { get; set; }

        [JsonPropertyName("mute")]
        public bool? Mute { get; set; }

        [JsonPropertyName("scale")]
        public ScaleTypes? Scale { get; set; }

        // Volume

        [JsonPropertyName("ramp_time")]
        public int? RampTime { get; set; }

        [JsonPropertyName("limit")]
        public float? Limit { get; set; }
               
        [JsonPropertyName("fader")]
        public Faders? Fader { get; set; }

        // Loudness

        [JsonPropertyName("reference_level")]
        public float? ReferenceLevel { get; set; }

        [JsonPropertyName("high_boost")]
        public float? HighBoost { get; set; }

        [JsonPropertyName("low_boost")]
        public float? LowBoost { get; set; }

        [JsonPropertyName("attenuate_mid")]
        public bool? AttenuateMid { get; set; }

        // Delay

        [JsonPropertyName("delay")]
        public float? Delay { get; set; }

        [JsonPropertyName("units")]
        public Units? Unit { get; set; }

        [JsonPropertyName("subsample")]
        public bool? Subsample { get; set; }

        // Fir - Raw
          

        [JsonPropertyName("format")]
        public RawFormats? Format { get; set; }

        [JsonPropertyName("skip_bytes_lines")]
        public int? SkipBytesLines { get; set; }

        [JsonPropertyName("read_bytes_lines")]
        public int? ReadBytesLines { get; set; }

        // Fir - Raw - Wav
        
        [JsonPropertyName("filename")]
        public string? Filename { get; set; }

        // Fir - wav
        [JsonPropertyName("channel")]
        public int? Channel { get; set; }

        // Fir - Free


        [JsonPropertyName("a1")]
        public float? A1 { get; set; }


        [JsonPropertyName("a2")]
        public float? A2 { get; set; }


        [JsonPropertyName("b0")]
        public float? B0 { get; set; }


        [JsonPropertyName("b1")]
        public float? B1 { get; set; }


        [JsonPropertyName("b2")]
        public float? B2 { get; set; }

        // Fir - Highpass/LowPass

        [JsonPropertyName("freq")]
        public int? Frequency { get; set; }

        [JsonPropertyName("Q")]
        public float? Q { get; set; }

        // Fir - Peaking

        [JsonPropertyName("bandwidth")]
        public float? Bandwidth { get; set; }

        // Fir - HighShelf

        [JsonPropertyName("slope")]
        public int? Slope { get; set; }

        // Fir - LinkwitzRileyHighpass
        [JsonPropertyName("order")]
        public int? Order { get; set; }

        // Fir - LinkwitzTransform

        [JsonPropertyName("freq_act")]
        public int? FrequencyAct { get; set; }

        [JsonPropertyName("q_act")]
        public float? QAct { get; set; }

        [JsonPropertyName("freq_target")]
        public int? FrequencyTarget { get; set; }

        [JsonPropertyName("q_target")]
        public float? QTarget { get; set; }

        // Fir - FivePointPeq

        [JsonPropertyName("fls")]
        public float? Fls { get; set; }

        [JsonPropertyName("gls")]
        public float? Gls { get; set; }

        [JsonPropertyName("qls")]
        public float? Qls { get; set; }

        [JsonPropertyName("fp1")]
        public float? Fp1 { get; set; }

        [JsonPropertyName("gp1")]
        public float? Gp1 { get; set; }

        [JsonPropertyName("qp1")]
        public float? Qp1 { get; set; }

        [JsonPropertyName("fp2")]
        public float? Fp2 { get; set; }

        [JsonPropertyName("gp2")]
        public float? Gp2 { get; set; }

        [JsonPropertyName("qp2")]
        public float? Qp2 { get; set; }

        [JsonPropertyName("fp3")]
        public float? Fp3 { get; set; }

        [JsonPropertyName("gp3")]
        public float? Gp3 { get; set; }

        [JsonPropertyName("qp3")]
        public float? Qp3 { get; set; }

        [JsonPropertyName("fhs")]
        public float? Fhs { get; set; }

        [JsonPropertyName("ghs")]
        public float? Ghs { get; set; }

        [JsonPropertyName("qhs")]
        public float? Qhs { get; set; }

    }
}
