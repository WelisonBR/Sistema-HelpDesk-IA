namespace ProjetoHelpDesk
{
    partial class PainelAluno
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PainelAluno));
            this.pnlLateral = new System.Windows.Forms.Panel();
            this.pnlBotoesConfig = new System.Windows.Forms.Panel();
            this.btnMudarSenha = new System.Windows.Forms.Button();
            this.btnModoEscuro = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnConfiguracoes = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.lblNomeCursoPainel = new System.Windows.Forms.Label();
            this.lblNomePainel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvChamados = new System.Windows.Forms.DataGridView();
            this.colDescricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrioridade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAtualizacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colApagar = new System.Windows.Forms.DataGridViewImageColumn();
            this.pnlCabecalho = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.btnNovoChamado = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timerAnimacaoConfig = new System.Windows.Forms.Timer(this.components);
            this.pnlLateral.SuspendLayout();
            this.pnlBotoesConfig.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChamados)).BeginInit();
            this.pnlCabecalho.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLateral
            // 
            this.pnlLateral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(116)))), ((int)(((byte)(247)))));
            this.pnlLateral.Controls.Add(this.pnlBotoesConfig);
            this.pnlLateral.Controls.Add(this.btnSair);
            this.pnlLateral.Controls.Add(this.btnConfiguracoes);
            this.pnlLateral.Controls.Add(this.label3);
            this.pnlLateral.Controls.Add(this.label2);
            this.pnlLateral.Controls.Add(this.button2);
            this.pnlLateral.Controls.Add(this.lblNomeCursoPainel);
            this.pnlLateral.Controls.Add(this.lblNomePainel);
            this.pnlLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLateral.Location = new System.Drawing.Point(0, 0);
            this.pnlLateral.Margin = new System.Windows.Forms.Padding(4);
            this.pnlLateral.Name = "pnlLateral";
            this.pnlLateral.Size = new System.Drawing.Size(200, 861);
            this.pnlLateral.TabIndex = 0;
            // 
            // pnlBotoesConfig
            // 
            this.pnlBotoesConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBotoesConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(116)))), ((int)(((byte)(247)))));
            this.pnlBotoesConfig.Controls.Add(this.btnMudarSenha);
            this.pnlBotoesConfig.Controls.Add(this.btnModoEscuro);
            this.pnlBotoesConfig.Location = new System.Drawing.Point(0, 661);
            this.pnlBotoesConfig.Name = "pnlBotoesConfig";
            this.pnlBotoesConfig.Size = new System.Drawing.Size(200, 103);
            this.pnlBotoesConfig.TabIndex = 4;
            // 
            // btnMudarSenha
            // 
            this.btnMudarSenha.BackColor = System.Drawing.Color.Transparent;
            this.btnMudarSenha.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMudarSenha.FlatAppearance.BorderSize = 0;
            this.btnMudarSenha.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(144)))), ((int)(((byte)(249)))));
            this.btnMudarSenha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMudarSenha.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMudarSenha.ForeColor = System.Drawing.Color.White;
            this.btnMudarSenha.Location = new System.Drawing.Point(10, 55);
            this.btnMudarSenha.Name = "btnMudarSenha";
            this.btnMudarSenha.Size = new System.Drawing.Size(180, 41);
            this.btnMudarSenha.TabIndex = 5;
            this.btnMudarSenha.Text = "Mudar Senha";
            this.btnMudarSenha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMudarSenha.UseVisualStyleBackColor = false;
            this.btnMudarSenha.Click += new System.EventHandler(this.btnMudarSenha_Click);
            // 
            // btnModoEscuro
            // 
            this.btnModoEscuro.BackColor = System.Drawing.Color.Transparent;
            this.btnModoEscuro.FlatAppearance.BorderSize = 0;
            this.btnModoEscuro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(144)))), ((int)(((byte)(249)))));
            this.btnModoEscuro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModoEscuro.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModoEscuro.ForeColor = System.Drawing.Color.White;
            this.btnModoEscuro.Location = new System.Drawing.Point(10, 8);
            this.btnModoEscuro.Name = "btnModoEscuro";
            this.btnModoEscuro.Size = new System.Drawing.Size(180, 41);
            this.btnModoEscuro.TabIndex = 4;
            this.btnModoEscuro.Text = "Mudar Tema";
            this.btnModoEscuro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModoEscuro.UseVisualStyleBackColor = false;
            this.btnModoEscuro.Click += new System.EventHandler(this.btnModoEscuro_Click);
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.Transparent;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(144)))), ((int)(((byte)(249)))));
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.Location = new System.Drawing.Point(10, 814);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(180, 38);
            this.btnSair.TabIndex = 2;
            this.btnSair.Text = "Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnConfiguracoes
            // 
            this.btnConfiguracoes.BackColor = System.Drawing.Color.Transparent;
            this.btnConfiguracoes.FlatAppearance.BorderSize = 0;
            this.btnConfiguracoes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(144)))), ((int)(((byte)(249)))));
            this.btnConfiguracoes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfiguracoes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfiguracoes.ForeColor = System.Drawing.Color.White;
            this.btnConfiguracoes.Location = new System.Drawing.Point(10, 770);
            this.btnConfiguracoes.Name = "btnConfiguracoes";
            this.btnConfiguracoes.Size = new System.Drawing.Size(180, 38);
            this.btnConfiguracoes.TabIndex = 0;
            this.btnConfiguracoes.Text = "Configurações";
            this.btnConfiguracoes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfiguracoes.UseVisualStyleBackColor = false;
            this.btnConfiguracoes.Click += new System.EventHandler(this.btnConfiguracoes_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(144)))), ((int)(((byte)(249)))));
            this.label3.Location = new System.Drawing.Point(-3, 763);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 1);
            this.label3.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 38);
            this.label2.TabIndex = 1;
            this.label2.Text = "Seus chamados em andamento:";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(144)))), ((int)(((byte)(249)))));
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(144)))), ((int)(((byte)(249)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(221)))), ((int)(((byte)(253)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(14, 83);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(176, 40);
            this.button2.TabIndex = 1;
            this.button2.Text = "Chamados";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // lblNomeCursoPainel
            // 
            this.lblNomeCursoPainel.AutoSize = true;
            this.lblNomeCursoPainel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeCursoPainel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(221)))), ((int)(((byte)(253)))));
            this.lblNomeCursoPainel.Location = new System.Drawing.Point(12, 40);
            this.lblNomeCursoPainel.Name = "lblNomeCursoPainel";
            this.lblNomeCursoPainel.Size = new System.Drawing.Size(0, 13);
            this.lblNomeCursoPainel.TabIndex = 1;
            // 
            // lblNomePainel
            // 
            this.lblNomePainel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomePainel.ForeColor = System.Drawing.Color.White;
            this.lblNomePainel.Location = new System.Drawing.Point(10, 19);
            this.lblNomePainel.Margin = new System.Windows.Forms.Padding(10);
            this.lblNomePainel.Name = "lblNomePainel";
            this.lblNomePainel.Size = new System.Drawing.Size(180, 20);
            this.lblNomePainel.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.dgvChamados);
            this.panel1.Controls.Add(this.pnlCabecalho);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(200, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 861);
            this.panel1.TabIndex = 1;
            // 
            // dgvChamados
            // 
            this.dgvChamados.AllowUserToAddRows = false;
            this.dgvChamados.AllowUserToResizeRows = false;
            this.dgvChamados.BackgroundColor = System.Drawing.Color.White;
            this.dgvChamados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChamados.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvChamados.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChamados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvChamados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChamados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDescricao,
            this.colStatus,
            this.colPrioridade,
            this.colId,
            this.colData,
            this.colAtualizacao,
            this.colApagar});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChamados.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvChamados.EnableHeadersVisualStyles = false;
            this.dgvChamados.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.dgvChamados.Location = new System.Drawing.Point(11, 59);
            this.dgvChamados.Name = "dgvChamados";
            this.dgvChamados.ReadOnly = true;
            this.dgvChamados.RowHeadersVisible = false;
            this.dgvChamados.RowTemplate.Height = 45;
            this.dgvChamados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChamados.Size = new System.Drawing.Size(761, 790);
            this.dgvChamados.TabIndex = 3;
            this.dgvChamados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChamados_CellDoubleClick);
            this.dgvChamados.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvChamados_CellPainting);
            this.dgvChamados.Sorted += new System.EventHandler(this.dgvChamados_Sorted);
            // 
            // colDescricao
            // 
            this.colDescricao.HeaderText = "Descrição";
            this.colDescricao.Name = "colDescricao";
            this.colDescricao.ReadOnly = true;
            this.colDescricao.Width = 113;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colPrioridade
            // 
            this.colPrioridade.HeaderText = "Prioridade";
            this.colPrioridade.Name = "colPrioridade";
            this.colPrioridade.ReadOnly = true;
            // 
            // colId
            // 
            this.colId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colId.HeaderText = "ID Chamado";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Width = 129;
            // 
            // colData
            // 
            this.colData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colData.HeaderText = "Data";
            this.colData.Name = "colData";
            this.colData.ReadOnly = true;
            this.colData.Width = 75;
            // 
            // colAtualizacao
            // 
            this.colAtualizacao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.colAtualizacao.DefaultCellStyle = dataGridViewCellStyle5;
            this.colAtualizacao.HeaderText = "Última Atualização";
            this.colAtualizacao.Name = "colAtualizacao";
            this.colAtualizacao.ReadOnly = true;
            this.colAtualizacao.Width = 169;
            // 
            // colApagar
            // 
            this.colApagar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colApagar.HeaderText = "";
            this.colApagar.Name = "colApagar";
            this.colApagar.ReadOnly = true;
            // 
            // pnlCabecalho
            // 
            this.pnlCabecalho.Controls.Add(this.button5);
            this.pnlCabecalho.Controls.Add(this.btnNovoChamado);
            this.pnlCabecalho.Controls.Add(this.label5);
            this.pnlCabecalho.Controls.Add(this.label4);
            this.pnlCabecalho.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCabecalho.ForeColor = System.Drawing.Color.Black;
            this.pnlCabecalho.Location = new System.Drawing.Point(0, 0);
            this.pnlCabecalho.Name = "pnlCabecalho";
            this.pnlCabecalho.Size = new System.Drawing.Size(784, 53);
            this.pnlCabecalho.TabIndex = 2;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(144)))), ((int)(((byte)(249)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Location = new System.Drawing.Point(619, 16);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(28, 28);
            this.button5.TabIndex = 3;
            this.button5.Text = "f";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Visible = false;
            // 
            // btnNovoChamado
            // 
            this.btnNovoChamado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(116)))), ((int)(((byte)(247)))));
            this.btnNovoChamado.FlatAppearance.BorderSize = 0;
            this.btnNovoChamado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(144)))), ((int)(((byte)(249)))));
            this.btnNovoChamado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNovoChamado.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovoChamado.ForeColor = System.Drawing.Color.White;
            this.btnNovoChamado.Location = new System.Drawing.Point(653, 16);
            this.btnNovoChamado.Name = "btnNovoChamado";
            this.btnNovoChamado.Size = new System.Drawing.Size(119, 28);
            this.btnNovoChamado.TabIndex = 3;
            this.btnNovoChamado.Text = "Novo Chamado";
            this.btnNovoChamado.UseVisualStyleBackColor = false;
            this.btnNovoChamado.Click += new System.EventHandler(this.btnNovoChamado_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 19);
            this.label5.TabIndex = 2;
            this.label5.Text = "Chamados";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.label4.Location = new System.Drawing.Point(-3, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(901, 1);
            this.label4.TabIndex = 1;
            // 
            // timerAnimacaoConfig
            // 
            this.timerAnimacaoConfig.Interval = 15;
            this.timerAnimacaoConfig.Tick += new System.EventHandler(this.timerAnimacaoConfig_Tick);
            // 
            // PainelAluno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 861);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlLateral);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PainelAluno";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Painel do Aluno";
            this.Load += new System.EventHandler(this.PainelAluno_Load);
            this.pnlLateral.ResumeLayout(false);
            this.pnlLateral.PerformLayout();
            this.pnlBotoesConfig.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChamados)).EndInit();
            this.pnlCabecalho.ResumeLayout(false);
            this.pnlCabecalho.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLateral;
        private System.Windows.Forms.Label lblNomeCursoPainel;
        private System.Windows.Forms.Label lblNomePainel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnConfiguracoes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlCabecalho;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btnNovoChamado;
        private System.Windows.Forms.DataGridView dgvChamados;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrioridade;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colData;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAtualizacao;
        private System.Windows.Forms.DataGridViewImageColumn colApagar;
        private System.Windows.Forms.Panel pnlBotoesConfig;
        private System.Windows.Forms.Button btnMudarSenha;
        private System.Windows.Forms.Button btnModoEscuro;
        private System.Windows.Forms.Timer timerAnimacaoConfig;
    }
}

