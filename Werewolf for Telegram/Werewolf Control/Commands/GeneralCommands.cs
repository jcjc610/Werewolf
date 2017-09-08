using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Database;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Werewolf_Control.Attributes;
using Werewolf_Control.Helpers;

namespace Werewolf_Control
{
    public static partial class Commands
    {
        [Command(Trigger = "ping")]
        public static void Ping(Update update, string[] args)
        {
            Console.Clear();
            try
            {
                var ts = DateTime.UtcNow - update.Message.Date;
                var send = DateTime.UtcNow;
                var message = GetLocaleString("PingInfo", GetLanguage(update.Message.From.Id), $"{ts:mm\\:ss\\.ff}",
                    Program.AvgCpuTime.ToString("F0"),
                    $"\n{Program.MessagePxPerSecond.ToString("F0")} MAX IN | {Program.MessageTxPerSecond.ToString("F0")} MAX OUT");
                message +=
                    $"\nIN last min: {Program.MessagesReceived.Sum()}\nOUT last min: {Program.MessagesSent.Sum()}";
                var result = Bot.Send(message, update.Message.Chat.Id).Result;
                ts = DateTime.UtcNow - send;
                message += "\n" + GetLocaleString("Ping2", GetLanguage(update.Message.From.Id), $"{ts:mm\\:ss\\.ff}");
                result = Bot.Api.EditMessageText(update.Message.Chat.Id, result.MessageId, message).Result;
            }
            catch (AggregateException e)
            {
                Bot.Send(e.InnerExceptions[0].Message, update.Message.From.Id);
            }
            catch (Exception e)
            {
                Bot.Send(e.Message, update.Message.From.Id);
            }

        }
#if (false)
        [Command(Trigger = "achv")]
        public static void GetAchievements(Update u, string[] args)
        {
            Send("Please use /stats", u.Message.Chat.Id);
        }
#endif
        [Command(Trigger = "help")]
        public static void Help(Update update, string[] args)
        {
            Bot.Api.SendTextMessage(update.Message.Chat.Id, "This is a clone of @werewolfbot by @jeffffc.\n[Official Website](http://www.tgwerewolf.com/?referrer=help)\n/rolelist (don't forget to /setlang first!)\n[Telegram Werewolf Support Group](http://telegram.me/greywolfsupport)\n[Telegram Werewolf Dev Channel](https://telegram.me/greywolfdev)",
                                                        parseMode: ParseMode.Markdown);
        }

        [Command(Trigger = "chatid")]
        public static void ChatId(Update update, string[] args)
        {
            Send(update.Message.Chat.Id.ToString(), update.Message.Chat.Id);

        }

        [Command(Trigger = "donate")]
        public static void Donate(Update u, string[] args)
        {
            Bot.Api.SendTextMessage(u.Message.Chat.Id, "Please go to @werewolfbot and use <code>/donate</code>.", parseMode: ParseMode.Html);
        }

        [Command(Trigger = "changelog")]
        public static void ChangeLog(Update update, string[] args)
        {
            Send("Changelog moved to <a href=\"www.tgwerewolf.com/#changes?referrer=changelog\">here</a>\nAlso check out the dev channel @werewolfdev", update.Message.Chat.Id);
        }

        [Command(Trigger = "runinfo")]
        public static void RunInfo(Update update, string[] args)
        {
            var result = "*Run information*\n";
            result +=
                $"Uptime: {DateTime.UtcNow - Bot.StartTime}\nConnected Nodes: {Bot.Nodes.Count}\n" +
                $"Current Games: {Bot.Nodes.Sum(x => x.CurrentGames)}\n" +
                $"Current Players: {Bot.Nodes.Sum(x => x.CurrentPlayers)}";
            Bot.Api.SendTextMessage(update.Message.Chat.Id, result, parseMode: ParseMode.Markdown);
        }

