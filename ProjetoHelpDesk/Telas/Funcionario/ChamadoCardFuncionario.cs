using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoHelpDesk
{
    public partial class ChamadoCardFuncionario : UserControl
    {
        public int ChamadoID { get; private set; }
        public event EventHandler CardClicado;

        // Cores de fundo do card
        private Color _corFundoNormal = Color.White;
        private Color _corFundoHover = Color.FromArgb(249, 250, 251);
        private Color _corFundoSelecionado = Color.FromArgb(239, 246, 255);
        private bool _estaSelecionado = false;


        public ChamadoCardFuncionario()
        {
            InitializeComponent();

            // Define a cor de fundo padrão
            this.BackColor = _corFundoNormal;

            // Chama o método para ligar os eventos de clique e hover
            LigarEventosRecursivamente(this);
        }

        // Método para o PainelFuncionario chamar e preencher os dados
        public void PreencherDados(int id, string titulo, string aluno, string curso, string status, string prioridade, string data)
        {
            this.ChamadoID = id;
            lblTituloChamado.Text = titulo;
            lblAlunoCurso.Text = $"{aluno} - {curso}";
            lblIDChamado.Text = $"#{id}";
            lblData.Text = data;

            lblStatus.Text = status;
            lblStatus.Font = new Font("Arial", 8F, FontStyle.Bold); // Garantir fonte Bold
            lblStatus.Padding = new Padding(6, 3, 6, 3); // Padding para criar o "badge"
            lblStatus.AutoSize = true; // Garante que o Label se ajusta ao texto + padding

            if (status.Equals("Aberto", StringComparison.OrdinalIgnoreCase))
            {
                lblStatus.BackColor = Color.FromArgb(254, 226, 226); // Fundo Vermelho Claro
                lblStatus.ForeColor = Color.FromArgb(185, 28, 28);   // Texto Vermelho Escuro
            }
            else if (status.Equals("Em andamento", StringComparison.OrdinalIgnoreCase))
            {
                lblStatus.BackColor = Color.FromArgb(219, 234, 254); // Fundo Azul Claro
                lblStatus.ForeColor = Color.FromArgb(29, 78, 216);    // Texto Azul Escuro
            }
            else if (status.Equals("Resolvido", StringComparison.OrdinalIgnoreCase))
            {
                lblStatus.BackColor = Color.FromArgb(240, 240, 240); // Cinza claro
                lblStatus.ForeColor = Color.Gray;                   // Texto cinza
                lblStatus.Font = new Font("Segoe UI", 8F, FontStyle.Regular); // Fonte normal
            }
            else
            { // Outros (Aguardando resposta, etc.)
                lblStatus.BackColor = Color.FromArgb(254, 243, 199); // Fundo Amarelo Claro
                lblStatus.ForeColor = Color.FromArgb(180, 83, 9);    // Texto Amarelo Escuro
            }

            // Estiliza o LABEL lblPrioridade
            lblPrioridade.Text = prioridade;
            lblPrioridade.Padding = new Padding(6, 3, 6, 3); // Padding para criar o "badge"
            lblPrioridade.AutoSize = true; // Garante que se ajusta

            if (prioridade.Equals("Urgente", StringComparison.OrdinalIgnoreCase) || prioridade.Equals("Alta", StringComparison.OrdinalIgnoreCase))
            {
                lblPrioridade.BackColor = Color.FromArgb(240, 240, 240); // Fundo cinza
                lblPrioridade.ForeColor = Color.DarkRed; // Destaque para prioridade alta
                lblPrioridade.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            }
            else
            { // Média ou Baixa
                lblPrioridade.BackColor = Color.FromArgb(240, 240, 240); // Cinza claro
                lblPrioridade.ForeColor = Color.FromArgb(55, 65, 81);   // Texto cinza escuro
                lblPrioridade.Font = new Font("Segoe UI", 8F, FontStyle.Regular); // Fonte normal
            }
        }

        // Métodos para controlar a seleção visual
        public void DefinirComoSelecionado(bool selecionado)
        {
            _estaSelecionado = selecionado;
            this.BackColor = _estaSelecionado ? _corFundoSelecionado : _corFundoNormal;
        }

        // - Lógica de Eventos -
        // Método recursivo para ligar eventos a TODOS os controlos dentro do card
        private void LigarEventosRecursivamente(Control container)
        {
            foreach (Control c in container.Controls)
            {
                c.Click += Card_Click; // Qualquer clique dispara o evento principal
                c.MouseEnter += Card_MouseEnter;
                c.MouseLeave += Card_MouseLeave;

                if (c.HasChildren)
                {
                    LigarEventosRecursivamente(c); 
                }
            }
        }

        private void Card_Click(object sender, EventArgs e)
        {
            // Dispara o nosso evento personalizado "CardClicado"
            CardClicado?.Invoke(this, EventArgs.Empty);
        }

        private void Card_MouseEnter(object sender, EventArgs e)
        {
            // Só muda a cor se não estiver selecionado
            if (!_estaSelecionado)
            {
                this.BackColor = _corFundoHover;
            }
        }

        private void Card_MouseLeave(object sender, EventArgs e)
        {
            // Só muda a cor se não estiver selecionado
            if (!_estaSelecionado)
            {
                this.BackColor = _corFundoNormal;
            }
        }
    }
}


