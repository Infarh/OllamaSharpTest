using OllamaSharp;
using OllamaSharp.Models.Chat;

namespace OllamaTest;


internal class WeatherTool : Tool
{
    public WeatherTool()
    {
        Function = new()
        {
            Description = "Получает погоду для выбранного города",
            Name = "get_current_weather",
            Parameters = new()
            {
                Properties = new()
                {
                    ["city"] = new() { Type = "string", Description = "Название города" }
                },
                Required = ["city"],
                //Type = "object",
            },
        };
        Type = "function";
    }
}

internal class SampleTools
{
    [OllamaTool]
    public static string GetWeather(string city, TemperatureUnit unit = TemperatureUnit.Celsius)
    {
        Console.WriteLine($" ==>> Запрос погоды от LLM для города {city} ид.изм {unit}");
        return $"Погода в {city} - солнечно, 25 градусов {unit}.";
    }

    public enum TemperatureUnit
    {
        Celsius,
        Fahrenheit,
    }
}