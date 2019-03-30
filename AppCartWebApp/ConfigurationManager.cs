using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace AppConfiguration
{
    static class ConfigurationManager
    {
        public static IConfiguration AppSetting { get; }
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
    }
}

//{
//    "GrandParent_Key" : {
//        "Parent_Key" : {
//            "Child_Key" : "value1"
//        }
//    },
//    "Parent_Key" : {
//        "Child_Key" : "value2"
//    },
//    "Child_Key" : "value3"
//}


//string value1 = ConfigurationManager.AppSetting["GrandParent_Key:Parent_Key:Child_Key"];
//string value2 = ConfigurationManager.AppSetting["Parent_Key:Child_Key"];
//string value3 = ConfigurationManager.AppSetting["Child_Key"];