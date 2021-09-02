using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace citrix_winform_demo
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var cmd = new RootCommand
            {
                new Option<string>("--ws-url", "WebSocket server url"),
                new Option<string>("--proxy-url", "Manifest URL for OpenFin proxy app"),
            };

            cmd.Handler = CommandHandler.Create<string, string, IConsole>(HandleGreeting);
            cmd.Invoke(args);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form1 = new Form1()
            {
                Channel = channel
            };
            Application.Run(form1);

        }
        private static Form1 form1;
        private static AppChannel channel;

        static void HandleGreeting(string wsURL, string proxyUrl, IConsole console)
        {
            channel = new AppChannel()
            {
                ProxyUrl = proxyUrl,
                WsURL = wsURL,
                Logger = LogText
            };
        }

        static void LogText(string m)
        {
            form1.ShowLog(m);
        }
    }
}
