using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using YamlDotNet.Core;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.EventEmitters;
using YamlDotNet.Serialization.NamingConventions;

namespace ConsoleAppYaml
{

    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("1-RU 2-EUW");

            var key = Convert.ToInt16(Console.ReadLine());

            string server = key == 1 ?"RU": "EUW";


            string ProgramDataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var lol_live_product_settings = "\\Riot Games\\Metadata\\league_of_legends.live\\league_of_legends.live.product_settings.yaml";
            string product_install_full_path="";
            string LeagueClientSettings = "\\Config\\LeagueClientSettings.yaml";

            if (ChekingFiles(ProgramDataDir, lol_live_product_settings))
            {

                var reader = new StreamReader(ProgramDataDir + lol_live_product_settings);
                var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();
             //   try { 
                var p = deserializer.Deserialize<LauncherSetting>(reader);
                    product_install_full_path = p.product_install_full_path;
                    p.locale_data.available_locales = new List<string>() { "ru_RU", "en_GB", "de_DE", "es_ES" };
                    p.locale_data.default_locale = "ru_RU";
                    p.settings.locale = "ru_RU";
                    Console.WriteLine(p);
                    reader.Close();
                    WriteProductSettings(p, ProgramDataDir + lol_live_product_settings);
            //    }
               // catch(Exception e) {
                     //   Console.WriteLine(e);
          //      };

            }

            if (ChekingFiles(product_install_full_path, LeagueClientSettings))
            {
                var reader = new StreamReader(product_install_full_path + LeagueClientSettings);
                var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();
                var p = deserializer.Deserialize<ClientSetting>(reader);

                p.install.crash_reporting.enabled = false;
                p.install.globals.region = server;
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
                Console.WriteLine($"File \"{fileName}\" found");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("To: " + fileName);
                Console.ResetColor();
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"File \"{fileName}\" not found");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("To: " + fileName);
                Console.ResetColor();
                return false;
            }
        }
    }
}