        /*
        [Command(Trigger = "getstatus")]
        public static void GetStatus(Update u, string[] args)
        {
            using (var db = new WWContext())
            {
                var msg =
                    db.BotStatus.ToList().Select(x => $"{x.BotName} (@{x.BotLink}):{(x.BotName == "Bot 2" ? "RETIRED" : x.BotStatus)} ").ToList()
                        .Aggregate((a, b) => a + "\n" + b);
                Send(msg, u.Message.Chat.Id);
            }
        }*/

        [Command(Trigger = "version")]
        public static void Version(Update update, string[] args)
        {
            var version = Program.GetVersion();
            try
            {
                var node =
                    Bot.Nodes.ToList().FirstOrDefault(x => x.Games.Any(g => g.GroupId == update.Message.Chat.Id));

                version += !String.IsNullOrWhiteSpace(node?.Version)
                    ? $"\nNode Version: {node?.Version}\nNode Id: {node?.ClientId}"
                    : "\nNode Version: You are not on a node right now (no game running in this group)";
            }
            catch
            {
                // ignored
            }
            Send(version, update.Message.Chat.Id);
        }

        [Command(Trigger = "setlang")]
        public static void SetLang(Update update, string[] args)
        {
            Player p = null;
            using (var db = new WWContext())
            {
                p = db.Players.FirstOrDefault(x => x.TelegramId == update.Message.From.Id);

                if (p == null)
                {


                    p = new Player
                    {
                        TelegramId = update.Message.From.Id,
                        Language = "English",
#if RELEASE
                        HasPM = update.Message.Chat.Type == ChatType.Private
#elif RELEASE2
                        HasPM2 = update.Message.Chat.Type == ChatType.Private
#elif DEBUG
                        HasDebugPM = update.Message.Chat.Type == ChatType.Private
#endif
                    };
                    db.Players.Add(p);


                    p.UserName = update.Message.From.Username;
                    p.Name = $"{update.Message.From.FirstName} {update.Message.From.LastName}".Trim();

                    db.SaveChanges();
                    //user obvious has no PM status, notify them
#if RELEASE
                    if (p.HasPM != true)
#elif RELEASE2
                    if (p.HasPM2 != true)
#elif DEBUG
                    if (p.HasDebugPM != true)
#endif
                    {
                        RequestPM(update.Message.Chat.Id);
                        return;
                    }
                }


            }
            //user wants to pick personal language
            var langs = Directory.GetFiles(Bot.LanguageDirectory, "*.xml").Select(x => new LangFile(x)).ToList();


            List<InlineKeyboardButton> buttons = langs.Select(x => x.Base).Distinct().OrderBy(x => x).Select(x => new InlineKeyboardButton(x, $"setlang|{update.Message.From.Id}|{x}|null|base")).ToList();

            var baseMenu = new List<InlineKeyboardButton[]>();
            for (var i = 0; i < buttons.Count; i++)
            {
                if (buttons.Count - 1 == i)
                {
                    baseMenu.Add(new[] { buttons[i] });
                }
                else
                    baseMenu.Add(new[] { buttons[i], buttons[i + 1] });
                i++;
            }

            var menu = new InlineKeyboardMarkup(baseMenu.ToArray());

            var curLangFileName = GetLanguage(update.Message.From.Id);
            var curLang = langs.First(x => x.FileName == curLangFileName);
            Bot.Api.SendTextMessage(update.Message.From.Id, GetLocaleString("WhatLang", curLangFileName, curLang.Base),
                replyMarkup: menu);
            if (update.Message.Chat.Type != ChatType.Private)
                Send(GetLocaleString("SentPrivate", GetLanguage(update.Message.From.Id)), update.Message.Chat.Id);
        }

