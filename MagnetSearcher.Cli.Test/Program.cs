using MagnetSearcher.Models;
using Newtonsoft.Json;

namespace MagnetSearcher.Cli.Test {
    internal class Program {
        static async Task Main(string[] args) {
            var client = new HttpClient();
            
            for (int i = 0; i < 1000; i++) {
                var result = await client.PostAsync(
                "http://127.0.0.1:5257/v1/DHT/add/12345",
                new StringContent(
                    JsonConvert.SerializeObject(new MagnetInfo() {
                        InfoHash = i.ToString(),
                        Name = i.ToString(),
                        Length = 200,
                        GetDateTime = DateTime.Now.Ticks,
                        Files = new List<MagnetFile>() {
                            new MagnetFile() {
                                Path = new List<string>() {
                                    "1.txt"
                                },
                                Length = 200
                            }
                        },
                        RawMetaDataBase64 = "123"
                    }), 
                    System.Text.Encoding.UTF8, 
                    "application/json"
                    )
                );
                Console.WriteLine(await result.Content.ReadAsStringAsync());
            }
            Console.WriteLine("Hello, World!");
        }
    }
}