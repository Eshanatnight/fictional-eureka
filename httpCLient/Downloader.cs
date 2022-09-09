using System;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace HTTPClient
{
    class Downloader
    {
        public static string? url = "https://aka.ms/vscode-server-launcher/";
        public static string? filename = "x86_64-pc-windows-msvc-code-server.exe";

        public static int getURL()
        {
            Console.WriteLine("url?");
            url = Console.ReadLine();
            Console.WriteLine("Filename?");
            filename = Console.ReadLine();

            if(url == null || filename == null)
            {
                return 1;
            }

            return 0;
        }


        public static async Task DownloadWebPage()
        {
            Console.WriteLine("Starting Download...");

            using(HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if(response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Got it...");
                    byte[] data = await response.Content.ReadAsByteArrayAsync();

                    FileStream fstream = System.IO.File.Create(filename);
                    await fstream.WriteAsync(data, 0, data.Length);
                    fstream.Close();

                    Console.WriteLine("Done!");
                }
            }
        }

        public static void Main(String[] args)
        {
            if(args.Length == 1)
            {
                getURL();
                if(filename == null || url == null)
                System.Environment.Exit(1);
            }

            Task dl = DownloadWebPage();

            Console.WriteLine("Waiting...");
            Thread.Sleep(TimeSpan.FromSeconds(5));

            dl.GetAwaiter().GetResult();
        }
    }
}