        [Command(Trigger = "start")]
        public static void Start(Update u, string[] args)
        {
            if (u.Message.Chat.Type == ChatType.Private)
            {
                if (u.Message.From != null)
                {
                    using (var db = new WWContext())
                    {
                        var p = GetDBPlayer(u.Message.From.Id, db);
                        if (p == null)
                        {
                            var usr = u.Message.From;
                            p = new Player
                            {
                                UserName = usr.Username,
                                Name = (usr.FirstName + " " + usr.LastName).Trim(),
                                TelegramId = usr.Id,
                                Language = "English"
                            };
                            db.Players.Add(p);
                            db.SaveChanges();
                            p = GetDBPlayer(u.Message.From.Id, db);
                        }
#if RELEASE
                        p.HasPM = true;
#elif RELEASE2
                        p.HasPM2 = true;
#elif BETA
                        p.HasDebugPM = true;
#endif
                        db.SaveChanges();

                        if (String.IsNullOrEmpty(args[1]))
                        {
                            var msg = $"Hi there! I'm @{Bot.Me.Username}, a clone of @werewolfbot, and I moderate games of Werewolf." +
                                      $"\nUse /setlang to set PM language and /config after you added me into a group." +
                                      $"\nFor role information, use /rolelist." +
                                      $"\nBe sure to stop by <a href=\"https://telegram.me/greywolfsupport\">Werewolf Support</a> for any questions, and subscribe to @greywolfdev for updates from the developer." +
                                      $"\nMore infomation can be found <a href=\"https://www.tgwerewolf.com/?referrer=start\">here</a>!";
                            Bot.Send(msg,u.Message.Chat.Id);
                        }
                        /*
                        else
                        {
                            var uid = args[1];


                            
                            

                            //check the database for that user
                            {
                                var aspuser = db.AspNetUsers.Find(uid);
                                
                                //we have the asp user, let's find the player
                                var user = db.Players.FirstOrDefault(x => x.TelegramId == u.Message.From.Id);
                                if (user == null)
                                    return;
                                user.WebUserId = uid; //linked!
                                db.SaveChanges();
                                Send($"Your telegram account is now linked to your web account - {aspuser.Email}", u.Message.From.Id);
                            }
                        }
                        */
                    }
                }
            }
        }

        [Command(Trigger = "nextgame", Blockable = true, InGroupOnly = true)]
        public static void NextGame(Update update, string[] args)
        {
            var id = update.Message.Chat.Id;
            using (var db = new WWContext())
            {
                var grp = db.Groups.FirstOrDefault(x => x.GroupId == id);
                if (grp == null)
                {
                    grp = MakeDefaultGroup(id, update.Message.Chat.Title, "nextgame");
                    db.Groups.Add(grp);
                    db.SaveChanges();
                }

                //check nodes to see if player is in a game
                //node = GetPlayerNode(update.Message.From.Id);
                var game = GetGroupNodeAndGame(update.Message.Chat.Id);
                if (game != null)
                {

                    if (game?.Users.Contains(update.Message.From.Id) ?? false)
                    {
                        if (game?.GroupId != update.Message.Chat.Id)
                        {
                            //player is already in a game, and alive
                            Send(
                                GetLocaleString("AlreadyInGame", grp.Language ?? "English",
                                    game.ChatGroup.ToBold()), update.Message.Chat.Id);
                            return;
                        }
                    }
                }

                if (db.NotifyGames.Any(x => x.GroupId == id && x.UserId == update.Message.From.Id))
                {
                    Send(GetLocaleString("AlreadyOnWaitList", grp.Language, grp.Name.ToBold()),
                        update.Message.From.Id);
                }
                else
                {
                    db.Database.ExecuteSqlCommand(
                        $"INSERT INTO NotifyGame VALUES ({update.Message.From.Id}, {id})");
                    db.SaveChanges();
                    Send(GetLocaleString("AddedToWaitList", grp.Language, grp.Name.ToBold()),
                        update.Message.From.Id);
                }
            }
        }

