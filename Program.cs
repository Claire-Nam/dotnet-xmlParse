using System;
using System.Threading.Tasks;

namespace WebDownloader
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //HttpClientExample example = new HttpClientExample();
            //await example.RunAsync();

            WeatherInfo weatherInfo = new WeatherInfo();
            await weatherInfo.RunAsync();
        }
    }
}
