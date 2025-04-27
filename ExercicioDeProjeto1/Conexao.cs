using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace ExercicioDeProjeto1
{
    class Conexao
    {
        public string conec = "SERVER=localhost; DATABASE=aula; UID=root; PWD=; PORT=;"; //variável String com as informações do servidor, banco de dados (pegar no XAMP

        public MySqlConnection con = null; //variável que carrega conexão somente e tiver  a referência MYSql.Data instalado

        //abrir conexão
        public void AbrirConexao()
        {
            //testar
            try
            {
                con = new MySqlConnection(conec);
                con.Open();
            }
            // se der erro
            catch (Exception ex)
            {
                MessageBox.Show("Erro no servidor: " + ex.Message);

            }
        
        }

        //fechar conexão
        public void FecharConexao()
        {
            //testar
            try
            {
                con = new MySqlConnection(conec);
                con.Close();
            }
            // se der erro
            catch (Exception ex)
            {
                MessageBox.Show("Erro no servidor: " + ex.Message);
            }
        }

    }
}
