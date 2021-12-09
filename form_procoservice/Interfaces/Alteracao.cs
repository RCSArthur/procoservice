using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using System.Windows.Forms;
using form_procoservice.Utils;
using System.IO;

namespace form_procoservice.Interfaces
{
    public partial class Alteracao : Form
    {
        private readonly FirestoreDb _fireDb = FirebaseService.Conectar();
        private string telefone;



        public Alteracao(FirestoreDb firestoreDb)
        {
            _fireDb = firestoreDb;

        }
        public Alteracao(string cpfCnpj, string nome, string telefone, string rua, string numero, string bairro, string cidade, string Uf, string cep)
        {
            InitializeComponent();
            mtxtCpfCnpj.Text = cpfCnpj;
            txtNome.Text = nome;
            mtxtTelefone.Text = telefone;
            txtRua.Text = rua;
            txtNumero.Text = numero;           
            txtBairro.Text = bairro;
            txtCidade.Text = cidade;
            txtUF.Text = Uf;
            mtxtCEP.Text = cep;

        }
        private static readonly FirestoreDb firestoreDb = FirebaseService.Conectar();
        private FirestoreDb database = firestoreDb;


        async void Update_especifico()
        {

            object documento = "";

            Query cityque = database.Collection("clientes");
            QuerySnapshot snape = await cityque.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snape.Documents)
            {
                Cliente docs = docsnap.ConvertTo<Cliente>();
                if (mtxtCpfCnpj.Text == docs.cpfCnpj)
                {
                    documento = docsnap.Id;
                }
            }

                DocumentReference docref = database.Collection("clientes").Document(documento.ToString());
                Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"cpfCnpj", mtxtCpfCnpj.Text },
                {"nome",  txtNome.Text},
                {"telefone", mtxtTelefone.Text},
                {"rua", txtRua.Text },
                {"numero", txtNumero.Text},
                {"bairro", txtBairro.Text},
                {"cidade", txtCidade.Text},
                {"uf", txtUF.Text},
                {"cep", mtxtCEP.Text}
            };

                DocumentSnapshot snap = await docref.GetSnapshotAsync();
                if (snap.Exists)
                {
                    await docref.UpdateAsync(data);
                }
            MessageBox.Show("Alteração realizada com sucesso!");

            }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Update_especifico();
        }

        private void Alteracao_Load(object sender, EventArgs e)
        {
            txtNome.Focus();
            string path = AppDomain.CurrentDomain.BaseDirectory + "@cloudfire.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = firestoreDb;

        }

        protected void LocalizaCEP()
        {
            try
            {
                if (mtxtCEP.Text.Length == 9)
                {
                    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://viacep.com.br/ws/" + mtxtCEP.Text + "/json/");
                    request.AllowAutoRedirect = false;
                    System.Net.HttpWebResponse ChecaServidor = (System.Net.HttpWebResponse)request.GetResponse();

                    if (ChecaServidor.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        MessageBox.Show("Servidor indisponível");
                        return;
                    }

                    using Stream webStream = ChecaServidor.GetResponseStream();
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new(webStream))
                        {
                            string response = responseReader.ReadToEnd();
                            response = System.Text.RegularExpressions.Regex.Replace(response, "[{},]", string.Empty);
                            response = response.Replace("\"", "");

                            String[] substrings = response.Split('\n');

                            int cont = 0;
                            foreach (var substring in substrings)
                            {
                                if (cont == 1)
                                {
                                    string[] valor = substring.Split(":".ToCharArray());
                                    if (valor[0] == "  erro")
                                    {
                                        MessageBox.Show("CEP não encontrado");
                                        mtxtCEP.Focus();
                                        return;
                                    }
                                }
                                //Logradouro
                                if (cont == 2)
                                {
                                    string[] valor = substring.Split(":".ToCharArray());
                                    txtRua.Invoke((MethodInvoker)delegate
                                    {
                                        txtRua.Text = valor[1];
                                    });
                                }
                                //Bairro
                                if (cont == 4)
                                {
                                    string[] valor = substring.Split(":".ToCharArray());
                                    txtBairro.Invoke((MethodInvoker)delegate
                                    {
                                        txtBairro.Text = valor[1];
                                    });
                                }
                                //Localidade (Cidade)
                                if (cont == 5)
                                {
                                    string[] valor = substring.Split(":".ToCharArray());
                                    txtCidade.Invoke((MethodInvoker)delegate
                                    {
                                        txtCidade.Text = valor[1];
                                    });
                                }
                                //Estado (UF)
                                if (cont == 6)
                                {
                                    string[] valor = substring.Split(":".ToCharArray());
                                    txtUF.Invoke((MethodInvoker)delegate
                                    {
                                        txtUF.Text = valor[1];
                                    });
                                }
                                cont++;
                            }
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show("Erro ao buscar CEP!\n" + e);
            }
        }
        private void mtxtCEP_Leave(object sender, EventArgs e)
        {
            
        }

        private void mtxtCEP_Leave_1(object sender, EventArgs e)
        {
            LocalizaCEP();
        }

        private void mtxtTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            telefone = mtxtTelefone.Text.ReplaceNumeros();
            mtxtTelefone.Invoke((MethodInvoker)delegate
            {
                mtxtTelefone.Mask = "(00) 0000-0000";
            });
            if (telefone.Length > 2)
            {
                string ddd = telefone.Substring(0, 2);
                if (telefone[(telefone.IndexOfAny(ddd.ToCharArray()) + 2)..].IndexOf("9") == 0)
                {
                    mtxtTelefone.Invoke((MethodInvoker)delegate
                    {
                        mtxtTelefone.Mask = "(00) 00000-0000";
                    });
                    if (telefone.Length > 10)
                        mtxtTelefone.Invoke((MethodInvoker)delegate
                        {
                            mtxtTelefone.Text = telefone;
                        });
                }
            }
            mtxtTelefone.Telefone(telefone);
        }

        private void mtxtTelefone_MouseClick(object sender, MouseEventArgs e)
        {
            telefone = mtxtTelefone.Text.ReplaceNumeros();
            mtxtTelefone.Telefone(telefone);
        }
    }
}
