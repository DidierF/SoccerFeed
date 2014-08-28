using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public 
        class Player
    {
        private int id;
        private string name;
        private string number;
        private string position;
        private bool hasPlayed;

        public int ID { get { return id; } }
        public string Name { get { return name; } }
        public string Number { get { return number; } }
        public string Position { get { return position; } }
        public bool HasPlayed { get { return hasPlayed; } set { hasPlayed = value; } }
        
        public Player(string name, string number, string pos, int id)
        {
            this.name = name;
            this.number = number;
            this.position = pos;
            this.id = id;
            hasPlayed = false;
        }

    }
}
