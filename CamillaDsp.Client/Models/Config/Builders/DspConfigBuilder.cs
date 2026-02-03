using CamillaDsp.Client.Models.Config.Filters;
using CamillaDsp.Client.Models.Config.Mixers;
using CamillaDsp.Client.Models.Config.Processors;

namespace CamillaDsp.Client.Models.Config.Builders
{
    public class DspConfigBuilder
    {
        public static DspConfigBuilder Create() => new();
        public static DspConfigBuilder Open(DspConfig config) => new(config);

        public readonly DspConfig DspConfig;

        private DspConfigBuilder(DspConfig config)
        {
            DspConfig = config;
        }

        private DspConfigBuilder() : this(new())
        {
        }

        /// <summary>
        /// Set config title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public DspConfigBuilder SetTitle(string title)
        {
            DspConfig.Title = title;
            return this;
        }

        /// <summary>
        /// Set config description
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public DspConfigBuilder SetDescription(string description)
        {
            DspConfig.Description = description;
            return this;
        }

        /// <summary>
        /// Add a filter
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public DspConfigBuilder AddFilter(string filterName, Filter filter)
        {
            DspConfig.AddFilter(filterName, filter);
            return this;
        }

        /// <summary>
        /// Remove a filter
        /// </summary>
        /// <param name="filterName"></param>
        /// <returns></returns>
        public DspConfigBuilder RemoveFilter(string filterName)
        {
            DspConfig.RemoveFilter(filterName);
            return this;
        }

        /// <summary>
        /// Add processor
        /// </summary>
        /// <param name="processorName"></param>
        /// <param name="processor"></param>
        /// <returns></returns>
        public DspConfigBuilder AddProcessor(string processorName, Processor processor)
        {
            DspConfig.AddProcessor(processorName, processor);
            return this;
        }

        /// <summary>
        /// Remove processor
        /// </summary>
        /// <param name="processorName"></param>
        /// <returns></returns>
        public DspConfigBuilder RemoveProcessor(string processorName)
        {
            DspConfig.RemoveProcessor(processorName);
            return this;
        }


        /// <summary>
        /// Add mixer
        /// </summary>
        /// <param name="mixerName"></param>
        /// <param name="mixer"></param>
        /// <returns></returns>
        public DspConfigBuilder AddMixer(string mixerName, Mixer mixer)
        {
            DspConfig.AddMixer(mixerName, mixer);
            return this;
        }

        /// <summary>
        /// Remove mixer
        /// </summary>
        /// <param name="mixerName"></param>
        /// <returns></returns>
        public DspConfigBuilder Removemixer(string mixerName)
        {
            DspConfig.RemoveMixer(mixerName);
            return this;
        }
    }
}
