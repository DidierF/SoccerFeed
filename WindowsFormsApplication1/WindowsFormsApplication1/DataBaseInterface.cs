using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Handles all connections to the DataBase.
    /// </summary>
    public class DataBaseInterface
    {
        //TODO: Save/Get Annotation
        //TODO: Save Game

        SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(Properties.Settings.Default.SoccerFeedConnectionString);

        public List<Team> GetTeams()
        {
            List<Team> teams = new List<Team>();
            
            csb.MaxPoolSize = 2;
            SqlConnection connection = new SqlConnection(csb.ConnectionString);
            

            using (connection)
            {
                string teamName = "";
                List<Player> p;

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from Team;";

                SqlCommand command2 = new SqlCommand();
                command2.Connection = connection;
                command2.CommandType = CommandType.Text;


                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                SqlDataReader reader2;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        teamName = RemoveSpaces(String.Format("{0}", reader[0]));
                        teams.Add(new Team(teamName, RemoveSpaces(String.Format("{0}", reader[1]))));
                        command2.CommandText = "select * from player where TeamName = '" + teamName + "'";
                        reader2 = command2.ExecuteReader();
                        p = new List<Player>();
                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                int n;
                                Int32.TryParse(String.Format("{0}", reader2[0]), out n);
                                p.Add(new Player(RemoveSpaces(String.Format("{0}", reader2[1])),
                                    RemoveSpaces(String.Format("{0}", reader2[2])),
                                    RemoveSpaces(String.Format("{0}", reader2[3])), n ));
                            }
                        }
                        teams[teams.Count - 1].Members = p;
                        reader2.Close();
                    }
                }

                reader.Close();
            }

            return teams;
        }

        private string RemoveSpaces(string str)
        {
            string result = "";

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != ' ' || (str[i] == ' ' && i != str.Length - 1 && str[i + 1] != ' '))
                {
                    result += str[i];
                }
            }

            return result;
        }

        public void SaveGame(Game gm)
        {
            //TODO: Must Save GameTeam;

            SqlConnection connection = new SqlConnection(csb.ConnectionString);
            int ID = gm.ID;
            string stadium = gm.homeTeam().Stadium;
            DateTime time = gm.GameTime;

            using (connection)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "insert into Game value (" + ID + ", " + stadium + ", " + time + ")" ;

                connection.Open();

                command.ExecuteNonQuery();
            }

        }

        public List<Game> GetAllGames()
        {
            List<Game> games = new List<Game>();
            List<Team> teams = new List<Team>();
            csb.MaxPoolSize = 2;

            SqlConnection connection = new SqlConnection(csb.ConnectionString);


            using(connection)
            {
                SqlCommand GameIDCommand = new SqlCommand();
                GameIDCommand.Connection = connection;
                GameIDCommand.CommandType = CommandType.Text;
                GameIDCommand.CommandText = "Select * from Game";

                SqlCommand TeamsCommand = new SqlCommand();
                TeamsCommand.Connection = connection;
                TeamsCommand.CommandType = CommandType.Text;

                connection.Open();
                SqlDataReader idReader = GameIDCommand.ExecuteReader();
                SqlDataReader teamsReader;

                if (idReader.HasRows)
                {
                    while (idReader.Read())
                    {
                        int id;
                        Team[] gameTeams = new Team[2];

                        Int32.TryParse(String.Format("{0}", idReader[0]), out id);

                        TeamsCommand.CommandText = "select * from GameTeam where GameID = " + id.ToString();
                        teamsReader = TeamsCommand.ExecuteReader();

                        if (teamsReader.HasRows)
                        {
                            int i = 0;
                            while (teamsReader.Read())
                            {
                                foreach (Team t in teams)
                                {
                                    if (t.Name == RemoveSpaces(String.Format("{0}", teamsReader[0])))
                                    {
                                        gameTeams[i++] = t;
                                    }
                                }

                            }
                        }
                        games.Add(new Game(id, teams[0], teams[1]));
                        teamsReader.Close();
                    }
                }
                idReader.Close();
            }


            return games;
        }

        public Game GetGame(int gameID) //Not Implemented
        {
            return null;
        }

        public int GetNewGameID()
        {
            SqlConnection connection = new SqlConnection(csb.ConnectionString);
            int games = 0;
            using (connection)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "select count(gameID) from game";

                connection.Open();
                games = (int)command.ExecuteScalar();

            }
            return games;
        }

        public void SaveAnnotation(Annotation n) //Not Implemented
        {
            int ID = 0;
        }

        public List<Annotation> GetAnnotations(Game gm) //Not Implemented
        {
            List<Annotation> annotations = new List<Annotation>();

            return annotations;
        }

        public int GetNewAnnotationID()
        {
            SqlConnection connection = new SqlConnection(csb.ConnectionString);
            int annotations = 0;
            using (connection)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "select count(gameID) from game";

                connection.Open();
                annotations = (int)command.ExecuteScalar();

            }
            return annotations;
        }
    }
}
