using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using static Spoofer.Spoofer;
using static Spoofer.Binaries;
using static Spoofer.Form1;
using Lib_K_Relay.GameData;
using System.Xml.Linq;

namespace Spoofer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static XDocument doc = new XDocument();
        public static bool block = false;

        public static int GetObjectType(string Name)
        {
            var memes = doc.Element("Objects").Elements().SingleOrDefault(x => x.AttrDefault("id", "") == Name);
            if (memes != null)
            {
                var meme = memes.Nodes().Select(x => (XElement)x);
                var mem = meme.Select(x => x.Name.LocalName);
                var me = 0;
                me = mem.Contains("Tex1") ? meme.Single(x => x.Name.LocalName == "Tex1").Value.ToHex() : 0;  // too lazy to do regex
                me = mem.Contains("Tex2") && me == 0 ? meme.Single(x => x.Name.LocalName == "Tex2").Value.ToHex() : 0;

                return me != 0 ? me : memes.AttrDefault("type", "0x0").ToHex();
            }
            else
            {
                return 0;
            }
        }

        public static string GetIdFromType(int Type)
        {
            string idk = "0x" + Type.ToString("X");
            var memes = doc.Element("Objects").Elements().SingleOrDefault(x => x.AttrDefault("type", "") == idk.ToLower());
            return memes.AttrDefault("id", "");
        }

        private void Check(TextBox textbox, out int Variable, bool Object = false)
        {
            Variable = 0;
            if (block) return;
            if (Object && textbox.Text != "")
            {
                int value = GetObjectType(textbox.Text);
                if (value != 0)
                {
                    Variable = value;
                    textbox.BackColor = Color.White;
                }
                else
                {
                    textbox.BackColor = Color.LightPink;
                }
            }
            else if (textbox.Text == "")
            {
                textbox.BackColor = Color.White;
            }
            else
            {
                if (textbox.Text.ToLower().Contains("x"))
                {
                    int number = textbox.Text.ToHex();
                    if (number == 0)
                    {
                        Check(textbox, out Variable, true);
                    }
                    else textbox.BackColor = Color.White;
                }
                else
                {
                    if (int.TryParse(textbox.Text, out Variable) == false)
                    {
                        Check(textbox, out Variable, true);
                    }
                    else textbox.BackColor = Color.White;
                }
            }
        }
        #region Bunch of Integer/ObjectType checks
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Check(textBox2, out HealthBonus);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Check(textBox3, out MaximumMP);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Check(textBox4, out ManaBonus);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            FakeName = textBox5.Text;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            Check(textBox6, out Stars);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            Check(textBox7, out Skin);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            Check(textBox8, out PetType);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            Check(textBox9, out CharacterFame);
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            Check(textBox18, out Texture1);
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            Check(textBox17, out Texture2);
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            Check(textBox16, out Inventory0);
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            Check(textBox12, out PetSkin);
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            Check(textBox15, out Inventory1);
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            Check(textBox14, out Inventory2);
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            Check(textBox13, out Inventory3);
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            Check(textBox11, out Level);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            Check(textBox10, out Experience);
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            Check(textBox27, out NextLevelExperience);
        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            Check(textBox26, out FriendPet0);
        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            Check(textBox25, out FriendPet1);
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            Check(textBox24, out FriendPet2);
        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            GuildName = textBox21.Text;
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            Check(textBox23, out Class);
        }
