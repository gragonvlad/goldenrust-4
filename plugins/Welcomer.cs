using Newtonsoft.Json;
using Oxide.Core.Libraries.Covalence;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Oxide.Plugins
{
    [Info("Welcomer", "Tricky", "1.4.1")]
    [Description("Provides welcome and join/leave messages")]

    public class Welcomer : RustPlugin
    {
        #region Config
        ConfigData configData;

        class ConfigData
        {
            [JsonProperty(PropertyName = "Enable: Welcome Message")]
            public bool WelcomeMessage { get; set; }

            [JsonProperty(PropertyName = "Enable: Join Messages")]
            public bool JoinMessages { get; set; }

            [JsonProperty(PropertyName = "Enable: Leave Messages")]
            public bool LeaveMessages { get; set; }

            [JsonProperty(PropertyName = "Chat Icon (SteamID64)")]
            public ulong ChatIcon { get; set; }

            [JsonProperty(PropertyName = "Display Steam Avatar of Player - Join/Leave")]
            public bool SteamAvatar { get; set; }

            [JsonProperty(PropertyName = "Broadcast To Console - Join/Leave")]
            public bool BroadcasttoConsole { get; set; }
        }

        protected override void LoadDefaultConfig()
        {
            var config = new ConfigData
            {
                WelcomeMessage = true,
                JoinMessages = true,
                LeaveMessages = true,
                ChatIcon = 0,
                SteamAvatar = true,
                BroadcasttoConsole = true
            };
            SaveConfig(config);
        }

        void LoadConfigVariables() => configData = Config.ReadObject<ConfigData>();

        void SaveConfig(ConfigData config) => Config.WriteObject(config, true);

        void Init()
        {
            LoadConfigVariables();
        }
        #endregion

        #region Permissions
        private void OnServerInitialized()
        {
            permission.RegisterPermission("welcomer.bypass", this);
        }
        #endregion

        #region API Class
        class Response
        {
            [JsonProperty("country")]
            public string Country { get; set; }
        }
        #endregion

        #region Lang
        protected override void LoadDefaultMessages()
        {
            lang.RegisterMessages(new Dictionary<string, string>
            {
                ["Welcome"] = "<size=17>Welcome to <color=#0099CC>{0}</color></size>\n--------------------------------------------\n<color=#0099CC>•</color> Type <color=#0099CC>/info</color> for all available commands\n<color=#0099CC>•</color> Read the server rules by typing <color=#0099CC>/info</color>\n<color=#0099CC>•</color> Have fun and respect other players",
                ["Joined"] = "<color=#37BC61>✔</color> {0} <color=#37BC61>joined the server</color> from <color=#37BC61>{1}</color>",
                ["JoinedUnknown"] = "<color=#37BC61>✔</color> {0} <color=#37BC61>joined the server</color>",
                ["Left"] = "<color=#FF4040>✘</color> {0} <color=#FF4040>left the server</color> ({1})"
            }, this);
        }
        #endregion

        #region Collection
        List<ulong> connected = new List<ulong>();
        #endregion

        #region OnPlayerHooks
        private void OnPlayerInit(BasePlayer player)
        {
            if (connected.Contains(player.userID))
                return;

            connected.Add(player.userID);
        }

        private void OnPlayerSleepEnded(BasePlayer player)
        {
            if (!connected.Contains(player.userID))
                return;

            connected.Remove(player.userID);

            if (configData.WelcomeMessage)
            {
                if (permission.UserHasPermission(player.UserIDString, "welcomer.bypass"))
                    return;

                string message = lang.GetMessage("Welcome", this, player.UserIDString);

                Player.Message(player, string.Format(message, ConVar.Server.hostname), configData.ChatIcon);
            }

            if (configData.JoinMessages)
            {
                if (permission.UserHasPermission(player.UserIDString, "welcomer.bypass"))
                    return;

                string playerAddress = player.net.connection.ipaddress.Split(':')[0];
                webrequest.Enqueue("http://ip-api.com/json/" + playerAddress, null, (code, response) =>
                {
                    if (code != 200 || response == null)
                    {
                        string unknownmsg = lang.GetMessage("JoinedUnknown", this, player.UserIDString);

                        Broadcast(unknownmsg, player, null);
                        return;
                    }

                    string message = lang.GetMessage("Joined", this, player.UserIDString);
                    string country = JsonConvert.DeserializeObject<Response>(response).Country;

                    Broadcast(message, player, country);

                }, this);
            }
        }

        private void OnPlayerDisconnected(BasePlayer player, string reason)
        {
            if (!configData.LeaveMessages)
                return;

            if (permission.UserHasPermission(player.UserIDString, "welcomer.bypass"))
                return;

            string message = lang.GetMessage("Left", this, player.UserIDString);

            Broadcast(message, player, reason);
        }
        #endregion

        #region Helpers
        private void Broadcast(string message, BasePlayer player, string info)
        {
            Server.Broadcast(string.Format(message, player.displayName, info), null, configData.SteamAvatar ? player.userID : configData.ChatIcon);

            if (configData.BroadcasttoConsole)
            {
                var stringReplacements = new string[]
                {
                    "<b>", "</b>",
                    "<i>", "</i>",
                    "</size>",
                    "</color>"
                };

                var regexReplacements = new Regex[]
                {
                    new Regex(@"<color=.+?>"),
                    new Regex(@"<size=.+?>"),
                };

                foreach (var replacement in stringReplacements)
                    message = message.Replace(replacement, string.Empty);

                foreach (var replacement in regexReplacements)
                    message = replacement.Replace(message, string.Empty);

                PrintWarning(string.Format(Formatter.ToPlaintext(message), player.displayName, info));
            }
        }
        #endregion
    }
}
