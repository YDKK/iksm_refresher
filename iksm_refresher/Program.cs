using System;
using System.IO;
using System.Net.Http;
using System.Reactive.Linq;
using Newtonsoft.Json;
using Topshelf;

namespace iksm_refresher
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<Instance>(s =>
                {
                    s.ConstructUsing(name => new Instance());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();
                x.SetDescription("This is iksm_refresher service.");
                x.SetDisplayName("iksm_refresher_service");
            });
        }
    }

    class Instance
    {
        private readonly dynamic _config;
        private IDisposable _timer;

        public void Start()
        {
            _timer = Observable.Timer(TimeSpan.Zero, TimeSpan.FromHours(1)).Subscribe(async x =>
            {
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Get, "https://app.splatoon2.nintendo.net/home"))
                {
                    request.Headers.Add("Cookie", $"iksm_session={_config.iksm_session}");
                    await client.SendAsync(request);
                }
            });
        }

        public void Stop()
        {
            _timer.Dispose();
        }

        public Instance()
        {
            var configFile = File.ReadAllText("config.json");
            _config = JsonConvert.DeserializeObject(configFile);
        }
    }
}
