using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace ConsoleAppYaml
{
    public class ClientSetting
    {
        public Install install { get; set; }
    }

    public class Install
    {
       
        public CrashReport crash_reporting { get; set; }

        [YamlMember(Alias = "game-settings",ApplyNamingConventions = false)]
        public GameSettings game_settings { get; set; }

        [YamlMemberAttribute(Alias = "gameflow-patcher-lock", ApplyNamingConventions = false)]
        public string gameflow_patcher_lock { get; set; }

        [YamlMemberAttribute(Alias = "gameflow-process-info", ApplyNamingConventions = false)]
        public string gameflow_process_info { get; set; }

        [YamlMember(Alias = "gameflow-spectate-reconnect-info", ApplyNamingConventions = false)]
        public string gameflow_spectate_reconnect_info { get; set; }
        public Globals globals { get; set; }

        [YamlMemberAttribute(Alias = "lcu-settings", ApplyNamingConventions = false)]
        public IcuSettings icuSettings { get; set; }

        [YamlMember(Alias = "npe-splash", ApplyNamingConventions = false)]
        public NpeSplash npe_splash { get; set; }

        public Patcher patcher { get; set; }

        [YamlMember(Alias = "perks-settings", ApplyNamingConventions = false)]
        public PerksSettings perksSettings { get; set; }     
        
        [YamlMember(Alias = "riotclient-upgrade", ApplyNamingConventions = false)]
        public RiotClientUpgrade riotClientUpgrade { get; set; }       
        
        [YamlMember(Alias = "rso-auth", ApplyNamingConventions = false)]
        public RsoAuth rsoAuth { get; set; }      
        
        [YamlMember(Alias = "telemetry", ApplyNamingConventions = false)]
        public Telemetry telemetry { get; set; }

}

    public class Telemetry
    {
        public bool fresh_installation { get; set; }
        public string installation_id { get; set; }
    }

    public class RsoAuth
    {
        [YamlMember(Alias = "install-identifier", ApplyNamingConventions = false)]
        public string install_identifier { get; set; }
    }

    public class RiotClientUpgrade
    {
        [YamlMember(Alias = "seq-success-count", ApplyNamingConventions = false)]
        public int seq_success_count { get; set; }

        [YamlMember(Alias = "success-count", ApplyNamingConventions = false)]
        public int success_count { get; set; }
    }

    public class PerksSettings
    {
        [YamlMemberAttribute(Alias = "accountId", ApplyNamingConventions = false)]
        public long accountId { get; set; }
        public bool modified { get; set; }
        public long timestamp { get; set; }
    }

    public class Patcher
    {
        public bool client_migrated { get; set; }
        public bool client_patcher_available { get; set; }
        public string client_patcher_migrated_time { get; set; }
        public bool game_migrated { get; set; }
        public bool game_patcher_available { get; set; }
        public string game_patcher_migrated_time { get; set; }
        public List<string>locales { get; set; }
        public Toggles toggles { get; set; }
    }

    public class Toggles
    {
        public int new_client_patcher { get; set; }
        public int new_game_patcher { get; set; }

    }

    public class Globals
    {
        [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
        public string locale { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.DoubleQuoted)]
        public string region { get; set; }

    }

    public class IcuSettings
    {
        [YamlMemberAttribute(Alias = "accountId", ApplyNamingConventions = false)]
        public long accountId { get; set; }
        public bool modified { get; set; }
        public long timestamp { get; set; }

    }
    public class CrashReport
    {
        public bool enabled { get; set; }
        public string type { get; set; }
    }

    
    public class NpeSplash{
        [YamlMember(Alias = "enableNewPlayerSplash", ApplyNamingConventions = false)]
        public bool enableNewPlayerSplash { get; set; }
    }

    public class GameSettings
    {
        [YamlMemberAttribute(Alias = "accountId", ApplyNamingConventions = false)]
        public long acc_ount_id { get; set; }
        public bool modified { get; set; }
        public long timestamp { get; set; }
    }
}
