
using MySql.Data.MySqlClient;
using System.Data;

public class ClienteRepository {
    private string stringConexao =
        "server=localhost;Port=3306;database=cliente;user=root;Password=";

    public DataTable BuscarClientePorId(int id) {
        using (MySqlConnection conexao = new MySqlConnection(stringConexao)) {
            conexao.Open();

            string query = "SELECT * FROM clientes WHERE id = @id";

            using (MySqlCommand cmd = new MySqlCommand(query, conexao)) {
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }


}

