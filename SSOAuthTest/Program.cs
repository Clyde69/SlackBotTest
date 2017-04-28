using HtmlAgilityPack;
using Microsoft.Owin.Builder;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Owin;
using Owin.Security.Providers.EVEOnline;
using R3MUS.Devpack.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSOAuthTest
{
    class Program
    {
        static string baseUri = "https://login.eveonline.com?redirect_uri=eveauth-r3mus-esi://callback";
        static string authUrl = "https://login.eveonline.com/oauth/authorize/?response_type=code&redirect_uri=";

        static string uri1 = "https://login.eveonline.com/Account/LogOn?ReturnUrl=";

        static void Main(string[] args)
        {
            EveOWIN();
        }
        public static string HttpPostRequest(string url, string post)
        {
            CookieCollection cookies = new CookieCollection();
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create("https://login.eveonline.com");
            request1.CookieContainer = new CookieContainer();
            request1.CookieContainer.Add(cookies);
            HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
            cookies = response1.Cookies;

            var encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(post);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(cookies);
            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            String result;
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }

        public static string WebPost(string uri, NameValueCollection pairs)
        {
            byte[] response = null;
            using (WebClient client = new WebClient())
            {
                response = client.UploadValues(uri, pairs);
            }
            var result = System.Text.Encoding.UTF8.GetString(response);
            return result;
        }

        public static string SeleniumPost()
        {
            ChromeDriver cd = new ChromeDriver(Environment.CurrentDirectory);
            cd.Url = @"https://login.eveonline.com";
            cd.Navigate();
            IWebElement e = cd.FindElementById("UserName");
            e.SendKeys("");
            e = cd.FindElementById("Password");
            e.SendKeys("");
            e = cd.FindElementById("submitButton");
            
            e.Click();

            var cc = new CookieContainer();

            foreach (OpenQA.Selenium.Cookie c in cd.Manage().Cookies.AllCookies)
            {
                string name = c.Name;
                string value = c.Value;
                cc.Add(new System.Net.Cookie(name, value, c.Path, c.Domain));

                Console.WriteLine(string.Format("{0}: {1}: {2}: {3}", name, value, c.Path, c.Domain));
            }
            Console.ReadLine();

            cd.Url = authUrl;
            cd.Navigate();
            e = cd.FindElementByXPath(@"//*[@id='main']/form/div[3]/input[1]");
            e.Click();
            foreach (OpenQA.Selenium.Cookie c in cd.Manage().Cookies.AllCookies)
            {
                string name = c.Name;
                string value = c.Value;
                cc.Add(new System.Net.Cookie(name, value, c.Path, c.Domain));

                Console.WriteLine(string.Format("{0}: {1}: {2}: {3}", name, value, c.Path, c.Domain));
            }
            Console.ReadLine();

            //HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(authUrl);
            //hwr.CookieContainer = cc;
            //hwr.Method = "POST";
            //hwr.ContentType = "application/x-www-form-urlencoded";
            //StreamWriter swr = new StreamWriter(hwr.GetRequestStream());
            //swr.Write("feeds=35");
            //swr.Close();

            //WebResponse wr = hwr.GetResponse();
            //string result = new System.IO.StreamReader(wr.GetResponseStream()).ReadToEnd();

            //return result;
            return string.Empty;
        }

        public static void EveOWIN()
        {
            var app = new AppBuilder();
            var provider = new EveOnlineAuthenticationProvider();

            provider.OnAuthenticated += Authenticated;

            var authOptions = new EveOnlineAuthenticationOptions()
            {
                ClientId = "585af301715a42f9b66a0d20f9493220",
                ClientSecret = "2uSMv3JRGocExxCl3xH8SVe0AoS9m9Xh8yILeybf",
                Provider = new EveOnlineAuthenticationProvider(),
                CallbackPath = new Microsoft.Owin.PathString("/eveauth-r3mus-esi://callback")
            };
            app.UseEveOnlineAuthentication(authOptions);

            var cI = new ClaimsIdentity();
            cI.AddClaim(new Claim("Username", ""));
            cI.AddClaim(new Claim("Password", ""));

            var request = WebRequest.Create(uri1) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            var context = System.Web.HttpContext.Current;
        }

        private static Task Authenticated(EveOnlineAuthenticatedContext arg)
        {
            Console.WriteLine(arg.CharacterName);
            Console.ReadLine();

            return Task.FromResult<EveOnlineAuthenticatedContext>(arg);
        }

        private static void Cock()
        {
            var request = WebRequest.Create(uri1) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = new CookieContainer();

            //var authMgr = request.
        }
    }
}
