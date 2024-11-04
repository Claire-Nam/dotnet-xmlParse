using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebDownloader
{
    public class HttpClientExample
    {
        public async Task RunAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://www.naver.com/");

            // 오류 검사
            if(!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"서버에서 오류를 반환했습니다. 반환 코드 = {response.StatusCode}");
                return;
            }
            string content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
    }
}
