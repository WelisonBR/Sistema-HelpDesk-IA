using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;


namespace ProjetoHelpDesk
{
    public partial class DetalhesDoChamado : Form
    {
        private int _chamadoID;
        private string connectionString = ConfigurationManager.ConnectionStrings["MinhaConexao"]?.ConnectionString;
        private AIService _aiService;
        private int _originalFormHeight;
        private const int ExpandedFormHeight = 1025;
        private string _statusChamadoAtual = ""; // Guarda o status atual do chamado

        public DetalhesDoChamado()
        {
            InitializeComponent();
        }

        public DetalhesDoChamado(int chamadoID)
        {
            InitializeComponent();
            this._chamadoID = chamadoID;
        }

        private void DetalhesDoChamado_Load(object sender, EventArgs e)
        {
            // Inicializa a IA
            _aiService = new AIService();
            if (!_aiService.IsInitialized)
            {
                btnConsultarIA.Enabled = false;
                lblRespostaIA.Text = "Serviço de IA indisponível.";
                MessageBox.Show(_aiService.InitializationError ?? "Erro ao inicializar IA.", "Erro IA");
            }

            // Define altura inicial
            _originalFormHeight = this.Height;
            this.Height = _originalFormHeight;

            // Carrega dados
            CarregarInformacoesChamado();
            CarregarRespostas();

            this.CenterToScreen();
        }

        // Carregamento de Dados
        private void CarregarInformacoesChamado()
        {
            string query = @"
                SELECT 
                    ch.ID, ch.Titulo, ch.Descricao, ch.Status, ch.Prioridade,
                    A.Nome AS AlunoNome, A.RegistroAluno AS AlunoRA, u.Email AS AlunoEmail,
                    cat.NomeCategoria, cur.NomeCurso
                FROM Chamados ch
                INNER JOIN Usuarios u ON ch.UsuarioID = u.ID 
                INNER JOIN Alunos A ON u.ID = A.UsuarioID    
                LEFT JOIN Categorias cat ON ch.CategoriaID = cat.ID 
                LEFT JOIN Cursos cur ON A.CursoID = cur.ID       
                WHERE ch.ID = @ChamadoID AND u.Papel = 'Aluno';
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
                            // Preenche os Labels 
                            lblNomeAluno.Text = leitor["AlunoNome"] != DBNull.Value ? leitor["AlunoNome"].ToString() : "N/A";
                            lblRA.Text = leitor["AlunoRA"] != DBNull.Value ? leitor["AlunoRA"].ToString() : "N/A";
                            lblEmail.Text = leitor["AlunoEmail"] != DBNull.Value ? leitor["AlunoEmail"].ToString() : "N/A";
                            lblCurso.Text = leitor["NomeCurso"] != DBNull.Value ? leitor["NomeCurso"].ToString() : "N/A";
                            lblChamadoId.Text = leitor["ID"].ToString();
                            lblStatus.Text = leitor["Status"].ToString();
                            lblPrioridade.Text = leitor["Prioridade"].ToString();
                            lblCategoria.Text = leitor["NomeCategoria"] != DBNull.Value ? leitor["NomeCategoria"].ToString() : "N/A";
                            txtDescricao.Text = leitor["Descricao"] != DBNull.Value ? leitor["Descricao"].ToString() : "";
                            this.Text = $"Detalhes do Chamado #{leitor["ID"]} - {leitor["Titulo"]}";

                            // Guarda o status na variável de classe
                            _statusChamadoAtual = lblStatus.Text;

                            // Verifica se bloqueia a interação
                            VerificarStatusParaBloquearChat();
                        }
                        else
                        {
                            MessageBox.Show($"Chamado #{_chamadoID} não encontrado.", "Erro");
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar detalhes: {ex.Message}", "Erro de Banco de Dados");
                this.Close();
            }
        }

