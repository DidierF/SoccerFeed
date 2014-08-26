using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class StartWindow : Form
    {
        private List<Team> teams;
        private Game newGame;
        //private DataBaseInterface dbI;
        private SqlConnectionStringBuilder cnb;
        public Game NewGame { get { return newGame; } }
        
        public StartWindow()
        {
            //TODO: Open and Save Buttons

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            InitializeComponent();
            
            cnb = new SqlConnectionStringBuilder(Properties.Settings.Default.SoccerFeedConnectionString);
            cnb.MaxPoolSize = 2;

            //teams = new List<Team>();

            teams = new DataBaseInterface().GetTeams();

            this.home.SelectedIndexChanged += new System.EventHandler(home_SelectedIndexChanged);
            this.away.SelectedIndexChanged += new System.EventHandler(away_SelectedIndexChanged);
            this.Date.Text = System.DateTime.Now.ToString();
            foreach (Team t in teams)
            {
                home.Items.Add(t.Name);
                away.Items.Add(t.Name);
            }
        }

        

        private void home_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string std = "";
            foreach (Team t in teams)
            {
                if(t.Name == (string)cb.SelectedItem){
                    std = t.Stadium;
                    Team1Players.Clear();
                    foreach (Player p in t.Members)
                    {
                        Team1Players.AppendText(p.Number + " " + p.Position + " " + p.Name + "\n");
                    }
                    break;
                }
            }
            this.Stadium.Text = std;

        }

        private void away_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            foreach (Team t in teams)
            {
                if (t.Name == (string)cb.SelectedItem)
                {
                    Team2Players.Clear();
                    foreach (Player p in t.Members)
                    {
                        Team2Players.AppendText(p.Number + " " + p.Position + " " + p.Name + "\n");
                    }
                    break;
                }
            }
        }
        
        private void Start_Click(object sender, EventArgs e)
        {
            Team[] playingTeams = new Team[2];

            foreach (Team t in teams)
            {
                if (t.Name == (string)home.SelectedItem)
                {
                    playingTeams[0] = t;
                }
                else if (t.Name == (string)away.SelectedItem)
                {
                    playingTeams[1] = t;
                }
            }

            newGame = new Game(new DataBaseInterface().GetNewGameID(),playingTeams[0], playingTeams[1]);
            if (playingTeams[0] != null && playingTeams[1] != null && playingTeams[0] != playingTeams[1])
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        

    }
}
