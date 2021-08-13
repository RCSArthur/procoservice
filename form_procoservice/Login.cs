using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace form_procoservice
{
    public partial class Login : Form
    {
        private const string ADMIN = "admin";
        private FirestoreDb database;

        public Login()
        {
            InitializeComponent();
        }
        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                string PATH = AppDomain.CurrentDomain.BaseDirectory + "procoservice-firebase.json";
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", PATH);

                database = FirestoreDb.Create("procoservice-d428b");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falha ao conectar com o Firebase:\n{ex}");
            }
        }

        void addDocWithAutoID()
        {
            try
            {
                CollectionReference coll = database.Collection("addDocWithAutoID");
                Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"First Name", "tacv"},
                {"Last Name", "amazing" }
            };
                coll.AddAsync(data);
                MessageBox.Show("Dados adicionados com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inserir dados!\n{ex}");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == ADMIN
                && txtSenha.Text == ADMIN)
            {
                addDocWithAutoID();
                Menu menu = new Menu();
                this.Hide();
                menu.ShowDialog();
            }

        }

    }
}