using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjetoHelpDesk
{
    public partial class NovoChamado : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MinhaConexao"]?.ConnectionString;
        private string prioridadeAutomatica = "Baixa";

        public NovoChamado()
        {
            InitializeComponent();
        }

        private void NovoChamado_Load(object sender, EventArgs e)
        {
            txtNomeFixo.Text = Login.UsuarioLogadoNome;
            txtEmailFixo.Text = Login.UsuarioLogadoEmail;
            txtRaFixo.Text = Login.UsuarioLogadoRA;
            txtNomeCursoFixo.Text = Login.UsuarioLogadoCurso;

            CarregarCategorias();
        }

        private void CarregarCategorias()
        {
            string query = "SELECT ID, NomeCategoria FROM Categorias ORDER BY NomeCategoria";
            DataTable dtCategorias = new DataTable();

            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                using (SqlCommand comando = new SqlCommand(query, conexao))
                using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                {
                    conexao.Open();
                    adapter.Fill(dtCategorias);

                    // Configura o ComboBox
                    cmbCategoria.DataSource = dtCategorias;
                    cmbCategoria.DisplayMember = "NomeCategoria"; // O que o usuário vê
                    cmbCategoria.ValueMember = "ID";              // O que o código usa (o ID)
                    cmbCategoria.SelectedIndex = -1;
                    cmbCategoria.Text = "Selecione uma categoria";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar categorias: " + ex.Message, "Erro de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botão de Criar o Chamado
        private void btnCriarChamado_Click(object sender, EventArgs e)
        {
            // Validação
            if (string.IsNullOrWhiteSpace(txtTituloProblema.Text) ||
            string.IsNullOrWhiteSpace(txtDescricao.Text) ||
            cmbCategoria.SelectedIndex <= 0) 
            {
                MessageBox.Show("Título, Descrição e Categoria são obrigatórios.", "Campos Vazios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string queryInsert = @"INSERT INTO Chamados (UsuarioID, Titulo, Descricao, Status, Prioridade, CategoriaID, DataCriacao, DataAtualizacao) 
                               VALUES (@UsuarioID, @Titulo, @Descricao, @Status, @Prioridade, @CategoriaID, GETDATE(), GETDATE())";

            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                using (SqlCommand comando = new SqlCommand(queryInsert, conexao))
                {
                    comando.Parameters.AddWithValue("@UsuarioID", Login.UsuarioLogadoId);
                    comando.Parameters.AddWithValue("@Titulo", txtTituloProblema.Text);
                    comando.Parameters.AddWithValue("@Descricao", txtDescricao.Text);
                    comando.Parameters.AddWithValue("@Status", "Aberto");
                    comando.Parameters.AddWithValue("@Prioridade", this.prioridadeAutomatica); 
                    comando.Parameters.AddWithValue("@CategoriaID", Convert.ToInt32(cmbCategoria.SelectedValue));

                    conexao.Open();
                    comando.ExecuteNonQuery();

                    MessageBox.Show($"Chamado criado com sucesso!\nPrioridade definida como: {this.prioridadeAutomatica}", "Sucesso");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar chamado: {ex.Message}", "Erro de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botão de Cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Evento que dispara a cada letra digitada no Título
        private void txtTituloDoProblema_TextChanged(object sender, EventArgs e)
        {
            string titulo = txtTituloProblema.Text.ToLower();

            // Palavras-chave para prioridade
            string[] urgenteKeywords = { "urgente", "parado", "nao funciona", "emergencia", "critico", "bloqueado" };
            string[] altaKeywords = { "erro", "lento", "travando", "problema", "falha" };
            string[] mediaKeywords = { "ajuda", "dificuldade", "atraso", "duvida", "acesso" };

            // Define o padrão como "Baixa"
            this.prioridadeAutomatica = "Baixa";

            // Verifica se deve aumentar a prioridade (da mais alta para a mais baixa)
            foreach (string keyword in urgenteKeywords)
            {
                if (titulo.Contains(keyword))
                {
                    this.prioridadeAutomatica = "Urgente";
                    return;
                }
            }

            foreach (string keyword in altaKeywords)
            {
                if (titulo.Contains(keyword))
                {
                    this.prioridadeAutomatica = "Alta";
                    return;
                }
            }

            foreach (string keyword in mediaKeywords)
            {
                if (titulo.Contains(keyword))
                {
                    this.prioridadeAutomatica = "Média";
                    return;
                }
            }
        }
    }
}
