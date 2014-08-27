using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class Annotation
    {
        //TODO: Finish writing possible motives.
        //TODO: Add  toString method.
        //TODO: Revisar el string de anotaciones. 
        /// <summary>
        /// Annotation Motives.
        /// </summary>
        public static int GOAL = 1;
        public static int FOUL = 2;
        public static int CARD = 3;
        public static int SUBSTITUTION = 4;
        public static int GOAL_KICK = 5;
        public static int THROW_IN = 6;
        public static int CORNER = 7;
        public static int OFFSIDE = 8;
        public static int FREE_THROW = 9;
        public static int PENALTY = 10;

        private int id;
        private int motive;
        private string aux;
        private Player player;
        private Player auxPlayer;
        private DateTime time;

        public int ID { get { return id; } }
        public DateTime Time { get { return time; } }
        public Player Player { get { return player; } }
        public Player AuxPlayer { get { return auxPlayer; } set { this.auxPlayer = value; } }
        public string Aux { get { return aux; } set { this.aux = value; } }
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

        public Annotation(DateTime time, Player player, int motive)
        {
            this.time = time;
            this.player = player;
            this.motive = motive;
        }

        public Annotation(DateTime time, Player player, Player auxplayer, int motive)
        {
            this.time = time;
            this.player = player;
            this.auxPlayer = auxplayer;
            this.motive = motive; 
        }

        public override string ToString()
        {
            string result = "";

            switch (motive)
            {
                case 1:
                    if (auxPlayer != null)
                    {
                        result = "[" + time + "] " + player + " scored a " + Motive + " assisted by " + auxPlayer;
                    }
                    else
                    {
                        result = "[" + time + "] " + player + " scored a " + Motive;
                    }
                    break; 

                case 2:
                    if (auxPlayer != null)
                    {
                        result = "[" + time + "] " + player + " performed a " + Motive + " assisted by " + auxPlayer;
                    }
                    else
                    {
                        result = "[" + time + "] " + player + " performed a " + Motive + "kick";
                    }
                    break; 

                case 3:
                     if (auxPlayer != null)
                    {
                        result = "[" + time + "] " + player + " performed a " + Motive + " assisted by " + auxPlayer;
                    }
                    else
                    {
                        result = "[" + time + "] " + player + " performed a " + Motive + "kick";
                    }
                    break; 

            }
            //TODO
            return result;
        }
    }
}
