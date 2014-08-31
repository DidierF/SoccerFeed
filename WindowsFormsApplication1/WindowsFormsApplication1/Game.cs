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

        public int ID { get { return id; } }
        public DateTime GameTime { get { return startTime; } }

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
        }

        public Team homeTeam()
        {
            return teams[0];
        }
        public Team awayTeam()
        {
            return teams[1];
        }
    }
}
