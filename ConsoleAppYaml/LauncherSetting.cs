using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.EventEmitters;

namespace ConsoleAppYaml
{
    class LauncherSetting
    {
            public bool auto_patching_enabled_by_player { get; set; }

            public Dictionary<string, Dependencie> dependencies { get; set; }

            [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
            public LocalData locale_data { get; set; }

            [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
            public string patching_policy { get; set; }

            [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
            public string patchline_patching_ask_policy { get; set; }

            [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
            public string product_install_full_path { get; set; }

            [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
            public string product_install_root { get; set; }

            public Setting settings { get; set; }
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
            public string create_shortcut { get; set; }
            public string create_uninstall_key { get; set; }

            [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
            public string locale { get; set; }
        }
    
}
