using form_procoservice.Interfaces.Clientes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace form_procoservice
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnHistorico_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Historico());
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Cadastro());
        }

        private void btnServicos_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Servicos());
        }

        private void btnOrcamento_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Orcamento());
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Clientes());
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        private void panelSideMenu_Paint(object sender, PaintEventArgs e) { }

        private void Menu_Load(object sender, EventArgs e) { }

        private Form activeForm = null;
        private void OpenChildForm(Form childForm)
        {
            activeForm?.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
    }
}
