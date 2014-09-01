using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class Game
    {
        //TODO: Get the Time from StartWindow.

        private int id;
        //Currently playing teams.
        private Team[] teams;
        //Game annotations.
        private List<Annotation> annotations;
        //Game starting time.
        private DateTime startTime;

        private TimeSpan gameTime;

        private int[] score = new int[] { 0, 0 };

        public int ID { get { return id; } }
        public DateTime StartTime { get { return startTime; } }
        public int[] Score { get { return score; } }

        public TimeSpan GameTime { get { return gameTime; } }

        public Game(int id, Team t1, Team t2)
        {
            this.id = id;
            teams = new Team[] { t1, t2 };
            annotations = new List<Annotation>();
            startTime = System.DateTime.Now;
        }

        public void addAnnotation(Annotation an)
        {
            annotations.Add(an);
            if (an.Motive == "Goal")
            {
                Team t = getPlayersTeam(an.Player);
                if (t.Name == teams[0].Name)
                {
                    score[0]++;
                }
                else if (t.Name == teams[1].Name)
                {
                    score[1]++;
                }
            }
        }

        private Team getPlayersTeam(Player player)
        {
            Team team = null;
            bool found = false;
            foreach (Team t in teams)
            {
                foreach (Player p in t.Members)
                {
                    if (p.ID == player.ID)
                    {
                        team = t;
                    }
                    if (found) break;
                }
                if (found) break;
            }
            return team;
        }

        public Team homeTeam()
        {
            return teams[0];
        }
        public Team awayTeam()
        {
            return teams[1];
        }

        public void GameTimeTick(TimeSpan ts)
        {
            gameTime.Add(ts);
        }
    }
}