        [Command(Trigger = "getlang")]
        public static void GetLang(Update update, string[] args)
        {
            //var glangs = Directory.GetFiles(Bot.LanguageDirectory)
            //                                            .Select(x => XDocument.Load(x)
            //                                                        .Descendants("language")
            //                                                        .First()
            //                                                        .Attribute("name")
            //                                                        .Value
            //                                            ).ToList();
            //glangs.Insert(0, "All");

            //foreach (var lang in glangs)
            //{
            //    var test =
            //        $"getlang|-1001049529775|" + lang;
            //    var count = Encoding.UTF8.GetByteCount(test);
            //    if (count > 64)
            //    {
            //        Send("Problem with " + lang + ": name is too long!", update.Message.Chat.Id);
            //    }
            //}
            //var gbuttons = glangs.Select(x => new InlineKeyboardButton(x, $"getlang|{update.Message.Chat.Id}|{x}")).ToList();
            //var baseMenu = new List<InlineKeyboardButton[]>();
            //for (var i = 0; i < gbuttons.Count; i++)
            //{
            //    if (gbuttons.Count - 1 == i)
            //    {
            //        baseMenu.Add(new[] { gbuttons[i] });
            //    }
            //    else
            //        baseMenu.Add(new[] { gbuttons[i], gbuttons[i + 1] });
            //    i++;
            //}

            //var gmenu = new InlineKeyboardMarkup(baseMenu.ToArray());

            var langs = Directory.GetFiles(Bot.LanguageDirectory, "*.xml").Select(x => new LangFile(x)).ToList();


            List<InlineKeyboardButton> buttons = langs.Select(x => x.Base).Distinct().OrderBy(x => x).Select(x => new InlineKeyboardButton(x, $"getlang|{update.Message.From.Id}|{x}|null|base")).ToList();
            buttons.Insert(0, new InlineKeyboardButton("All", $"getlang|{update.Message.From.Id}|All|null|base"));

            var baseMenu = new List<InlineKeyboardButton[]>();
            for (var i = 0; i < buttons.Count; i++)
            {
                if (buttons.Count - 1 == i)
                {
                    baseMenu.Add(new[] { buttons[i] });
                }
                else
                    baseMenu.Add(new[] { buttons[i], buttons[i + 1] });
                i++;
            }

            var menu = new InlineKeyboardMarkup(baseMenu.ToArray());
            try
            {
                Bot.Api.SendTextMessage(update.Message.Chat.Id, GetLocaleString("GetLang", GetLanguage(update.Message.Chat.Id)),
                    replyToMessageId: update.Message.MessageId, replyMarkup: menu);
            }
            catch (AggregateException e)
            {
                foreach (var ex in e.InnerExceptions)
                {
                    var x = ex as ApiRequestException;

                    Send(x.Message, update.Message.Chat.Id);
                }
            }
            catch (ApiRequestException ex)
            {
                Send(ex.Message, update.Message.Chat.Id);
            }
        }

