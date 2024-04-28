using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;



namespace PlayEuwRusClient
{

    class Program
    {
        private static string ProgramDataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        private static string lol_live_product_settings = "\\Riot Games\\Metadata\\league_of_legends.live\\league_of_legends.live.product_settings.yaml";

        private static bool isEnable = true;
        private static Localization localization = Localization();

        static void Main()
        {

            string asciiText = @"
	                         ..:?JJJJJJ!.
	       :::::::::.     ~JG&&@@@@@@@@&BP7:
	      ?##&@@@&@@&G555G#&&GJJB&&BY!JB&&&B:
	        .!7JGB#&@@@@@@&&&#G7:B&5.  ~&&&B~5&&P!!!~.
	            .::~!7PGG#&&&#5~!B&&GY5B&&P!5&@@@@@@@BY.
	:5BBBB#&&&#BPPPP5JY&B5YJ??!7P&&&&&&&#P?JPBB&@@&B5~^. .
	.::::!JJJ7J5PPPY5@@@@@&&####BBBPJ?JYJPGP55?77?5Y: ^YP~
	              .Y@@@@@@@&BJ!Y7:!^7Y?777???5GPPG&@&:755!
	             7#@@@@@&GY7JJ7P55#~?#GP? :7?JPGGGB##:7YJ?^          .....
	          :J#@@@@#GYJ55PB#&#5YJ:J#BBY.:::~~:^?J7:7YY?7~.  .   ^77P&57J7~:
	        :5&@@@#P!^JG&&&#P55Y!^:.J#BP!.^^^!!~~7!~!Y5YJJJ7?5&&!?GP7?&Y:7&#J~.
	      ^P&@&BY~!B#&@&BP5?^::^~~~7G#B?.^^^^~!77!!5GGG5?~!G@@&5^~?J!^^:^^5&J~Y?.
	     ~B&BJ^.   ?@@B~: ~?7??7~B&#BBJ..:.:^!7!::JGGGGJ::P@@&::^..^^^^^^^:::.7&B^
	    .?5!.       :!   ~PPJJ7!!&&BP7::::~!7!^^..?Y55J:^!Y&@&:^!~:...:::::.   .~:
	                     ^7YGBBG#GY!::^^:^!~!77?^:!7??7~^~!JBB5!^:::^^!7.
	                       .!7!~!^..::^!7??JJ?777~:~7777~^::75GP5J?Y5PY^
	            ..:?5^     :!~..?YPGPPP555J?777777~.::~!!~^:..::~!7?~.
	          .~?7JB5.:YGGG?~!~:~YPGBBG5YYYYJJ?!~^^::::::^~!!!!~::.
	         .!JGB5!^:.?G&@@B!~^.^~J55P5YJJJ?!:.:~~~^^^^^:~Y7777~^^.   .....
	         ^JY5P57:^:.7!G&#?.:. !?::~~~~~~?7.:7777!~~~^::?J7777~^^..!JYYYY7^.
	         .?Y557^^^:.~.7BB?   .YP~~5PP5YYY?^^!7777777!!::J?7777~:^::?YYYYYYJ^
	          :7PGP?^^::~7557:   :5G77PGGG5YYY?::777777777!~~J7777!!7~:~77?JYY?7^
	            ...... ....      .YB77PGGGG5YY?^:!777777777^.!!~~^~!!7~.?YJ7~!77~
	         .^~.             !?^J?Y5JJGGGGG5YY?~:7777!!!^:^7?J?7!~:.::.7Y?7~::7~
	         :7YJ~:.        .?P5???77!!J5PGG5YY?~^:~!!!!^^7JJY5#&#G555J^~~!77!::~
	          .!YYYJ?!:::::?B&@@@!~!?J7!!^^^^^^^^^~!~~~:^?5B&@@@@@@@&&&&#5^~77^.:
	           .^7??7?YYY?P@@@@@@#!:^~~~~~^^^~^^^^^^^^^.^P&@@@@@@@&#####&&5^~7~.
	             .^^:?YYJ^G@&BPPPGG?^:^^^^^^^^^^^^^^^^^:Y&&@@@##5!7JJJ?!JP#~:~.
	                .!777^^7YY?~.^~~!~::^^:^^^^^^^^^^^^~G&&&BY?~^.~YYYY7.:~...
	                  :~!7.^YYJ7~^~!!~~!!^:...:^^^^^^^^:7BBJ~^!!7^^7JYYY?.^.
	                       .!77!^.:~!!77!~~.:::..........^^.!77777::7???7.~.
	                       :^^~7777!^:^!!: .^^^^^:..:::::.  ^77!~~~^:::::^^.
	                     !5PP5YJ55?777!^:                    .::^~!777!!77~.
	                     !YG#57#&GJ:..                         ~?GY!775BJ!GBJ.
	                           ^:                              .#&J:.!#&#7^5G!.
	                                                            ^.     .^.";
            Console.WriteLine(asciiText);
            Thread.Sleep(1000);
            Console.OutputEncoding = Encoding.UTF8;
            int x = 0;

            ReadLocalConfig();

            if (isEnable)
            {
                while (x < 1)
                {
                    Console.Clear();
                    CheckProcess();
                    ReadLocalConfig();
                    Console.Title = "PlayEuwRusClient";
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($@"
            + ---------------------------------------------+-
            |                                             |
            |      Copyright (c) 2022-2024,zloisupport    |
            |                  v 1.0.3.2                  |
            |                                             |
            +---------------------------------------------+-");

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($@"
             {localization.select_action}  : 
            -(1) {localization.action_set_russian_server}
            -(2) {localization.action_set_euw_server}
            -(3) {localization.action_set_game_path}
            -(01) {localization.action_kill_riot_client} 
            -(02) {localization.action_kill_leagueflegends_client} 
            -(03) {localization.action_kill_leagueflegends_game} 
            -(04) {localization.action_kill_riot_all} 
            -(00) {localization.action_exit} 
            {localization.enter_command}:
            ");
                    Console.ResetColor();
                    string input = Console.ReadLine().Trim().ToLowerInvariant();


                    switch (input)
                    {
                        case "rus":
                        case "1":
                            SetLocalConfig(Lang.ru_RU,Server.RU);
                            break;
                        case "eng":
                        case "2":
                            SetLocalConfig(Lang.ru_RU, Server.EUW);
                            break;
                        case "exit":
                        case "00":
                            x += 1;
                            Environment.Exit(0);
                            break;
                        case "path":
                        case "3":
                            //Console.WriteLine($"{localization.action_set_game_path}:({localization.cancel_action_set_path_game})");
                            //var setFullpath = Convert.ToString(Console.ReadLine());
                            //if (setFullpath != "0")
                            //{
                            //    AppConfigs(null, setFullpath);
                            //}
                            break;
                        case "01":
                            KillProcess("RiotClientServices");
                            break;
                        case "02":
                            KillProcess("LeagueClient");
                            break;
                        case "03":
                            KillProcess("League of Legends");
                            break;
                        case "04":
                            KillProcess("League of Legends");
                            KillProcess("RiotClientServices");
                            KillProcess("LeagueClient");
                            break;
                    }
                }
            }

        }


        enum Server
        {
            RU,
            EUW
        }

        enum Lang
        {
            ru_RU,
            en_US
        }


        private static Localization Localization()
        {
            Localization localization = new Localization();
            Localization local = localization.ReadLocalization();
            return local;
        }


        private static string ProductInstallFullPath()
        {
            string yaml = Path.Join(ProgramDataDir, lol_live_product_settings);
            string line;
            using (StreamReader reader = new StreamReader(yaml))
            {

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("product_install_full_path"))
                    {
                        Match match = Regex.Match(line, "\"([^\"]*)\"");

                        if (match.Success)
                        {
                            line = match.Groups[1].Value;
                            return line;
                        }
                    }
                }
            };
            return line;
        }


