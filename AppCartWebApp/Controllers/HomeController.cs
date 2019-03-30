using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppCartWebApp.Models;
using System.Net.Http;
using System.Net.NetworkInformation;
using AppConfiguration;

namespace AppCartWebApp.Controllers
{
    public class Info
    {
        public string Key { get; set; }
        public string Message { get; set; }
    }

    public class HomeViewModel
    {
        public List<Info> ListInfo { get; set; }
    }

    public class HomeController : Controller
    {

        public async Task<IActionResult> Index()
        {
            var version  = ConfigurationManager.AppSetting["Version"];
            var url = "http://paymentapi-svc/api/values";
            Uri Endpoint = null;
            string messageResult = "";
            HttpClient client = new HttpClient();
            try
            {
                Endpoint = new Uri(url);
                var message = await client.GetAsync(Endpoint);
                messageResult = message.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {

                //Endpoint = new Uri("http://appcartfrontapi/front/api/values");
                messageResult = "Error";
            }
            
            
            
            

            //ViewData["Url"] = Endpoint;
            //ViewData["Message"] = message;

            var viewModel = new HomeViewModel();
            viewModel.ListInfo = new List<Info>();

            viewModel.ListInfo.Add(new Info() { Key = "Version", Message = version });
            viewModel.ListInfo.Add(new Info() { Key = "Url", Message = url });
            viewModel.ListInfo.Add(new Info() { Key = "HostName Wep App", Message = System.Net.Dns.GetHostName() });
            //viewModel.ListInfo.Add(new Info() { Key = "IP Web App", Message = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString()});

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    Console.WriteLine(ni.Name);
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            //Console.WriteLine(ip.Address.ToString());
                            viewModel.ListInfo.Add(new Info() { Key = "IP", Message = ip.Address.ToString() });
                        }
                    }
                }
            }

            viewModel.ListInfo.Add(new Info() { Key = "Get FrontApiUrl", Message = messageResult});

            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
