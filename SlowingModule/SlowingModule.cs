﻿using System;
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
            var urlContainsFilter = ConfigurationManager.AppSettings["SlowingModule:UrlContainsFilter"];

            if (!int.TryParse(timeToWaitSetting, out var timeToWait))
            {
                timeToWait = 400000;
            }

            context.BeginRequest += (object sender, EventArgs e) =>
            {
                if (string.IsNullOrEmpty(urlContainsFilter))
                {
                    Thread.Sleep(timeToWait);
                }
                else
                {
                    if (HttpContext.Current.Request.Url.AbsoluteUri.Contains(urlContainsFilter))
                    {
                        Thread.Sleep(timeToWait);
                    }
                }
                
            };
        }

        public void Dispose()
        {
        }
    }
}
