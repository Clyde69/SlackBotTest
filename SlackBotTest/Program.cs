using eZet.EveLib.Core;
using eZet.EveLib.EveXmlModule;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using R3MUS.Devpack.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static eZet.EveLib.EveXmlModule.Models.Character.UpcomingCalendarEvents;

namespace SlackBotTest
{

    class Program
    {
        [DllImport("user32.dll")]
        internal static extern bool SendMessage(IntPtr hWnd, Int32 msg, Int32 wParam, Int32 lParam);
        static Int32 WM_SYSCOMMAND = 0x0112;
        static Int32 SC_MINIMIZE = 0x0F020;
        static void Main(string[] args)
        {
            SendMessage(Process.GetCurrentProcess().MainWindowHandle, WM_SYSCOMMAND, SC_MINIMIZE, 0);

            while (true)
            {
                try
                {
                    Task t = ConnectionTest();
                    t.Wait();
                }
                catch(Exception ex) {
                    //Console.WriteLine(ex.InnerException.Message);
                    System.Threading.Thread.Sleep(20000);
                }
            }
        }        

        static async Task ConnectionTest()
        {
            var start = (ResponseRoot)Web.BaseRequest("https://slack.com/api/rtm.start/?token=xoxb-7037557250-iIxD9Vgm1UlZdpbXpXJEJR4d").Deserialize(typeof(ResponseRoot));

            var channelDeets = start.channels.First(f => f.name.Equals("public"));
            var denDeets = start.channels.First(f => f.name.Equals("den"));
            var groupDeets = start.groups.First(f => f.name.Equals("it_projects"));

            using (ClientWebSocket ws = new ClientWebSocket())
            {
                Uri serverUri = new Uri(start.url);
                await ws.ConnectAsync(serverUri, CancellationToken.None);
                if (ws.State == WebSocketState.Open)
                {
                    ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
                    WebSocketReceiveResult result = await ws.ReceiveAsync(bytesReceived, CancellationToken.None);
                    Console.WriteLine(Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count));
                }
                while (ws.State == WebSocketState.Open)
                {
                    ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
                    WebSocketReceiveResult result = await ws.ReceiveAsync(bytesReceived, CancellationToken.None);

                    try
                    {
                        var rcv = Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);

                        var responseObj = (BaseType)rcv.Deserialize(typeof(BaseType));
                        
                        if(responseObj.type == "presence_change")
                        {
                            //var message = (Presence)rcv.Deserialize(typeof(Presence));
                            //Console.WriteLine(rcv);
                        }
                        else if (responseObj.type == "message")
                        {
                            var message = (MessageRx)rcv.Deserialize(typeof(MessageRx));

                            if (message.channel.Equals(channelDeets.id) && (message.user == null))
                            {
                                //ws.SendMessage("We have a visitor in #public. Please can someone go & talk to them?", groupDeets.id);
                            }
                            message.User = start.users.First(f => f.id == message.user).real_name;
                            if((message.User == "Exile Erika") && (message.text.ToLower().Contains("destiny")))
                            {
                                if (message.channel.StartsWith("G"))
                                {
                                    message.Channel = start.groups.First(f => f.id == message.channel).name;
                                }
                                else
                                {
                                    message.Channel = start.channels.First(f => f.id == message.channel).name;
                                }
                                ws.SendMessage("For the love of all that's holy, stop talking about bloody Destiny. It's not a real game & we just don't care.", message.Channel);
                            }

                            if ((message.text.StartsWith("!") && (!message.channel.Equals(channelDeets.id))))
                            {
                                if (message.channel.StartsWith("G"))
                                {
                                    message.Channel = start.groups.First(f => f.id == message.channel).name;
                                }
                                else
                                {
                                    message.Channel = start.channels.First(f => f.id == message.channel).name;
                                }
                                Console.WriteLine(string.Concat(
                                    DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"), ": ",
                                    message.Channel, ": ", 
                                    message.User, ":  ", 
                                    message.text));

                                var command = message.text.Split(' ').First().Replace("!", "");

                                if (GetCommands().Contains(command)) {
                                    switch (((Commands)Enum.Parse(typeof(Commands), command)))
                                    {
                                        case Commands.ops:
                                            ws.SendMessage(string.Concat("*Eve Time is now ", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), "*\r\n", GetCalendar()), message.channel);
                                            break;
                                        case Commands.deploymentops:
                                            ws.SendMessage(string.Concat("*Eve Time is now ", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), "*\r\n", GetCalendar("deployment")), message.channel);
                                            break;
                                        case Commands.evetime:
                                            ws.SendMessage(string.Format("The time, at the 3rd beep, will be {0}. Beep. Beep. Beep.", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")), message.channel);
                                            break;
                                        case Commands.serverstatus:
                                            ws.SendMessage(string.Format("TQ status: {0}", GetServerStatus()), message.channel);
                                            break;
                                        case Commands.status:
                                            ws.SendMessage(string.Format("I'm fine thank you. How are you?"), message.channel);
                                            break;
                                        case Commands.winninglotterynumbers:
                                            ws.SendMessage(string.Format("42 & 69"), message.channel);
                                            break;
                                        case Commands.towers:
                                            ws.SendMessage(GetTowers(), message.channel);
                                            break;
                                        case Commands.commands:
                                            ws.SendMessage(GetCommands(), message.channel);
                                            break;
                                        case Commands.help:
                                            ws.SendMessage(string.Format("I don't think I'm qualified to give you the help you need.", command), message.channel);
                                            break;
                                        case Commands.unbugger:
                                            ws.SendMessage(string.Format("I'm sorry Dave, but I can't let you do that."), message.channel);
                                            break;
                                        case Commands.moinlocation:
                                            ws.SendMessage(string.Format("His head is up his arse, second shelf on the right."), message.channel);
                                            break;
                                        case Commands.vaslocation:
                                            ws.SendMessage(string.Format("'Polishing his Erebus'."), message.channel);
                                            break;
                                        case Commands.ctacountdown:
                                            ws.SendMessage(GetCTACountdown(), message.channel);
                                            break;
                                        case Commands.joellocation:
                                            ws.SendMessage("'Looking for his banana.'", message.channel);
                                            break;
                                        case Commands.shouldiweartrouserstoday:
                                            //if(message.User == "Deepfry")
                                            //{
                                            //    ws.SendMessage(string.Format("YES!!! For the love of all that's holy PUT IT AWAY UNTIL YOU'VE SHAVED IT!!!."), message.channel);
                                            //}
                                            //else
                                            //{
                                            //ws.SendMessage(string.Format("No."), message.channel);
                                            //}

                                            ws.SendMessage(string.Format("Kilts FTW!!!!."), message.channel);
                                            break;
                                        case Commands.skintassong:
                                            ws.SendMessage(GetSkintasSong(), message.channel);
                                            break;
                                        default:
                                            ws.SendMessage(string.Format("I have no idea what '{0}' means", command), message.channel);
                                            break;
                                    }
                                }
                                else
                                {
                                    ws.SendMessage(string.Format("I have no idea what '{0}' means", command), message.channel);
                                }
                            }
                            else if (message.text.ToLower().Contains("slackbot"))
                            {
                                message.User = start.users.First(f => f.id == message.user).real_name;

                                if (message.channel.StartsWith("G"))
                                {
                                    message.Channel = start.groups.First(f => f.id == message.channel).name;
                                }
                                else
                                {
                                    message.Channel = start.channels.First(f => f.id == message.channel).name;
                                }
                                Console.WriteLine(string.Concat(
                                    DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"), ": ",
                                    message.Channel, ": ",
                                    message.User, ":  ",
                                    message.text));

                                if (message.text.IsShutUp())
                                {
                                    ws.SendMessage(string.Concat("Come & make me, ", message.User), message.channel);
                                }
                                else if (message.text.IsLoveMessage())
                                {
                                    ws.SendMessage(string.Format("I love you too, {0}, but I feel we should still see other people", message.User), message.channel);
                                }
                                else if (message.text.IsInsult())
                                {
                                    ws.SendMessage(string.Format(GenerateInsult(), message.User), message.channel);
                                }
                                else if (message.text.IsThankYou())
                                {
                                    ws.SendMessage(string.Format("You're very welcome, {0}!", message.User), message.channel);
                                }
                            }
                        }
                    }
                    catch(Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public static string GenerateInsult()
        {
            Random r = new Random();
            int rInt = r.Next(0, 6);

            switch (rInt)
            {
                case 1:
                    return "{0}, your mother was a hamster & your father smelled of elderberries! Now go away or I shall taunt you a second time!";
                case 2:
                    return "I'd insult you back, {0}, but nature did a far better job :P";
                case 3:
                    return "I'm jealous of people that don't know you, {0}.";
                case 4:
                    return "You're the reason they need to put instructions on shampoo bottles.";
                case 5:
                    return "{0}: The reason the gene pool needs a lifeguard.";
                default:
                    return "You kiss your mother with that mouth???"; 
            }
        }

        public static string GetSkintasSong()
        {
            var song = new List<string>();
            song.Add("His name was Skinta,");
            song.Add("He was an FC,");
            song.Add("Heated railguns and a beer");
            song.Add("and a tiny bladder here.\r\n");
            song.Add("He likes to solo ");
            song.Add("and do a blops op");
            song.Add("Don't invite him round for tea,");
            song.Add("You'll get Copson and Ali P\r\n");
            song.Add("With hunters pre - assigned ");
            song.Add(" Keep range, we're not aligned! ");
            song.Add("You'll loose some ships but on zkillboard, ");
            song.Add(" Some dank kills you'll find! \r\n");
            song.Add("At the blops op, the r3mus bloooops op. ");
            song.Add("Torpedos and TPs get Chrispus all weepy ");
            song.Add("at the blooooops op...we whelp in style.");

            return string.Join("\r\n", song);
        }

        public static async Task Outh2Test()
        {

        }

        public static string GetTowers()
        {
            var sheet = new CharacterKey(4355544, "0knl1LoJnR1ycZqSaPUCB9iXF2fwqEfISLHQcbpzrCJD0uE5lMSKnbY7dzVoj9Yj");
            var events = GetCalendarEvents().Where(s =>
                s.EventTitle.ToLower().Contains("control tower")
                &&
                (s.EventDate - DateTime.UtcNow).Days <= 7
                ).Select(s => string.Concat(s.EventDateAsString, ": ", s.EventTitle)).ToArray<string>();
            if (events.Count() == 0)
            {
                return "AIN'T GOT NO TOWERS GOIN' OFFLINE, FOOL!";
            }
            else
            {
                return string.Join("\r\n", events);
            }
        }

        public static string GetCalendar(string type = "all")
        {
            var events = GetCalendarEvents().Where(s => 
                !s.EventTitle.ToLower().Contains("control tower")
                &&
                s.OwnerName.ToLower() != "ccp"
                &&
                s.OwnerName.ToLower() != "eve system"
                &&
                s.EventDate >= DateTime.UtcNow
                &&
                (type == "all" || s.EventTitle.ToLower().Contains(type))
                ).Take(5).Select(s => string.Concat(s.EventDateAsString, ": ", s.EventTitle)).ToArray<string>();

            return string.Join("\r\n", events);
        }

        public static string GetServerStatus()
        {
            var response = Web.BaseRequest("https://api.eveonline.com/Server/ServerStatus.xml.aspx");
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(eveapi));
            var rdr = new System.IO.StringReader(response);
            var responseObj = (eveapi)serializer.Deserialize(rdr);

            try
            {
                switch (Convert.ToBoolean(responseObj.result.serverOpen))
                {
                    case false:
                        return "Down";
                    case true:
                        return "Up";
                    default:
                        return "Buggered if I know";
                }
            }
            catch
            {
                return "Probably Down";
            }
        }

        public static string GetCommands()
        {
            var result = new List<string>();
            result.Add("The following commands are available:");
            foreach (string command in Enum.GetNames(typeof(Commands)))
            {
                result.Add(string.Concat("!", command));
            }
            return string.Join("\n", result);
        }

        public static List<Event> GetCalendarEvents()
        {
            var sheet = new CharacterKey(4355544, "0knl1LoJnR1ycZqSaPUCB9iXF2fwqEfISLHQcbpzrCJD0uE5lMSKnbY7dzVoj9Yj");
            return sheet.Characters.First().GetUpcomingCalendarEvents().Result.Events.ToList();
        }

        public static string GetCTACountdown()
        {
            var CTA = GetCalendarEvents().Where(w => w.EventTitle.ToLower().Contains("cta")).FirstOrDefault();
            if(CTA != null)
            {
                var tDays = (CTA.EventDate - DateTime.UtcNow).Days;
                var tHrs = (CTA.EventDate - DateTime.UtcNow).Hours;
                var tMins = (CTA.EventDate - DateTime.UtcNow).Minutes;
                var tSecs = (CTA.EventDate - DateTime.UtcNow).Seconds;
                return string.Format("{0} Days {1} Hours {2} Minutes {3} Seconds til the next CTA", tDays.ToString(), tHrs.ToString(), tMins.ToString(), tSecs.ToString());
            }
            else
            {
                return "No CTAs on the calendar.";
            }
        }
    }

