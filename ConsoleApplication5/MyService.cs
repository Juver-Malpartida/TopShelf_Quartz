using Common.Logging;
using MyService.Schedulerr;
using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Topshelf;

namespace ConsoleApplication5
{
    public class MyService : IDisposable
    {
        private readonly ILog _logger;
        private readonly JobScheduler _scheduler;
        private readonly HttpSelfHostServer server;
        private readonly HttpSelfHostConfiguration config;

        public MyService(string baseAddress)
        {
            _scheduler = new JobScheduler();
            _logger = LogManager.GetLogger("ServiceContainer");
            config = new HttpSelfHostConfiguration(baseAddress);
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.Routes.MapHttpRoute(
                name :"DefaultApi",
                routeTemplate : "api/{controller}/{action}/{id}",
                defaults : new {id = RouteParameter.Optional}
             );
            server = new HttpSelfHostServer(config);

        }

        public void Dispose()
        {
            Stop(null);
        }

        public bool Start(HostControl control)
        {
            _logger.Debug("Starting Service...");
            _scheduler.Run();
            server.OpenAsync().Wait();
            _logger.Debug("Waiting for messages...");
            return true;
        }
        public bool Stop(HostControl control)
        {
            _logger.Debug("Stopping Service...");
            _scheduler.Stop();
            server.CloseAsync();
            server.Dispose();

            return true;
        }
    }
}
