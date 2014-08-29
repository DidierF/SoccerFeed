using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MainWindow : Form
    {
        private Game game;
        private DateTime currentTime;
        private TimeSpan actualTime;
        private Team homeTeam; 
        private Player mainPlayer, auxPlayer; 
        private int annotationMotive; 

        public Game Game
        {
            get { return game; }
            set
            {
                if (game == null)
                {
                    game = value;
                }
            }
        }

        public MainWindow(Game gm)
        {
            InitializeComponent();
            ///Sets the window not resizable.
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Game = gm;
            team1Check.Text = game.homeTeam().Name;
            team2Check.Text = game.awayTeam().Name;
            currentTime = new DateTime(); 
            playComboBox.Items.AddRange(new string[] 
            {
                "Goal", "Foul", "Red Card", "Yellow Card", "Substitution",
                "Goal Kick", "Throw In", "Corner", "Offside", "Free Throw", "Penalty"
            });
            timer1.Interval = 1000; 
            timer1.Start(); 
        }

        private void team1Check_CheckedChanged(object sender, EventArgs e)
        {
            if (team1Check.Checked)
            {
                playerComboBox.Text = "";
                playerComboBox.Items.Clear();
                foreach (Player p in game.homeTeam().InGamePlayers)
                {
                    playerComboBox.Items.Add(p.Name);
                }
            }
        }

        private void team2Check_CheckedChanged(object sender, EventArgs e)
        {
            if (team2Check.Checked)
            {
                playerComboBox.Text = "";
                playerComboBox.Items.Clear();
                foreach (Player p in game.awayTeam().InGamePlayers)
                {
                    playerComboBox.Items.Add(p.Name);
                }
            }
        }

        private void insertBtn_Click(object sender, EventArgs e)
        {
            DateTime currentTime = new DateTime(); 
            int motive = playerComboBox.SelectedIndex;

            if(auxComboBox.Enabled == true)
            {
                game.addAnnotation(new Annotation(currentTime.Add(actualTime), this.mainPlayer, this.auxPlayer, annotationMotive)); 
            }
            else
            {
                game.addAnnotation(new Annotation(currentTime.Add(actualTime), this.mainPlayer, annotationMotive)); 
            }
            //game.addAnnotation(new Annotation(time, player, motive));
        }

        private void playComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            List<Player> players = new List<Player>();
            switch (cb.SelectedIndex)
            {
                case 2:
                case 3:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    auxComboBox.ResetText();
                    auxComboBox.Enabled = false;
                    break;
                case 0:
                    auxComboBox.ResetText();
                    auxComboBox.Enabled = true;
                    if (team1Check.Checked)
                    {
                        players = game.homeTeam().InGamePlayers;
                    }
                    else if (team2Check.Checked)
                    {
                        players = game.awayTeam().InGamePlayers;
                    }
                    break;
                case 4:
                    auxComboBox.ResetText();
                    auxComboBox.Enabled = true;
                    if (team1Check.Checked)
                    {
                        players = game.homeTeam().AvailablePlayers();
                    }
                    else if (team2Check.Checked)
                    {
                        players = game.awayTeam().AvailablePlayers();
                    }
                    break;
                case 1:
                    auxComboBox.ResetText();
                    auxComboBox.Enabled = true;
                    if (team1Check.Checked)
                    {
                        players = game.awayTeam().InGamePlayers;
                    }
                    else if (team2Check.Checked)
                    {
                        players = game.homeTeam().InGamePlayers;
                    }
                    break;
            }
            auxComboBox.Items.Clear();
            foreach (Player p in players)
            {
                auxComboBox.Items.Add(p.Name);
            }
            annotationMotive = cb.SelectedIndex;
            if(team1Check.Checked)
            {
                this.mainPlayer = game.homeTeam().AvailablePlayers()[playerComboBox.SelectedIndex];
                try
                {
                    this.auxPlayer = game.awayTeam().AvailablePlayers()[auxComboBox.SelectedIndex];
                }
                catch { }
            }
            else
            {
                this.mainPlayer = game.awayTeam().AvailablePlayers()[playerComboBox.SelectedIndex];
                try
                {
                    this.auxPlayer = game.homeTeam().AvailablePlayers()[auxComboBox.SelectedIndex]; 
                }
                catch { }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           currentTime = System.DateTime.Now; 
           actualTime = currentTime.Subtract(game.GameTime);
           time.Text = actualTime.Minutes + ":" + actualTime.Seconds;  
        }




    }
}