        [Command(Trigger = "stats")]
        public static void GetStats(Update u, string[] args)
        {
            //var reply = $"[Global Stats](www.tgwerewolf.com/Stats)\n";
            //if (update.Message.Chat.Type != ChatType.Private)
            //    reply += $"[Group Stats](www.tgwerewolf.com/Stats/Group/{update.Message.Chat.Id}) ({update.Message.Chat.Title})\n";
            //reply += $"[Player Stats](www.tgwerewolf.com/Stats/Player/{update.Message.From.Id}) ({update.Message.From.FirstName})";
            var name = u.Message.From.FirstName;
            var id = u.Message.From.Id;
            var username = u.Message.From.Username;
            if (u.Message.ReplyToMessage != null)
            {
                var m = u.Message.ReplyToMessage;
                while (m.ReplyToMessage != null)
                    m = m.ReplyToMessage;
                //check for forwarded message
                name = m.From.FirstName;
                id = m.From.Id;
                username = m.From.Username;
                if (m.ForwardFrom != null)
                {
                    id = m.ForwardFrom.Id;
                    name = m.ForwardFrom.FirstName;
                    username = m.ForwardFrom.Username;
                }
                /*
                var buttons = new List<InlineKeyboardButton[]>
                {
                    new[]
                    {
                    new InlineKeyboardButton
                    {
                        Text = $"{name} Stats",
                        Url = "http://www.tgwerewolf.com/Stats/Player/" + id + "?referrer=stats"
                    }
                }

                };
                var menu = new InlineKeyboardMarkup(buttons.ToArray());
                */
            }
            string Content;
            try
            {
                using (var db = new WWContext())
                {
                    Content = "";
                    //find the player
                    var p = db.Players.FirstOrDefault(x => x.TelegramId == id);
                    if (p == null)
                    {
                        //remove the command
                        throw new Exception("Player has never played a game.");
                    }

                    var gamesPlayed = p.GamePlayers.Count();
                    var won = p.GamePlayers.Count(x => x.Won);
                    var lost = gamesPlayed - won;
                    var survived = p.GamePlayers.Count(x => x.Survived);
                    var roleInfo = db.PlayerRoles(id).ToList();
                    var killed = db.PlayerMostKilled(id).FirstOrDefault();
                    var killedby = db.PlayerMostKilledBy(id).FirstOrDefault();
                    var ach = (Achievements)(p.Achievements ?? 0);
                    var count = ach.GetUniqueFlags().Count();

                    Content = String.IsNullOrWhiteSpace(username)
                        ? $"{name.FormatHTML()} the {roleInfo.OrderByDescending(x => x.times).FirstOrDefault()?.role ?? "Noob"}"
                        : $"<a href=\"https://telegram.me/{username}\">{name.FormatHTML()} the {roleInfo.OrderByDescending(x => x.times).FirstOrDefault()?.role ?? "Noob"}</a>";
                    Content += $"\n{count.Pad()}Achievements Unlocked!\n" +
                               $"{won.Pad()}Games won ({won * 100 / gamesPlayed}%)\n" +
                               $"{lost.Pad()}Games lost ({lost * 100 / gamesPlayed}%)\n" +
                               $"{survived.Pad()}Games survived ({survived * 100 / gamesPlayed}%)\n" +
                               $"{gamesPlayed.Pad()}Total Games\n" +
                               $"<code>{killed?.times}</code>\ttimes I've gleefully killed {killed?.Name.FormatHTML()}\n" +
                               $"<code>{killedby?.times}</code>\ttimes I've been slaughted by {killedby?.Name.FormatHTML()}";
                }
            }
            catch (Exception e)
            {
                Content = "Unable to load stats: " + e.Message;
            }
            Bot.Api.SendTextMessage(u.Message.Chat.Id, Content, parseMode: ParseMode.Html, disableWebPagePreview: true);
            
        }


        private static string GetLocaleAchvString(string key, XDocument doc)
        {
            var strings = doc.Descendants("string").FirstOrDefault(x => x.Attribute("key").Value == key);
            if (strings == null)
            {
                //fallback to English

                strings = Bot.English.Descendants("string").FirstOrDefault(x => x.Attribute("key").Value == key);
            }
            var values = strings.Descendants("value");
            var choice = Bot.R.Next(values.Count());
            var selected = values.ElementAt(choice);
            return String.Format(selected.Value.FormatHTML()).Replace("\\n", Environment.NewLine);
        }


