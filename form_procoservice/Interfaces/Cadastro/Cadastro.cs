using form_procoservice.Utils;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace form_procoservice
{
    public partial class Cadastro : Form
    {
        private static readonly FirestoreDb firestoreDb = FirebaseService.Conectar();
        private FirestoreDb database;
        private string telefone;


        public Cadastro() => InitializeComponent();

        private void Cadastro_Load(object sender, EventArgs e)
        {
            database = firestoreDb;
            txtNome.Focus();
        }

        protected void LocalizaCEP()
        {
            try
            {
                if (mtxtCEP.Text.Length == 9)
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://viacep.com.br/ws/" + mtxtCEP.Text + "/json/");
                    request.AllowAutoRedirect = false;
                    HttpWebResponse ChecaServidor = (HttpWebResponse)request.GetResponse();

                    if (ChecaServidor.StatusCode != HttpStatusCode.OK)
                    {
                        MessageBox.Show("Servidor indisponível");
                        return;
                    }

                    using Stream webStream = ChecaServidor.GetResponseStream();
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            string response = responseReader.ReadToEnd();
                            response = Regex.Replace(response, "[{},]", string.Empty);
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
                                    txtRua.Text = valor[1];
                                }
                                //Bairro
                                if (cont == 4)
                                {
                                    string[] valor = substring.Split(":".ToCharArray());
                                    txtBairro.Text = valor[1];
                                }
                                //Localidade (Cidade)
                                if (cont == 5)
                                {
                                    string[] valor = substring.Split(":".ToCharArray());
                                    txtCidade.Text = valor[1];
                                }
                                //Estado (UF)
                                if (cont == 6)
                                {
                                    string[] valor = substring.Split(":".ToCharArray());
                                    txtUF.Text = valor[1];
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
            LocalizaCEP();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if ((this).Validar())
            {
                try
                {
                    CollectionReference coll = database.Collection("clientes");
                    Dictionary<string, object> data = new()
                    {
                        { "nome", txtNome.Text },
                        { "telefone", mtxtTelefone.Text },
                        { "cep", mtxtCEP.Text },
                        { "rua", txtRua.Text },
                        { "bairro", txtBairro.Text },
                        { "cidade", txtCidade.Text },
                        { "UF", txtUF.Text },
                        { "numero", txtNumero.Text }
                    };
                    coll.AddAsync(data);
                    MessageBox.Show("Cliente " + txtNome.Text + " criado com sucesso!");
                    (this).Limpar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao cadastrar cliente!\n" + ex);
                }

            }
        }

        private void mtxtTelefone_MouseClick(object sender, MouseEventArgs e)
        {
            telefone = mtxtTelefone.Text.ReplaceNumeros();
            mtxtTelefone.Telefone(telefone);
        }

        private void mtxtCEP_MouseClick(object sender, MouseEventArgs e)
        {
            string cep = mtxtCEP.Text.ReplaceNumeros();
            mtxtCEP.Cep(cep);
        }

        async void VerificarNome()
        {
            Query documentos = database.Collection("clientes");
            QuerySnapshot snap = await documentos.GetSnapshotAsync();
            foreach (DocumentSnapshot docsnap in snap.Documents)
            {
                Cliente docs = docsnap.ConvertTo<Cliente>();
                if (docsnap.Exists)
                {
                    if (string.Equals(docs.nome, txtNome.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        btnCadastrar.Enabled = false;
                        lblVerificaNome.Visible = true;
                        lblVerificaNome.Text = "Nome já cadastrado, insira outro";
                        break;
                    }
                    else
                    {
                        btnCadastrar.Enabled = true;
                        lblVerificaNome.Visible = false;
                    }
                }
            }
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            VerificarNome();
        }

        private void rbtnPessoaFisica_CheckedChanged(object sender, EventArgs e)
        {
            lblCpfCnpj.Text = "CPF";
            mtxtCpfCnpj.Mask = @"000\.000\.000-00";
            lblNome.Text = "Nome";
        }

        private void rbtnPessoaJuridica_CheckedChanged(object sender, EventArgs e)
        {
            lblCpfCnpj.Text = "CNPJ";
            mtxtCpfCnpj.Mask = @"00\.000\.000/0000-00";
            lblNome.Text = "Razão Social";
        }

        private void mtxtTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            telefone = mtxtTelefone.Text.ReplaceNumeros();
            mtxtTelefone.Mask = "(00) 0000-0000";
            if (telefone.Length > 2)
            {
                string ddd = telefone.Substring(0, 2);
                if (telefone[(telefone.IndexOfAny(ddd.ToCharArray()) + 2)..].IndexOf("9") == 0)
                {
                    mtxtTelefone.Mask = "(00) 00000-0000";
                    if (telefone.Length > 10)
                        mtxtTelefone.Text = telefone;
                }
            }
            mtxtTelefone.Telefone(telefone);
        }
    }
}
