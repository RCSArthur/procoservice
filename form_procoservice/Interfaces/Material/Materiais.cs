using form_procoservice.Utils;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading.Tasks;
using form_procoservice.Domain.Material;
using System.Globalization;

namespace form_procoservice
{
    public partial class Materiais : Form
    {
        private static readonly FirestoreDb firestoreDb = FirebaseService.Conectar();
        private FirestoreDb database;

        public Materiais() => InitializeComponent();

        private void Material_Load(object sender, EventArgs e)
        {
            database = firestoreDb;
            txtDescr.Focus();
        }

        double qtd;
        double unit;
        double total;

        private void btnCadastrar_Click(object sender, EventArgs e)
        {

            if ((this).Validar())
            {
                try
                {
                    qtd = Convert.ToDouble(txtQtd.Text, CultureInfo.InvariantCulture);
                    unit = Convert.ToDouble(txtPreco.Text, CultureInfo.InvariantCulture);
                    total = qtd * unit;
                    MessageBox.Show(unit.ToString("00.00"), total.ToString("00.00"));
                    CollectionReference coll = database.Collection("materiais");
                    Dictionary<string, object> data = new()
                    {
                        
                        { "descricao", txtDescr.Text },
                        { "quantidade", int.Parse(txtQtd.Text) },
                        { "precoUnitario", unit },
                        { "precoTotal", total },
                    };
                    coll.AddAsync(data);
                    MessageBox.Show("Material " + txtDescr.Text + " criado com sucesso!");
                    (this).Limpar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao cadastrar cliente!\n" + ex);
                }
            }

        }

        private void txtDescr_TextChanged(object sender, EventArgs e)
        {
            VerificarMaterial();
        }

        private async void VerificarMaterial()
        {
            try
            {
                Query documentos = database.Collection("materiais");
                QuerySnapshot snap = await documentos.GetSnapshotAsync();
                foreach (DocumentSnapshot docsnap in snap.Documents)
                {
                    Material docs = docsnap.ConvertTo<Material>();
                    if (docsnap.Exists)
                    {
                        if (string.Equals(docs.descricao, txtDescr.Text, StringComparison.OrdinalIgnoreCase))
                        {
                            btnCadastrar.Invoke((MethodInvoker)delegate
                            {
                                btnCadastrar.Enabled = false;
                            });
                            lblVerificaNome.Invoke((MethodInvoker)delegate
                            {
                                lblVerificaNome.Visible = true;
                                lblVerificaNome.Text = "Já cadastrado!";
                            });
                            break;
                        }
                        else
                        {
                            btnCadastrar.Invoke((MethodInvoker)delegate
                            {
                                btnCadastrar.Enabled = true;
                            });
                            lblVerificaNome.Invoke((MethodInvoker)delegate
                            {
                                lblVerificaNome.Visible = false;
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro\n" + ex);
            }
        }

        private void txtQtd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
           (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -2))
            {
                e.Handled = true;
            }

        }

        private void txtPreco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