        [Command(Trigger = "achv")]
        public static void GetAchievements(Update u, string[] args)
        {
            var name = u.Message.From.FirstName;
            var id = u.Message.From.Id;
            var username = u.Message.From.Username;

            
            string Content;
            string Content2;
            try
            {
                using (var db = new WWContext())
                {
                    Content = "";
                    Content2 = "";
                    //find the player
                    var p = db.Players.FirstOrDefault(x => x.TelegramId == id);
                    if (p == null)
                    {
                        //remove the command
                        throw new Exception("Player has never played a game.");
                    }

                    var language = p.Language;
                    var doc = new XDocument();
                    try
                    {
                        var files = Directory.GetFiles(Bot.LanguageDirectory);
                        var file = files.First(x => Path.GetFileNameWithoutExtension(x) == language);
                        {
                            doc = XDocument.Load(file);
                        }
                    }
                    catch
                    {
                        if (language != "English")
                        {
                            var files = Directory.GetFiles(Bot.LanguageDirectory);
                            var file = files.First(x => Path.GetFileNameWithoutExtension(x) == "English");
                            {
                                doc = XDocument.Load(file);
                            }
                        }
                    }

                    var ach = (Achievements)(p.Achievements ?? 0);
                    var attained = ach.GetUniqueFlags();
                    var count = attained.Count();
                    var total = Enum.GetNames(typeof(Achievements)).Length - 1;
                    Content = $"<b>Attained Achievements ({count}/{total}):</b>\n";
                    foreach (Achievements achv in attained)
                    {
                        Content += $"{GetLocaleAchvString($"{achv + "N"}", doc)}\n";
                    }
                    int nocount = 0;
                        
                    var all = Enum.GetValues(typeof(Achievements));
                    foreach (Achievements achv in all)
                    {
                        if (!attained.Contains(achv) && achv != 0)
                        {
                            Content2 += $"{GetLocaleAchvString($"{achv + "N"}", doc)}\n<code>--{GetLocaleAchvString($"{achv + "D"}", doc)}</code>\n";
                            nocount++;
                        }
                    }
                    Content2 = $"<b>Unattained Achievements: ({nocount}/{total})\n</b>" + Content2;

                }
            }
            catch (Exception e)
            {
                Content = "Unable to load stats: " + e.Message;
                Bot.Api.SendTextMessage(u.Message.Chat.Id, Content, parseMode: ParseMode.Html, disableWebPagePreview: true);
                return;
            }
            try
            {
                Bot.Api.SendTextMessage(u.Message.From.Id, Content, parseMode: ParseMode.Html, disableWebPagePreview: true);
                Bot.Api.SendTextMessage(u.Message.From.Id, Content2, parseMode: ParseMode.Html, disableWebPagePreview: true);
                if (u.Message.Chat.Type != ChatType.Private)
                    Bot.Api.SendTextMessage(u.Message.Chat.Id, "I have sent you a PM.", replyToMessageId: u.Message.MessageId);

            }
            catch
            {
                // cant send PM
                Bot.Api.SendTextMessage(u.Message.Chat.Id, "Please start me in private first.", parseMode: ParseMode.Html, disableWebPagePreview: true, replyToMessageId: u.Message.MessageId);
            }
        }

