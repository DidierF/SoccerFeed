using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class Player
    {
        private string name;
        private string number;
        private string position;
        private bool hasPlayed;

        public string Name { get { return name; } }
        public string Number { get { return number; } }
        public string Position { get { return position; } }
        public bool HasPlayed { get { return hasPlayed; } }
        
        public Player(string name, string number, string pos)
        {
            this.name = name;
            this.number = number;
            this.position = pos;
            hasPlayed = false;
        }

    }
}
