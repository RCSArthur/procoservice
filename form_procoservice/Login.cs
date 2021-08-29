using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;

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

        IFirebaseConfig ifc = new FirebaseConfig() {
            AuthSecret = "2dlvEYvOuhdvxZlgSAmPHAXMfoODfi3rDaTZT9t3",
            BasePath="https://procoservice-d428b-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        private void Login_Load(object sender, EventArgs e)
        {

            try
            {
                client = new FireSharp.FirebaseClient(ifc);
            }

            catch
            {
                MessageBox.Show("Sem conexão com a internet ou problema de conexão.");
            }
        }




        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) && string.IsNullOrEmpty(txtSenha.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
            }

            else
            {
                FirebaseResponse res = client.Get(@"Usuario/" + "1");
                Usuario ResUser = res.ResultAs<Usuario>(); //resultado da query
                if (ResUser.Email == txtEmail.Text && ResUser.Senha == txtSenha.Text)
                {
                    Menu menu = new Menu();
                    this.Hide();
                    menu.ShowDialog();
                }

                else
                {
                    MessageBox.Show("E-mail ou senha incorretos, por favor tente novamente.");
                }
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) && string.IsNullOrEmpty(txtSenha.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
            }

            Usuario usuario = new Usuario()
            {
                //addDocWithAutoID();
                Email = txtEmail.Text,
                Senha = txtSenha.Text
            };

            var setter = client.Set("Usuario/" + "1", usuario);
            MessageBox.Show("Usuario registrado com sucesso!");
        }
    }
}