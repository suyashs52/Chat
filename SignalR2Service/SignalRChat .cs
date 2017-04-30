using System.ServiceProcess;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin;
using Microsoft.AspNet.SignalR.Hubs;
[assembly: OwinStartup(typeof(SignalR2Service.Startup))] 
namespace SignalR2Service
{
    public partial class SignalRChat  : ServiceBase
    {
        public SignalRChat()
        {
            InitializeComponent();
        }
        
        protected override void OnStart(string[] args)
        {
            
            string url = "http://localhost:8080";
            WebApp.Start(url); 
        }
        protected override void OnStop()
        {
           
        } 
        
    }
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
    [HubName("myHub")]
    public class MyHub : Hub
    {
        public static void SendFromServer(string connectionId, string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MyHub>();

            context.Clients.Client(connectionId).serverresponse(message);
        }

        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }
    } 
}
