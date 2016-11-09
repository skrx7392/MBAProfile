using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MBAProfile
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new CompressionDelegateHandler());
            config.MapHttpAttributeRoutes();
            var json=config.Formatters.JsonFormatter;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            json.SerializerSettings.ReferenceLoopHandling =ReferenceLoopHandling.Serialize;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            json.UseDataContractJsonSerializer = true;
            config.Formatters.Add(json);
        }
    }
}