#endregion

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(@textBox20.Text))
                {
                    string[] Config = File.ReadAllLines(@textBox20.Text);
                    textBox2.Text = Config[0].Split('=')[1];
                    textBox3.Text = Config[1].Split('=')[1];
                    textBox4.Text = Config[2].Split('=')[1];
                    textBox5.Text = Config[3].Split('=')[1];
                    textBox6.Text = Config[4].Split('=')[1];
                    textBox7.Text = Config[5].Split('=')[1];
                    textBox8.Text = Config[6].Split('=')[1];
                    textBox9.Text = Config[7].Split('=')[1];
                    textBox18.Text = Config[8].Split('=')[1];
                    textBox17.Text = Config[9].Split('=')[1];
                    textBox16.Text = Config[10].Split('=')[1];
                    textBox15.Text = Config[11].Split('=')[1];
                    textBox14.Text = Config[12].Split('=')[1];
                    textBox13.Text = Config[13].Split('=')[1];
                    comboBox3.SelectedItem = Config[14].Split('=')[1];
                    textBox11.Text = Config[15].Split('=')[1];
                    textBox10.Text = Config[16].Split('=')[1];
                    textBox27.Text = Config[17].Split('=')[1];
                    textBox21.Text = Config[18].Split('=')[1];
                    textBox22.Text = Config[19].Split('=')[1];
                    textBox26.Text = Config[20].Split('=')[1];
                    textBox25.Text = Config[21].Split('=')[1];
                    textBox24.Text = Config[22].Split('=')[1];
                    textBox23.Text = Config[23].Split('=')[1];
                    comboBox2.SelectedItem = Config[24].Split('=')[1];
                    textBox12.Text = Config[25].Split('=')[1];
                }
                else MessageBox.Show("Config doesn't exist!");
            } catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string[] Config = new string[26];
                Config[0] = "HP Bonus=" + textBox2.Text;
                Config[1] = "Max MP=" + textBox3.Text;
                Config[2] = "MP Bonus=" + textBox4.Text;
                Config[3] = "Name=" + textBox5.Text;
                Config[4] = "Stars=" + textBox6.Text;
                Config[5] = "Skin=" + textBox7.Text;
                Config[6] = "Pet Type=" + textBox8.Text;
                Config[7] = "Fame=" + textBox9.Text;
                Config[8] = "Texture 1=" + textBox18.Text;
                Config[9] = "Texture 2=" + textBox17.Text;
                Config[10] = "Weapon=" + textBox16.Text;
                Config[11] = "Spell=" + textBox15.Text;
                Config[12] = "Armor=" + textBox14.Text;
                Config[13] = "Ring=" + textBox13.Text;
                Config[14] = "Glowing=" + comboBox3.SelectedItem;
                Config[15] = "Level=" + textBox11.Text;
                Config[16] = "XP=" + textBox10.Text;
                Config[17] = "Next Level XP=" + textBox27.Text;
                Config[18] = "Guild Name=" + textBox21.Text;
                Config[23] = "Class=" + textBox23.Text;
                Config[19] = "Friends Name=" + textBox22.Text;
                Config[20] = "Friends Pet Lvl. 1=" + textBox26.Text;
                Config[21] = "Friends Pet Lvl. 2=" + textBox25.Text;
                Config[22] = "Friends Pet Lvl. 3=" + textBox24.Text;
                Config[24] = "Guild Rank=" + comboBox2.SelectedItem;
                Config[25] = "Pet Skin=" + textBox12.Text;
                File.WriteAllLines(@textBox19.Text, Config);
            } catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            Spoofer.Name = textBox22.Text;
        }

        List<Character> charList = new List<Character>();

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox30.Text == "") return;
            var web = new System.Net.WebClient();
            Uri url;
            if (!Uri.TryCreate(textBox30.Text, UriKind.Absolute, out url)) return;
            web.Headers.Add("User-Agent: Other");
            string nut = web.DownloadString(url);
            string[] characters = nut.Split("tbody")[1].Split("tr");
            foreach (string html in characters)
            {
                if (html.Contains("class=\"character\""))
                {
                    Character chara = new Character(html);
                    if (chara.Name != null) charList.Add(chara);
                }
            }
            comboBox1.Items.Clear();
            foreach (Character chara in charList)
            {
                comboBox1.Items.Add(chara.Name);
            }
            textBox6.Text = nut.Split("star-container\">")[1].Split('<')[0];
            textBox21.Text = nut.Split("/guild/").Last().Split('>')[1].Split('<')[0];
            comboBox2.SelectedItem = nut.Split("Guild Rank")[1].Split("<td>")[1].Split('<')[0];
            textBox5.Text = textBox30.Text.Split('/').Last();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            doc = XDocument.Parse(Lib_K_Relay.GameData.GameData.RawObjectsXML);
        }

        private void textBox30_Enter(object sender, EventArgs e)
        {
            if (textBox30.Text == "Realmeye Url")
            {
                textBox30.Text = "";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Character chara = charList[comboBox1.SelectedIndex];
            block = true;
            textBox2.Text = chara.HPBonus.ToString();
            textBox3.Text = chara.MaximumMP.ToString();
            textBox4.Text = chara.MPBonus.ToString();
            textBox7.Text = chara.Skin.ToString();
            textBox8.Text = chara.PetType.ToString();
            textBox9.Text = chara.CharacterFame.ToString();
            textBox18.Text = chara.Texture1.ToString();
            textBox17.Text = chara.Texture2.ToString();
            textBox16.Text = chara.Inventory0.ToString();
            textBox15.Text = chara.Inventory1.ToString();
            textBox14.Text = chara.Inventory2.ToString();
            textBox13.Text = chara.Inventory3.ToString();
            textBox11.Text = chara.Level.ToString();
            textBox10.Text = chara.Exp.ToString();
            textBox23.Text = chara.Class.ToString();
            textBox12.Text = chara.PetSkin.ToString();

            MaximumHP = chara.MaximumHP;
            HealthBonus = chara.HPBonus;
            MaximumMP = chara.MaximumMP;
            ManaBonus = chara.MPBonus;
            Skin = chara.Skin;
            PetType = chara.PetType;
            PetSkin = chara.PetSkin;
            CharacterFame = chara.CharacterFame;
            Texture1 = chara.Texture1;
            Texture2 = chara.Texture2;
            Inventory0 = chara.Inventory0;
            Inventory1 = chara.Inventory1;
            Inventory2 = chara.Inventory2;
            Inventory3 = chara.Inventory3;
            Level = chara.Level;
            Experience = chara.Exp;
            Class = chara.Class;

            block = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedItem)
            {
                case "None":
                    GuildRank = 0;
                    break;
                case "Member":
                    GuildRank = 10;
                    break;
                case "Officer":
                    GuildRank = 20;
                    break;
                case "Leader":
                    GuildRank = 30;
                    break;
                case "Founder":
                    GuildRank = 40;
                    break;
                default:
                    GuildRank = 0;
                    break;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Glowing = comboBox3.SelectedItem.ToString() == "Yes" ? 0 : -1;
        }
    }

    public class Character
    {
        public string Name;
        public int Class;
        public int Skin;
        public int Texture1;
        public int Texture2;
        public int CharacterFame;
        public int Exp;
        public int PetType;
        public int PetSkin;
        public int Inventory0;
        public int Inventory1;
        public int Inventory2;
        public int Inventory3;
        public int Level;
        public int MaximumHP;
        public int MaximumMP;
        public int HPBonus;
        public int MPBonus;

        public Character(string Html)
        {
            string[] gear = Html.Split("<td>");
            if (gear.Count() != 11) return;
            string[] items = gear[9].Split("<span class=\"item-wrapper\"");
            if (items.Count() != 5) return;

            string pet = GetIdFromType(GetValue(gear[1], "data-item"));
            PetSkin = GetObjectType(pet + " Skin");
            PetType = GetObjectType(pet);
            Class = GetValue(gear[2], "data-class");
            Skin = GetValue(gear[2], "data-skin");
            Texture1 = GetValue(gear[2], "dye1");
            Texture2 = GetValue(gear[2], "dye2");
            Name = gear[3].Split('<')[0];
            Level = int.Parse(gear[4].Split('<')[0]);
            CharacterFame = gear[6].Split('<')[0].RipInts();
            Exp = gear[7].Split('<')[0].RipInts();
            Inventory0 = GetValuee(items[0], "title");
            Inventory1 = GetValuee(items[1], "title");
            Inventory2 = GetValuee(items[2], "title");
            Inventory3 = GetValuee(items[3], "title");
            string[] stats = GetValueee(gear[10], "data-stats").ToString().Split('[')[1].Split(',');
            string[] bonuses = GetValueee(gear[10], "data-bonuses").ToString().Split('[')[1].Split(',');
            MaximumHP = int.Parse(stats[0]);
            MaximumMP = int.Parse(stats[1]);
            HPBonus = int.Parse(bonuses[0]);
            MPBonus = int.Parse(bonuses[1]);
        }
    }

    public static class Binaries
    {
        public static string[] Split(this string Input, string Seperator)
        {
            return Input.Split(new[] { Seperator }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static int ToHex(this string Input)
        {
            int result;
            if (Input.Contains("x") == false || int.TryParse(Input.Split('x')[1], NumberStyles.HexNumber, CultureInfo.InstalledUICulture, out result) == false) return 0;
            return result;
        }

        public static int GetValue(string Input, string Attribute)
        {
            int result;
            if (!int.TryParse(Input.Split(Attribute + "=\"")[1].Split('"')[0], out result)) return 0;
            return result;
        }

        public static string GetValueee(string Input, string Attribute)
        {
            return Input.Split(Attribute + "=\"")[1].Split('"')[0];
        }

        public static int GetValuee(string Input, string Attribute)
        {
            string Name = Input.Split(Attribute + "=\"")[1].Split('"')[0].Kill(new string[] { " UT", " T15", " T14", " T13", " T12", " T11", " T10", " T9", " T8", " T7", " T6", " T5", " T6", " T5", " T4", " T3", " T2", " T1", " T0" });
            return GetObjectType(Name);
        }

        public static string Kill(this string Input, string[] Strings)
        {
            foreach (string thing in Strings)
            {
                Input = Input.Replace(thing, "");
            }
            return Input;
        }

        public static int RipInts(this string Input)
        {
            string result = "0";
            foreach (char car in Input)
            {
                int output;
                if (int.TryParse(car.ToString(), out output))
                {
                    result += output.ToString();
                }
            }
            return int.Parse(result);
        }
    }
}
