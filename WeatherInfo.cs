using System;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace WebDownloader
{
    public class WeatherInfo
    {
        public async Task RunAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://www.kma.go.kr/weather/forecast/mid-term-rss3.jsp?stnId=108");

            // 오류 검사
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"서버에서 오류를 반환했습니다. 반환 코드 = {response.StatusCode}");
                return;
            }

            // RSS 데이터 읽기
            string content = await response.Content.ReadAsStringAsync();
            
            //XML 파싱
            XmlDocument document = new XmlDocument();
            document.LoadXml(content);

            XmlNodeList nodes = document.DocumentElement.SelectNodes("descendant::location");
            foreach (XmlNode node in nodes)
            {
                var provinceNode = node.SelectSingleNode("province");
                var cityNode = node.SelectSingleNode("city");

                if(provinceNode == null  || cityNode == null) continue;
                if (provinceNode.InnerText != "강원도영서" || cityNode.InnerText != "춘천") continue;

                PrintNode(node);
                break;
            }
        }

        private void PrintNode(XmlNode sourceNode)
        {
            var nodes = sourceNode.SelectNodes("descendant::data");
            foreach (XmlNode node in nodes)
            {
                var dateNode = node.SelectSingleNode("tmEf");
                var weartherNode = node.SelectSingleNode("wf");

                if (dateNode == null || weartherNode == null) continue;
                Console.WriteLine($"날짜: {dateNode.InnerText}, 날씨: {weartherNode.InnerText}");
            }
        }
    }
}
