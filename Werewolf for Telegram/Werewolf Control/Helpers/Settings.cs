using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Werewolf_Control.Helpers
{
    internal static class Settings
    {

        //By the way, these admin ports will have IP whitelisting.  Don't even bother trying to connect to them :P
        //The regular ports are not even open on the firewall
#if DEBUG
        public static int Port = 9049;
        public static int AdminPort = 9059;
#elif RELEASE
        public static int Port = 9050;  //9050-@werewolfbot 
        public static int AdminPort = 9060;
#elif RELEASE2
        public static int Port = 9051;  //9051-@werewolfIIbot
        public static int AdminPort = 9063; //9061 not responding
#elif BETA
        public static int Port = 9052;
        public static int AdminPort = 9062;
#endif



        public static string TcpSecret => Environment.MachineName.GetHashCode().ToString();
        public static long PersianSupportChatId = -1001059174638;
        public static long MainChatId = -1001049529775; //Beta group
        public static long SupportChatId = -1001060486754; //@werewolfsupport
        public static long PrimaryChatId = -1001030085238; //@werewolfgame
        public static string DevChannelId = "@werewolfdev"; //@werewolfdev
        public static long VeteranChatId = -1001094614730;
        public static string VeteranChatLink = "werewolfvets";
        public static long ChiSinLoChatId = -1001079906864;
#if RELEASE2
        public static List<string> VillagerDieImages = new List<string> { "CgADBAADn58AAm0eZAebrI6GQFzQYwI" };
        public static List<string> WolfWin = new List<string> { "CgADBAADI58AAicaZAeV5IaHAvDlYAI" };
        public static List<string> WolvesWin = new List<string> { "CgADBAADH6YAArYeZAciua2vIMQ71wI" };
        public static List<string> VillagersWin = new List<string> { "CgADBAADD40AAksXZAfqJkdPFUx9pwI" };
        public static List<string> NoWinner = new List<string> { "CgADBAAD8QgAAqIeZAdrpaRWB_YBNAI" };
        public static List<string> StartGame = new List<string> { "CgADBQADawEAArPzsw9LZdsH3rNbkgI" };
        public static List<string> StartChaosGame = new List<string> { "CgADBAAD6pIAAjEZZAfIsboY9etCVwI" };
        public static List<string> TannerWin = new List<string> { "CgADBAADBp8AApIcZAdrBfPE_A27RAI" };
        public static List<string> CultWins = new List<string> { "CgADBAADyzYAArkcZAcsY3gLgQvIDgI" };
        public static List<string> SerialKillerWins = new List<string> { "CgADBAADxZ4AAlEXZAdPoehicWdIVQI" };
        public static List<string> LoversWin = new List<string> { "CgADBAADD5YAAvkcZAfmQAoM3czh2QI" };
        public static List<string> VillagerDieBySK = new List<string> { "CgADBAADiqAAAo8aZAflDbJLPTyeSgI" };
#elif RELEASE
        public static List<string> VillagerDieImages = new List<string> { "CgADBAADn58AAm0eZAebrI6GQFzQYwI" };
        public static List<string> WolfWin = new List<string> { "CgADBAADI58AAicaZAeV5IaHAvDlYAI" };
        public static List<string> WolvesWin = new List<string> { "CgADBAADH6YAArYeZAciua2vIMQ71wI" };
        public static List<string> VillagersWin = new List<string> { "CgADBAADD40AAksXZAfqJkdPFUx9pwI" };
        public static List<string> NoWinner = new List<string> { "CgADBAAD8QgAAqIeZAdrpaRWB_YBNAI" };
        public static List<string> StartGame = new List<string> { "CgADBQADawEAArPzsw9LZdsH3rNbkgI" };
        public static List<string> StartChaosGame = new List<string> { "CgADBAAD6pIAAjEZZAfIsboY9etCVwI" };
        public static List<string> TannerWin = new List<string> { "CgADBAADBp8AApIcZAdrBfPE_A27RAI" };
        public static List<string> CultWins = new List<string> { "CgADBAADyzYAArkcZAcsY3gLgQvIDgI" };
        public static List<string> SerialKillerWins = new List<string> { "CgADBAADxZ4AAlEXZAdPoehicWdIVQI" };
        public static List<string> LoversWin = new List<string> { "CgADBAADD5YAAvkcZAfmQAoM3czh2QI" };
        public static List<string> VillagerDieBySK = new List<string> { "CgADBAADiqAAAo8aZAflDbJLPTyeSgI" };
#elif DEBUG
        public static List<string> VillagerDieImages = new List<string> { "CgADBAADKgMAAoMbZAeB6WaedzGH2gI", "CgADBAADWAMAAt4cZAdsR41JQEyzzgI" };
        public static List<string> WolfWin = new List<string> { "CgADAwADgQADdBexB96NwT3fKFSoAg", "CgADAwADgAADdBexB1hbmm0m-w8AAQI" };
        public static List<string> WolvesWin = new List<string> { "CgADBAADcAMAAn8ZZAdlle-stVSplQI", "CgADBAADlwMAAtgaZAesA21ghnsW_QI" };
        public static List<string> VillagersWin = new List<string> { "CgADAwADgwADdBexBzj_9iwU5UIKAg" };
        public static List<string> NoWinner = new List<string> { "CgADBAAD8QgAAqIeZAcZUfHXAm5P4wI", "CgADBAADuAMAAlUXZAd8wCzVE3c-dgI" };
        public static List<string> StartGame = new List<string> { "CgADBAADujAAAp8dZAdZ9_HbpFIlwgI", "CgADBAADujAAAp8dZAdZ9_HbpFIlwgI" };
        public static List<string> StartChaosGame = new List<string> { "CgADBAADujAAAp8dZAdZ9_HbpFIlwgI", "CgADBAADujAAAp8dZAdZ9_HbpFIlwgI" };
        public static List<string> TannerWin = new List<string> { "CgADBAAD_gMAAtgaZAdqfJmltuAvJwI", "CgADBAADQwgAAuQaZAcV_TPG9xKx-gI" };
        public static List<string> CultWins = new List<string> { "CgADBAADHwsAAgUYZAenff8l98drhgI", "CgADBAADWAMAAosYZAfQnPT3Xp-8LwI" };
        public static List<string> SerialKillerWins = new List<string> { "CgADBAADdQMAAsEcZAc0XumvOhY2kAI", "CgADBAADOAQAAqUXZAfwKDeXc5uodgI" };
        public static List<string> LoversWin = new List<string> { "CgADBAAD8hUAAhYYZAfuiWd1FF4IegI", "CgADBAADYAMAAkMdZAcY4AQhzMPCWQI" };
#else
        public static List<string> VillagerDieImages = new List<string> { "CgADBAADn58AAm0eZAebrI6GQFzQYwI" };
        public static List<string> WolfWin = new List<string> { "CgADBAADI58AAicaZAeV5IaHAvDlYAI" };
        public static List<string> WolvesWin = new List<string> { "CgADBAADH6YAArYeZAciua2vIMQ71wI" };
        public static List<string> VillagersWin = new List<string> { "CgADBAADD40AAksXZAfqJkdPFUx9pwI" };
        public static List<string> NoWinner = new List<string> { "CgADBAAD8QgAAqIeZAdrpaRWB_YBNAI" };
        public static List<string> StartGame = new List<string> { "CgADBQADawEAArPzsw9LZdsH3rNbkgI" };
        public static List<string> StartChaosGame = new List<string> { "CgADBAAD6pIAAjEZZAfIsboY9etCVwI" };
        public static List<string> TannerWin = new List<string> { "CgADBAADBp8AApIcZAdrBfPE_A27RAI" };
        public static List<string> CultWins = new List<string> { "CgADBAADyzYAArkcZAcsY3gLgQvIDgI" };
        public static List<string> SerialKillerWins = new List<string> { "CgADBAADxZ4AAlEXZAdPoehicWdIVQI" };
        public static List<string> LoversWin = new List<string> { "CgADBAADD5YAAvkcZAfmQAoM3czh2QI" };
        public static List<string> VillagerDieBySK = new List<string> { "CgADBAADiqAAAo8aZAflDbJLPTyeSgI" };

#endif

        /// <summary>
        /// How many games are allowed for any given node
        /// </summary>
        public static int MaxGamesPerNode = 10;

        /// <summary>
        /// How many games on each node before starting a new node (to be added later)
        /// </summary>
#if DEBUG
        public static int NewNodeThreshhold = 8;
#else
        public static int NewNodeThreshhold = 8;
#endif
        public static int ShutDownNodesAt = 10;

        public static int
#if DEBUG
            MinPlayers = 1,
#else
            MinPlayers = 5,
#endif
            MaxPlayers = 35,
            TimeDay = 60,
            TimeNight = 90,
            TimeLynch = 90,
#if DEBUG
            PlayerCountSeerCursed = 6,
            PlayerCountHarlot = 7,
            PlayerCountBeholderChance = 8,
            PlayerCountSecondWolf = 9,
            PlayerCountGunner = 9,
            PlayerCountTraitor = 10,
            PlayerCountGuardianAngel = 11,
            PlayerCountDetective = 12,
            PlayerCountApprenticeSeer = 13,
            PlayerCountCultist = 15,
            PlayerCountThirdWolf = 16,
            PlayerCountWildChild = 17,
            PlayerCountFoolChance = 18,
            PlayerCountMasons = 21,
            PlayerCountSecondCultist = 22,
            MaxGames = 80,
            TannerChance = 40,
            FoolChance = 20,
            BeholderChance = 50,
            SeerConversionChance = 40,
            GuardianAngelConversionChance = 60,
            DetectiveConversionChance = 70,
            CursedConversionChance = 60,
            HarlotConversionChance = 70,
            SorcererConversionChance = 40,
            BlacksmithConversionChance = 75,
            HarlotDiscoverCultChance = 50,
            ChanceDetectiveCaught = 40,

#else
            PlayerCountSeerCursed = 6,
            PlayerCountHarlot = 7,
            PlayerCountBeholderChance = 8,
            PlayerCountSecondWolf = 9,
            PlayerCountGunner = 9,
            PlayerCountTraitor = 10,
            PlayerCountGuardianAngel = 11,
            PlayerCountDetective = 12,
            PlayerCountApprenticeSeer = 13,
            PlayerCountCultist = 15,
            PlayerCountThirdWolf = 16,
            PlayerCountWildChild = 17,
            PlayerCountFoolChance = 18,
            PlayerCountMasons = 21,
            PlayerCountSecondCultist = 22,
            MaxGames = 80,
            TannerChance = 40,
            FoolChance = 20,
            BeholderChance = 50,
            SeerConversionChance = 40,
            GuardianAngelConversionChance = 60,
            DetectiveConversionChance = 70,
            CursedConversionChance = 60,
            HarlotConversionChance = 70,
            SorcererConversionChance = 40,
            BlacksmithConversionChance = 75,
            HarlotDiscoverCultChance = 50,
            ChanceDetectiveCaught = 40,
#endif

            GameJoinTime = 180,
            MaxExtend = 60;
    }
}
