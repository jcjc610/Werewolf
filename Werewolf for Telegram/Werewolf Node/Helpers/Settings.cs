using System.Collections.Generic;

namespace Werewolf_Node
{
    static class Settings
    {
#if DEBUG
        public static string ServerIP = "127.0.0.1";
#else
        public static string ServerIP = "127.0.0.1";

#endif
#if DEBUG
        public static int Port = 9049;
#elif RELEASE
        public static int Port = 9050;
#elif RELEASE2
        public static int Port = 9051;
#elif BETA
        public static int Port = 9052;
#endif


#if RELEASE2
        public static List<string> VillagerDieImages = new List<string> { "CgADBAADWAMAAt4cZAf35BMV0JYkfAI", "CgADBAADKgMAAoMbZAePlvc1JHixBwI" };
        public static List<string> WolfWin = new List<string> { "CgADAwADgAADdBexB4goLSo1iF0PAg", "CgADAwADgQADdBexB64GLEOu3rHaAg" };
        public static List<string> WolvesWin = new List<string> { "CgADBAADlwMAAtgaZAfjNXLMeDUnEQI", "CgADBAADcAMAAn8ZZAfA3JpApQnWsgI" };
        public static List<string> VillagersWin = new List<string> { "CgADAwADgwADdBexB4WAJX1UUMcGAg" };
        public static List<string> NoWinner = new List<string> { "CgADBAADuAMAAlUXZAcqbs8FFzH_jAI", "CgADBAAD8QgAAqIeZAdrpaRWB_YBNAI" };
        public static List<string> StartGame = new List<string> { "CgADBAADwg0AAu0XZAcbDzeArykLxQI", "CgADAwADhAADdBexB2zmCWD0LgIFAg" };
        public static List<string> StartChaosGame = new List<string> { "CgADBAAD7wYAAgcYZAdEj1wGQhVkeQI", "CgADBAAD_wcAAiUYZAeI2HgYq0u3lAI" };
        public static List<string> TannerWin = new List<string> { "CgADBAADQwgAAuQaZAcXpDdmRAVXlgI", "CgADBAAD_gMAAtgaZAdm4Eeh6BsZiQI" };
        public static List<string> CultWins = new List<string> { "CgADBAADHwsAAgUYZAcakpcsHjxOTwI", "CgADBAADWAMAAosYZAdqu927o1YyjgI" };
        public static List<string> SerialKillerWins = new List<string> { "CgADBAADOAQAAqUXZAfxzqJFU74_4AI", "CgADBAADdQMAAsEcZAdc0CiVwKMuagI" };
        public static List<string> LoversWin = new List<string> { "CgADBAAD8hUAAhYYZAf3gNsjhsQUUwI", "CgADBAADYAMAAkMdZAcd_U89p6WSowI" };
        public static List<string> VillagerDieBySK = new List<string> { "CgADBAAD0KAAAvAZZAcxkvxjfklmJgI" };
#elif RELEASE
        public static List<string> VillagerDieImages = new List<string> { "CgADBAADWAMAAt4cZAf35BMV0JYkfAI", "CgADBAADKgMAAoMbZAePlvc1JHixBwI" };
        public static List<string> WolfWin = new List<string> { "CgADAwADgAADdBexB4goLSo1iF0PAg", "CgADAwADgQADdBexB64GLEOu3rHaAg" };
        public static List<string> WolvesWin = new List<string> { "CgADBAADlwMAAtgaZAfjNXLMeDUnEQI", "CgADBAADcAMAAn8ZZAfA3JpApQnWsgI" };
        public static List<string> VillagersWin = new List<string> { "CgADAwADgwADdBexB4WAJX1UUMcGAg" };
        public static List<string> NoWinner = new List<string> { "CgADBAADuAMAAlUXZAcqbs8FFzH_jAI", "CgADBAAD8QgAAqIeZAdrpaRWB_YBNAI" };
        public static List<string> StartGame = new List<string> { "CgADBAADwg0AAu0XZAcbDzeArykLxQI", "CgADAwADhAADdBexB2zmCWD0LgIFAg" };
        public static List<string> StartChaosGame = new List<string> { "CgADBAAD7wYAAgcYZAdEj1wGQhVkeQI", "CgADBAAD_wcAAiUYZAeI2HgYq0u3lAI" };
        public static List<string> TannerWin = new List<string> { "CgADBAADQwgAAuQaZAcXpDdmRAVXlgI", "CgADBAAD_gMAAtgaZAdm4Eeh6BsZiQI" };
        public static List<string> CultWins = new List<string> { "CgADBAADHwsAAgUYZAcakpcsHjxOTwI", "CgADBAADWAMAAosYZAdqu927o1YyjgI" };
        public static List<string> SerialKillerWins = new List<string> { "CgADBAADOAQAAqUXZAfxzqJFU74_4AI", "CgADBAADdQMAAsEcZAdc0CiVwKMuagI" };
        public static List<string> LoversWin = new List<string> { "CgADBAAD8hUAAhYYZAf3gNsjhsQUUwI", "CgADBAADYAMAAkMdZAcd_U89p6WSowI" };
        public static List<string> VillagerDieBySK = new List<string> { "CgADBAAD0KAAAvAZZAcxkvxjfklmJgI" };
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
        public static List<string> VillagerDieBySK = new List<string> { "CgADBAADWgMAAl8dZAcWwtelXjb1KAI" };
#else
        public static List<string> VillagerDieImages = new List<string> { "CgADBAADWAMAAt4cZAf35BMV0JYkfAI", "CgADBAADKgMAAoMbZAePlvc1JHixBwI" };
        public static List<string> WolfWin = new List<string> { "CgADAwADgAADdBexB4goLSo1iF0PAg", "CgADAwADgQADdBexB64GLEOu3rHaAg" };
        public static List<string> WolvesWin = new List<string> { "CgADBAADlwMAAtgaZAfjNXLMeDUnEQI", "CgADBAADcAMAAn8ZZAfA3JpApQnWsgI" };
        public static List<string> VillagersWin = new List<string> { "CgADAwADgwADdBexB4WAJX1UUMcGAg" };
        public static List<string> NoWinner = new List<string> { "CgADBAADuAMAAlUXZAcqbs8FFzH_jAI", "CgADBAAD8QgAAqIeZAdrpaRWB_YBNAI" };
        public static List<string> StartGame = new List<string> { "CgADBAADwg0AAu0XZAcbDzeArykLxQI", "CgADAwADhAADdBexB2zmCWD0LgIFAg" };
        public static List<string> StartChaosGame = new List<string> { "CgADBAAD7wYAAgcYZAdEj1wGQhVkeQI", "CgADBAAD_wcAAiUYZAeI2HgYq0u3lAI" };
        public static List<string> TannerWin = new List<string> { "CgADBAADQwgAAuQaZAcXpDdmRAVXlgI", "CgADBAAD_gMAAtgaZAdm4Eeh6BsZiQI" };
        public static List<string> CultWins = new List<string> { "CgADBAADHwsAAgUYZAcakpcsHjxOTwI", "CgADBAADWAMAAosYZAdqu927o1YyjgI" };
        public static List<string> SerialKillerWins = new List<string> { "CgADBAADOAQAAqUXZAfxzqJFU74_4AI", "CgADBAADdQMAAsEcZAdc0CiVwKMuagI" };
        public static List<string> LoversWin = new List<string> { "CgADBAAD8hUAAhYYZAf3gNsjhsQUUwI", "CgADBAADYAMAAkMdZAcd_U89p6WSowI" };
        public static List<string> VillagerDieBySK = new List<string> { "CgADBAAD0KAAAvAZZAcxkvxjfklmJgI" };

#endif

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
#if !DEBUG
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
            PlayerCountDoppelGanger = 22,
            PlayerCountCupid = 23,
            PlayerCountHunter = 24,
            PlayerCountSerialKiller = 25,
            PlayerCountSecondCultist = 26,
            MaxGames = 80,
            TannerChance = 40,
            FoolChance = 20,
            BeholderChance = 50,
            SeerConversionChance = 40,
            GuardianAngelConversionChance = 60,
            DetectiveConversionChance = 70,
            CursedConversionChance = 60,
            HarlotConversionChance = 70,
            HarlotDiscoverCultChance = 50,
            ChanceDetectiveCaught = 40,
            HunterConversionChance = 50,
            HunterKillCultChance = 50,
            HunterKillWolfChanceBase = 30,
            SerialKillerConversionChance = 20,
            AlphaWolfConversionChance = 20,
            SorcererConversionChance = 40,
            BlacksmithConversionChance = 75,
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
            PlayerCountDoppelGanger = 22,
            PlayerCountCupid = 23,
            PlayerCountHunter = 24,
            PlayerCountSerialKiller = 25,
            PlayerCountSecondCultist = 26,
            MaxGames = 80,
            TannerChance = 40,
            FoolChance = 20,
            BeholderChance = 50,
            SeerConversionChance = 40,
            GuardianAngelConversionChance = 60,
            DetectiveConversionChance = 70,
            CursedConversionChance = 60,
            HarlotConversionChance = 70,
            HarlotDiscoverCultChance = 50,
            ChanceDetectiveCaught = 40,
            HunterConversionChance = 50,
            HunterKillCultChance = 50,
            HunterKillWolfChanceBase = 30,
            SerialKillerConversionChance = 20,
            AlphaWolfConversionChance = 100,
            SorcererConversionChance = 40,
            BlacksmithConversionChance = 75,
#endif

            GameJoinTime = 180,
            MaxJoinTime = 300;



#if DEBUG
        //public static long MainChatId = -134703013;
        public static long MainChatId = -1001049529775; //Beta group
#else
        public static long MainChatId = -1001030085238;
#endif
        public static long VeteranChatId = -1001094614730;
        public static string VeteranChatLink = "werewolfvets";

        public static bool RandomLynch = false;
    }
}
