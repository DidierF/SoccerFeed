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
        private Player mainPlayer, auxPlayer; 
        private int annotationMotive;//, team1, team2; 

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
            annotationHistory.ReadOnly = true;
            annotationHistory.BackColor = System.Drawing.SystemColors.Window;
            this.Game = gm;
            team1Check.Text = game.homeTeam().Name;
            team2Check.Text = game.awayTeam().Name;
            currentTime = new DateTime(); 
            //team1 = 0;
            //team2 = 0; 
            playComboBox.Items.AddRange(new string[] 
            {
                "Goal", "Foul", "Red Card", "Yellow Card", "Substitution",
                "Goal Kick", "Throw In", "Corner", "Offside", "Free Throw", "Penalty"
            });
            annotationHistory.AppendText("[" + System.DateTime.Now + "] " + "Game Start\n");
            annotationHistory.Update(); 
            timer1.Interval = 1000; 
            timer1.Start();
            DisplayAnnotations(game.ID);
        }

        private void DisplayAnnotations(int p)
        {
            List<Annotation> anns = new DataBaseInterface().GetAnnotations(p);

            foreach (Annotation a in anns)
            {
                game.addAnnotation(a);
                annotationHistory.AppendText(a.ToString() + "\n");
                UpdateScore(a);
            }
        }

        private void UpdateScore(Annotation a)
        {

            if (a.Motive == "Goal")
            {
                team1Score.Text = game.Score[0].ToString();
                team2Score.Text = game.Score[1].ToString();
            }
            team1Score.Update();
            team2Score.Update();
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
            if (!(playerComboBox.SelectedItem == null || playComboBox.SelectedItem == null) && !(auxComboBox.Enabled && auxComboBox.SelectedItem == null) && playComboBox.SelectedIndex == 0)
            {
            DateTime currentTime = game.GameTime.Add(actualTime);
            int motive = playComboBox.SelectedIndex;
            int newID = new DataBaseInterface().GetNewAnnotationID();
            Annotation ann = new Annotation(currentTime, this.mainPlayer, this.auxPlayer, annotationMotive, newID);

            game.addAnnotation(ann);
            new DataBaseInterface().SaveAnnotation(game, ann);

            annotationHistory.AppendText(ann.ToString() + "\n");
            annotationHistory.Update();
            UpdateScore(ann);

            if (annotationMotive == 4)
            {
                substitution(); 
            }
            playerComboBox.ResetText();
            playComboBox.ResetText();
            auxComboBox.ResetText();
            mainPlayer = null;
            annotationMotive = '0';
            auxPlayer = null;
            auxComboBox.Enabled = false; 
            }

        }

        private void playComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox playCB = (ComboBox)sender;
            List<Player> players = new List<Player>();
            switch (playCB.SelectedIndex)
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
            }
            auxComboBox.Items.Clear();
            foreach (Player p in players)
            {
                auxComboBox.Items.Add(p.Name);
            }
            annotationMotive = playCB.SelectedIndex;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           currentTime = System.DateTime.Now; 
           actualTime = currentTime.Subtract(game.GameTime);
           time.Text = actualTime.Minutes + ":" + actualTime.Seconds;  
        }

        private void auxComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox auxCB = (ComboBox)sender;

            switch (annotationMotive)
            {
                case 0: 
                    if(team1Check.Checked)
                    {
                        this.auxPlayer = game.homeTeam().InGamePlayers[auxCB.SelectedIndex]; 
                    }
                    if(team2Check.Checked)
                    {
                        this.auxPlayer = game.awayTeam().InGamePlayers[auxCB.SelectedIndex];
                    }
                    break; 
                case 1:
                    if(team1Check.Checked)
                    {
                        this.auxPlayer = game.awayTeam().InGamePlayers[auxCB.SelectedIndex]; 
                    }
                    if (team2Check.Checked)
                    {
                        this.auxPlayer = game.homeTeam().InGamePlayers[auxCB.SelectedIndex]; 
                    }
                    break;
                case 4:
                    if(team1Check.Checked)
                    {
                        this.auxPlayer = game.homeTeam().AvailablePlayers()[auxCB.SelectedIndex];
                    }
                    if(team2Check.Checked)
                    {
                        this.auxPlayer = game.awayTeam().AvailablePlayers()[auxCB.SelectedIndex]; 
                    }
                    break;
                default:
                    break; 
            }
        }

        private void playerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (team1Check.Checked)
            {
                this.mainPlayer = game.homeTeam().InGamePlayers[playerComboBox.SelectedIndex];
            }
            else
            {
                this.mainPlayer = game.awayTeam().InGamePlayers[playerComboBox.SelectedIndex];
            }
        }

        private void substitution()
        {
            if (team1Check.Checked)
            {
                mainPlayer.HasPlayed = false;
                auxPlayer.HasPlayed = true; 
                game.homeTeam().InGamePlayers.Add(auxPlayer);
                game.homeTeam().InGamePlayers.Remove(mainPlayer);
                game.homeTeam().AvailablePlayers().Add(mainPlayer);
                game.homeTeam().AvailablePlayers().Remove(auxPlayer);
                playerComboBox.Items.Clear();
                auxComboBox.Items.Clear();
                foreach (Player p in game.homeTeam().InGamePlayers)
                {
                    playerComboBox.Items.Add(p.Name);
                }
                foreach (Player p in game.homeTeam().AvailablePlayers())
                {
                    auxComboBox.Items.Add(p.Name);
                }
            } if (team2Check.Checked)
            {
                mainPlayer.HasPlayed = false;
                auxPlayer.HasPlayed = true; 
                game.awayTeam().InGamePlayers.Add(auxPlayer);
                game.awayTeam().InGamePlayers.Remove(mainPlayer);
                game.awayTeam().AvailablePlayers().Add(mainPlayer);
                game.awayTeam().AvailablePlayers().Remove(auxPlayer);
                playerComboBox.Items.Clear();
                auxComboBox.Items.Clear();
                foreach (Player p in game.awayTeam().InGamePlayers)
                {
                    playerComboBox.Items.Add(p.Name);
                }
                foreach (Player p in game.awayTeam().AvailablePlayers())
                {
                    auxComboBox.Items.Add(p.Name);
                }
            }
        }
    }
}
