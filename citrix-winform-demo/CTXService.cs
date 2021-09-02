using System;
using System.Collections.Generic;
using System.Text;

using WebSocketSharp;
using WebSocketSharp.Server;

namespace citrix_winform_demo
{
    class CTXService : WebSocketBehavior
    {
        public LogDelegate Logger { get; set; }
        public MessageDelegate MessageHandler { get; set; }

        protected override void OnOpen()
        {
            Logger("WS client connected");
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            MessageHandler(e.Data);
        }
        protected override void OnClose(CloseEventArgs e)
        {
            Logger(String.Format("WS Client closed: {0}", e.Reason));
        }

    }
}
