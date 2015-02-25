using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VK.API.LPServer
{
    public class LPServerInfo
    {
        [JsonProperty("server")]
        public String Server = null;

        [JsonProperty("key")]
        public String Key = null;

        [JsonProperty("ts")]
        public int TS;
    }

    public class LPServerResponse
    {
        [JsonProperty("ts")]
        public int TS = 0;

        [JsonProperty("updates")]
        public object[] Updates = null;

        [JsonProperty("failed")]
        public int Failed = -1;
    }

    /*
        0,$message_id,0 — удаление сообщения с указанным local_id
        1,$message_id,$flags — замена флагов сообщения (FLAGS:=$flags)
        2,$message_id,$mask[,$user_id] — установка флагов сообщения (FLAGS|=$mask)
        3,$message_id,$mask[,$user_id] — сброс флагов сообщения (FLAGS&=~$mask)
        4,$message_id,$flags,$from_id,$timestamp,$subject,$text,$attachments — добавление нового сообщения
        6,$peer_id,$local_id — прочтение всех входящих сообщений с $peer_id вплоть до $local_id включительно
        7,$peer_id,$local_id — прочтение всех исходящих сообщений с $peer_id вплоть до $local_id включительно
        8,-$user_id,$extra — друг $user_id стал онлайн, $extra не равен 0, если в mode был передан флаг 64, в младшем байте (остаток от деления на 256) числа $extra лежит идентификатор платформы (таблица ниже)
        9,-$user_id,$flags — друг $user_id стал оффлайн ($flags равен 0, если пользователь покинул сайт (например, нажал выход) и 1, если оффлайн по таймауту (например, статус away))

        51,$chat_id,$self — один из параметров (состав, тема) беседы $chat_id были изменены. $self - были ли изменения вызваны самим пользователем
        61,$user_id,$flags — пользователь $user_id начал набирать текст в диалоге. событие должно приходить раз в ~5 секунд при постоянном наборе текста. $flags = 1
        62,$user_id,$chat_id — пользователь $user_id начал набирать текст в беседе $chat_id.
        70,$user_id,$call_id — пользователь $user_id совершил звонок имеющий идентификатор $call_id.
        80,$count,0 — новый счетчик непрочитанных в левом меню стал равен $count.
    */
    public enum LPEventType
    {
        Unknown = -1,

        MessageDeleted = 0,
        MessageFlagsReplaced = 1,
        MessageFlagsSet = 2,
        MessageFlagsDeleted = 3,
        MessageAdded = 4,

        InMessagesRead = 6,
        OutMessagesRead = 7,

        FriendOnline = 8,
        FriendOffline = 9,

        ChatUpdated = 51,

        UserTyping = 61,
        UserTypingInChat = 62,

        UserCalled = 70,

        UnreadCountUpdated = 80
    }

    public class LPEvent : EventArgs
    {
        public LPEventType EventType = LPEventType.Unknown;
        public object[] Data;

        public LPEvent(object[] data)
        {
            if (data.Length == 0)
                return;

            int eType = Convert.ToInt32(data[0]);

            if(data.Length > 1){
                object[] ndata = new object[data.Length - 1];
                for (int i = 1; i < data.Length; i++)
                    ndata[i - 1] = data[i];

                data = ndata;
            }

            this.Data = data;

            if(Enum.IsDefined(typeof(LPEventType), eType))
                this.EventType = (LPEventType) eType;
        }
    }

    public static class LongPollServer
    {
        private static LPServerInfo _server;

        private static readonly Dictionary<LPEventType, ExEvent<LPEvent>> Events = new Dictionary<LPEventType, ExEvent<LPEvent>>();

        public static void On(LPEventType type, ExEventDelegate<LPEvent> d)
        {
            if (!Events.ContainsKey(type))
                Events.Add(type, new ExEvent<LPEvent>());

            Events[type] += d;
        }

        private static async Task<LPServerInfo> GetServer()
        {
            try
            {
                _server = (await API.Call<LPServerInfo>("messages.getLongPollServer")).Response;

                return _server;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(e.StackTrace);
            }

            return await GetServer();
        }

        public static void Connect(bool failed = false)
        {
            new Thread(async () =>
            {
                try
                {
                    while (true)
                    {
                        while (_server == null || failed)
                        {
                            _server = await GetServer();
                            failed = false;
                        }

                        String u = "http://" + _server.Server + "?act=a_check&key=" + _server.Key + "&ts=" + _server.TS +
                                   "&wait=25&mode=2";

                        HttpWebRequest req = (HttpWebRequest) WebRequest.Create(u);

                        WebResponse r = await req.GetResponseAsync();

                        Stream s = r.GetResponseStream();

                        if (s != null)
                        {
                            StreamReader sr = new StreamReader(s);

                            String str = sr.ReadToEnd();

                            if (!String.IsNullOrEmpty(str))
                            {
                                LPServerResponse resp = JsonConvert.DeserializeObject<LPServerResponse>(str);

                                if (resp.Failed != -1)
                                {
                                    failed = true;
                                }
                                else
                                {
                                    _server.TS = resp.TS;

                                    foreach (JArray upd in resp.Updates)
                                    {
                                        LPEvent e = new LPEvent(upd.Cast<object>().ToArray());

                                        Console.WriteLine(e.EventType + @": " + String.Join(", ", e.Data));

                                        if (Events.ContainsKey(e.EventType))
                                        {
                                            Events[e.EventType].Dispatch(e);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }).Start();
        }
    }
}
