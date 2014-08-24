using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Annotation
    {
        //TODO: Finish writing possible motives
        /// <summary>
        /// Annotation Motives.
        /// </summary>
        public static int GOAL = 1;
        public static int PENALTY = 2;
        public static int CORNER = 3;
        public static int FREE_THROW = 4;

        
        private float time;
        private string player;
        private string Aux;
        private int motive;

        public float Time { get { return time; } }
        public string Player { get { return player; } }
        public string Motive
        {
            get
            {
                switch (motive)
                {
                    case 1:
                        return "Goal";
                    case 2:
                        return "Penalty";
                    case 3:
                        return "Corner";
                    default:
                        return "";
                }
            }
        }

        public Annotation(float time, String playerName, int motive)
        {
            this.time = time;
            this.player = playerName;
            this.motive = motive;
        }
    }
}
