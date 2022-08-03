using System.ComponentModel;
using Exiled.API.Interfaces;

namespace CombatLog
{
    public sealed class Config : IConfig
    {
        [Description("Plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Message duration time in seconds")]
        public ushort MessageDurationInSeconds { get; set; } = 10;

    }
}