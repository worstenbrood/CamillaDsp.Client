using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CamillaDsp.Client.Core;
using CamillaDsp.Client.Models.Config.AudioDevices;
using CamillaDsp.Client.Models.Config.Filters;
using CamillaDsp.Client.Models.Config.Mixers;
using CamillaDsp.Client.Models.Config.Processors;

namespace CamillaDsp.Client.Models.Config
{
    public class DspConfig
    {
        /// <summary>
        /// Config title
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Config description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Playback and capture device configuration
        /// </summary>
        [JsonPropertyName("devices")]
        public Devices? Devices { get; set; }

        /// <summary>
        /// Dynamic object keyed by filter name (e.g. "Unnamed Filter 1")
        /// </summary>
        [JsonPropertyName("filters")]
        public Dictionary<string, Filter>? Filters { get; set; }

        /// <summary>
        /// Add filter. Allocates Fikters if null.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filter"></param>
        public void AddFilter(string name, Filter filter)
        {
            Filters ??= new Dictionary<string, Filter>(StringComparer.OrdinalIgnoreCase);
            Filters.Add(name, filter);
        }

        /// <summary>
        /// Remove filter if exists
        /// </summary>
        /// <param name="name"></param>
        public void RemoveFilter(string name)
        {
            if (Filters == null || !Filters.ContainsKey(name))
            {
                return;
            }

            Filters.Remove(name);
        }

        /// <summary>
        /// Present as an empty object in the sample JSON.
        /// </summary>
        [JsonPropertyName("mixers")]
        public Dictionary<string, Mixer>? Mixers { get; set; }

        /// <summary>
        /// Add mixer. Allocates <see cref="Mixers"/> if null.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mixer"></param>
        public void AddMixer(string name, Mixer mixer)
        {
            Mixers ??= new Dictionary<string, Mixer>(StringComparer.OrdinalIgnoreCase);
            Mixers.Add(name, mixer);
        }

        /// <summary>
        /// Remove mixer if exists
        /// </summary>
        /// <param name="name"></param>
        public void RemoveMixer(string name)
        {
            if (Mixers == null || !Mixers.ContainsKey(name))
            {
                return;
            }

            Mixers.Remove(name);
        }

        /// <summary>
        /// Dynamic object keyed by processor name (e.g. "Unnamed Processor 1")
        /// </summary>
        [JsonPropertyName("processors")]
        public Dictionary<string, Processor>? Processors { get; set; }

        /// <summary>
        /// Add mixer. Allocates <see cref="Mixers"/> if null.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="processor"></param>
        public void AddProcessor(string name, Processor processor)
        {
            Processors ??= new Dictionary<string, Processor>(StringComparer.OrdinalIgnoreCase);
            Processors.Add(name, processor);
        }

        /// <summary>
        /// Remove mixer if exists
        /// </summary>
        /// <param name="name"></param>
        public void RemoveProcessor(string name)
        {
            if (Processors == null || !Processors.ContainsKey(name))
            {
                return;
            }

            Processors.Remove(name);
        }

        /// <summary>
        /// Pipelines
        /// </summary>
        [JsonPropertyName("pipeline")]
        public List<Pipeline>? Pipeline { get; set; }

        /// <summary>
        /// Return this object serialized as a JSON string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Serializer.Serialize(this);
        }
    }
}
