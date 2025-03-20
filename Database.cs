using Godot;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;


public partial class Database : Node
{
	private string connectionString = "Server=db4free.net;Database=linuxdefender;User ID=linuxdefender;Password=46412345;SslMode=none;";

    private void TestConnection()
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                GD.Print("Conexão com MySQL bem-sucedida!");
            }
            catch (Exception e)
            {
                GD.PrintErr("Erro ao conectar ao MySQL: ", e.Message);
            }
        }
    }
	public class HighScore
    {
        public string Nick { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }
    }

    public async Task<List<HighScore>> GetScores()
    {
        List<HighScore> scores = new List<HighScore>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            await conn.OpenAsync();
            string query = "SELECT nickName, points, level FROM linuxdefender.HighScore ORDER BY points DESC;";
            
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    scores.Add(new HighScore
                    {
                        Nick = reader.GetString(0),
                        Points = reader.GetInt32(1),
                        Level = reader.GetInt32(2)
                    });
                }
            }
        }

        return scores;
    }
    public void InsertScore(string name, int points, int level)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO linuxdefender.HighScore (nickName, points, `level`) VALUES(@nick, @points, @level);";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@nick", name);
                cmd.Parameters.AddWithValue("@points", points);
                cmd.Parameters.AddWithValue("@level", level);
                cmd.ExecuteNonQuery();
                GD.Print("Pontuação inserido com sucesso!");
            }
        }
    }
}


