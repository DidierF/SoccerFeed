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
            SqlConnection connection = new SqlConnection(csb.ConnectionString);
            int ID = gm.ID;
            string stadium = gm.homeTeam().Stadium;
            DateTime time = gm.GameTime;
            
            //Not sure if connection works like that.
            //Might have to create different using.
            using (connection)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "insert into Game value (" + ID + ", " + stadium + ", " + time + ")" ;

                connection.Open();
                
                command.ExecuteNonQuery();

                connection.Close();
                connection.Open();
                
                command.CommandText = "insert into GameTeam value (" + gm.ID + ", " + gm.homeTeam().Name + ")";
                command.ExecuteNonQuery();

                connection.Close();
                connection.Open();

                command.CommandText = "insert into GameTeam value (" + gm.ID + ", " + gm.awayTeam().Name + ")";
                command.ExecuteNonQuery();
                
            }

        }

        public List<Game> GetAllGames()
        {
            List<Game> games = new List<Game>();
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

                        GetGameTeams(gameTeams, teamsReader);
                        games.Add(new Game(id, gameTeams[0], gameTeams[1]));
                        teamsReader.Close();
                    }
                }
                idReader.Close();
            }


            return games;
        }

        public Game GetGame(int gameID)
        {
            SqlConnection connection = new SqlConnection(csb.ConnectionString);
            Game g;
            Team[] t = new Team[2];
            using(connection)
            {
                //Not sure if needed
                //SqlCommand cmd = new SqlCommand();
                //cmd.Connection = connection;
                //cmd.CommandType = CommandType.Text;
                //cmd.CommandText = "select * from Game where GameID = " + gameID;

                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = connection;
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "select * from GameTeam where GameID = " + gameID;

                //Not sure if needed
                //SqlDataReader rdr1 = cmd.ExecuteReader();
                //if (rdr1.HasRows)
                //{
                    
                //}
                //rdr1.Close();
                

                SqlDataReader rdr2 = cmd2.ExecuteReader();
                GetGameTeams(t, rdr2);
                
                rdr2.Close();
            }
            
            g = new Game(gameID, t[0], t[1]);

            return g;
        }

        private void GetGameTeams(Team[] teamsArray, SqlDataReader reader)
        {
            if (reader.HasRows)
            {
                int i = 0;
                List<Team> teams = new List<Team>();
                while (reader.Read())
                {
                    foreach (Team tm in teams)
                    {
                        if (tm.Name == RemoveSpaces(String.Format("{0}", reader[0])))
                        {
                            teamsArray[i++] = tm;
                        }
                    }

                }
            }
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

        public void SaveAnnotation(Game g, Annotation n)
        {
            SqlConnection connection = new SqlConnection(csb.ConnectionString);

            using (connection)
            {
                string query = "insert into annotation (ID, Motive, Time, Game, MainPlayerID, AuxPlayerID)" +  
                "value (" + n.ID + "," + n.Motive + "," + n.Time + "," + g.ID +"," + n.Player + ","  + n.AuxPlayer + ")";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();


            }

        }

        public List<Annotation> GetAnnotations(Game gm) //Not Implemented
        {
            List<Annotation> annotations = new List<Annotation>();

            SqlConnection connection = new SqlConnection(csb.ConnectionString);

            using(connection)
            {
                string query = "select * from Annotation where GameID = " + gm.ID;
                
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.CommandType = CommandType.Text;

                SqlCommand playerCmd = new SqlCommand();
                playerCmd.Connection = connection;
                playerCmd.CommandType = CommandType.Text;

                SqlDataReader rdr = cmd.ExecuteReader();
                SqlDataReader playerRdr;
                if (rdr.HasRows)
                {
                    DateTime time;
                    while (rdr.Read())
                    {
                        time = rdr.GetDateTime(3);
                        //TODO: Get the annotation motive as string.

                        playerCmd.CommandText = "select * from player where ID = " + rdr.GetInt32(5);
                        playerRdr = playerCmd.ExecuteReader();
                        
                        int motive = GetMotiveInt(RemoveSpaces(string.Format("{0}", rdr[1])));

                        annotations.Add(new Annotation(time,
                            new Player(RemoveSpaces(string.Format("{0}", playerRdr[1])), string.Format("{0}", playerRdr[2]),
                                RemoveSpaces(string.Format("{0}", playerRdr[3])), playerRdr.GetInt32(0)), motive));
                        playerRdr.Close();
                    }
                }
                rdr.Close();
            }

            return annotations;
        }

        private int GetMotiveInt(string motiveString)
        {
            int motiveInt = 0;
            string[] allMotives = new string[11];
            Annotation.GetPossibleMotives(allMotives);

            for (int i = 0; i < 11; i++ )
            {
                if (allMotives[i] == motiveString)
                {
                    motiveInt = i + 1;
                }
            }

            return motiveInt;
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