        private void CarregarRespostas()
        {
            // Limpa o histórico visual
            flpChatHistory.Controls.Clear();

            string query = @"
                SELECT 
                    r.Mensagem, r.DataEnvio, 
                    COALESCE(A.Nome, F.Nome) AS AutorNome
                FROM Respostas r
                INNER JOIN Usuarios u ON r.UsuarioID = u.ID
                LEFT JOIN Alunos A ON u.ID = A.UsuarioID AND u.Papel = 'Aluno'
                LEFT JOIN Funcionarios F ON u.ID = F.UsuarioID AND u.Papel IN ('Funcionario', 'Admin')
                WHERE r.ChamadoID = @ChamadoID
                ORDER BY r.DataEnvio ASC;";

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
                            // Lê os dados
                            string autor = leitor["AutorNome"] != DBNull.Value ? leitor["AutorNome"].ToString() : "Utilizador Sistema";
                            string data = ((DateTime)leitor["DataEnvio"]).ToString("dd/MM/yyyy HH:mm");
                            string msg = leitor["Mensagem"].ToString();

                            // Cria o balão de chat
                            AdicionarMensagemAoChat(autor, data, msg);
                        }
                    }
                }
                // Faz scroll para o fim
                if (flpChatHistory.Controls.Count > 0)
                {
                    flpChatHistory.ScrollControlIntoView(flpChatHistory.Controls[flpChatHistory.Controls.Count - 1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar histórico de respostas: " + ex.Message, "Erro");
            }
        }

        // Cria e adiciona o balão de mensagem visual ao chat.
        private void AdicionarMensagemAoChat(string autor, string data, string mensagem)
        {
            // Painel container cinza
            Panel panelMensagem = new Panel
            {
                BackColor = Color.FromArgb(226, 232, 240),
                Margin = new Padding(5, 3, 15, 10),
                Padding = new Padding(8),
                Width = flpChatHistory.ClientSize.Width - 25,
            };

            // Painel para o cabeçalho (Autor+Data)
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 20,
                Margin = new Padding(0, 0, 0, 3),
                BackColor = Color.Transparent
            };

            // Label do Autor (Esquerda)
            Label lblAutor = new Label
            {
                Text = autor,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81),
                Dock = DockStyle.Left,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Label da Data (Direita)
            Label lblData = new Label
            {
                Text = $"({data})",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                Dock = DockStyle.Right,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight
            };

            // Adiciona Autor e Data ao pnlHeader
            pnlHeader.Controls.Add(lblAutor); pnlHeader.Controls.Add(lblData);

            // Label da Mensagem (Corpo)
            Label lblMensagem = new Label
            {
                Text = mensagem,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(17, 24, 39),
                AutoSize = false,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            lblMensagem.MaximumSize = new Size(panelMensagem.Width - panelMensagem.Padding.Horizontal, 0);
            lblMensagem.AutoSize = true; // Cresce verticalmente

            // Adiciona controlos ao painel principal
            panelMensagem.Controls.Add(lblMensagem);
            panelMensagem.Controls.Add(pnlHeader);

            // Ajusta a altura do painel principal (CORRIGIDO)
            int totalHeight = panelMensagem.Padding.Top + pnlHeader.Height + pnlHeader.Margin.Bottom + lblMensagem.Height + panelMensagem.Padding.Bottom;
            panelMensagem.Height = totalHeight;

            // Adiciona o balão ao FlowLayoutPanel
            flpChatHistory.Controls.Add(panelMensagem);
            flpChatHistory.ScrollControlIntoView(panelMensagem); // Scroll ao adicionar
        }

        // Lógica de Interação (Chat e IA)
        private void btnEnviarResposta_Click(object sender, EventArgs e)
        {
            string novaMensagem = txtNovaResposta.Text.Trim();
            if (string.IsNullOrEmpty(novaMensagem) || novaMensagem == "Digite sua resposta...") return;

            // Define o novo status (Aluno respondendo = Em andamento)
            string novoStatus = "Em andamento";
            string queryInsert = "INSERT INTO Respostas (ChamadoID, UsuarioID, Mensagem, DataEnvio) VALUES (@ChamadoID, @UsuarioID, @Mensagem, GETDATE());";
            // Atualiza status se estava 'Aguardando resposta'
            string queryUpdateStatus = "UPDATE Chamados SET Status = @NovoStatus, DataAtualizacao = GETDATE() WHERE ID = @ChamadoID AND Status = 'Aguardando resposta';";

            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();
                    using (SqlCommand cmdInsert = new SqlCommand(queryInsert, conexao))
                    {
                        cmdInsert.Parameters.AddWithValue("@ChamadoID", this._chamadoID);
                        cmdInsert.Parameters.AddWithValue("@UsuarioID", Login.UsuarioLogadoId);
                        cmdInsert.Parameters.AddWithValue("@Mensagem", novaMensagem);
                        cmdInsert.ExecuteNonQuery();
                    }
                    // Atualiza o Status do Chamado
                    using (SqlCommand cmdUpdate = new SqlCommand(queryUpdateStatus, conexao))
                    {
                        cmdUpdate.Parameters.AddWithValue("@ChamadoID", this._chamadoID);
                        cmdUpdate.Parameters.AddWithValue("@NovoStatus", novoStatus);
                        cmdUpdate.ExecuteNonQuery();
                    }
                }

                // Atualiza a UI
                txtNovaResposta.Clear();
                txtNovaResposta_Leave(sender, e); // Repõe placeholder

                CarregarRespostas(); // Recarrega o chat
                CarregarInformacoesChamado(); // Recarrega detalhes (para atualizar status)
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao enviar resposta: " + ex.Message, "Erro");
            }
        }

        private async void btnConsultarIA_Click(object sender, EventArgs e)
        {
            // Verifica se está expandido
            bool isExpanded = this.Height > _originalFormHeight;
            if (isExpanded)
            {
                // Retrai
                this.Height = _originalFormHeight;
                this.CenterToScreen(); // Centraliza
                pnlSugestaoIA.Visible = false;
                pnlFeedbackIA.Visible = false;
                lblRespostaIA.Text = "";
                return;
            }

            // Se estava retraído, consulta a IA
            if (!_aiService.IsInitialized) { return; }
            string descricaoProblema = txtDescricao.Text;
            if (string.IsNullOrWhiteSpace(descricaoProblema)) { return; }

            // Feedback visual
            btnConsultarIA.Text = "Consultando...";
            btnConsultarIA.Enabled = false;
            lblRespostaIA.Text = "Aguarde, consultando a IA...";
            pnlSugestaoIA.Visible = true; // Mostra painel com "Aguarde"
            pnlFeedbackIA.Visible = false;

            // Expande a altura (antes de chamar a IA, para ver o "Aguarde")
            this.Height = ExpandedFormHeight;
            this.CenterToScreen();

            try
            {
                string systemPrompt = "Você é um assistente de suporte técnico conciso...";
                string respostaIA = await _aiService.ObterSugestaoAsync(descricaoProblema, systemPrompt);

                lblRespostaIA.Text = respostaIA; // Mostra a resposta
                pnlFeedbackIA.Visible = true;    // Mostra feedback
            }
            catch (Exception ex)
            {
                lblRespostaIA.Text = $"ERRO ao consultar a IA: {ex.Message}";
                pnlFeedbackIA.Visible = false; // Não mostra feedback se deu erro
            }
            finally
            {
                btnConsultarIA.Text = "Consultar IA";
                btnConsultarIA.Enabled = true;
            }
        }

        // Eventos de Feedback da IA
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

                // 2. Desativa interações
                DesativarInteracao();

                // 3. Retrai a tela
                this.Height = _originalFormHeight;
                this.CenterToScreen();

                // 4. Atualiza o status local
                lblStatus.Text = "Resolvido";
                _statusChamadoAtual = "Resolvido";

                MessageBox.Show("O chamado foi marcado como resolvido. As opções de interação foram desativadas.", "Feedback Recebido");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar o status: {ex.Message}", "Erro de BD");
            }
        }

        private void btnBuscarAjuda_Click(object sender, EventArgs e) // Botão "Não, preciso de ajuda"
        {
            // Retrai a tela e centraliza
            this.Height = _originalFormHeight;
            this.CenterToScreen();

            pnlSugestaoIA.Visible = false;
            pnlFeedbackIA.Visible = false;

            MessageBox.Show("Entendido. Por favor, utilize o chat principal para detalhar sua dúvida.", "Continuar Atendimento");
        }

        private void VerificarStatusParaBloquearChat()
        {
            // Lê o status da variável de classe
            if (_statusChamadoAtual.Equals("Resolvido", StringComparison.OrdinalIgnoreCase) ||
                _statusChamadoAtual.Equals("Fechado", StringComparison.OrdinalIgnoreCase))
            {
                DesativarInteracao();
            }
        }

        private void DesativarInteracao()
        {
            // Desativa o chat principal
            txtNovaResposta.Enabled = false;
            btnEnviarResposta.Enabled = false;
            txtNovaResposta.Text = "Este chamado foi concluído.";
            txtNovaResposta.ForeColor = Color.Gray;

            // Desativa o botão de consultar IA
            btnConsultarIA.Enabled = false;

            // Garante que painéis da IA estão escondidos
            pnlSugestaoIA.Visible = false;
            pnlFeedbackIA.Visible = false;
        }

        // Eventos de Navegação e Placeholder
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNovaResposta_Enter(object sender, EventArgs e)
        {
            if (txtNovaResposta.Text == "Digite sua resposta...")
            {
                txtNovaResposta.Text = "";
                txtNovaResposta.ForeColor = Color.Black;
            }
        }

        private void txtNovaResposta_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNovaResposta.Text))
            {
                txtNovaResposta.Text = "Digite sua resposta...";
                txtNovaResposta.ForeColor = Color.Gray;
            }
        }
    }
}
