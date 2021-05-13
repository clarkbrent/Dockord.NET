using System;
using Xunit;

namespace Dockord.Bot.Services.Tests
{
    public class ConfigServiceTests
    {
        private enum ConfigSection
        {
            BotSettings,
            MinimumLevel,
            Override,
            Serilog,
        }

        [Fact]
        public void ConfigService_VerifyIsSingleton()
        {
            var config1 = ConfigService.Get().GetHashCode();
            var config2 = ConfigService.Get().GetHashCode();

            Assert.Equal(config1, config2);
        }

        [Fact]
        public void Get_ReturnsExpectedProperties()
        {
            Assert.True(DoesPropertyExist(ConfigSection.BotSettings, "AlwaysCacheMembers"));
            Assert.True(DoesPropertyExist(ConfigSection.BotSettings, "ErrorMessageDeleteSecondsDelay"));
            Assert.True(DoesPropertyExist(ConfigSection.BotSettings, "MessageCacheSize"));
            Assert.True(DoesPropertyExist(ConfigSection.BotSettings, "Prefix"));
            Assert.True(DoesPropertyExist(ConfigSection.BotSettings, "Token"));
            Assert.True(DoesPropertyExist(ConfigSection.MinimumLevel, "Default"));
            Assert.True(DoesPropertyExist(ConfigSection.Override, "Microsoft"));
            Assert.True(DoesPropertyExist(ConfigSection.Override, "System"));
        }

        [Fact]
        public void GetSerilogSection_ReturnsExpectedProperties()
        {
            IConfigService config = ConfigService.Get();
            var serilogConfig = config.GetSerilogSection();

            Assert.Equal("Serilog", serilogConfig.Key);
            Assert.Equal("Serilog", serilogConfig.Path);
        }

        private bool DoesPropertyExist(ConfigSection section, string propName)
        {
            IConfigService config = ConfigService.Get();

            return section switch
            {
                ConfigSection.BotSettings => !string.IsNullOrWhiteSpace(config.BotSettings.GetType().GetProperty(propName).Name),
                ConfigSection.MinimumLevel => !string.IsNullOrWhiteSpace(config.Serilog.MinimumLevel.GetType().GetProperty(propName).Name),
                ConfigSection.Override => !string.IsNullOrWhiteSpace(config.Serilog.MinimumLevel.Override.GetType().GetProperty(propName).Name),
                ConfigSection.Serilog => !string.IsNullOrWhiteSpace(config.Serilog.GetType().GetProperty(propName).Name),
                _ => throw new ArgumentException("Invalid section provided."),
            };
        }
    }

    //[Collection("Sequential")]
    //public class GetMinimumLogLevelTests
    //{
    //    [Fact]
    //    public void GetMinimumLogLevel_ReturnsTraceLogLevel()
    //    {
    //        IConfigService config = ConfigService.Get();

    //        System.Reflection.FieldInfo instance = typeof(ConfigService).GetField("_instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

    //        var configMock = new Mock<IConfigService>();
    //        configMock.SetupGet(x => x.Serilog.MinimumLevel.Default).Returns("trace");
    //        instance.SetValue(null, configMock.Object);

    //        var test = configMock.Object.Serilog.MinimumLevel.Default;

    //        var minLogLevel = configMock.Object.GetMinimumLogLevel();

    //        Assert.Equal(LogLevel.Trace, minLogLevel);
    //    }
    //}
}