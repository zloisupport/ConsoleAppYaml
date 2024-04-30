using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PlayEuwRusClientWF
{   
    public partial class Form : System.Windows.Forms.Form
    {
        private static Localization Locale()
        {
            Localization localization = new Localization();
            Localization local = localization.ReadLocalization();
            return local;
        }

        private static Localization locale = Locale();

        Dictionary<string, string> languages = new Dictionary<string, string>
            {
                { "ru_RU", "Русский" },
                { "en_US", "English" },
            };


        Dictionary<string, string> servers = new Dictionary<string, string>
            {
                { "RU", "Русский" },
                { "EUW", "EUW" },
            };
        

        private void SetLocale()
        {
            groupBox2.Text = locale.select_action;
            lblServer.Text = locale.server;
            lblLang.Text = locale.locale;
            btnExit.Text = locale.action_exit;
            btnSave.Text = locale.btn_save;
            groupBox1.Text = locale.end_task;
            button3.Text = locale.action_kill_riot_client;
            button4.Text = locale.action_kill_leagueflegends_game;
            button5.Text = locale.action_kill_riot_client;
            button6.Text = locale.action_kill_riot_all;

        }

        public Form()
        {
            InitializeComponent();
            cbxLang.Items.AddRange(languages.Values.ToArray());
            cbxServer.Items.AddRange(servers.Values.ToArray());
            lblVersion.Parent = pictureBox1;
            lblVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            ReadLocalConfig();
            SetLocale();
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private static void KillProcess(string processName)
        {
            foreach (var process in Process.GetProcessesByName(processName))
            {
                process.Kill();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KillProcess("RiotClientServices");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KillProcess("League of Legends");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            KillProcess("LeagueClient");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            KillProcess("League of Legends");
            KillProcess("RiotClientServices");
            KillProcess("LeagueClient");
        }


        private string ProductInstallFullPath()
        {

            string ProgramDataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string yaml = Path.Combine(ProgramDataDir, "Riot Games", "Metadata", "league_of_legends.live", "league_of_legends.live.product_settings.yaml");
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

            string ProgramDataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string yaml = Path.Combine(ProgramDataDir, "Riot Games", "Metadata", "league_of_legends.live", "league_of_legends.live.product_settings.yaml");
            string line;
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
            string yaml = Path.Combine(path, "Config", "LeagueClientSettings.yaml");
            string line;
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
                            line = line.Replace(match.Groups[1].Value, lang.ToString());
                        }
                    }

                    if (line.Trim().StartsWith("region"))
                    {
                        Match match = Regex.Match(line, "\"([^\"]*)\"");

                        if (match.Success)
                        {
                            line = line.Replace(match.Groups[1].Value, server.ToString());
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
            string ProgramDataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string yaml = Path.Combine(ProgramDataDir, "Riot Games", "Metadata", "league_of_legends.live", "league_of_legends.live.product_settings.yaml");

            string line;
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

            string yaml = Path.Combine(path, "Config", "LeagueClientSettings.yaml");
            string line;
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


        private void ReadLocalConfig()
        {
            string gamePath = ProductInstallFullPath();
            string locale = ProductInstallFullPathLanguage();
            string region = LeagueClientSettingsRegion(gamePath);

            string lng = languages.Where(x => x.Key.Equals(locale)).Select(x => x.Value).FirstOrDefault();
            string server = servers.Where(x => x.Key.Equals(region)).Select(x => x.Value).FirstOrDefault();
            cbxLang.SelectedItem = lng;
            cbxServer.SelectedItem = server;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string gamePath = ProductInstallFullPath();

            string lang = languages.Where(x => x.Value.Equals(cbxLang.SelectedItem)).Select(x => x.Key).FirstOrDefault();
            string server = servers.Where(x => x.Value.Equals(cbxServer.SelectedItem)).Select(x => x.Key).FirstOrDefault();


            Server serverEnum = (Server)Enum.Parse(typeof(Server), server);
            Lang langEnum = (Lang)Enum.Parse(typeof(Lang), lang);
            ClientLanguageModify(gamePath, langEnum, serverEnum);
            LaucnerLanguageModify(langEnum);
            Timer timer = new Timer();
            timer.Interval = 2000;
            lblStatus.Text = locale.parameter_applying;
            lblStatus.ForeColor = System.Drawing.Color.Green;
            timer.Tick += delegate
            {
                timer.Stop();
                timer.Dispose();
                lblStatus.Text = string.Empty;
            };
            timer.Start();
        }

    }
}
