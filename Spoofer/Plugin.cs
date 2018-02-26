using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using Lib_K_Relay.Networking.Packets.DataObjects;
using System.Linq;
using System.Text;

namespace Spoofer
{
    public class Spoofer : IPlugin
    {
        public string GetAuthor()
        {
            return "Jazz";
        }

        public string GetName()
        {
            return "Player Spoofer";
        }

        public string GetDescription()
        {
            return "Spoof any player (to get them banned ;)";
        }

        public string[] GetCommands()
        {
            return new string[]
            {
                "",
            };
        }

        public static string Name = "Oogily Boogily";
        public static string FakeName = "";
        public static string GuildName = "";
        public static int Stars = 0;
        public static int Class = 0;
        public static int Skin = 0;
        public static int PetType = 0;
        public static int PetSkin = 0;
        public static int CharacterFame;
        public static int Texture1 = 0;
        public static int Texture2 = 0;
        public static int Inventory0 = 0;
        public static int Inventory1 = 0;
        public static int Inventory2 = 0;
        public static int Inventory3 = 0;
        public static int HealthBonus = 0;
        public static int ManaBonus = 0;
        public static int MaximumHP = 0;
        public static int MaximumMP = 0;
        public static int Glowing = 0;
        public static int Level = 0;
        public static int Experience = 0;
        public static int NextLevelExperience = 0;
        public static int GuildRank = 0;

        public static int FriendPet0 = 0;
        public static int FriendPet1 = 0;
        public static int FriendPet2 = 0;

        public static Form1 form;

        public void Initialize(Proxy proxy)
        {
            form = new Form1();
            form.Show();
            proxy.HookPacket<UpdatePacket>(OnUpdate);
            proxy.HookPacket<TextPacket>(OnText);
        }

        private void OnText(Client client, TextPacket packet)
        {
            if (packet.Name == Name)
            {
                packet.Name = FakeName;
                packet.NumStars = Stars;
            }
        }

        private void OnUpdate(Client client, UpdatePacket packet)
        {
            if (FakeName != null && client.PlayerData.Name == FakeName) return;
            foreach (Entity ent in packet.NewObjs)
            {
                foreach (StatData statt in ent.Status.Data)
                {
                    if (statt.Id == StatsType.Name && statt.StringValue == Name)
                    {
                        //ent.ObjectType = (ushort)Class;
                        ent.Status.Data.Single(x => x.Id == StatsType.Skin).IntValue = Skin;
                        ent.ObjectType = (ushort)Class;
                        foreach (StatData stat in ent.Status.Data)
                        {
                            if (stat.Id == StatsType.Name) stat.StringValue = FakeName;
                            else if (stat.Id == StatsType.GuildName && GuildName != "") stat.StringValue = GuildName;
                            else if (stat.Id == StatsType.Skin && Skin != 0) stat.IntValue = Skin;
                            else if (stat.Id == StatsType.Stars && Stars != 0) stat.IntValue = Stars;
                            else if (stat.Id == StatsType.CharacterFame && CharacterFame != 0) stat.IntValue = CharacterFame;
                            else if (stat.Id == StatsType.Texture1 && Texture1 != 0) stat.IntValue = Texture1;
                            else if (stat.Id == StatsType.Texture2 && Texture2 != 0) stat.IntValue = Texture2;
                            else if (stat.Id == StatsType.Inventory0 && Inventory0 != 0) stat.IntValue = Inventory0;
                            else if (stat.Id == StatsType.Inventory1 && Inventory1 != 0) stat.IntValue = Inventory1;
                            else if (stat.Id == StatsType.Inventory2 && Inventory2 != 0) stat.IntValue = Inventory2;
                            else if (stat.Id == StatsType.Inventory3 && Inventory3 != 0) stat.IntValue = Inventory3;
                            else if (stat.Id == StatsType.HealthBonus && HealthBonus != 0) stat.IntValue = HealthBonus;
                            else if (stat.Id == StatsType.ManaBonus && ManaBonus != 0) stat.IntValue = ManaBonus;
                            //else if (stat.Id == StatsType.MaximumHP && MaximumHP != 0) stat.IntValue = MaximumHP;
                            else if (stat.Id == StatsType.MaximumMP && MaximumMP != 0) stat.IntValue = MaximumMP;
                            else if (stat.Id == StatsType.Glowing && Glowing != 0) stat.IntValue = Glowing;
                            else if (stat.Id == StatsType.Experience && Experience != 0) stat.IntValue = Experience;
                            else if (stat.Id == StatsType.Level && Level != 0) stat.IntValue = Level;
                            else if (stat.Id == StatsType.NextLevelExperience && NextLevelExperience != 0) stat.IntValue = NextLevelExperience;
                            else if (stat.Id == StatsType.GuildRank && GuildRank != 0) stat.IntValue = GuildRank;
                        }
                    }
                    else if (statt.Id == StatsType.PetLevel0 && statt.IntValue == FriendPet0)
                    {
                        if (ent.Status.Data.Single(x => x.Id == StatsType.PetLevel1).IntValue == FriendPet1 && ent.Status.Data.Single(x => x.Id == StatsType.PetLevel2).IntValue == FriendPet2)
                        {
                            ent.ObjectType = (ushort)PetType;
                            ent.Status.Data.Single(x => x.Id == StatsType.PetType).IntValue = PetType;
                            ent.Status.Data.Single(x => x.Id == StatsType.Skin).IntValue = PetSkin;
                        }
                    }
                }
            }
        }
    }
}