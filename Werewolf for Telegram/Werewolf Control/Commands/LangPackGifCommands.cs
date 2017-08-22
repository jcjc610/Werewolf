using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Werewolf_Control.Helpers;
using Werewolf_Control.Handler;
using Werewolf_Control.Models;
using RegHelper = Werewolf_Control.Helpers.RegHelper;

namespace Werewolf_Control
{
    public static partial class Commands
    {

        [Attributes.Command(Trigger = "langpackgif", LanguageAdminOnly = true)]
        public static void LangPackGif(Update update, string[] args)
        {
            // Send("test", u.Message.Chat.Id);
            var menu = UpdateHandler.GetLangPackGifMenu(update.Message.From.Id);
            Bot.Api.SendTextMessage(update.Message.From.Id, "Which language's gifs are you going to manage?",
                replyMarkup: menu);
        }

        public static void AddLangPackGif(Message message)
        {
            using (var db = new WWContext())
            {
                var data = message.ReplyToMessage.Text;
                var gifchoice = data.Substring(data.IndexOf("#") + 1).Split(' ')[0];
                var variantchoice = int.Parse(data.Substring(data.IndexOf("$") + 1));
                var variant = db.GameGifs.FirstOrDefault(x => x.VariantId == variantchoice);
                var fileid = message.Document.FileId;
                switch (gifchoice)
                {
                    case "vgeaten":
                        variant.VillagerDieImages = fileid;
                        break;
                    case "wolfwin":
                        variant.WolfWin = fileid;
                        break;
                    case "wolveswin":
                        variant.WolvesWin = fileid;
                        break;
                    case "vgwin":
                        variant.VillagersWin = fileid;
                        break;
                    case "tannerwin":
                        variant.TannerWin = fileid;
                        break;
                    case "cultwin":
                        variant.CultWins = fileid;
                        break;
                    case "loverwin":
                        variant.LoversWin = fileid;
                        break;
                    case "nowinner":
                        variant.NoWinner = fileid;
                        break;
                    case "startgame":
                        variant.StartGame = fileid;
                        break;
                    case "startchaos":
                        variant.StartChaosGame = fileid;
                        break;
                    case "skwin":
                        variant.SerialKillerWins = fileid;
                        break;
                    case "vgkilled":
                        variant.VillagerDieBySK = fileid;
                        break;
                }
                db.SaveChanges();
                InlineKeyboardMarkup newmenu = UpdateHandler.GetLangPackGifMenu(message.From.Id, variantchoice);
                Bot.Api.SendTextMessage(message.From.Id, "Please choose from the below Gif Titles.", replyMarkup: newmenu);
            }
        }
    }
}