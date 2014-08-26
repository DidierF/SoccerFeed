using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class Team
    {
        private string name;
        private string stadium;
        private List<Player> members;
        private List<Player> inGamePlayers;

        public string Name { get { return name; } }
        public List<Player> Members 
        { 
            get { return members; }
            set
            {
                if (members == null)
                {
                    members = value;
                    inGamePlayers = new List<Player>();
                    for (int i = 0; i < 11; i++)
                    {
                        inGamePlayers.Add(members[i]);
                    }
                }
            }
        }
        public List<Player> InGamePlayers { get { return inGamePlayers; } }
        public string Stadium { get { return stadium; } }

        public Team(string name, string std)
        {
            this.name = name;
            this.stadium = std;
            //TODO: Initialize both arrays;
        }

        public List<Player> AvailablePlayers(){

            List<Player> ap = new List<Player>();

            foreach(Player m in members){
                if (!m.HasPlayed)
                {
                    ap.Add(m);
                }
            }

            return ap;
        }

    }
}