    public enum Commands
    {
        ops,
        deploymentops,
        evetime,
        serverstatus,
        status,
        winninglotterynumbers,
        towers,
        help,
        commands,
        unbugger,
        moinlocation,
        vaslocation,
        ctacountdown,
        shouldiweartrouserstoday,
        joellocation,
        skintassong
    }

    public static class Extensions
    {
        public static void SendMessage(this ClientWebSocket ws, string msgText, string channelId)
        {
            Console.WriteLine(string.Concat(msgText, "\r\n"));
            var m = new MessageTx() { id = 999999, type = "message", channel = channelId, text = msgText };
            var msg = JsonConvert.SerializeObject(m);

            var encoded = Encoding.UTF8.GetBytes(msg);

            ArraySegment<byte> bytesToSend = new ArraySegment<byte>(encoded, 0, encoded.Length);
            var response = ws.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public static bool IsInsult(this string message)
        {
            var slagWords = new List<string>();
            slagWords.Add("ass");
            slagWords.Add("arse");
            slagWords.Add("fuck");
            slagWords.Add("bastard");
            slagWords.Add("shit");
            slagWords.Add("cunt");
            slagWords.Add("wanker");
            slagWords.Add("screw you");

            return slagWords.Any(a => message.ToLower().Contains(a));
        }

        public static bool IsLoveMessage(this string message)
        {
            return message.ToLower().Contains("i love you");
        }

        public static bool IsShutUp(this string message)
        {
            return message.ToLower().Contains("shut up");
        }
        public static bool IsThankYou(this string message)
        {
            return (message.ToLower().Contains("thanks") || message.ToLower().Contains("thank you"));
        }
    }

