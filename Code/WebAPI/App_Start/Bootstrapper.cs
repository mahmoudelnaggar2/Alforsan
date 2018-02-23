using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebAPI
{
    public class Bootstrapper
    {
        public static void Run(HttpConfiguration config)
        {
            // Configure Autofac
            AutofacWebapiConfig.Initialize(config);
        }
    }
}