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
  
            var yml = @"D:\Games\Riot Games\League of Legends\Config\LeagueClientSettings45.yaml";
            var yml1 = @"C:\Users\mercy\source\Repository\ParseYaml\yamldotnet\bin\Debug\docYaml1.yml";




            var reader = new StreamReader(yml);


            var deserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();

            var p = deserializer.Deserialize<ClientSetting>(reader);
            //var h = p.dependencies["Direct X 9"];
            // System.Console.WriteLine($"{p.auto_patching_enabled_by_player} {h.hash} {p.product_install_full_path}");

            Console.WriteLine($"{p.install.globals.region}");

            //string rus = "ru_RU";
            //string eng = "en_GB";

            //var F = new FileConfig
            //{
            //    auto_patching_enabled_by_player = false,
            //    dependencies = new Dictionary<string, Dependencie> { {
            //            "Direct X 9", new Dependencie() {
            //                hash = "Imported",
            //                version = @"1.0.0"
            //            } } },

            //    locale_data = new LocalData()
            //    {
            //        available_locales = new() { rus, eng },
            //        default_locale = "ru_RU"
            //    },
            //    patching_policy = "manual",
            //    patchline_patching_ask_policy = "ask",
            //    product_install_full_path = "D:\\Games\\Riot Games\\League of Legends",
            //    product_install_root = "D:/Games/Riot Games/",
            //    settings = new Setting
            //    {
            //        create_shortcut = null,
            //        create_uninstall_key= null,
            //        locale= "ru_RU"
            //    },
            //    should_repair=false
            //};




            ////var serializer = new SerializerBuilder()
            ////    .WithEventEmitter(nextEmitter => new QuoteSurroundingEventEmitter(nextEmitter))
            ////    .Build();


            //using (var writer = new StreamWriter(yml1))
            //{
            //    // Save Changes
            //    var serializer = new SerializerBuilder()
            //        .WithNamingConvention(UnderscoredNamingConvention.Instance)
            //        .Build();
            //    serializer.Serialize(writer, F);
            //}






        }
    }
}
