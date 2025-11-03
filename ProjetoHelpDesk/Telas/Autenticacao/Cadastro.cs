using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BCrypt.Net;

namespace ProjetoHelpDesk
{
    public partial class Cadastro : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MinhaConexao"].ConnectionString;

        public Cadastro()
        {
            InitializeComponent();
            // Carrega o cursos no comboB ao iniciar a tela
            CarregarCursos();
        }

        // Busca os cursos no banco de dados e preenche o ComboBox 'cmbCurso'
        private void CarregarCursos()
        {
            cmbCurso.Items.Clear();
            cmbCurso.ValueMember = "ID"; // O valor interno será o ID do curso
            cmbCurso.DisplayMember = "NomeCurso"; // O texto exibido será o NomeCurso

            string query = "SELECT ID, NomeCurso FROM Cursos ORDER BY NomeCurso";
            using (SqlConnection conexao = new SqlConnection(connectionString))
            using (SqlCommand comando = new SqlCommand(query, conexao))
            {
                try
                {
                    conexao.Open();
                    SqlDataReader leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        // Adiciona um objeto simples contendo ID e Nome para cada curso
                        cmbCurso.Items.Add(new { ID = Convert.ToInt32(leitor["ID"]), NomeCurso = leitor["NomeCurso"].ToString() });
                    }
                    // Define um placeholder ou deixa sem seleção inicial
                    cmbCurso.Text = "Selecione o curso...";
                    cmbCurso.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar lista de cursos: " + ex.Message, "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnCadastrar.Enabled = false; // Desabilita o botão de cadastrar se não conseguir carregar os cursos
                }
            }
        }

        // Evento do botão Cadastrar
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            // Validação de campos vazios
            if (string.IsNullOrWhiteSpace(txtNomeCadastro.Text) ||
                string.IsNullOrWhiteSpace(txtEmailCadastro.Text) ||
                string.IsNullOrWhiteSpace(txtRaAlunoCadastro.Text) ||
                string.IsNullOrWhiteSpace(txtSenhaCadastro.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmaSenha.Text) ||
                cmbCurso.SelectedIndex <= 0)
            {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios.", "Campos Vazios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validação do RA (máximo 7 caracteres)
            if (txtRaAlunoCadastro.Text.Trim().Length != 7)
            {
                MessageBox.Show("O RA deve ter 7 caracteres.", "RA Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRaAlunoCadastro.Focus();
                txtRaAlunoCadastro.SelectAll();
                return;
            }

            // Validação de senhas
            if (txtSenhaCadastro.Text != txtConfirmaSenha.Text)
            {
                MessageBox.Show("As senhas não coincidem.", "Erro de Senha", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmaSenha.Focus();
                txtConfirmaSenha.SelectAll();
                return;
            }

            // Validação de tamanho da senha
            if (txtSenhaCadastro.Text.Length < 8)
            {
                MessageBox.Show("A senha deve ter pelo menos 8 caracteres.", "Senha Curta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenhaCadastro.Focus();
                return;
            }

            // Hashing da Senha
            string senhaHash;
            try
            {
                senhaHash = BCrypt.Net.BCrypt.HashPassword(txtSenhaCadastro.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar hash da senha: {ex.Message}", "Erro de Segurança", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verifica a duplicidade de Email e RA ANTES de tentar inserir
            try
            {
                bool emailExiste = false;
                bool raExiste = false;

                using (SqlConnection conexaoVerifica = new SqlConnection(connectionString))
                {
                    conexaoVerifica.Open();

                    // Verifica Email na tabela Usuarios
                    string queryVerificaEmail = "SELECT COUNT(*) FROM Usuarios WHERE Email = @Email";
                    using (SqlCommand cmdEmail = new SqlCommand(queryVerificaEmail, conexaoVerifica))
                    {
                        cmdEmail.Parameters.AddWithValue("@Email", txtEmailCadastro.Text.Trim());
                        if ((int)cmdEmail.ExecuteScalar() > 0) emailExiste = true;
                    }

                    // Verifica RA na tabela Alunos
                    if (!emailExiste)
                    {
                        string queryVerificaRA = "SELECT COUNT(*) FROM Alunos WHERE RegistroAluno = @RegistroAluno";
                        using (SqlCommand cmdRA = new SqlCommand(queryVerificaRA, conexaoVerifica))
                        {
                            cmdRA.Parameters.AddWithValue("@RegistroAluno", txtRaAlunoCadastro.Text.Trim());
                            if ((int)cmdRA.ExecuteScalar() > 0) raExiste = true;
                        }
                    }
                }

                // Mostra mensagens de erro específicas
                if (emailExiste)
                {
                    MessageBox.Show("Este Email já está cadastrado.", "Email Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmailCadastro.Focus(); txtEmailCadastro.SelectAll(); return;
                }
                if (raExiste)
                {
                    MessageBox.Show("Este RA já está cadastrado.", "RA Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRaAlunoCadastro.Focus(); txtRaAlunoCadastro.SelectAll(); return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao verificar duplicidade: {ex.Message}", "Erro de Banco de Dados");
                return;
            }

            int novoUsuarioId = 0; // Para guardar o ID gerado
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();
                SqlTransaction transaction = conexao.BeginTransaction();

                // Tenta inserir nas duas tabelas dentro de uma transação
                try
                {
                    string queryInsertUsuario = @"
                        INSERT INTO Usuarios (Email, SenhaHash, Papel)
                        OUTPUT INSERTED.ID -- Retorna o ID gerado
                        VALUES (@Email, @SenhaHash, @Papel);";

                    using (SqlCommand comandoUsuario = new SqlCommand(queryInsertUsuario, conexao, transaction))
                    {
                        comandoUsuario.Parameters.AddWithValue("@Email", txtEmailCadastro.Text.Trim());
                        comandoUsuario.Parameters.AddWithValue("@SenhaHash", senhaHash);
                        comandoUsuario.Parameters.AddWithValue("@Papel", "Aluno"); // Inserir sempre como Aluno
                        novoUsuarioId = Convert.ToInt32(comandoUsuario.ExecuteScalar());
                    }

                    // Tenta inserir na tabela Alunos
                    string queryInsertAluno = @"
                        INSERT INTO Alunos (UsuarioID, Nome, RegistroAluno, CursoID)
                        VALUES (@UsuarioID, @Nome, @RegistroAluno, @CursoID);";

                    using (SqlCommand comandoAluno = new SqlCommand(queryInsertAluno, conexao, transaction))
                    {
 
                        comandoAluno.Parameters.AddWithValue("@UsuarioID", novoUsuarioId); // Usa o ID gerado
                        comandoAluno.Parameters.AddWithValue("@Nome", txtNomeCadastro.Text.Trim());
                        comandoAluno.Parameters.AddWithValue("@RegistroAluno", txtRaAlunoCadastro.Text.Trim());
                        comandoAluno.Parameters.AddWithValue("@CursoID", Convert.ToInt32(cmbCurso.SelectedValue));

                        comandoAluno.ExecuteNonQuery();
                    }

                    // Se chegou até aqui, confirma a transação
                    transaction.Commit();

                    MessageBox.Show("Cadastro de aluno realizado com sucesso!\nVocê já pode fazer login.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                }
                catch (Exception ex)
                {
                    // Se qualquer etapa falhar, desfaz a transação
                    try { transaction.Rollback(); } catch { /* Ignora erro no rollback */ }

                    // Verifica se o erro foi de duplicidade (pode acontecer numa race condition)
                    if (ex is SqlException sqlEx && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                    {
                        if (sqlEx.Message.ToLower().Contains("email")) { MessageBox.Show("Este Email já foi cadastrado (erro durante transação).", "Email Duplicado"); }
                        else if (sqlEx.Message.ToLower().Contains("registroaluno")) { MessageBox.Show("Este RA já foi cadastrado (erro durante transação).", "RA Duplicado"); }
                        else { MessageBox.Show($"Erro de duplicidade: {ex.Message}", "Erro de Banco de Dados"); }
                    }
                    else
                    {
                        MessageBox.Show($"Erro ao cadastrar aluno: {ex.Message}", "Erro Crítico");
                    }
                    // Não fecha a tela para o usuário corrigir os dados
                }
            }
        }
        
        private void lblVoltarLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}

