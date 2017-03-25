using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ConsoleApplication5
{
    internal static class ConfigureService
    {
        internal static void Configure()
        {
            HostFactory.Run(configure =>
            {
                configure.Service<MyService>(service =>
                {
                    service.ConstructUsing(() => new MyService($"http://localhost:5156/"));
                    service.WhenStarted((svc, hostControl) => svc.Start(hostControl));
                    service.WhenStopped((svc, hostControl) => svc.Stop(hostControl));
                    service.WhenShutdown((svc, hostControl) => svc.Stop(hostControl));
                    service.WhenPaused((svc, hostControl) => svc.Stop(hostControl));
                    service.WhenContinued((svc, hostControl) => svc.Start(hostControl));
                });
                //Setup Account that window service use to run.  
                configure.RunAsLocalSystem();
                configure.SetServiceName("MyWindowServiceWithTopshelf");
                configure.SetDisplayName("MyWindowServiceWithTopshelf");
                configure.SetDescription("My .Net windows service with Topshelf");
                configure.UseLog4Net();
            });
           
        }
    }
}
