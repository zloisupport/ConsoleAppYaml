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

    public class FileConfig
    {
        public bool auto_patching_enabled_by_player { get; set; }

        public  Dictionary<string, Dependencie> dependencies { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
        public  LocalData locale_data { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
        public string patching_policy { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
        public string patchline_patching_ask_policy { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
        public string product_install_full_path { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
        public string product_install_root { get; set; }

        public Setting settings {get; set; }
        public bool should_repair { get; set; }
    }


    /// <summary>
    /// Double qoutes on
    /// </summary>
    public class QuoteSurroundingEventEmitter : ChainedEventEmitter
    {
        public QuoteSurroundingEventEmitter(IEventEmitter nextEmitter) : base(nextEmitter)
        { }

        public override void Emit(ScalarEventInfo eventInfo, IEmitter emitter)
        {
            if (eventInfo.Source.StaticType == typeof(Object))
                eventInfo.Style = ScalarStyle.DoubleQuoted;
            base.Emit(eventInfo, emitter);
        }
    }

    public class Dependencie
    {
        [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
        public string hash { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
        public string phase { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
        public string version { get; set; }
    }

    public class LocalData
    {
        [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
        public List<string> available_locales { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
        public string default_locale { get; set; } 
    }

    public class Setting
    {
        public string create_shortcut {get; set;}
        public string create_uninstall_key { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
        public string locale {get; set;}
    }

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
                var p = deserializer.Deserialize<FileConfig>(reader);
                product_install_full_path = p.product_install_full_path;
                p.locale_data.available_locales =new List<string>(){"ru_RU", "en_GB","de_DE","es_ES" };
                p.locale_data.default_locale = "ru_RU";
                p.settings.locale= "ru_RU";
                reader.Close();


                WriteProductSettings(p, ProgramDataDir + lol_live_product_settings);
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
        
        private static void WriteProductSettings(FileConfig data,string file)
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
