using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProjetoHelpDesk
{
    public partial class PainelAluno : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MinhaConexao"]?.ConnectionString;

        private bool painelConfigVisivel = false;
        private int alturaAlvoPainelConfig = 103;
        private int passoAnimacao = 15;
        private int topInicialPainelConfig;

        private int topFinalPainelConfig;

        public PainelAluno()
        {
            InitializeComponent();
            InicializarPosicoesAnimacao();
        }

        private void PainelAluno_Load(object sender, EventArgs e)
        {
            // Verifica se tem um usuário logado
            if (Login.UsuarioLogadoId <= 0 || string.IsNullOrEmpty(Login.UsuarioLogadoNome))
            {
                MessageBox.Show("Erro: Não foi possível identificar o usuário logado. Faça login novamente.", "Erro de Autenticação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // Atualiza o Label com o nome do aluno
            lblNomePainel.Text = Login.UsuarioLogadoNome;

            string nomeCurso = BuscarNomeCurso(Login.UsuarioLogadoId);

            lblNomeCursoPainel.Text = nomeCurso ?? "Curso não informado";

            EstilizarGrid();
            CarregarChamados();
        }

        private void btnConfiguracoes_Click(object sender, EventArgs e)
        {
            timerAnimacaoConfig.Enabled = true;
        }

        private void InicializarPosicoesAnimacao()
        {
            // Calcula a posição inicial (logo abaixo do botão)
            topInicialPainelConfig = btnConfiguracoes.Top;
            // Calcula a posição final (altura desejada ACIMA da posição inicial)
            topFinalPainelConfig = btnConfiguracoes.Top - alturaAlvoPainelConfig;

            // Garante que o painel comece na posição e altura corretas
            pnlBotoesConfig.Top = topInicialPainelConfig;
            pnlBotoesConfig.Height = 0;
        }

        private void timerAnimacaoConfig_Tick(object sender, EventArgs e)
        {
            if (painelConfigVisivel) 
            {
                if (pnlBotoesConfig.Height > passoAnimacao)
                {
                    // Diminui a altura E move para baixo
                    pnlBotoesConfig.Height -= passoAnimacao;
                    pnlBotoesConfig.Top += passoAnimacao;
                }
                else // Chegou ou passou do mínimo (altura 0)
                {
                    pnlBotoesConfig.Height = 0;
                    pnlBotoesConfig.Top = topInicialPainelConfig;
                    painelConfigVisivel = false;
                    timerAnimacaoConfig.Enabled = false; 
                }
            }
            else 
            {
                if (pnlBotoesConfig.Height < alturaAlvoPainelConfig - passoAnimacao)
                {
                    // Aumenta a altura E move para cima
                    pnlBotoesConfig.Height += passoAnimacao;
                    pnlBotoesConfig.Top -= passoAnimacao;
                }
                else // Chegou ou passou do máximo
                {
                    // Garante a altura e posição finais exatas
                    pnlBotoesConfig.Height = alturaAlvoPainelConfig;
                    pnlBotoesConfig.Top = topFinalPainelConfig;
                    painelConfigVisivel = true; // Marca como visível
                    timerAnimacaoConfig.Enabled = false; // Para o timer
                }
            }
        }

        private void btnMudarSenha_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade para Mudar Senha ainda não implementada.",
                           "Em Construção",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information);

            if (painelConfigVisivel) timerAnimacaoConfig.Start();

        }

        private void btnModoEscuro_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidade para Mudar Tema ainda não implementada.",
                           "Em Construção",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information);

            if (painelConfigVisivel) timerAnimacaoConfig.Start();
        }

        private string BuscarNomeCurso(int usuarioId)
        {
            string nomeCurso = null;
            string query = @"
                SELECT C.NomeCurso 
                FROM Usuarios U
                INNER JOIN Alunos A ON U.ID = A.UsuarioID -- Junta com Alunos
                LEFT JOIN Cursos C ON A.CursoID = C.ID    -- Junta com Cursos (via Alunos)
                WHERE U.ID = @UsuarioID";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            using (SqlCommand comando = new SqlCommand(query, conexao))
            {
                comando.Parameters.AddWithValue("@UsuarioID", usuarioId);
                try
                {
                    conexao.Open();
                    object result = comando.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        nomeCurso = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao buscar nome do curso: {ex.Message}", "Erro BD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return nomeCurso; 
        }

        private void CarregarChamados()
        {

            string query = @"
                SELECT 
                    CH.Titulo AS Descricao, 
                    CH.Status, 
                    CH.Prioridade, 
                    CH.ID AS IDChamado, 
                    CAT.NomeCategoria AS Categoria, -- <<< ADICIONADO JOIN E SELECT DA CATEGORIA
                    CONVERT(varchar, CH.DataCriacao, 103) AS Data, 
                    FORMAT(CH.DataAtualizacao, 'dd/MM') AS UltimaAtualizacao
                FROM Chamados CH
                LEFT JOIN Categorias CAT ON CH.CategoriaID = CAT.ID -- <<< JOIN COM CATEGORIAS
                WHERE CH.UsuarioID = @UsuarioID -- <<< FILTRA PELO ALUNO LOGADO
                ORDER BY CH.DataAtualizacao DESC";

            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    // Adiciona o parâmetro do ID do usuário logado
                    comando.Parameters.AddWithValue("@UsuarioID", Login.UsuarioLogadoId);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(comando))
                    {
                        adapter.Fill(dt);
                        dgvChamados.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar seus chamados: {ex.Message}", "Erro BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EstilizarGrid()
        {
            dgvChamados.AutoGenerateColumns = false; 
            dgvChamados.Columns.Clear();

            // Coluna Descrição
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Descricao",
                HeaderText = "Descrição",
                DataPropertyName = "Descricao", // <-- LIGAÇÃO com a query SQL
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill // Ocupa o espaço
            });

            // Coluna Status
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status", // O Name tem que ser "Status" para o CellFormatting funcionar
                HeaderText = "Status",
                DataPropertyName = "Status", // <-- LIGAÇÃO
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells // Ajusta ao conteúdo
            });

            // Coluna Prioridade
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Prioridade",
                HeaderText = "Prioridade",
                DataPropertyName = "Prioridade", // <-- LIGAÇÃO
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // Coluna ID Chamado
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IDChamado",
                HeaderText = "ID Chamado",
                DataPropertyName = "IDChamado", // <-- LIGAÇÃO (Nome da query: ID AS 'IDChamado')
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // Coluna Data
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Data",
                HeaderText = "Data",
                DataPropertyName = "Data", 
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // Coluna Última Atualização
            dgvChamados.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UltimaAtualizacao",
                HeaderText = "Última Atualização",
                DataPropertyName = "UltimaAtualizacao",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });     
        }

        private void dgvChamados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verifica se estamos na coluna "Status" e não no cabeçalho
            if (e.RowIndex >= 0 && dgvChamados.Columns[e.ColumnIndex].Name == "Status")
            {
                string status = e.Value.ToString();

                // Limpa qualquer estilo anterior
                e.CellStyle.Font = dgvChamados.DefaultCellStyle.Font;
                e.CellStyle.BackColor = dgvChamados.DefaultCellStyle.BackColor;
                e.CellStyle.ForeColor = dgvChamados.DefaultCellStyle.ForeColor;
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.Padding = new Padding(0);

                if (e.Value != null)
                {
                    if (status.Equals("Em andamento", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.BackColor = Color.FromArgb(0, 123, 255);
                        e.CellStyle.ForeColor = Color.White;
                        e.CellStyle.Font = new Font("Arial", 12F, FontStyle.Bold);
                    }
                    else if (status.Equals("Aberto", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(239,68,68);
                        e.CellStyle.Font = new Font("Arial", 12F, FontStyle.Bold);
                    }
                    else if (status.Equals("Resolvido", StringComparison.OrdinalIgnoreCase) || status.Equals("Fechado", StringComparison.OrdinalIgnoreCase))
                    {
                        // Aplica o estilo cinzento
                        e.CellStyle.BackColor = Color.FromArgb(240, 240, 240);
                        e.CellStyle.ForeColor = Color.Gray;
                        e.CellStyle.Font = new Font("Arial", 12F, FontStyle.Regular);
                    }

                    e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            // Confirma se o usuário quer mesmo sair do painel
            DialogResult resposta = MessageBox.Show("Deseja realmente sair e voltar para a tela de login?",
                                                   "Sair",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Question);

            if (resposta == DialogResult.Yes)
            {
                // Limpa as informações do usuário logado (se você estiver usando as variáveis estáticas)
                Login.UsuarioLogadoId = 0;
                Login.UsuarioLogadoNome = null;
                Login.UsuarioLogadoPapel = null;

                // Tenta encontrar uma instância aberta do Login ou cria uma nova
                Login loginForm = Application.OpenForms.OfType<Login>().FirstOrDefault();

                if (loginForm == null) // Se não houver uma instância aberta, cria uma nova
                {
                    loginForm = new Login();
                    loginForm.Show(); // Mostra a nova instância
                }
                else // Se já existir, apenas traz para frente
                {
                    loginForm.Show();
                    loginForm.BringToFront();
                }

                // Fecha a tela atual (PainelAluno)
                this.Close();
            }
        }

        private void btnNovoChamado_Click(object sender, EventArgs e)
        {
            using (NovoChamado telaModal = new NovoChamado())
            {
                // O código PAUSA AQUI até o pop-up fechar
                DialogResult resultado = telaModal.ShowDialog(this);

                // e verifica se o resultado foi "OK"
                if (resultado == DialogResult.OK)
                {
                    //    Se o usuário criou um chamado, recarrega a lista
                    CarregarChamados();
                }
            }
        }

        private void dgvChamados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            try
            {

                if (dgvChamados.Columns["IDChamado"] == null)
                {
                    MessageBox.Show("ERRO DE CÓDIGO:\nNão existe nenhuma coluna com o NOME (Name) 'IDChamado'.\n\nVerifique o seu método EstilizarGrid().", "Erro Crítico");
                    return;
                }

                // A célula tem um valor?
                object valorCelula = dgvChamados.Rows[e.RowIndex].Cells["IDChamado"].Value;
                if (valorCelula == null || valorCelula == DBNull.Value)
                {
                    MessageBox.Show($"ERRO DE DADOS:\nO ID do Chamado na linha {e.RowIndex} está VAZIO (nulo).\nNão é possível abrir os detalhes.", "Erro");
                    return;
                }

                // O valor pode ser convertido para um número?
                int chamadoID = 0;
                try
                {
                    chamadoID = Convert.ToInt32(valorCelula);
                }
                catch (FormatException)
                {
                    MessageBox.Show($"ERRO DE DADOS:\nO valor '{valorCelula}' na coluna 'IDChamado' não é um número válido.", "Erro");
                    return;
                }

                // O ID é válido?
                if (chamadoID <= 0)
                {
                    MessageBox.Show($"ERRO DE DADOS:\nO ID do Chamado é '{chamadoID}', o que é inválido.", "Erro");
                    return;
                }

                // Se passou por todos os testes, tenta abrir o formulário
                using (DetalhesDoChamado frmDetalhe = new DetalhesDoChamado(chamadoID))
                {
                    frmDetalhe.ShowDialog(this);
                    CarregarChamados();
                }
            }
            catch (Exception ex)
            {
                // Se ainda assim der um erro, será um erro inesperado
                MessageBox.Show($"Não foi possível abrir os detalhes do chamado: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Erro Inesperado");
            }
        }

        // evento para customizar o desenho da célula de Status
        private void dgvChamados_Sorted(object sender, EventArgs e)
        {
            dgvChamados.Invalidate();
        }

        private void dgvChamados_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Ignora o cabeçalho e linhas inválidas
            if (e.RowIndex < 0) return;

            // Executa o desenho customizado APENAS para a coluna "Status"
            if (e.ColumnIndex == dgvChamados.Columns["Status"].Index) // Use .Index para obter o índice da coluna
            {
                // Define que vamos desenhar a célula manualmente
                e.Handled = true;

                // pinta toda a área da célula com a cor de fundo apropriada (branco ou azul de seleção)
                e.PaintBackground(e.ClipBounds, (e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected);

                // Verifica se há valor na célula
                if (e.Value != null)
                {
                    string status = e.Value.ToString();

                    // ESTILOS
                    Color backColor;
                    Color foreColor;
                    FontStyle fontStyle = FontStyle.Bold;
                    int borderRadius = 10;

                    // Define cores com base no status (igual a antes)
                    if (status.Equals("Aberto", StringComparison.OrdinalIgnoreCase))
                    {
                        backColor = Color.FromArgb(255, 99, 71); foreColor = Color.White;
                    }
                    else if (status.Equals("Em andamento", StringComparison.OrdinalIgnoreCase))
                    {
                        backColor = Color.FromArgb(0, 123, 255); foreColor = Color.White;
                    }
                    else if (status.Equals("Resolvido", StringComparison.OrdinalIgnoreCase))
                    {
                        backColor = Color.FromArgb(240, 240, 240); foreColor = Color.Gray; fontStyle = FontStyle.Regular;
                    }
                    else
                    {
                        backColor = Color.LightGray; foreColor = Color.Black; fontStyle = FontStyle.Regular;
                    }

                    Graphics g = e.Graphics;
                    Rectangle cellBounds = e.CellBounds;

                    // Define o retângulo do badge (com padding interno)
                    int innerPaddingHorizontal = 10;
                    int innerPaddingVertical = 4;

                    // Usa o estilo da célula para a fonte base
                    Font badgeFont = new Font(e.CellStyle.Font.FontFamily, 9F, fontStyle);
                    SizeF textSize = g.MeasureString(status, badgeFont);

                    // Calcula tamanho e posição do badge para centralizar
                    int badgeWidth = (int)textSize.Width + (innerPaddingHorizontal * 2);
                    int badgeHeight = (int)textSize.Height + (innerPaddingVertical * 2);
                    // Garante que o badge não seja maior que a célula (importante!)
                    badgeWidth = Math.Min(badgeWidth, cellBounds.Width - 4); // -4 para uma pequena margem
                    badgeHeight = Math.Min(badgeHeight, cellBounds.Height - 4);

                    int badgeX = cellBounds.X + (cellBounds.Width - badgeWidth) / 2;
                    int badgeY = cellBounds.Y + (cellBounds.Height - badgeHeight) / 2;
                    Rectangle badgeRect = new Rectangle(badgeX, badgeY, badgeWidth, badgeHeight);

                    // Configura alta qualidade de desenho para bordas suaves
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    // Desenha o badge arredondado
                    using (SolidBrush brush = new SolidBrush(backColor))
                    {
                        System.Drawing.Drawing2D.GraphicsPath path = CreateRoundedRectanglePath(badgeRect, borderRadius);
                        g.FillPath(brush, path);
                    }

                    // Desenha o texto do status por cima do badge
                    using (SolidBrush textBrush = new SolidBrush(foreColor))
                    {
                        using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                        {
                            RectangleF textRect = new RectangleF(badgeRect.X, badgeRect.Y, badgeRect.Width, badgeRect.Height);
                            g.DrawString(status, badgeFont, textBrush, textRect, sf);
                        }
                    }

                    // Limpa o objeto Font se não for mais necessário
                    badgeFont.Dispose();
                }
            }
            else
            {
                // É importante chamar e.PaintParts para que o conteúdo e a seleção sejam desenhados corretamente.
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
                e.Handled = true;

            }
        }

        // Para criar o retângulo arredondado
        private System.Drawing.Drawing2D.GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int diameter = cornerRadius * 2;
            // Garante que o diâmetro não seja maior que a largura ou altura
            diameter = Math.Min(diameter, rect.Width);
            diameter = Math.Min(diameter, rect.Height);

            if (diameter <= 0) // Se raio for 0 ou negativo, desenha retângulo normal
            {
                path.AddRectangle(rect);
                return path;
            }

            // Desenha os arcos e linhas
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90); // Top-left
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90); // Top-right
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90); // Bottom-right
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90); // Bottom-left
            path.CloseFigure();
            return path;
        }
    }     
}