        private static string ProductInstallFullPathLanguage()
        {

            string yaml = Path.Join(ProgramDataDir, lol_live_product_settings);
            string? line;
            using (StreamReader reader = new StreamReader(yaml))
            {

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Trim().StartsWith("locale"))
                    {
                        Match match = Regex.Match(line, "\"([^\"]*)\"");

                        if (match.Success)
                        {
                            line = match.Groups[1].Value;
                            return line;
                        }
                    }
                }
            };
            return line;
        }


        private static void ClientLanguageModify(string path, Lang lang, Server server)
        {
            string yaml = Path.Join(path, "Config", "LeagueClientSettings.yaml");
            string? line;
            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(yaml))
            {

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Trim().StartsWith("locale"))
                    {
                        Match match = Regex.Match(line, "\"([^\"]*)\"");

                        if (match.Success)
                        {
                            line = line.Replace(match.Groups[1].Value,lang.ToString());
                        }
                    }

                    if (line.Trim().StartsWith("region"))
                    {
                        Match match = Regex.Match(line, "\"([^\"]*)\"");

                        if (match.Success)
                        {
                            line = line.Replace(match.Groups[1].Value,server.ToString());
                        }
                    }

                    lines.Add(line);
                }
            };

            using (StreamWriter writer = new StreamWriter(yaml))
            {
                foreach (var item in lines)
                {
                    writer.WriteLine(item);
                }

            }
        }

        private static void LaucnerLanguageModify(Lang lang)
        {
            string yaml = Path.Join(ProgramDataDir, lol_live_product_settings);
            string? line;
            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(yaml))
            {

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Trim().StartsWith("default_locale"))
                    {
                        Match match = Regex.Match(line, "\"([^\"]*)\"");

                        if (match.Success)
                        {
                            line = line.Replace(match.Groups[1].Value, lang.ToString());
                        }
                    }

                    if (line.Trim().StartsWith("locale"))
                    {
                        Match match = Regex.Match(line, "\"([^\"]*)\"");

                        if (match.Success)
                        {
                            line = line.Replace(match.Groups[1].Value, lang.ToString());
                        }
                    }

                    lines.Add(line);
                }
            };

            using (StreamWriter writer = new StreamWriter(yaml))
            {
                foreach (var item in lines)
                {
                    writer.WriteLine(item);
                }

            }
        }

        private static string LeagueClientSettingsRegion(string path)
        {

            string yaml = Path.Join(path, "Config", "LeagueClientSettings.yaml");
            string? line;
            using (StreamReader reader = new StreamReader(yaml))
            {

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Trim().StartsWith("region"))
                    {
                        Match match = Regex.Match(line, "\"([^\"]*)\"");

                        if (match.Success)
                        {
                            line = match.Groups[1].Value;
                            return line;
                        }
                        Console.WriteLine(line.Trim());
                    }
                }
            };
            return line;
        }


        private static void ReadLocalConfig()
        {
            string gamePath = ProductInstallFullPath();
            string locale = ProductInstallFullPathLanguage();
            string region = LeagueClientSettingsRegion(gamePath);
            Console.WriteLine($"{localization.locale}: " + locale);
            Console.WriteLine($"{localization.server}: " + region);

        }

        private static void SetLocalConfig(Lang lang,Server server)
        {
            string gamePath = ProductInstallFullPath();
            ClientLanguageModify(gamePath, lang, server);
            LaucnerLanguageModify(lang);
        }

        private static void CheckProcess()
        {
            Process[] RiotProcess = Process.GetProcessesByName("RiotClientServices");
            if (RiotProcess.Length != 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{localization.warning_please_close_riot_apps}");
                Console.ResetColor();
            }
        }

        private static void KillProcess(string processName)
        {
            LogHelper.Log(LogTarget.File, $"KillProcess {processName}");
            Console.Clear();
            foreach (var process in Process.GetProcessesByName(processName))
            {

                process.Kill();
                LogHelper.Log(LogTarget.File, $"Process {processName} killed");
            }
        }
    }
}
