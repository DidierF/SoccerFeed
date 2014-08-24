using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Game
    {
        //Currently playing teams.
        private Team[] teams;
        //Game annotations.
        private List<Annotation> annotations;
        //Game starting time.
        private DateTime startTime;

        public DateTime GameTime { get { return startTime; } }

        public Game(Team t1, Team t2)
        {
            teams = new Team[] { t1, t2 };
            annotations = new List<Annotation>();
            startTime = System.DateTime.Now;
        }

        public void addAnnotation()
        {

        }

        public Team getTeam1()
        {
            return teams[0];
        }
        public Team getTeam2()
        {
            return teams[1];
        }
        

    }
}
