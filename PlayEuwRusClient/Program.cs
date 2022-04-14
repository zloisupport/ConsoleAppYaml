using Sharprompt;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using YamlDotNet.Core;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.EventEmitters;
using YamlDotNet.Serialization.NamingConventions;

namespace PlayEuwRusClient
{

    class Program
    {
        static void Main(string[] args)
        {
            int x = 0;
            while (x <1) { 
                Console.Title = "PlayEuwRusClient";
                Console.WriteLine(@"
+ ---------------------------------------------+-
|                                             |
|      Copyright (c) 2022,zloisupport         |
|                  v 1.0.1                    |
|                                             |
+---------------------------------------------+-
👾 Select server to playing  : 
-(1) Русский (RUSSIAN)  
-(2) European West (EUW)
-(3) Set League of Legends folder 
-(01) Kill Riot Client 
-(02) Kill League of Legends Client 
-(03) Kill League of Legends Game 
-(00) Exit
Enter command:
");
               
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
                        x +=1;
                        Environment.Exit(0);
                    break;
                case "path":
                case "3":
                    Console.WriteLine("Please set league of legends path:");
                    var setFullpath = Convert.ToString(Console.ReadLine());
                    AppConfigs(null, setFullpath);
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
            }

            }
        }

        private static void AppConfigs(string server,string setFullpath="")
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
                    Console.WriteLine(p);
                    reader.Close();
                    WriteProductSettings(p, ProgramDataDir + lol_live_product_settings);
                }
                catch (YamlException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Deserialize error:" + e);
                };

            }

            if (ChekingFiles(product_install_full_path, LeagueClientSettings))
            {

                Console.WriteLine(server);
                var reader = new StreamReader(product_install_full_path + LeagueClientSettings);
                var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();
                var p = deserializer.Deserialize<ClientSetting>(reader);

                p.install.crash_reporting.enabled = false;
                if(server !="") p.install.globals.region = server;

                reader.Close();
                WriteLeagueClient(p, product_install_full_path + LeagueClientSettings);

            }
        }

        #region  Write Data conf
        private static void WriteLeagueClient(ClientSetting data,string file)
        {

            using (var writer = new StreamWriter(file))
            {
                var serializer = new SerializerBuilder()
                    .WithNamingConvention(UnderscoredNamingConvention.Instance)
                    .Build();
                serializer.Serialize(writer, data);
            }

        } 
        

        private static void KillProcess(string processName)
        {
            Console.Clear();
            foreach (var process in Process.GetProcessesByName(processName))
            {
                process.Kill();
            }
        }
        private static void WriteProductSettings(LauncherSetting data,string file)
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



        private static bool ChekingFiles(string path,string fileName)
        {
          
            if (File.Exists(path+ fileName))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"File \"{path + fileName}\" found");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("To: "+path  + fileName);
                Console.ResetColor();
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"File \"{path + fileName}\" not found");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("To: "+path + fileName);
                Console.ResetColor();
                return false;
            }
        }
    }
}
