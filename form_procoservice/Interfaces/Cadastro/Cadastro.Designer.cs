﻿
namespace form_procoservice
{
    partial class Cadastro
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
            this.lblNome = new System.Windows.Forms.Label();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.lblCEP = new System.Windows.Forms.Label();
            this.lblRua = new System.Windows.Forms.Label();
            this.lblBairro = new System.Windows.Forms.Label();
            this.lblCidade = new System.Windows.Forms.Label();
            this.lblUF = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.txtRua = new System.Windows.Forms.TextBox();
            this.txtBairro = new System.Windows.Forms.TextBox();
            this.txtCidade = new System.Windows.Forms.TextBox();
            this.txtUF = new System.Windows.Forms.TextBox();
            this.lblNumero = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.btnCadastrar = new System.Windows.Forms.Button();
            this.mtxtCEP = new System.Windows.Forms.MaskedTextBox();
            this.mtxtTelefone = new System.Windows.Forms.MaskedTextBox();
            this.lblVerificaNome = new System.Windows.Forms.Label();
            this.rbtnPessoaFisica = new System.Windows.Forms.RadioButton();
            this.rbtnPessoaJuridica = new System.Windows.Forms.RadioButton();
            this.gboxTipoPessoa = new System.Windows.Forms.GroupBox();
            this.mtxtCpfCnpj = new System.Windows.Forms.MaskedTextBox();
            this.lblCpfCnpj = new System.Windows.Forms.Label();
            this.gboxTipoPessoa.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNome
            // 
            this.lblNome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(76, 104);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(40, 15);
            this.lblNome.TabIndex = 0;
            this.lblNome.Text = "Nome";
            // 
            // lblTelefone
            // 
            this.lblTelefone.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTelefone.AutoSize = true;
            this.lblTelefone.Location = new System.Drawing.Point(76, 144);
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.Size = new System.Drawing.Size(51, 15);
            this.lblTelefone.TabIndex = 1;
            this.lblTelefone.Text = "Telefone";
            // 
            // lblCEP
            // 
            this.lblCEP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCEP.AutoSize = true;
            this.lblCEP.Location = new System.Drawing.Point(373, 144);
            this.lblCEP.Name = "lblCEP";
            this.lblCEP.Size = new System.Drawing.Size(28, 15);
            this.lblCEP.TabIndex = 2;
            this.lblCEP.Text = "CEP";
            // 
            // lblRua
            // 
            this.lblRua.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRua.AutoSize = true;
            this.lblRua.Location = new System.Drawing.Point(76, 187);
            this.lblRua.Name = "lblRua";
            this.lblRua.Size = new System.Drawing.Size(27, 15);
            this.lblRua.TabIndex = 3;
            this.lblRua.Text = "Rua";
            // 
            // lblBairro
            // 
            this.lblBairro.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBairro.AutoSize = true;
            this.lblBairro.Location = new System.Drawing.Point(76, 224);
            this.lblBairro.Name = "lblBairro";
            this.lblBairro.Size = new System.Drawing.Size(38, 15);
            this.lblBairro.TabIndex = 4;
            this.lblBairro.Text = "Bairro";
            // 
            // lblCidade
            // 
            this.lblCidade.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCidade.AutoSize = true;
            this.lblCidade.Location = new System.Drawing.Point(76, 264);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Size = new System.Drawing.Size(44, 15);
            this.lblCidade.TabIndex = 5;
            this.lblCidade.Text = "Cidade";
            // 
            // lblUF
            // 
            this.lblUF.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUF.AutoSize = true;
            this.lblUF.Location = new System.Drawing.Point(373, 183);
            this.lblUF.Name = "lblUF";
            this.lblUF.Size = new System.Drawing.Size(21, 15);
            this.lblUF.TabIndex = 6;
            this.lblUF.Text = "UF";
            // 
            // txtNome
            // 
            this.txtNome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNome.Location = new System.Drawing.Point(157, 101);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(197, 23);
            this.txtNome.TabIndex = 0;
            // 
            // txtRua
            // 
            this.txtRua.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtRua.Enabled = false;
            this.txtRua.Location = new System.Drawing.Point(157, 181);
            this.txtRua.Name = "txtRua";
            this.txtRua.ReadOnly = true;
            this.txtRua.Size = new System.Drawing.Size(197, 23);
            this.txtRua.TabIndex = 10;
            // 
            // txtBairro
            // 
            this.txtBairro.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBairro.Enabled = false;
            this.txtBairro.Location = new System.Drawing.Point(157, 221);
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.ReadOnly = true;
            this.txtBairro.Size = new System.Drawing.Size(197, 23);
            this.txtBairro.TabIndex = 11;
            // 
            // txtCidade
            // 
            this.txtCidade.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCidade.Enabled = false;
            this.txtCidade.Location = new System.Drawing.Point(157, 261);
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.ReadOnly = true;
            this.txtCidade.Size = new System.Drawing.Size(100, 23);
            this.txtCidade.TabIndex = 12;
            // 
            // txtUF
            // 
            this.txtUF.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUF.Enabled = false;
            this.txtUF.Location = new System.Drawing.Point(417, 181);
            this.txtUF.Name = "txtUF";
            this.txtUF.ReadOnly = true;
            this.txtUF.Size = new System.Drawing.Size(100, 23);
            this.txtUF.TabIndex = 13;
            // 
            // lblNumero
            // 
            this.lblNumero.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(76, 303);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(51, 15);
            this.lblNumero.TabIndex = 14;
            this.lblNumero.Text = "Número";
            // 
            // txtNumero
            // 
            this.txtNumero.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNumero.Location = new System.Drawing.Point(157, 301);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(100, 23);
            this.txtNumero.TabIndex = 3;
            // 
            // btnCadastrar
            // 
            this.btnCadastrar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCadastrar.Location = new System.Drawing.Point(499, 380);
            this.btnCadastrar.Name = "btnCadastrar";
            this.btnCadastrar.Size = new System.Drawing.Size(104, 23);
            this.btnCadastrar.TabIndex = 4;
            this.btnCadastrar.Text = "Cadastrar";
            this.btnCadastrar.UseVisualStyleBackColor = true;
            this.btnCadastrar.Click += new System.EventHandler(this.btnCadastrar_Click);
            // 
            // mtxtCEP
            // 
            this.mtxtCEP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mtxtCEP.Location = new System.Drawing.Point(417, 141);
            this.mtxtCEP.Mask = "00000-000";
            this.mtxtCEP.Name = "mtxtCEP";
            this.mtxtCEP.Size = new System.Drawing.Size(100, 23);
            this.mtxtCEP.TabIndex = 2;
            this.mtxtCEP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mtxtCEP_MouseClick);
            this.mtxtCEP.Leave += new System.EventHandler(this.mtxtCEP_Leave);
            // 
            // mtxtTelefone
            // 
            this.mtxtTelefone.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mtxtTelefone.Location = new System.Drawing.Point(157, 141);
            this.mtxtTelefone.Mask = "(99) 0000-0000";
            this.mtxtTelefone.Name = "mtxtTelefone";
            this.mtxtTelefone.ResetOnPrompt = false;
            this.mtxtTelefone.ResetOnSpace = false;
            this.mtxtTelefone.Size = new System.Drawing.Size(100, 23);
            this.mtxtTelefone.TabIndex = 1;
            this.mtxtTelefone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtTelefone_KeyPress);
            // 
            // lblVerificaNome
            // 
            this.lblVerificaNome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblVerificaNome.AutoSize = true;
            this.lblVerificaNome.ForeColor = System.Drawing.Color.Red;
            this.lblVerificaNome.Location = new System.Drawing.Point(523, 104);
            this.lblVerificaNome.Name = "lblVerificaNome";
            this.lblVerificaNome.Size = new System.Drawing.Size(58, 15);
            this.lblVerificaNome.TabIndex = 15;
            this.lblVerificaNome.Text = "label_erro";
            this.lblVerificaNome.Visible = false;
            // 
            // rbtnPessoaFisica
            // 
            this.rbtnPessoaFisica.AutoSize = true;
            this.rbtnPessoaFisica.Checked = true;
            this.rbtnPessoaFisica.Location = new System.Drawing.Point(6, 20);
            this.rbtnPessoaFisica.Name = "rbtnPessoaFisica";
            this.rbtnPessoaFisica.Size = new System.Drawing.Size(93, 19);
            this.rbtnPessoaFisica.TabIndex = 16;
            this.rbtnPessoaFisica.TabStop = true;
            this.rbtnPessoaFisica.Text = "Pessoa Física";
            this.rbtnPessoaFisica.UseVisualStyleBackColor = true;
            this.rbtnPessoaFisica.CheckedChanged += new System.EventHandler(this.rbtnPessoaFisica_CheckedChanged);
            // 
            // rbtnPessoaJuridica
            // 
            this.rbtnPessoaJuridica.AutoSize = true;
            this.rbtnPessoaJuridica.Location = new System.Drawing.Point(135, 20);
            this.rbtnPessoaJuridica.Name = "rbtnPessoaJuridica";
            this.rbtnPessoaJuridica.Size = new System.Drawing.Size(104, 19);
            this.rbtnPessoaJuridica.TabIndex = 17;
            this.rbtnPessoaJuridica.Text = "Pessoa Jurídica";
            this.rbtnPessoaJuridica.UseVisualStyleBackColor = true;
            this.rbtnPessoaJuridica.CheckedChanged += new System.EventHandler(this.rbtnPessoaJuridica_CheckedChanged);
            // 
            // gboxTipoPessoa
            // 
            this.gboxTipoPessoa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gboxTipoPessoa.Controls.Add(this.rbtnPessoaFisica);
            this.gboxTipoPessoa.Controls.Add(this.rbtnPessoaJuridica);
            this.gboxTipoPessoa.Location = new System.Drawing.Point(132, 37);
            this.gboxTipoPessoa.Name = "gboxTipoPessoa";
            this.gboxTipoPessoa.Size = new System.Drawing.Size(245, 45);
            this.gboxTipoPessoa.TabIndex = 18;
            this.gboxTipoPessoa.TabStop = false;
            // 
            // mtxtCpfCnpj
            // 
            this.mtxtCpfCnpj.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mtxtCpfCnpj.Location = new System.Drawing.Point(417, 101);
            this.mtxtCpfCnpj.Mask = "000\\.000\\.000-00";
            this.mtxtCpfCnpj.Name = "mtxtCpfCnpj";
            this.mtxtCpfCnpj.Size = new System.Drawing.Size(100, 23);
            this.mtxtCpfCnpj.TabIndex = 19;
            this.mtxtCpfCnpj.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mtxtCpfCnpj_MouseClick);
            this.mtxtCpfCnpj.TextChanged += new System.EventHandler(this.mtxtCpfCnpj_TextChanged);
            // 
            // lblCpfCnpj
            // 
            this.lblCpfCnpj.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCpfCnpj.AutoSize = true;
            this.lblCpfCnpj.Location = new System.Drawing.Point(373, 104);
            this.lblCpfCnpj.Name = "lblCpfCnpj";
            this.lblCpfCnpj.Size = new System.Drawing.Size(28, 15);
            this.lblCpfCnpj.TabIndex = 20;
            this.lblCpfCnpj.Text = "CPF";
            // 
            // Cadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 436);
            this.Controls.Add(this.mtxtCpfCnpj);
            this.Controls.Add(this.lblCpfCnpj);
            this.Controls.Add(this.gboxTipoPessoa);
            this.Controls.Add(this.lblVerificaNome);
            this.Controls.Add(this.mtxtTelefone);
            this.Controls.Add(this.mtxtCEP);
            this.Controls.Add(this.btnCadastrar);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.lblNumero);
            this.Controls.Add(this.txtUF);
            this.Controls.Add(this.txtCidade);
            this.Controls.Add(this.txtBairro);
            this.Controls.Add(this.txtRua);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblUF);
            this.Controls.Add(this.lblCidade);
            this.Controls.Add(this.lblBairro);
            this.Controls.Add(this.lblRua);
            this.Controls.Add(this.lblCEP);
            this.Controls.Add(this.lblTelefone);
            this.Controls.Add(this.lblNome);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Cadastro";
            this.ShowIcon = false;
            this.Text = "Cadastro";
            this.Load += new System.EventHandler(this.Cadastro_Load);
            this.gboxTipoPessoa.ResumeLayout(false);
            this.gboxTipoPessoa.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblTelefone;
        private System.Windows.Forms.Label lblCEP;
        private System.Windows.Forms.Label lblRua;
        private System.Windows.Forms.Label lblBairro;
        private System.Windows.Forms.Label lblCidade;
        private System.Windows.Forms.Label lblUF;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.TextBox txtRua;
        private System.Windows.Forms.TextBox txtBairro;
        private System.Windows.Forms.TextBox txtCidade;
        private System.Windows.Forms.TextBox txtUF;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Button btnCadastrar;
        private System.Windows.Forms.MaskedTextBox mtxtCEP;
        private System.Windows.Forms.MaskedTextBox mtxtTelefone;
        private System.Windows.Forms.Label lblVerificaNome;
        private System.Windows.Forms.RadioButton rbtnPessoaFisica;
        private System.Windows.Forms.RadioButton rbtnPessoaJuridica;
        private System.Windows.Forms.GroupBox gboxTipoPessoa;
        private System.Windows.Forms.MaskedTextBox mtxtCpfCnpj;
        private System.Windows.Forms.Label lblCpfCnpj;
    }
}