using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ProjetoHelpDesk
{
    public partial class DetalhesDoChamado : Form
    {
        private int _chamadoID;
        private string connectionString = ConfigurationManager.ConnectionStrings["MinhaConexao"]?.ConnectionString;
        private AIService _aiService;
        private int _originalFormHeight;
        private const int ExpandedFormHeight = 1025;

        public DetalhesDoChamado()
        {
            InitializeComponent();
        }

        // --- ADICIONE ESTE NOVO CONSTRUTOR ---
        // Este é o construtor que aceita o ID
        public DetalhesDoChamado(int chamadoID)
        {
            InitializeComponent(); // Sempre chame isto primeiro

            // Armazena o ID que foi passado pelo PainelAluno
            this._chamadoID = chamadoID;
        }

        // --- MÉTODO 2: O Evento Load ---
        private void DetalhesDoChamado_Load(object sender, EventArgs e)
        {
            _aiService = new AIService();

            _originalFormHeight = this.Height;
            this.Height = _originalFormHeight;

            if (!_aiService.IsInitialized)
            {
                btnConsultarIA.Enabled = false;
                lblRespostaIA.Text = "Serviço de IA indisponível (verifique a chave de API).";
                MessageBox.Show(_aiService.InitializationError ?? "Erro ao inicializar IA.", "Erro IA");
            }

            CarregarInformacoesChamado();
            CarregarRespostas();
            VerificarStatusParaBloquearChat();
        }

        // --- MÉTODO 3: Carrega o lado ESQUERDO (Info do Chamado) ---
        private void CarregarInformacoesChamado()
        {
            // Query complexa que busca dados do Chamado, do Aluno, do Curso e da Categoria
            string query = @"
                SELECT 
                    ch.ID, ch.Titulo, ch.Descricao, ch.Status, ch.Prioridade,
                    u.Nome AS AlunoNome, u.Email AS AlunoEmail, u.RegistroAluno AS AlunoRA,
                    cat.NomeCategoria,
                    cur.NomeCurso
                FROM Chamados ch
                JOIN Usuarios u ON ch.UsuarioID = u.ID
                LEFT JOIN Categorias cat ON ch.CategoriaID = cat.ID
                LEFT JOIN Cursos cur ON u.CursoID = cur.ID
                WHERE ch.ID = @ChamadoID;
            ";

            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@ChamadoID", this._chamadoID);
                    conexao.Open();

                    using (SqlDataReader leitor = comando.ExecuteReader())
                    {
                        if (leitor.Read())
                        {
                            // Preenche os Labels da esquerda
                            lblNomeAluno.Text = leitor["AlunoNome"].ToString();
                            lblRA.Text = leitor["AlunoRA"].ToString();
                            lblEmail.Text = leitor["AlunoEmail"].ToString();
                            lblCurso.Text = leitor["NomeCurso"] != DBNull.Value ? leitor["NomeCurso"].ToString() : "N/A";

                            lblChamadoId.Text = leitor["ID"].ToString();
                            lblStatus.Text = leitor["Status"].ToString();
                            lblPrioridade.Text = leitor["Prioridade"].ToString();
                            lblCategoria.Text = leitor["NomeCategoria"] != DBNull.Value ? leitor["NomeCategoria"].ToString() : "N/A";

                            txtDescricao.Text = leitor["Descricao"].ToString();

                            // (Opcional) Título do formulário
                            this.Text = $"Detalhes do Chamado #{leitor["ID"]} - {leitor["Titulo"]}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar detalhes do chamado: " + ex.Message, "Erro");
                this.Close();
            }
        }

        // --- MÉTODO 4: Carrega o lado DIREITO (Chat) ---
        private void CarregarRespostas()
        {
            // Limpa o chat antigo antes de carregar o novo
            flpChatHistory.Controls.Clear();

            string query = @"
                SELECT 
                    r.Mensagem, 
                    r.DataEnvio, 
                    u.Nome AS AutorNome
                FROM Respostas r
                JOIN Usuarios u ON r.UsuarioID = u.ID
                WHERE r.ChamadoID = @ChamadoID
                ORDER BY r.DataEnvio ASC; -- Mais antigas primeiro
            ";

            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@ChamadoID", this._chamadoID);
                    conexao.Open();

                    using (SqlDataReader leitor = comando.ExecuteReader())
                    {
                        while (leitor.Read())
                        {
                            string autor = leitor["AutorNome"].ToString();
                            string data = ((DateTime)leitor["DataEnvio"]).ToString("dd/MM/yyyy HH:mm");
                            string msg = leitor["Mensagem"].ToString();

                            // Cria a mensagem de chat dinamicamente
                            AdicionarMensagemAoChat(autor, data, msg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar histórico de respostas: " + ex.Message, "Erro");
            }
        }

        // --- MÉTODO 5: Helper para adicionar mensagens ao chat ---
        private void AdicionarMensagemAoChat(string autor, string data, string mensagem)
        {
            // 1. Cria o 'Cabeçalho' (Autor e Data)
            Label lblHeader = new Label();
            lblHeader.Text = $"{autor}   ({data})";
            lblHeader.Font = new Font(this.Font.FontFamily, 9, FontStyle.Bold);
            lblHeader.ForeColor = Color.DarkSlateGray;
            lblHeader.AutoSize = true;
            lblHeader.Margin = new Padding(17, 5, 5, 0); // Espaçamento

            // 2. Cria a 'Mensagem'
            Label lblMensagem = new Label();
            lblMensagem.Text = mensagem;
            lblMensagem.Font = new Font(this.Font.FontFamily, 10);
            lblMensagem.AutoSize = true;
            lblMensagem.Margin = new Padding(17, 0, 5, 10); // Espaçamento (10 em baixo)
            lblMensagem.MaximumSize = new Size(flpChatHistory.Width - 30, 0); // Quebra de linha

            // 3. Adiciona ao FlowLayoutPanel
            flpChatHistory.Controls.Add(lblHeader);
            flpChatHistory.Controls.Add(lblMensagem);
        }

        // --- MÉTODO 6: Botão de Enviar Resposta ---
        private void btnEnviarResposta_Click(object sender, EventArgs e)
        {
            string novaMensagem = txtNovaResposta.Text.Trim();

            if (string.IsNullOrEmpty(novaMensagem))
            {
                return; // Não envia mensagem vazia
            }

            string queryInsert = @"
                INSERT INTO Respostas (ChamadoID, UsuarioID, Mensagem, DataEnvio)
                VALUES (@ChamadoID, @UsuarioID, @Mensagem, GETDATE());
            ";

            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                using (SqlCommand comando = new SqlCommand(queryInsert, conexao))
                {
                    comando.Parameters.AddWithValue("@ChamadoID", this._chamadoID);
                    comando.Parameters.AddWithValue("@UsuarioID", Login.UsuarioLogadoId);
                    comando.Parameters.AddWithValue("@Mensagem", novaMensagem);

                    conexao.Open();
                    comando.ExecuteNonQuery();


                    txtNovaResposta.Clear();
                    CarregarRespostas();
                    CarregarInformacoesChamado();
                    txtNovaResposta_Leave(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao enviar resposta: " + ex.Message, "Erro");
            }
        }

        private async void btnConsultarIA_Click(object sender, EventArgs e)
        {
            bool isExpanded = this.Height > _originalFormHeight;

            if (isExpanded)
            {
                // Se expandido, retrai instantaneamente
                this.Height = _originalFormHeight;
                // Esconde os painéis explicitamente (embora mudar a altura os esconda)
                pnlSugestaoIA.Visible = false;
                pnlFeedbackIA.Visible = false;
                lblRespostaIA.Text = ""; // Limpa a resposta
                return;
            }

            // Se os painéis já estão visíveis, apenas os esconde e sai.
            if (pnlSugestaoIA.Visible || pnlFeedbackIA.Visible)
            {
                pnlSugestaoIA.Visible = false;
                pnlFeedbackIA.Visible = false;
                lblRespostaIA.Text = ""; // Limpa a resposta ao esconder
                return;
            }

            if (!_aiService.IsInitialized) { /* Mensagem erro */ return; }
            string descricaoProblema = txtDescricao.Text;
            if (string.IsNullOrWhiteSpace(descricaoProblema)) { /* Mensagem erro */ return; }

            btnConsultarIA.Text = "Consultando...";
            btnConsultarIA.Enabled = false;
            lblRespostaIA.Text = "Aguarde, consultando a IA...";
            pnlSugestaoIA.Visible = true; // Mostra painel com "Aguarde"
            pnlFeedbackIA.Visible = false;

            try
            {
                string systemPrompt = "Você é um assistente de suporte técnico conciso...";
                string respostaIA = await _aiService.ObterSugestaoAsync(descricaoProblema, systemPrompt);
                
                lblRespostaIA.Text = respostaIA;
                pnlSugestaoIA.Visible = true;
                pnlFeedbackIA.Visible = true;
            }
            catch (Exception ex)
            {
                lblRespostaIA.Text = $"ERRO ao consultar a IA: {ex.Message}";
                // Mostra o painel de sugestão (mas não o de feedback) para exibir o erro
                pnlSugestaoIA.Visible = true;
                pnlFeedbackIA.Visible = false; // Garante que feedback não aparece com erro
            }
            finally
            {
                btnConsultarIA.Text = "Consultar IA";
                btnConsultarIA.Enabled = true;
            }
        }

        private void btnExcluirChamado_Click(object sender, EventArgs e)
        {
            // Lógica de exclusão (Cuidado!)
            MessageBox.Show("Funcionalidade 'Excluir Chamado' ainda não implementada.");
        }

        private void txtNovaResposta_Enter(object sender, EventArgs e)
        {
            if (txtNovaResposta.Text == "Digite sua resposta...")
            {
                txtNovaResposta.Text = "";
                txtNovaResposta.ForeColor = Color.Black; // Muda a cor para texto normal
            }
        }

        private void txtNovaResposta_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNovaResposta.Text))
            {
                txtNovaResposta.Text = "Digite sua resposta...";
                txtNovaResposta.ForeColor = Color.Gray; // Volta a cor para cinza
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VerificarStatusParaBloquearChat()
        {
            // Lê o status do Label que já foi preenchido pelo CarregarInformacoesChamado
            string statusAtual = lblStatus.Text;

            if (statusAtual.Equals("Resolvido", StringComparison.OrdinalIgnoreCase) ||
                statusAtual.Equals("Fechado", StringComparison.OrdinalIgnoreCase))
            {
                BloquearChat();
            }
            else
            {
                // Garante que está desbloqueado se o status for outro
                // DesbloquearChat(); // Só descomente se precisar de lógica de reabertura
            }
        }

        private void BloquearChat()
        {
            txtNovaResposta.Enabled = false;
            btnEnviarResposta.Enabled = false;
            txtNovaResposta.Text = "Este chamado foi resolvido.";
            txtNovaResposta.ForeColor = Color.Gray;
        }

        private void DesbloquearChat()
        {
            txtNovaResposta.Enabled = true;
            btnEnviarResposta.Enabled = true;
            txtNovaResposta.Text = ""; // Ou o placeholder
            txtNovaResposta.ForeColor = Color.Black;
        }

        private void btnBuscarAjuda_Click(object sender, EventArgs e)
        {
            pnlSugestaoIA.Visible = false;
            pnlFeedbackIA.Visible = false;

            MessageBox.Show("Entendido. Por favor, utilize o chat principal à direita para detalhar melhor sua dúvida ou aguardar a resposta de um atendente.",
                    "Continuar Atendimento", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSimResolvido_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Atualiza Status para Resolvido
                string queryUpdate = "UPDATE Chamados SET Status = 'Resolvido', DataAtualizacao = GETDATE() WHERE ID = @ChamadoID";
                using (SqlConnection conexao = new SqlConnection(connectionString))
                using (SqlCommand comando = new SqlCommand(queryUpdate, conexao))
                {
                    comando.Parameters.AddWithValue("@ChamadoID", this._chamadoID);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                }

                btnConsultarIA.Enabled = false; 
                BloquearChat();

                pnlSugestaoIA.Visible = false;
                pnlFeedbackIA.Visible = false;

                MessageBox.Show("O chamado foi marcado como resolvido.", "Feedback Recebido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar o status do chamado: {ex.Message}", "Erro de BD");
            }
        }
    }
}
