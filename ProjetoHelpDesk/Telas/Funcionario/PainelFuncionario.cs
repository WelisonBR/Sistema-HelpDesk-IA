using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProjetoHelpDesk
{
    public partial class PainelFuncionario : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MinhaConexao"]?.ConnectionString;
        private int _idChamadoSelecionado = 0;
        private string _statusChamadoAtual = "";

        public PainelFuncionario()
        {
            InitializeComponent();
        }

        private void PainelFuncionario_Load(object sender, EventArgs e)
        {
            if (cmbFiltrarChamado.Items.Count > 0)
            {
                cmbFiltrarChamado.SelectedIndex = 0;
            }

            ExibirPlaceholders(true);

            CarregarTodosChamados("Todos");
        }

        private void CarregarTodosChamados(string statusChamado = "Todos")
        {
            flpListaChamados.Controls.Clear();

            // Busca chamados dos alunos
            string queryBase = @"
                SELECT 
                    CH.ID AS IDChamado, CH.Titulo, CH.Status, CH.Prioridade, 
                    CONVERT(varchar, CH.DataCriacao, 103) AS Data,
                    A.Nome AS AlunoNome,
                    C.NomeCurso
                FROM Chamados CH
                JOIN Usuarios U ON CH.UsuarioID = U.ID
                JOIN Alunos A ON U.ID = A.UsuarioID
                JOIN Cursos C ON A.CursoID = C.ID
                WHERE U.Papel = 'Aluno' ";

            // Adiciona filtro de status
            string queryFiltro = "";
            if (statusChamado != "Todos")
            {
                queryFiltro = " AND CH.Status = @StatusFiltro ";
            }

            string queryOrderBy = " ORDER BY CH.DataAtualizacao DESC";
            string query = queryBase + queryFiltro + queryOrderBy;

            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    // Adiciona o parâmetro @StatusFiltro APENAS se necessário
                    if (statusChamado != "Todos")
                    {
                        comando.Parameters.AddWithValue("@StatusFiltro", statusChamado);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        lblContagemChamados.Text = $"Chamados ({dt.Rows.Count})";

                        foreach (DataRow row in dt.Rows)
                        {
                            ChamadoCardFuncionario card = new ChamadoCardFuncionario();
                            card.PreencherDados(
                                Convert.ToInt32(row["IDChamado"]),
                                row["Titulo"].ToString(),
                                row["AlunoNome"].ToString(),
                                row["NomeCurso"].ToString(),
                                row["Status"].ToString(),
                                row["Prioridade"].ToString(),
                                row["Data"].ToString()
                            );
                            card.CardClicado += Card_ChamadoSelecionado;
                            flpListaChamados.Controls.Add(card);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar chamados: {ex.Message}", "Erro BD");
            }
        }

        private void CarregarDetalhesChamado(int chamadoID)
        {
            string query = @"
                SELECT 
                    ch.ID, ch.Titulo, ch.Descricao, ch.Status, ch.Prioridade,
                    A.Nome AS AlunoNome,          
                    A.RegistroAluno AS AlunoRA,   
                    u.Email AS AlunoEmail,        
                    cat.NomeCategoria,            
                    cur.NomeCurso                 
                FROM Chamados ch
                INNER JOIN Usuarios u ON ch.UsuarioID = u.ID 
                INNER JOIN Alunos A ON u.ID = A.UsuarioID    
                LEFT JOIN Categorias cat ON ch.CategoriaID = cat.ID 
                LEFT JOIN Cursos cur ON A.CursoID = cur.ID       
                WHERE ch.ID = @ChamadoID 
                  AND u.Papel = 'Aluno'; 
            ";

            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    using (SqlCommand comando = new SqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@ChamadoID", chamadoID);
                        conexao.Open();

                        using (SqlDataReader leitor = comando.ExecuteReader())
                        {
                            if (leitor.Read())
                            {
                                // Preenche os Labels e TextBoxes
                                lblTituloDetalhes.Text = $"Detalhes do Chamado #{leitor["ID"]}";
                                lblNomeAluno.Text = leitor["AlunoNome"] != DBNull.Value ? leitor["AlunoNome"].ToString() : "N/A";
                                lblRegistroAluno.Text = leitor["AlunoRA"] != DBNull.Value ? leitor["AlunoRA"].ToString() : "N/A";
                                lblEmailAluno.Text = leitor["AlunoEmail"] != DBNull.Value ? leitor["AlunoEmail"].ToString() : "N/A";
                                lblCursoAluno.Text = leitor["NomeCurso"] != DBNull.Value ? leitor["NomeCurso"].ToString() : "N/A";
                                lblCategoriaChamado.Text = leitor["NomeCategoria"] != DBNull.Value ? leitor["NomeCategoria"].ToString() : "N/A";
                                txtDescricaoChamado.Text = leitor["Descricao"] != DBNull.Value ? leitor["Descricao"].ToString() : "";

                                // Guarda o status na variável de classe
                                _statusChamadoAtual = leitor["Status"] != DBNull.Value ? leitor["Status"].ToString() : "N/A";

                                // Verifica o status para bloquear/desbloquear
                                VerificarStatusParaBloquearFuncionario();
                            }
                            else 
                            {
                                ExibirPlaceholders(true);
                                lblInicial.Text = $"Erro: Chamado #{chamadoID} não encontrado.";
                            }
                        } 
                    } 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERRO DENTRO DO CarregarDetalhesDoChamado: {ex.Message}", "Debug Detalhes - ERRO CRÍTICO");
                ExibirPlaceholders(true);
                lblInicial.Text = "Erro ao carregar dados.";
            }
        } 

        private void CarregarConversaChamado(int chamadoID)
        {
            flpConversaHistorico.Controls.Clear();

            string query = @"
                SELECT 
                    r.Mensagem, r.DataEnvio, 
                    COALESCE(A.Nome, F.Nome) AS AutorNome 
                FROM Respostas r
                JOIN Usuarios u ON r.UsuarioID = u.ID
                LEFT JOIN Alunos A ON u.ID = A.UsuarioID AND u.Papel = 'Aluno'
                LEFT JOIN Funcionarios F ON u.ID = F.UsuarioID AND u.Papel IN ('Funcionario', 'Admin')
                WHERE r.ChamadoID = @ChamadoID
                ORDER BY r.DataEnvio ASC";

            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@ChamadoID", chamadoID);
                    conexao.Open();
                    using (SqlDataReader leitor = comando.ExecuteReader())
                    {
                        while (leitor.Read())
                        {
                            string autor = leitor["AutorNome"] != DBNull.Value ? leitor["AutorNome"].ToString() : "Utilizador Sistema";
                            string data = ((DateTime)leitor["DataEnvio"]).ToString("dd/MM/yyyy HH:mm");
                            string msg = leitor["Mensagem"].ToString();

                            AdicionarMensagemAoChat(autor, data, msg);
                        }
                    }
                }

                // Scroll para o fim
                if (flpConversaHistorico.Controls.Count > 0)
                {
                    flpConversaHistorico.PerformLayout();
                    flpConversaHistorico.ScrollControlIntoView(flpConversaHistorico.Controls[flpConversaHistorico.Controls.Count - 1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar conversa: {ex.Message}", "Erro BD");
            }
        }

        private void AdicionarMensagemAoChat(string autor, string data, string mensagem)
        {
            Panel panelMensagem = new Panel
            {
                BackColor = Color.FromArgb(226, 232, 240),
                Margin = new Padding(5, 3, 15, 10), 
                Padding = new Padding(8),      
                Width = flpConversaHistorico.ClientSize.Width - 25,
            };

            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 20, 
                Margin = new Padding(0, 0, 0, 3), 
                BackColor = Color.Transparent
            };

            Label lblAutor = new Label
            {
                Text = autor,
                Font = new Font("Arial", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81),
                Dock = DockStyle.Left,
                AutoSize = true, 
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label lblData = new Label
            {
                Text = $"({data})",
                Font = new Font("Arial", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                Dock = DockStyle.Right,
                AutoSize = true, 
                TextAlign = ContentAlignment.MiddleRight
            };

            pnlHeader.Controls.Add(lblAutor);
            pnlHeader.Controls.Add(lblData);

            // Label da Mensagem (Corpo)
            Label lblMensagem = new Label
            {
                Text = mensagem,
                Font = new Font("Arial", 10F),
                ForeColor = Color.FromArgb(17, 24, 39),
                AutoSize = false,
                Dock = DockStyle.Fill, 
                BackColor = Color.Transparent
            };

            lblMensagem.MaximumSize = new Size(panelMensagem.Width - panelMensagem.Padding.Horizontal, 0);
            lblMensagem.AutoSize = true;

            panelMensagem.Controls.Add(lblMensagem); 
            panelMensagem.Controls.Add(pnlHeader); 

            int totalHeight = panelMensagem.Padding.Top + pnlHeader.Height + pnlHeader.Margin.Bottom + lblMensagem.Height + panelMensagem.Padding.Bottom;
            panelMensagem.Height = totalHeight;

            flpConversaHistorico.Controls.Add(panelMensagem);

        }

        // Métodos para bloquear/desbloquear a interação do funcionário
        private void VerificarStatusParaBloquearFuncionario()
        {
            string statusAtual = _statusChamadoAtual;

            if (statusAtual.Equals("Resolvido", StringComparison.OrdinalIgnoreCase) ||
                statusAtual.Equals("Fechado", StringComparison.OrdinalIgnoreCase))
            {
                DesativarInteracaoFuncionario();
            }
            else
            {
                AtivarInteracaoFuncionario();
            }
        }

        // Desativa os controles de interação
        private void DesativarInteracaoFuncionario()
        {
            txtNovaMensagemFunc.Enabled = false;
            btnEnviarMensagemFunc.Enabled = false;
            txtNovaMensagemFunc.Text = "Este chamado foi concluído.";
            txtNovaMensagemFunc.ForeColor = Color.Gray;

            if (btnResolvido != null)
            {
                btnResolvido.Enabled = false;
                btnResolvido.Text = "Chamado Resolvido";
            }
        }

        // Ativa os controles de interação
        private void AtivarInteracaoFuncionario()
        {
            txtNovaMensagemFunc.Enabled = true;
            btnEnviarMensagemFunc.Enabled = true;
            txtNovaMensagemFunc.Text = ""; // Ou o placeholder
            txtNovaMensagemFunc.ForeColor = Color.Black;

            if (btnResolvido != null)
            {
                btnResolvido.Enabled = true;
                btnResolvido.Text = "Marcar como Resolvido";
            }
        }

        private void btnResolvido_Click(object sender, EventArgs e)
        {
            if (_idChamadoSelecionado == 0) return;

            var confirmacao = MessageBox.Show(
                $"Tem a certeza que deseja marcar o chamado #{_idChamadoSelecionado} como 'Resolvido'?",
                "Confirmar Resolução", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacao == DialogResult.No) return;

            try
            {
                string queryUpdate = "UPDATE Chamados SET Status = 'Resolvido', DataAtualizacao = GETDATE() WHERE ID = @ChamadoID";
                using (SqlConnection conexao = new SqlConnection(connectionString))
                using (SqlCommand comando = new SqlCommand(queryUpdate, conexao))
                {
                    comando.Parameters.AddWithValue("@ChamadoID", _idChamadoSelecionado);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                }

                _statusChamadoAtual = "Resolvido";
                DesativarInteracaoFuncionario(); 
                CarregarTodosChamados(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao marcar chamado como resolvido: {ex.Message}", "Erro de BD");
            }
        }

        private void btnEnviarMensagemFunc_Click(object sender, EventArgs e)
        {
            string novaMensagem = txtNovaMensagemFunc.Text.Trim();
            if (string.IsNullOrEmpty(novaMensagem) || _idChamadoSelecionado == 0) return;

            string novoStatus = "Aguardando resposta"; // Status padrão ao responder

            string queryInsert = "INSERT INTO Respostas (ChamadoID, UsuarioID, Mensagem, DataEnvio) VALUES (@ChamadoID, @UsuarioID, @Mensagem, GETDATE());";
            string queryUpdateStatus = "UPDATE Chamados SET Status = @NovoStatus, DataAtualizacao = GETDATE() WHERE ID = @ChamadoID;";

            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    // Insere a Resposta
                    using (SqlCommand cmdInsert = new SqlCommand(queryInsert, conexao))
                    {
                        cmdInsert.Parameters.AddWithValue("@ChamadoID", _idChamadoSelecionado);
                        cmdInsert.Parameters.AddWithValue("@UsuarioID", Login.UsuarioLogadoId); 
                        cmdInsert.Parameters.AddWithValue("@Mensagem", novaMensagem);
                        cmdInsert.ExecuteNonQuery();
                    }
                    // Atualiza o Status do Chamado
                    using (SqlCommand cmdUpdate = new SqlCommand(queryUpdateStatus, conexao))
                    {
                        cmdUpdate.Parameters.AddWithValue("@ChamadoID", _idChamadoSelecionado);
                        cmdUpdate.Parameters.AddWithValue("@NovoStatus", novoStatus);
                        cmdUpdate.ExecuteNonQuery();
                    }
                }

                txtNovaMensagemFunc.Clear();

                CarregarConversaChamado(_idChamadoSelecionado);
                CarregarDetalhesChamado(_idChamadoSelecionado);
                CarregarTodosChamados();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao enviar resposta: {ex.Message}", "Erro BD");
            }
        }

        private void ExibirPlaceholders(bool mostrarPlaceholders)
        {
            try
            {
                // Painel principal (nunca esconda o pai)
                pnlPlaceholder.Visible = true;

                // Se mostrarPlaceholders = true → mostra só o texto inicial e esconde os detalhes
                // Se mostrarPlaceholders = false → mostra os detalhes e esconde o texto inicial
                lblInicial.Visible = mostrarPlaceholders;
                pnlColunaDetalhes.Visible = !mostrarPlaceholders;

                pnlColunaConversa.Visible = !mostrarPlaceholders;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"ERRO no ExibirPlaceholders: {ex.Message}\n\nVerifique os nomes: pnlPlaceholder, pnlColunaDetalhes, pnlColunaConversa.",
                    "Erro Crítico de UI"
                );
            }
        }

        private void Card_ChamadoSelecionado(object sender, EventArgs e)
        {
            MessageBox.Show("Clique no card detectado!", "Debug Teste");

            if (sender is ChamadoCardFuncionario cardClicado)
            {
                _idChamadoSelecionado = cardClicado.ChamadoID;

                ExibirPlaceholders(false);

                CarregarDetalhesChamado(_idChamadoSelecionado);
                CarregarConversaChamado(_idChamadoSelecionado);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogResult resposta = MessageBox.Show("Deseja realmente sair e voltar para a tela de login?",
                "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resposta == DialogResult.Yes)
            {
                // Limpa as informações do usuário logado
                Login.UsuarioLogadoId = 0;
                Login.UsuarioLogadoNome = null;
                Login.UsuarioLogadoPapel = null;

                Login loginForm = Application.OpenForms.OfType<Login>().FirstOrDefault();

                if (loginForm == null) 
                {
                    loginForm = new Login();
                    loginForm.Show(); 
                }
                else
                {
                    loginForm.Show();
                    loginForm.BringToFront();
                }

                this.Close();
            }
        }

        private void cmbFiltrarChamado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFiltrarChamado.SelectedItem != null)
            {
                string status = cmbFiltrarChamado.SelectedItem.ToString();
                // Chama o método modificado com o status selecionado
                CarregarTodosChamados(status);

                ExibirPlaceholders(true);
                _idChamadoSelecionado = 0;
            }

        }
    }
}
