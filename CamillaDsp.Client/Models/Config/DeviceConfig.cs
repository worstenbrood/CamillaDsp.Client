using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config
{
    public class DeviceConfig
    {
        [JsonPropertyName("adjust_period")]
        public int? AdjustPeriod { get; set; }

        [JsonPropertyName("capture")]
        public IoDevice? Capture { get; set; }

        [JsonPropertyName("capture_samplerate")]
        public int? CaptureSamplerate { get; set; }

        [JsonPropertyName("chunksize")]
        public int? Chunksize { get; set; }

        [JsonPropertyName("enable_rate_adjust")]
        public bool? EnableRateAdjust { get; set; }

        [JsonPropertyName("multithreaded")]
        public bool? Multithreaded { get; set; }

        [JsonPropertyName("playback")]
        public IoDevice? Playback { get; set; }

        [JsonPropertyName("queuelimit")]
        public int? QueueLimit { get; set; }

        [JsonPropertyName("rate_measure_interval")]
        public int? RateMeasureInterval { get; set; }

        [JsonPropertyName("resampler")]
        public ResamplerConfig? Resampler { get; set; }

        [JsonPropertyName("samplerate")]
        public int? Samplerate { get; set; }

        [JsonPropertyName("silence_threshold")]
        public double? SilenceThreshold { get; set; }

        [JsonPropertyName("silence_timeout")]
        public double? SilenceTimeout { get; set; }

        [JsonPropertyName("stop_on_rate_change")]
        public bool? StopOnRateChange { get; set; }

        [JsonPropertyName("target_level")]
        public double? TargetLevel { get; set; }

        [JsonPropertyName("volume_limit")]
        public double? VolumeLimit { get; set; }

        [JsonPropertyName("volume_ramp_time")]
        public double? VolumeRampTime { get; set; }

        [JsonPropertyName("worker_threads")]
        public int? WorkerThreads { get; set; }
    }
}
