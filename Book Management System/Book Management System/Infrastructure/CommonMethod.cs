using Book_Management_System.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Script.Serialization;

namespace Book_Management_System.Infrastructure
{
    public static class CommonMethod
    {
        
        public static string JsonSerialize<T>(List<T> item)
        {
            var Jsonlist = JsonConvert.SerializeObject(item,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
            return Jsonlist;
        }
        public static T JsonDeserialize<T>(string item)
        {
            var ListCmt = new JavaScriptSerializer().Deserialize<T>(item);
            return ListCmt;
        }
        
    }
}