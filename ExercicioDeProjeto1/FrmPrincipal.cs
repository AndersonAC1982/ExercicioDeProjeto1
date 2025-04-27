using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ExercicioDeProjeto1
{
    public partial class FrmPrincial: Form
    {
        Conexao con = new Conexao();
        string sql;
        MySqlCommand cmd;

        public FrmPrincial()
        {
            InitializeComponent();
        }

        private void FrmPrincial_Load(object sender, EventArgs e)
        {
            ListarGrid();
        }
        private void FormatarGrid()
        {
            grid.Columns[0].HeaderText = "Código";
            grid.Columns[1].HeaderText = "Nome";
            grid.Columns[2].HeaderText = "Endereço";
            grid.Columns[3].HeaderText = "CPF";
            grid.Columns[4].HeaderText = "Telefone";

        }

        private void ListarGrid()
        {
            con.AbrirConexao();
            sql = "SELECT * FROM cliente ORDER BY NOME ASC";
            cmd = new MySqlCommand(sql, con.con); // Comando dentro da conexão
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            con.FecharConexao();
            FormatarGrid();

        }
        
        private void btnNovo_Click(object sender, EventArgs e)
        {
            HabilitarCampos();
            LimparCampos();
            txtNome.Focus();

            HabilitarBotoes();
            btnNovo.Enabled = false;

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Nome vazio! Por favor digite um nome.");
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }
            if (txtCPF.Text.ToString().Trim() == "   .   .   -  " || txtCPF.Text.Length < 14)
            {
                MessageBox.Show("CPF vazio ou inclompleto! Por favor digite um CPF completo.");
                txtCPF.Text = "";
                txtCPF.Focus();
                return;
            }
            // abrir conexão do Banco de dados
            con.AbrirConexao(); 
            //para Inserir os dados ao clicar no botão salvar
            sql = "INSERT INTO cliente (nome, endereco, cpf, telefone) VALUES(@nome, @endereco, @cpf, @telefone)";
            //comando para transferir os dados informados na variável "sql" para o BD através da instância "con.con"
            cmd = new MySqlCommand(sql, con.con);
            //comando que define o que deve ser feito com os dados, no caso Adicionar com os valores (AddWithValue)
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@cpf", txtCPF.Text);
            cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
            cmd.ExecuteNonQuery();
            con.FecharConexao();

            MessageBox.Show("Cadastro incluso com sucesso!");

            LimparCampos();
            DesabilitarCampos();
            DesabilitarBotoes();
            btnNovo.Enabled = true;


        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Nome vazio! Por favor digite um nome.");
                txtNome.Text = "";
                txtNome.Focus();
                return;
            }
            if (txtCPF.Text.ToString().Trim() == "   .   .   -  " || txtCPF.Text.Length < 14)
            {
                MessageBox.Show("CPF vazio ou inclompleto! Por favor digite um CPF completo.");
                txtCPF.Text = "";
                txtCPF.Focus();
                return;
            }
            con.AbrirConexao();
            //para Inserir os dados ao clicar no botão ALTERAR
            sql = "UPDATE cliente SET nome = @nome, endereco = @endereco, cpf = @cpf, telefone = @telefone";
            //comando para transferir os dados informados na variável "sql" para o BD através da instância "con.con"
            cmd = new MySqlCommand(sql, con.con);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@cpf", txtCPF.Text);
            cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
            cmd.ExecuteNonQuery();
            con.FecharConexao();

            MessageBox.Show("Cadastro incluso com sucesso!");

            LimparCampos();
            DesabilitarCampos();
            DesabilitarBotoes();
            btnNovo.Enabled = true;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            LimparCampos();
            DesabilitarCampos();
            DesabilitarBotoes();
            btnNovo.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DesabilitarBotoes();
            DesabilitarCampos();
            LimparCampos();
            btnNovo.Enabled = true;
        }
        //Metodo para habilitar botoes
        private void HabilitarBotoes()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = true;
            btnExcluir.Enabled = true;
            btnCancelar.Enabled = true;
        }

        //Metodo para desabilitar botoes
        private void DesabilitarBotoes()
        {
            btnNovo.Enabled = false;
            btnSalvar.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = false;
        }
        //Metodo para habilitar campos
        private void HabilitarCampos()
        {
            txtNome.Enabled = true;
            txtCPF.Enabled = true;
            txtEndereco.Enabled = true;
            txtTelefone.Enabled = true;
        }
        //Metodo para desabilitar campos
        private void DesabilitarCampos()
        {
            txtNome.Enabled = false;
            txtCPF.Enabled = false;
            txtEndereco.Enabled = false;
            txtTelefone.Enabled = false;
        }
        //Metodo Limpar Campos
        private void LimparCampos()
        {
            txtNome.Text = "";
            txtCPF.Text = "";
            txtEndereco.Text = "";
            txtTelefone.Text = "";
        }

    }//fim
}
