using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config
{
    public class Devices
    {
        [JsonPropertyName("adjust_period")]
        public int? AdjustPeriod { get; set; }

        [JsonPropertyName("capture")]
        public IoDevice? Capture { get; set; }

        [JsonPropertyName("capture_samplerate")]
        public int? CaptureSamplerate { get; set; }

        /// <summary>
        /// All processing is done in chunks of data. 
        /// The chunksize is the number of samples each chunk will have per channel.
        /// Suggested starting points for different sample rates:
        ///
        /// 44.1 or 48 kHz: 1024
        /// 88.2 or 96 kHz: 2048
        /// 176.4 or 192 kHz: 4096
        /// </summary>
        [JsonPropertyName("chunksize")]
        public int? ChunkSize { get; set; }

        /// <summary>
        /// This enables the playback device to control the rate of the capture device, 
        /// in order to avoid buffer underruns or a slowly increasing latency. 
        /// This is currently supported when using an Alsa, Wasapi or CoreAudio playback device 
        /// (and any capture device). 
        /// Setting the rate can be done in two ways.
        /// </summary>
        [JsonPropertyName("enable_rate_adjust")]
        public bool? EnableRateAdjust { get; set; }

        [JsonPropertyName("multithreaded")]
        public bool? Multithreaded { get; set; }

        [JsonPropertyName("playback")]
        public IoDevice? Playback { get; set; }

        /// <summary>
        /// The field queuelimit should normally be left out to use the default of 4. 
        /// It sets the limit for the length of the queues between the capture device and 
        /// the processing thread, as well as between the processing thread and the playback device. 
        /// The total queue size limit will be 2*chunksize*queuelimit samples per channel.
        /// </summary>
        [JsonPropertyName("queuelimit")]
        public int? QueueLimit { get; set; }

        [JsonPropertyName("rate_measure_interval")]
        public int? RateMeasureInterval { get; set; }

        [JsonPropertyName("resampler")]
        public Resampler? Resampler { get; set; }

        /// <summary>
        /// The samplerate setting decides the sample rate that everything will run at. 
        /// This rate must be supported by both the capture and playback device.
        /// </summary>
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
