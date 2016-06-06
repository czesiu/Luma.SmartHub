using System;

namespace Luma.SmartHub
{
    public class EventConfigurationAttribute : Attribute
    {
        public Type ConfigurationType { get; }

        public EventConfigurationAttribute(Type configurationType)
        {
            ConfigurationType = configurationType;
        }
    }
}