        [Command(Trigger = "statistics")]
        public static void GetFullStats(Update u, string[] args)
        {
            var name = u.Message.From.FirstName;
            var id = u.Message.From.Id;
            var username = u.Message.From.Username;
            string Content;
            try
            {
                using (var db = new WWContext())
                {
                    Content = "";
                    using (var DB = new WWContext())
                    {
                        DB.Database.CommandTimeout = 600;
                        var gamePlayed = DB.Games.Count();
                        var night1death = DB.GlobalNight1Death().First();
                        var day1lynch = DB.GlobalDay1Lynch().First();
                        var day1death = DB.GlobalDay1Death().First();
                        var survivor = DB.GlobalSurvivor().First();


                        var playersKilled = DB.GamePlayers.Count(x => !x.Survived);
                        var playersSurvived = DB.GamePlayers.Count(x => x.Survived);
                        var totalGroups = DB.Groups.Count();
                        var totalPlayers = DB.Players.Count();
                        var bestSurvivor = survivor.Name;
                        var bestSurvivorPercent = (int)survivor.pct;
                        var bestSurvivorId = survivor.TelegramId;
                        var mostKilledFirstDay = day1death.Name;
                        var mostKilledFirstDayPercent = day1death.pct;
                        var mostKilledFirstDayId = day1death.TelegramId;
                        var mostKilledFirstNight = night1death.Name;
                        var mostKilledFirstPercent = night1death.pct;
                        var mostKilledFirstNightId = night1death.TelegramId;
                        var mostLynchedFirstDay = day1lynch.Name;
                        var mostLynchedFirstPercent = day1lynch.pct;
                        var mostLynchedFirstDayId = day1lynch.TelegramId;

                        Content += $"Total Games: {gamePlayed}\nNo. of Groups: {totalGroups}\nNo. of Players: {totalPlayers}\n";
                        Content += $"Best Survivor: {bestSurvivor} ({bestSurvivorPercent}%)\nMost Killed 1st Night: {mostKilledFirstNight} ({mostKilledFirstPercent}%)\n";
                        Content += $"Most Lynched 1st Day: {mostLynchedFirstDay} ({mostLynchedFirstPercent}%)";

                        /* later
                        if (u.Message.Chat.Id < 0)
                        {
                            // group
                            var groupid = u.Message.Chat.Id;
                            var grp = db.Groups.FirstOrDefault(x => x.GroupId == groupid);

                            Content += $"\nGroup Stats for {u.Message.Chat.Title}:\n";

                            var runStats = false;

                            //first, check that we should even run stats on this group
                            var stat = db.GroupStats.FirstOrDefault(x => x.GroupId == groupid);

                            if (stat == null)
                                runStats = true;
                            else
                            {
                                if ((grp.Games.OrderByDescending(x => x.TimeEnded).FirstOrDefault()?.TimeEnded ??
                                     DateTime.MinValue) > (stat.LastRun ?? DateTime.MinValue.AddSeconds(1)))
                                {
                                    runStats = true;
                                }
                            }

                            if (!runStats)
                            {
                                // later
                            }
                            var gamesPlayed = grp.Games.Count;
                            night1death = db.GroupNight1Death(groupid).FirstOrDefault();
                            day1lynch = db.GroupDay1Lynch(groupid).FirstOrDefault();
                            day1death = db.GroupDay1Death(groupid).FirstOrDefault();
                            survivor = db.GroupSurvivor(groupid).FirstOrDefault();

                            if (stat == null)
                            {
                                stat = db.GroupStats.Create();
                                stat.GroupId = groupid;
                                db.GroupStats.Add(stat);
                            }

                            stat.GroupName = grp.Name;
                            //TODO add this metric later
                            //stat.PlayersKilled = db.GamePlayers.Count(x => !x.Survived);
                            //stat.PlayersSurvived = db.GamePlayers.Count(x => x.Survived);

                            if (survivor != null)
                            {
                                stat.BestSurvivor = survivor.Name;
                                stat.BestSurvivorPercent = (int)survivor.pct;
                            }
                            stat.GamesPlayed = gamesPlayed;
                            stat.LastRun = DateTime.Now;
                            if (day1death != null)
                            {
                                stat.MostDeadFirstDay = day1death.Name;
                                stat.MostDeadFirstPercent = day1death.pct;
                            }
                            if (night1death != null)
                            {
                                stat.MostKilledFirstNight = night1death.Name;
                                stat.MostKilledFirstPercent = night1death.pct;
                            }
                            if (day1lynch != null)
                            {
                                stat.MostLynchedFirstNight = day1lynch.Name;
                                stat.MostLynchFirstPercent = day1lynch.pct;
                            }


                        }
                        later */
                        // DB.SaveChanges();
                        Console.WriteLine("Done");
                        Bot.Api.SendTextMessage(u.Message.Chat.Id, Content, replyToMessageId: u.Message.MessageId);
                    }
                }
            }
            catch
            {
                // nothing
            }
        }
    }
}
