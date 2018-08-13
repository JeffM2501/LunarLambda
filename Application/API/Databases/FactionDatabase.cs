using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.API.Databases
{
    public class FactionInfo
    {
        public enum Relations
        {
            Unaware,
            Friendly,
            Neutral,
            Hostile
        };

        public int ID { get; internal set; } = -1;
        public string Name = string.Empty;
        public string Description = string.Empty;
        public Color GMColor = Color.White;
        public string Logo = string.Empty;

        public bool Playable = false;
		public bool Known = true;

        public Dictionary<int, Relations> Relationships = new Dictionary<int, Relations>();

        public Relations GetRelationship(FactionInfo other)
        {
            if (ID == other.ID)
                return Relations.Friendly;

            if (!Relationships.ContainsKey(other.ID))
                return Relations.Unaware;

            return Relationships[other.ID];
        }

        public void SetRelationShip(FactionInfo other, Relations relation, bool mutual = false)
        {
            if (ID == other.ID)
                return;

            if (!Relationships.ContainsKey(other.ID))
                Relationships.Add(other.ID, relation);
            else
                Relationships[other.ID] = relation;

            if (mutual)
                other.SetRelationShip(this, relation, false);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetColor (Color c)
        {
            GMColor = c;
        }

        public void SetColor(int r, int g, int b)
        {
            GMColor = Color.FromArgb(Math.Min(r,255), Math.Min(g, 255), Math.Min(b, 255));
        }

        public void SetLogo(string logo)
        {
            Logo = logo;
        }

    }
    public static class FactionDatabase
    {
        internal static List<FactionInfo> Factions = new List<FactionInfo>();

        public static FactionInfo AddFaction(string name)
        {
            if (name == null || name == string.Empty || Factions.Find((x) => x.Name == name) != null) 
                return null;

            FactionInfo info = new FactionInfo();
            info.ID = Factions.Count + 1;
            info.Name = name;
            Factions.Add(info);
            return info;
        }

        public static FactionInfo GetFaction(string name)
        {
            return Factions.Find((x) => x.Name == name);
        }
		public static FactionInfo GetFaction(int id)
		{
			return Factions.Find((x) => x.ID == id);
		}

		public static List<FactionInfo> GetFactionsWithRelation(FactionInfo faction, FactionInfo.Relations relation)
        {
            return Factions.FindAll((x) => x != faction && x.GetRelationship(faction) == relation);
        }


		public static string GetLocalRelationString(FactionInfo.Relations relation)
		{
			switch (relation)
			{
				case FactionInfo.Relations.Friendly:
					return DefaultDatabaseStrings.FriendlyRelation;
				case FactionInfo.Relations.Hostile:
					return DefaultDatabaseStrings.HostileRelation;
				case FactionInfo.Relations.Neutral:
					return DefaultDatabaseStrings.NeutralRelation;
				case FactionInfo.Relations.Unaware:
					return DefaultDatabaseStrings.UnawareRelation;
			}
			return string.Empty;
		}
    }
}
