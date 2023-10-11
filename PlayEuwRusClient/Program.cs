using Sharprompt;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using YamlDotNet.Core;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.EventEmitters;
using YamlDotNet.Serialization.NamingConventions;



namespace PlayEuwRusClient
{

    class Program
    {
        private static bool isEnable = true;
        private static int GetLineNumber([System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
        {
            return lineNumber;
        }
        private static Localization localization = Localization();
        static void Main(string[] args)
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
            |      Copyright (c) 2022-2023,zloisupport    |
            |                  v 1.0.3.0                  |
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
                            AppConfigs("RU");
                            break;
                        case "eng":
                        case "2":
                            AppConfigs("EUW");
                            break;
                        case "exit":
                        case "00":
                            x += 1;
                            Environment.Exit(0);
                            break;
                        case "path":
                        case "3":
                            Console.WriteLine($"{localization.action_set_game_path}:({localization.cancel_action_set_path_game})");
                            var setFullpath = Convert.ToString(Console.ReadLine());
                            if (setFullpath != "0")
                            {
                                AppConfigs(null, setFullpath);
                            }
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


        private static string Reader(string path, string name)
        {
            StreamReader reader = new StreamReader(path + name);
            string content = reader.ReadToEnd();
            reader.Close();
            return content;
        }


        private static Localization Localization()
        {
            Localization localization = new Localization();
            Localization local = localization.ReadLocalization();
            return local;
        }

        private static void ReadLocalConfig()
        {

            string ProgramDataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var lol_live_product_settings = "\\Riot Games\\Metadata\\league_of_legends.live\\league_of_legends.live.product_settings.yaml";
            string product_install_full_path = "";
            string LeagueClientSettings = "\\Config\\LeagueClientSettings.yaml";
            if (ChekingFiles(ProgramDataDir, lol_live_product_settings))
            {
                var reader = Reader(ProgramDataDir, lol_live_product_settings);

                var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();

                try
                {
                    Console.WriteLine($"{localization.current_config}");
                    var p = deserializer.Deserialize<LauncherSetting>(reader);
                    product_install_full_path = p.product_install_full_path;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"{localization.locale}: " + p.settings.locale);
                }
                catch (YamlException LauncherSettingExpection)
                {
                    LogHelper.Log(LogTarget.File, $"-------Exception-------");
                    LogHelper.Log(LogTarget.File, $"File: {ProgramDataDir}\\{lol_live_product_settings}");

                    using (StreamReader lcsr = new StreamReader($"{ProgramDataDir}\\{lol_live_product_settings}"))
                    {
                        int linenum = 0;
                        while (!lcsr.EndOfStream)
                        {
                            string line = lcsr.ReadLine();
                            linenum++;
                            LogHelper.Log(LogTarget.File, $"{linenum} {line}");
                        }

                    }

                    LogHelper.Log(LogTarget.File, $"Expection LauncherSetting: {LauncherSettingExpection.Message}");
                    LogHelper.Log(LogTarget.File, $"-------END Exception-------");
                    isEnable = false;
                }

                try
                {
                    reader = Reader(product_install_full_path, LeagueClientSettings);
                    var clientSetting = deserializer.Deserialize<ClientSetting>(reader);
                    Console.WriteLine($"{localization.server}: " + clientSetting.install.globals.region);

                }
                catch (YamlException clientSettingException)
                {

                    LogHelper.Log(LogTarget.File, $"-------Exception-------");
                    LogHelper.Log(LogTarget.File, $"File: {product_install_full_path}\\{LeagueClientSettings}");
                    LogHelper.Log(LogTarget.File, $"Expection: {clientSettingException.Message}");
                    using (StreamReader ccsr = new StreamReader($"{product_install_full_path}\\{LeagueClientSettings}"))
                    {
                        int linenum = 0;
                        while (!ccsr.EndOfStream)
                        {
                            string line = ccsr.ReadLine();
                            linenum++;
                            LogHelper.Log(LogTarget.File, $"{linenum} {line}");
                        }
                    }
                    LogHelper.Log(LogTarget.File, $"-------END Exception-------");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Expection: please open Launch.log");
                    isEnable = false;
                }
            }
        }

        private static void AppConfigs(string server, string setFullpath = "")
        {
            if (isEnable)
            {
                Console.Clear();
                string ProgramDataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                var lol_live_product_settings = "\\Riot Games\\Metadata\\league_of_legends.live\\league_of_legends.live.product_settings.yaml";
                string product_install_full_path = "";
                string LeagueClientSettings = "\\Config\\LeagueClientSettings.yaml";

                if (ChekingFiles(ProgramDataDir, lol_live_product_settings))
                {

                    var reader = new StreamReader(ProgramDataDir + lol_live_product_settings);
                    var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(UnderscoredNamingConvention.Instance)
                    .Build();
                    try
                    {
                        var p = deserializer.Deserialize<LauncherSetting>(reader);
                        product_install_full_path = p.product_install_full_path;
                        p.locale_data.available_locales = new List<string>() { "ru_RU", "en_GB", "de_DE", "es_ES" };
                        p.locale_data.default_locale = "ru_RU";
                        p.settings.locale = "ru_RU";
                        if (setFullpath != "") { p.product_install_full_path = setFullpath; }
                        reader.Close();
                        WriteProductSettings(p, ProgramDataDir + lol_live_product_settings);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{localization.parameter_applying} {server} {localization.server}!");
                        Console.WriteLine($"{localization.message_success}!");
                        Console.ResetColor();
                    }
                    catch (YamlException e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Deserialize error:" + e);
                    };

                }

                if (ChekingFiles(product_install_full_path, LeagueClientSettings))
                {

                    var reader = new StreamReader(product_install_full_path + LeagueClientSettings);
                    var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(UnderscoredNamingConvention.Instance)
                    .Build();
                    var p = deserializer.Deserialize<ClientSetting>(reader);

                    p.install.crash_reporting.enabled = false;
                    if (server != "") p.install.globals.region = server;

                    reader.Close();
                    WriteLeagueClient(p, product_install_full_path + LeagueClientSettings);

                }
            }
        }

        #region  Write Data conf
        private static void WriteLeagueClient(ClientSetting data, string file)
        {

            using (var writer = new StreamWriter(file))
            {
                var serializer = new SerializerBuilder()
                    .WithNamingConvention(UnderscoredNamingConvention.Instance)
                    .Build();
                serializer.Serialize(writer, data);
            }

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
        private static void WriteProductSettings(LauncherSetting data, string file)
        {

            using (var writer = new StreamWriter(file))
            {
                var serializer = new SerializerBuilder()
                    .WithNamingConvention(UnderscoredNamingConvention.Instance)
                    .Build();
                serializer.Serialize(writer, data);
            }

        }
        #endregion



        private static bool ChekingFiles(string path, string fileName)
        {
            LogHelper.Log(LogTarget.File, $"Cheking Files");
            if (File.Exists(path + fileName))
            {
                return true;
            }
            else
            {
                LogHelper.Log(LogTarget.File, $"File \"{path + fileName}\" not found");
                return false;
            }
        }
    }
}
