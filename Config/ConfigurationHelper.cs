using Microsoft.Extensions.Configuration;

namespace Project2.ApiTests.Config;

public static class ConfigurationHelper
{
    public static TestSettings GetTestSettings()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        var settings = new TestSettings();
        configuration.GetSection("TestSettings").Bind(settings);

        return settings;
    }
}