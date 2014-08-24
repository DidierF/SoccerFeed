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
        public StartWindow()
        {
            teams = new List<Team>();
            InitializeComponent();
            AddTeams();
            this.home.SelectedIndexChanged += new System.EventHandler(home_SelectedIndexChanged);
            this.away.SelectedIndexChanged += new System.EventHandler(away_SelectedIndexChanged);
            this.Date.Text = System.DateTime.Now.ToString();

        }

        private void AddTeams()
        {
            SqlConnectionStringBuilder cnb = new SqlConnectionStringBuilder(Properties.Settings.Default.SoccerFeedConnectionString);
            cnb.MaxPoolSize = 2;

            using (SqlConnection cn = new SqlConnection(cnb.ConnectionString))
            {
                string teamName = "";
                List<Player> p;

                SqlCommand command = new SqlCommand();
                command.Connection = cn;
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from Team;";

                SqlCommand command2 = new SqlCommand();
                command2.Connection = cn;
                command2.CommandType = CommandType.Text;
                
                
                cn.Open();

                SqlDataReader reader = command.ExecuteReader();
                SqlDataReader reader2; 
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        teamName = removeSpaces(String.Format("{0}", reader[0]));
                        teams.Add(new Team(teamName, removeSpaces(String.Format("{0}", reader[1]))));
                        command2.CommandText = "select * from player where TeamName = '" + teamName + "'";
                        reader2 = command2.ExecuteReader();
                        p = new List<Player>();
                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                p.Add(new Player(removeSpaces(String.Format("{0}", reader2[1])), 
                                    removeSpaces(String.Format("{0}", reader2[2])),
                                    removeSpaces(String.Format("{0}", reader2[3]))));
                            }
                        }
                        teams[teams.Count - 1].Members = p;
                        reader2.Close();
                    }
                }

                reader.Close();
            }

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

        private string removeSpaces(string str)
        {
            string result = "";
            char c;
            for(int i = 0; i < str.Length; i++){
                c = str[i];
                if(c != ' ' || (c == ' ' && i != str.Length-1 && str[i + 1] != ' ')){
                    result += c;
                }
            }

            return result;
        }
        

    }
}
