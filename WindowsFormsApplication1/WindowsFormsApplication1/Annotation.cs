﻿using System;
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
        public static int RED_CARD = 3;
        public static int YELLOW_CARD = 4;
        public static int SUBSTITUTION = 5;
        public static int GOAL_KICK = 6;
        public static int THROW_IN = 7;
        public static int CORNER = 8;
        public static int OFFSIDE = 9;
        public static int FREE_THROW = 10;
        public static int PENALTY = 11;

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
                    case 1:
                        return "Goal";
                    case 2:
                        return "Foul";
                    case 3:
                        return "Red Card";
                    case 4:
                        return "Yellow Card";
                    case 5:
                        return "Substitution";
                    case 6:
                        return "Goal Kick";
                    case 7:
                        return "Throw In";
                    case 8:
                        return "Corner";
                    case 9:
                        return "Offside";
                    case 10:
                        return "Free Throw";
                    case 11:
                        return "Penalty";
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
           // this.id = id; 
        }

        public Annotation(DateTime time, Player player, Player auxplayer, int motive)
        {
            this.time = time;
            this.player = player;
            this.auxPlayer = auxplayer;
            this.motive = motive;
           // this.id = id; 
        }

        public override string ToString()
        {
            string result = "";

            switch (motive)
            {
                case 1:
                    if (auxPlayer != null)
                    {
                        result = "[" + time + "] " + player + " scored a" + Motive + "assisted by " + auxPlayer;
                    }
                    else
                    {
                        result = "[" + time + "] " + player + " scored a " + Motive;
                    }
                    break; 
                case 2:
                    result = "[" + time + "] " + player + " performed a" + Motive + "to " + auxPlayer;
                    break; 
                case 3:
                    result = "[" + time + "] " + player + " received a " + Motive;
                    break; 
                case 4:
                    result = "[" + time + "] " + player + " received a " + Motive;
                    break; 
                case 5: 
                    result = "[" + time + "]" + player + " was replaced by " + auxPlayer;
                    break; 
                case 6:
                    result = "[" + time + "] " + player + " performed a " + Motive;
                    break; 
                case 7:
                    result = "[" + time + "] " + player + " performed a " + Motive;
                    break; 
                case 8:
                    result = "[" + time + "] " + player + " performed a " + Motive;
                    break; 
                case 9: 
                    result = "[" + time + "] " + player + " performed a " + Motive;
                    break; 
                case 10: 
                    result = "[" + time + "] " + player + " performed a " + Motive;
                    break; 
                case 11: 
                    result = "[" + time + "] " + player + " performed a " + Motive;
                    break; 


            }
            //TODO
            return result;
        }

        public static void GetPossibleMotives(string[] str)
        {
            if (str.Length >= 11)
            {
                str = new string[] { "Goal", "Foul", "Red Card",
                    "Yellow Card", "Substitution", "Goal Kick", 
                    "Throw In", "Corner", "Offside", "Free Throw", "Penalty"};
            }
        }
    }
}
