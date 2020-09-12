using System;
using System.Configuration;
using System.Threading;
using System.Web;

namespace SlowingModule
{
    public class SlowingModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            var timeToWaitSetting = ConfigurationManager.AppSettings["SlowingModule:TimeToWait"];

            if (!int.TryParse(timeToWaitSetting, out var timeToWait))
            {
                timeToWait = 400000;
            }

            context.BeginRequest += (object sender, EventArgs e) =>
            {
                Thread.Sleep(timeToWait);
            };
        }

        public void Dispose()
        {
        }
    }
}