    public class MessageRx : BaseType
    {
        public string channel { get; set; }
        [JsonIgnore]
        public string Channel { get; set; }
        public string user { get; set; }
        [JsonIgnore]
        public string User { get; set; }
        public string text { get; set; }
        public string ts { get; set; }
        public string source_team { get; set; }
        //public string type { get; set; }
        public string team { get; set; }
    }

    public class MessageTx : BaseType
    {
        public int id { get; set; }
        public string channel { get; set; }
        //public string type { get; set; }
        public string text { get; set; }
    }

    public class BaseType
    {
        public string type { get; set; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class eveapi
    {

        private string currentTimeField;

        private eveapiResult resultField;

        private string cachedUntilField;

        private byte versionField;

        /// <remarks/>
        public string currentTime
        {
            get
            {
                return this.currentTimeField;
            }
            set
            {
                this.currentTimeField = value;
            }
        }

        /// <remarks/>
        public eveapiResult result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }

        /// <remarks/>
        public string cachedUntil
        {
            get
            {
                return this.cachedUntilField;
            }
            set
            {
                this.cachedUntilField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class eveapiResult
    {

        private string serverOpenField;

        private ushort onlinePlayersField;

        /// <remarks/>
        public string serverOpen
        {
            get
            {
                return this.serverOpenField;
            }
            set
            {
                this.serverOpenField = value;
            }
        }

        /// <remarks/>
        public ushort onlinePlayers
        {
            get
            {
                return this.onlinePlayersField;
            }
            set
            {
                this.onlinePlayersField = value;
            }
        }
    }
}
