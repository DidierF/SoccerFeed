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
        /// <summary>
        /// Annotation Motives.
        /// </summary>
        //public static int GOAL = 0;
        //public static int FOUL = 0;
        //public static int RED_CARD = 2;
        //public static int YELLOW_CARD = 3;
        //public static int SUBSTITUTION = 4;
        //public static int GOAL_KICK = 5;
        //public static int THROW_IN = 6;
        //public static int CORNER = 7;
        //public static int OFFSIDE = 8;
        //public static int FREE_THROW = 9;
        //public static int PENALTY = 10;

        private int id;
        private int motive;
        private Player player;
        private Player auxPlayer;
        private DateTime time;

        public int ID { get { return id; } }
        public DateTime Time { get { return time; } }
        public Player Player { get { return player; } }
        public Player AuxPlayer { get { return auxPlayer; } }
        public string Motive
        {
            get
            {
                switch (motive)
                {
                    case 0:
                        return "Goal";
                    case 1:
                        return "Foul";
                    case 2:
                        return "Red Card";
                    case 3:
                        return "Yellow Card";
                    case 4:
                        return "Substitution";
                    case 5:
                        return "Goal Kick";
                    case 6:
                        return "Throw In";
                    case 7:
                        return "Corner";
                    case 8:
                        return "Offside";
                    case 9:
                        return "Free Throw";
                    case 10:
                        return "Penalty";
                    default:
                        return "Default";
                }
            }
        }

        public Annotation(DateTime time, Player player, int motive, int id)
        {
            this.time = time;
            this.player = player;
            this.motive = motive;
            this.id = id;
        }

        public Annotation(DateTime time, Player player, Player auxplayer, int motive, int id)
        {
            this.time = time;
            this.player = player;
            this.auxPlayer = auxplayer;
            this.motive = motive;
            this.id = id;
        }

        public override string ToString()
        {
            string result = "";

            switch (motive)
            {
                case 0:
                    if (auxPlayer != null)
                    {
                        result = "[" + time + "] " + player.Name + " scored a " + Motive + " assisted by " + auxPlayer.Name;
                    }
                    else
                    {
                        result = "[" + time + "] " + player.Name + " scored a " + Motive;
                    }
                    break; 
                case 1:
                    result = "[" + time + "] " + player.Name + " performed a " + Motive + " to " + auxPlayer.Name;
                    break; 
                case 2:
                case 3:
                    result = "[" + time + "] " + player.Name + " received a " + Motive;
                    break; 
                case 4:
                    if (auxPlayer != null)
                    {
                        result = "[" + time + "]" + player.Name + " was replaced by " + auxPlayer.Name;
                    }
                    break; 
                case 5:
                case 6:
                case 7:
                case 8:
                case 9: 
                case 10:  
                    result = "[" + time + "] " + player.Name + " performed a " + Motive;
                    break; 
                default:
                    result = "Default motive string";
                    break;

            }
            return result;
        }

        public static void GetPossibleMotives(string[] str)
        {
            if (str.Length >= 11)
            {
                str[0] = "Goal";
                str[1] = "Foul";
                str[2] = "Red Card";
                str[3] = "Yellow Card";
                str[4] = "Substitution";
                str[5] = "Goal Kick";
                str[6] = "Throw In";
                str[7] = "Corner";
                str[8] = "Offside";
                str[9] = "Free Throw";
                str[10] = "Penalty";
            }
        }
    }
}
