using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DeviceTypes
    {
        RawFile,
        WavFile,
        File,
        SignalGenerator,
        Stdin,
        Stdout,
        Bluez,
        Jack,
        Wasapi,
        CoreAudio,
        Alsa,
        Pulse,
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Formats
    {
        S16LE,
        S24LE,
        S24LE3,
        S32LE,
        FLOAT32LE,
        FLOAT64LE,
    }

    public class IoDevice
    {
        [JsonPropertyName("type")]
        public DeviceTypes? Type { get; set; }

        [JsonPropertyName("channels")]
        public int? Channels { get; set; }

        [JsonPropertyName("device")]
        public string? Device { get; set; }

        [JsonPropertyName("filename")]
        public string? Filename { get; set; }

        [JsonPropertyName("format")]
        public Formats? Format { get; set; }

        [JsonPropertyName("skip_bytes")]
        public int? SkipBytes { get; set; }

        [JsonPropertyName("read_bytes")]
        public int? ReadBytes { get; set; }

        [JsonPropertyName("extra_samples")]
        public int? ExtraSamples { get; set; }

        [JsonPropertyName("wav_headder")]
        public bool? Wavheader { get; set; }

        [JsonPropertyName("labels")]
        public string[]? Labels { get; set; }

        [JsonPropertyName("link_mute_control")]
        public string? LinkMuteControl { get; set; }

        [JsonPropertyName("link_volume_control")]
        public string? LinkVolumeControl { get; set; }

        [JsonPropertyName("stop_on_inactive")]
        public bool? StopOnInactive { get; set; }

        [JsonPropertyName("signal")]
        public Signal? Signal { get; set; }
    }
}
