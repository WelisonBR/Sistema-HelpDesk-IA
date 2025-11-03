namespace ProjetoHelpDesk
{
    partial class DetalhesDoChamado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetalhesDoChamado));
            this.btnVoltar = new System.Windows.Forms.Button();
            this.lblRespostas = new System.Windows.Forms.Label();
            this.txtNovaResposta = new System.Windows.Forms.TextBox();
            this.btnEnviarResposta = new System.Windows.Forms.Button();
            this.pnlInfoEsquerda = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConsultarIA = new System.Windows.Forms.Button();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.lblPrioridade = new System.Windows.Forms.Label();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblChamadoId = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblCurso = new System.Windows.Forms.Label();
            this.lblRA = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblNomeAluno = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlSugestaoIA = new System.Windows.Forms.Panel();
            this.lblRespostaIA = new System.Windows.Forms.RichTextBox();
            this.btnSimResolvido = new System.Windows.Forms.Button();
            this.btnBuscarAjuda = new System.Windows.Forms.Button();
            this.pnlFeedbackIA = new System.Windows.Forms.Panel();
            this.lblPerguntaFeedback = new System.Windows.Forms.Label();
            this.pnlChatDireita = new System.Windows.Forms.Panel();
            this.pnlTituloRespostas = new System.Windows.Forms.Panel();
            this.flpChatHistory = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlEntradaChat = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlInfoEsquerda.SuspendLayout();
            this.pnlSugestaoIA.SuspendLayout();
            this.pnlFeedbackIA.SuspendLayout();
            this.pnlChatDireita.SuspendLayout();
            this.pnlTituloRespostas.SuspendLayout();
            this.pnlEntradaChat.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnVoltar
            // 
            this.btnVoltar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.Location = new System.Drawing.Point(16, 21);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(129, 34);
            this.btnVoltar.TabIndex = 0;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // lblRespostas
            // 
            this.lblRespostas.AutoSize = true;
            this.lblRespostas.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRespostas.Location = new System.Drawing.Point(14, 78);
            this.lblRespostas.Margin = new System.Windows.Forms.Padding(17);
            this.lblRespostas.Name = "lblRespostas";
            this.lblRespostas.Size = new System.Drawing.Size(118, 24);
            this.lblRespostas.TabIndex = 28;
            this.lblRespostas.Text = "Respostas";
            // 
            // txtNovaResposta
            // 
            this.txtNovaResposta.AcceptsReturn = true;
            this.txtNovaResposta.BackColor = System.Drawing.Color.White;
            this.txtNovaResposta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNovaResposta.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNovaResposta.ForeColor = System.Drawing.Color.Black;
            this.txtNovaResposta.Location = new System.Drawing.Point(55, 8);
            this.txtNovaResposta.Margin = new System.Windows.Forms.Padding(20, 20, 20, 0);
            this.txtNovaResposta.MaxLength = 1000;
            this.txtNovaResposta.Multiline = true;
            this.txtNovaResposta.Name = "txtNovaResposta";
            this.txtNovaResposta.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNovaResposta.Size = new System.Drawing.Size(445, 79);
            this.txtNovaResposta.TabIndex = 0;
            this.txtNovaResposta.Enter += new System.EventHandler(this.txtNovaResposta_Enter);
            this.txtNovaResposta.Leave += new System.EventHandler(this.txtNovaResposta_Leave);
            // 
            // btnEnviarResposta
            // 
            this.btnEnviarResposta.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarResposta.Location = new System.Drawing.Point(55, 112);
            this.btnEnviarResposta.Margin = new System.Windows.Forms.Padding(20, 10, 20, 20);
            this.btnEnviarResposta.Name = "btnEnviarResposta";
            this.btnEnviarResposta.Size = new System.Drawing.Size(445, 34);
            this.btnEnviarResposta.TabIndex = 28;
            this.btnEnviarResposta.Text = "Enviar Resposta";
            this.btnEnviarResposta.UseVisualStyleBackColor = true;
            this.btnEnviarResposta.Click += new System.EventHandler(this.btnEnviarResposta_Click);
            // 
            // pnlInfoEsquerda
            // 
            this.pnlInfoEsquerda.Controls.Add(this.label8);
            this.pnlInfoEsquerda.Controls.Add(this.label3);
            this.pnlInfoEsquerda.Controls.Add(this.btnVoltar);
            this.pnlInfoEsquerda.Controls.Add(this.btnConsultarIA);
            this.pnlInfoEsquerda.Controls.Add(this.txtDescricao);
            this.pnlInfoEsquerda.Controls.Add(this.lblPrioridade);
            this.pnlInfoEsquerda.Controls.Add(this.lblCategoria);
            this.pnlInfoEsquerda.Controls.Add(this.label7);
            this.pnlInfoEsquerda.Controls.Add(this.label5);
            this.pnlInfoEsquerda.Controls.Add(this.lblStatus);
            this.pnlInfoEsquerda.Controls.Add(this.label2);
            this.pnlInfoEsquerda.Controls.Add(this.lblChamadoId);
            this.pnlInfoEsquerda.Controls.Add(this.label16);
            this.pnlInfoEsquerda.Controls.Add(this.lblEmail);
            this.pnlInfoEsquerda.Controls.Add(this.lblCurso);
            this.pnlInfoEsquerda.Controls.Add(this.lblRA);
            this.pnlInfoEsquerda.Controls.Add(this.label11);
            this.pnlInfoEsquerda.Controls.Add(this.label10);
            this.pnlInfoEsquerda.Controls.Add(this.label6);
            this.pnlInfoEsquerda.Controls.Add(this.lblNomeAluno);
            this.pnlInfoEsquerda.Controls.Add(this.label4);
            this.pnlInfoEsquerda.Controls.Add(this.label1);
            this.pnlInfoEsquerda.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlInfoEsquerda.Location = new System.Drawing.Point(0, 0);
            this.pnlInfoEsquerda.Name = "pnlInfoEsquerda";
            this.pnlInfoEsquerda.Size = new System.Drawing.Size(452, 722);
            this.pnlInfoEsquerda.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(120)))), ((int)(((byte)(146)))));
            this.label8.Location = new System.Drawing.Point(11, 546);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 16);
            this.label8.TabIndex = 27;
            this.label8.Text = "Descrição da dúvida";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.label3.Location = new System.Drawing.Point(13, 668);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(414, 1);
            this.label3.TabIndex = 26;
            // 
            // btnConsultarIA
            // 
            this.btnConsultarIA.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultarIA.Location = new System.Drawing.Point(15, 681);
            this.btnConsultarIA.Name = "btnConsultarIA";
            this.btnConsultarIA.Size = new System.Drawing.Size(412, 34);
            this.btnConsultarIA.TabIndex = 3;
            this.btnConsultarIA.Text = "Consultar IA";
            this.btnConsultarIA.UseVisualStyleBackColor = true;
            this.btnConsultarIA.Click += new System.EventHandler(this.btnConsultarIA_Click);
            // 
            // txtDescricao
            // 
            this.txtDescricao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.txtDescricao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescricao.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.ForeColor = System.Drawing.Color.Black;
            this.txtDescricao.Location = new System.Drawing.Point(15, 577);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.ReadOnly = true;
            this.txtDescricao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescricao.Size = new System.Drawing.Size(414, 79);
            this.txtDescricao.TabIndex = 25;
            // 
            // lblPrioridade
            // 
            this.lblPrioridade.AutoSize = true;
            this.lblPrioridade.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrioridade.Location = new System.Drawing.Point(12, 444);
            this.lblPrioridade.Name = "lblPrioridade";
            this.lblPrioridade.Size = new System.Drawing.Size(0, 16);
            this.lblPrioridade.TabIndex = 24;
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoria.Location = new System.Drawing.Point(12, 514);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(0, 16);
            this.lblCategoria.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(120)))), ((int)(((byte)(146)))));
            this.label7.Location = new System.Drawing.Point(11, 418);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 16);
            this.label7.TabIndex = 22;
            this.label7.Text = "Prioridade";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(120)))), ((int)(((byte)(146)))));
            this.label5.Location = new System.Drawing.Point(11, 488);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 16);
            this.label5.TabIndex = 21;
            this.label5.Text = "Categoria";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(233, 372);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 16);
            this.lblStatus.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(120)))), ((int)(((byte)(146)))));
            this.label2.Location = new System.Drawing.Point(233, 343);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "Status";
            // 
            // lblChamadoId
            // 
            this.lblChamadoId.AutoSize = true;
            this.lblChamadoId.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChamadoId.Location = new System.Drawing.Point(12, 372);
            this.lblChamadoId.Name = "lblChamadoId";
            this.lblChamadoId.Size = new System.Drawing.Size(0, 16);
            this.lblChamadoId.TabIndex = 18;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(120)))), ((int)(((byte)(146)))));
            this.label16.Location = new System.Drawing.Point(11, 343);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(78, 16);
            this.label16.TabIndex = 17;
            this.label16.Text = "ID Chamado";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(11, 298);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(0, 16);
            this.lblEmail.TabIndex = 16;
            // 
            // lblCurso
            // 
            this.lblCurso.AutoSize = true;
            this.lblCurso.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurso.Location = new System.Drawing.Point(233, 226);
            this.lblCurso.Name = "lblCurso";
            this.lblCurso.Size = new System.Drawing.Size(0, 16);
            this.lblCurso.TabIndex = 15;
            // 
            // lblRA
            // 
            this.lblRA.AutoSize = true;
            this.lblRA.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRA.Location = new System.Drawing.Point(12, 226);
            this.lblRA.Name = "lblRA";
            this.lblRA.Size = new System.Drawing.Size(0, 16);
            this.lblRA.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(120)))), ((int)(((byte)(146)))));
            this.label11.Location = new System.Drawing.Point(11, 199);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 16);
            this.label11.TabIndex = 13;
            this.label11.Text = "RA";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(120)))), ((int)(((byte)(146)))));
            this.label10.Location = new System.Drawing.Point(233, 199);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 16);
            this.label10.TabIndex = 12;
            this.label10.Text = "Curso";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(120)))), ((int)(((byte)(146)))));
            this.label6.Location = new System.Drawing.Point(11, 269);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "E-mail";
            // 
            // lblNomeAluno
            // 
            this.lblNomeAluno.AutoSize = true;
            this.lblNomeAluno.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeAluno.Location = new System.Drawing.Point(11, 157);
            this.lblNomeAluno.Name = "lblNomeAluno";
            this.lblNomeAluno.Size = new System.Drawing.Size(0, 16);
            this.lblNomeAluno.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(120)))), ((int)(((byte)(146)))));
            this.label4.Location = new System.Drawing.Point(12, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Aluno";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Informações do Chamado";
            // 
            // pnlSugestaoIA
            // 
            this.pnlSugestaoIA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSugestaoIA.AutoScroll = true;
            this.pnlSugestaoIA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.pnlSugestaoIA.Controls.Add(this.lblRespostaIA);
            this.pnlSugestaoIA.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSugestaoIA.Location = new System.Drawing.Point(12, 726);
            this.pnlSugestaoIA.Name = "pnlSugestaoIA";
            this.pnlSugestaoIA.Size = new System.Drawing.Size(452, 173);
            this.pnlSugestaoIA.TabIndex = 28;
            this.pnlSugestaoIA.Visible = false;
            // 
            // lblRespostaIA
            // 
            this.lblRespostaIA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.lblRespostaIA.ForeColor = System.Drawing.Color.Black;
            this.lblRespostaIA.Location = new System.Drawing.Point(19, 21);
            this.lblRespostaIA.Name = "lblRespostaIA";
            this.lblRespostaIA.ReadOnly = true;
            this.lblRespostaIA.Size = new System.Drawing.Size(413, 129);
            this.lblRespostaIA.TabIndex = 1;
            this.lblRespostaIA.Text = "";
            // 
            // btnSimResolvido
            // 
            this.btnSimResolvido.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSimResolvido.Location = new System.Drawing.Point(19, 41);
            this.btnSimResolvido.Name = "btnSimResolvido";
            this.btnSimResolvido.Size = new System.Drawing.Size(190, 34);
            this.btnSimResolvido.TabIndex = 28;
            this.btnSimResolvido.Text = "Sim, problema resolvido";
            this.btnSimResolvido.UseVisualStyleBackColor = true;
            this.btnSimResolvido.Click += new System.EventHandler(this.btnSimResolvido_Click);
            // 
            // btnBuscarAjuda
            // 
            this.btnBuscarAjuda.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarAjuda.Location = new System.Drawing.Point(221, 41);
            this.btnBuscarAjuda.Name = "btnBuscarAjuda";
            this.btnBuscarAjuda.Size = new System.Drawing.Size(211, 34);
            this.btnBuscarAjuda.TabIndex = 29;
            this.btnBuscarAjuda.Text = "Não, preciso de mais ajuda";
            this.btnBuscarAjuda.UseVisualStyleBackColor = true;
            this.btnBuscarAjuda.Click += new System.EventHandler(this.btnBuscarAjuda_Click);
            // 
            // pnlFeedbackIA
            // 
            this.pnlFeedbackIA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlFeedbackIA.Controls.Add(this.lblPerguntaFeedback);
            this.pnlFeedbackIA.Controls.Add(this.btnSimResolvido);
            this.pnlFeedbackIA.Controls.Add(this.btnBuscarAjuda);
            this.pnlFeedbackIA.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlFeedbackIA.Location = new System.Drawing.Point(12, 905);
            this.pnlFeedbackIA.Name = "pnlFeedbackIA";
            this.pnlFeedbackIA.Size = new System.Drawing.Size(452, 78);
            this.pnlFeedbackIA.TabIndex = 0;
            this.pnlFeedbackIA.Visible = false;
            // 
            // lblPerguntaFeedback
            // 
            this.lblPerguntaFeedback.AutoSize = true;
            this.lblPerguntaFeedback.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPerguntaFeedback.Location = new System.Drawing.Point(17, 10);
            this.lblPerguntaFeedback.Name = "lblPerguntaFeedback";
            this.lblPerguntaFeedback.Size = new System.Drawing.Size(219, 19);
            this.lblPerguntaFeedback.TabIndex = 28;
            this.lblPerguntaFeedback.Text = "Sua dúvida foi respondida?";
            // 
            // pnlChatDireita
            // 
            this.pnlChatDireita.Controls.Add(this.pnlTituloRespostas);
            this.pnlChatDireita.Controls.Add(this.flpChatHistory);
            this.pnlChatDireita.Controls.Add(this.pnlEntradaChat);
            this.pnlChatDireita.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlChatDireita.Location = new System.Drawing.Point(458, 0);
            this.pnlChatDireita.Margin = new System.Windows.Forms.Padding(17);
            this.pnlChatDireita.Name = "pnlChatDireita";
            this.pnlChatDireita.Size = new System.Drawing.Size(544, 722);
            this.pnlChatDireita.TabIndex = 1;
            // 
            // pnlTituloRespostas
            // 
            this.pnlTituloRespostas.Controls.Add(this.lblRespostas);
            this.pnlTituloRespostas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTituloRespostas.Location = new System.Drawing.Point(0, 0);
            this.pnlTituloRespostas.Name = "pnlTituloRespostas";
            this.pnlTituloRespostas.Size = new System.Drawing.Size(544, 102);
            this.pnlTituloRespostas.TabIndex = 30;
            // 
            // flpChatHistory
            // 
            this.flpChatHistory.AutoScroll = true;
            this.flpChatHistory.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpChatHistory.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flpChatHistory.Location = new System.Drawing.Point(17, 103);
            this.flpChatHistory.Margin = new System.Windows.Forms.Padding(20);
            this.flpChatHistory.Name = "flpChatHistory";
            this.flpChatHistory.Size = new System.Drawing.Size(527, 468);
            this.flpChatHistory.TabIndex = 0;
            this.flpChatHistory.WrapContents = false;
            // 
            // pnlEntradaChat
            // 
            this.pnlEntradaChat.Controls.Add(this.label9);
            this.pnlEntradaChat.Controls.Add(this.txtNovaResposta);
            this.pnlEntradaChat.Controls.Add(this.btnEnviarResposta);
            this.pnlEntradaChat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlEntradaChat.Location = new System.Drawing.Point(0, 569);
            this.pnlEntradaChat.Name = "pnlEntradaChat";
            this.pnlEntradaChat.Size = new System.Drawing.Size(544, 153);
            this.pnlEntradaChat.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.label9.Location = new System.Drawing.Point(52, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(447, 1);
            this.label9.TabIndex = 29;
            // 
            // DetalhesDoChamado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1002, 722);
            this.Controls.Add(this.pnlFeedbackIA);
            this.Controls.Add(this.pnlSugestaoIA);
            this.Controls.Add(this.pnlInfoEsquerda);
            this.Controls.Add(this.pnlChatDireita);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DetalhesDoChamado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.DetalhesDoChamado_Load);
            this.pnlInfoEsquerda.ResumeLayout(false);
            this.pnlInfoEsquerda.PerformLayout();
            this.pnlSugestaoIA.ResumeLayout(false);
            this.pnlFeedbackIA.ResumeLayout(false);
            this.pnlFeedbackIA.PerformLayout();
            this.pnlChatDireita.ResumeLayout(false);
            this.pnlTituloRespostas.ResumeLayout(false);
            this.pnlTituloRespostas.PerformLayout();
            this.pnlEntradaChat.ResumeLayout(false);
            this.pnlEntradaChat.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Panel pnlInfoEsquerda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNomeAluno;
        private System.Windows.Forms.Label lblPrioridade;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblChamadoId;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblCurso;
        private System.Windows.Forms.Label lblRA;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnConsultarIA;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label lblRespostas;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNovaResposta;
        private System.Windows.Forms.Button btnEnviarResposta;
        private System.Windows.Forms.Panel pnlSugestaoIA;
        private System.Windows.Forms.Button btnSimResolvido;
        private System.Windows.Forms.Button btnBuscarAjuda;
        private System.Windows.Forms.Panel pnlFeedbackIA;
        private System.Windows.Forms.Label lblPerguntaFeedback;
        private System.Windows.Forms.RichTextBox lblRespostaIA;
        private System.Windows.Forms.Panel pnlChatDireita;
        private System.Windows.Forms.FlowLayoutPanel flpChatHistory;
        private System.Windows.Forms.Panel pnlEntradaChat;
        private System.Windows.Forms.Panel pnlTituloRespostas;
        private System.Windows.Forms.Label label9;
    }
}