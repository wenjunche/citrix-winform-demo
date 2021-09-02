using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.CommandLine.Invocation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using WebSocketSharp;
using WebSocketSharp.Server;

namespace citrix_winform_demo
{
    public delegate void LogDelegate(string message);
    public delegate void MessageDelegate(string message);

    public class AppChannel
    {
        [DllImport("ctxopenfin.dll")]
        static extern byte VD_LoadURL(string url);

        public string WsURL { get; set; }
        public string ProxyUrl { get; set; }

        private String CTX_SERVICE_PATH = "/ctxservice";
        private WebSocketServer webSocketServer;
        public LogDelegate Logger { get;  set; }

        public void Initialize()
        {
            if (ProxyUrl.IsNullOrEmpty())
            {
                Logger("Missing proxy url");
            }
            else
            {
                string url = String.Format("{0}/?$$wsurl={1}{2}", ProxyUrl, WsURL, CTX_SERVICE_PATH);
                Logger(String.Format("Invoking proxy app at {0}", url));
                var r = VD_LoadURL(url);
            }

            if (WsURL.IsNullOrEmpty())
            {
                Logger("Missing WS url");
            }
            else
            {
                Logger(String.Format("Starting Websocket server a {0}", WsURL));
                webSocketServer = new WebSocketServer(WsURL);
                Logger(String.Format("Registring CTX service path {0}", CTX_SERVICE_PATH));
                webSocketServer.AddWebSocketService<CTXService>(CTX_SERVICE_PATH, CreateWSService);
                webSocketServer.Start();
            }
        }

        private CTXService CreateWSService()
        {
            return new CTXService()
            {
                Logger = Logger,
                MessageHandler = OnMessage
            };
        }

        public void Send(string m)
        {
            webSocketServer.WebSocketServices.Hosts.FirstOrDefault().Sessions.Broadcast(m);
        }

        public void OnMessage(string m)
        {
            Logger(m);
        }

    }
}
