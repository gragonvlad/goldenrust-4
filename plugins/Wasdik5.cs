using Oxide.Core.Libraries.Covalence;
using System.Collections.Generic;

namespace Oxide.Plugins
{
    [Info("First plugin", "Wasdik", "0.1.0")]
    [Description("Our first pugin for Rust")]
    class Wasdik5 : CovalencePlugin
    {
        private void Init()
        {
            
        }
		
		protected override void LoadDefaultMessages()
		{
			lang.RegisterMessages(new Dictionary<string, string>
			{
				["EpicThing"] = "An epic thing has happened",
				["EpicTimes"] = "An epic thing has happened: {0} time(s) {1} !!!"
			}, this);
			
			lang.RegisterMessages(new Dictionary<string, string> 
			{
				["EpicThing"] = "Это первая фраза",
				["EpicTimes"] = "Эта фраза вывелась: {0} раз(а)"
			}, this, "ru");
			
			lang.RegisterMessages(new Dictionary<string, string>
			{
				["EpicThing"] = "Це перша фраза",
				["EpicTimes"] = "Ця фраза вивелася: {0} раз(iв)"
			}, this, "uk");
		}
		
		[Command("test10")]
		private void TestCommand10(IPlayer player, string command, string[] args)
		{
			Dictionary<string, string> messages = lang.GetMessages("en", this);

			Puts($"Messages for {Title}:");
			foreach(KeyValuePair<string, string> message in messages)
			{
				Puts($"{message.Key}: {message.Value}");
			}
			
			Dictionary<string, string> messages_ru = lang.GetMessages("ru", this);

			Puts($"Messages for {Title}:");
			foreach(KeyValuePair<string, string> message in messages_ru)
			{
				Puts($"{message.Key}: {message.Value}");
			}

		}
		
		[Command("epicstuff.message")]
		private void TestMessageCommand(IPlayer player)
		{
			string message = lang.GetMessage("EpicThing", this, player.Id);
			Puts(message);
		}
		
		int amount = 0;

		[Command("epicstuff.amount")]
		private void TestAmountCommand(IPlayer player)
		{
			amount++;
			string message = lang.GetMessage("EpicTimes", this, player.Id);
			Puts(string.Format(message, amount.ToString(), "777"));
		}

		[Command("epicstuff.language")]
		private void TestLanguageCommand(IPlayer player)
		{
			Puts(lang.GetLanguage(player.Id));
			// Will output (by default): en
		}
		
		[Command("epicstuff.french")]
		private void TestUpdateCommand(IPlayer player)
		{
			lang.SetLanguage("fr", player.Id);
			Puts("Merci bien! Votre langue est le français");
		}
		
		[Command("epicstuff.getlanguages")]
		private void TestUpdateCommand2(IPlayer player)
		{
			string[] languages = lang.GetLanguages(this);

			Puts($"Supported languages for {Title}:");
			foreach(string language in languages)
			{
				Puts(language);
			}
		}
		[Command("epicstuff.setserverlanguages")]
		private void TestUpdateCommand3(IPlayer player)
		{
			lang.SetServerLanguage("en");
		}
		[Command("epicstuff.getserverlanguages")]
		private void TestUpdateCommand4(IPlayer player)
		{
			Puts(lang.GetServerLanguage()); // Will output (by default): en
		}
    }
}