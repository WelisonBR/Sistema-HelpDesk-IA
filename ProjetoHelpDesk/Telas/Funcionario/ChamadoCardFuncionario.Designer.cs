namespace ProjetoHelpDesk
{
    partial class ChamadoCardFuncionario
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

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTituloChamado = new System.Windows.Forms.Label();
            this.lblAlunoCurso = new System.Windows.Forms.Label();
            this.lblIDChamado = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPrioridade = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTituloChamado
            // 
            this.lblTituloChamado.AutoSize = true;
            this.lblTituloChamado.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloChamado.Location = new System.Drawing.Point(5, 6);
            this.lblTituloChamado.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.lblTituloChamado.Name = "lblTituloChamado";
            this.lblTituloChamado.Size = new System.Drawing.Size(37, 15);
            this.lblTituloChamado.TabIndex = 0;
            this.lblTituloChamado.Text = "Titulo";
            // 
            // lblAlunoCurso
            // 
            this.lblAlunoCurso.AutoSize = true;
            this.lblAlunoCurso.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlunoCurso.Location = new System.Drawing.Point(5, 35);
            this.lblAlunoCurso.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.lblAlunoCurso.Name = "lblAlunoCurso";
            this.lblAlunoCurso.Size = new System.Drawing.Size(85, 15);
            this.lblAlunoCurso.TabIndex = 1;
            this.lblAlunoCurso.Text = "Nome - Curso";
            // 
            // lblIDChamado
            // 
            this.lblIDChamado.AutoSize = true;
            this.lblIDChamado.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIDChamado.Location = new System.Drawing.Point(77, 65);
            this.lblIDChamado.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.lblIDChamado.Name = "lblIDChamado";
            this.lblIDChamado.Size = new System.Drawing.Size(13, 14);
            this.lblIDChamado.TabIndex = 4;
            this.lblIDChamado.Text = "#";
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblData.Location = new System.Drawing.Point(226, 65);
            this.lblData.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(29, 14);
            this.lblData.TabIndex = 2;
            this.lblData.Text = "Data";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(5, 65);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(29, 14);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Data";
            // 
            // lblPrioridade
            // 
            this.lblPrioridade.AutoSize = true;
            this.lblPrioridade.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrioridade.Location = new System.Drawing.Point(226, 7);
            this.lblPrioridade.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.lblPrioridade.Name = "lblPrioridade";
            this.lblPrioridade.Size = new System.Drawing.Size(55, 14);
            this.lblPrioridade.TabIndex = 9;
            this.lblPrioridade.Text = "Prioridade";
            // 
            // ChamadoCardFuncionario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.Controls.Add(this.lblPrioridade);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblIDChamado);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.lblAlunoCurso);
            this.Controls.Add(this.lblTituloChamado);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ChamadoCardFuncionario";
            this.Size = new System.Drawing.Size(290, 89);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTituloChamado;
        private System.Windows.Forms.Label lblAlunoCurso;
        private System.Windows.Forms.Label lblIDChamado;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPrioridade;
    }
}
