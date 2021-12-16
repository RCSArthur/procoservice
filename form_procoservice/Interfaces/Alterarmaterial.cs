using form_procoservice.Domain.Material;
using form_procoservice.Utils;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace form_procoservice.Interfaces
{
    public partial class Alterarmaterial : Form
    {
        private readonly FirestoreDb _fireDb = FirebaseService.Conectar();


        public Alterarmaterial(FirestoreDb firestoreDb)
        {
            _fireDb = firestoreDb;
        }

        string valorAntigo;
        public Alterarmaterial(string descricao, string qtd, string precoUnit, string precoTotal)
        {
            InitializeComponent();
            txtDescr.Text = descricao;
            txtQtd.Text = qtd;
            txtPreco.Text = precoUnit;
            txrPrecoTotal.Text = precoTotal;
            valorAntigo = txtDescr.Text;
        }

        
        private static readonly FirestoreDb firestoreDb = FirebaseService.Conectar();
        private FirestoreDb database = firestoreDb;

        async void Update_especifico()
        {

            object documento = "";

            Query cityque = database.Collection("materiais");
            QuerySnapshot snape = await cityque.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snape.Documents)
            {
                Material docs = docsnap.ConvertTo<Material>();
                if (valorAntigo.ToString().Equals(docs.descricao, StringComparison.OrdinalIgnoreCase))
                {
                    documento = docsnap.Id;
                }
            }

            DocumentReference docref = database.Collection("materiais").Document(documento.ToString());
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"descricao", txtDescr.Text },
                {"quantidade",  Int32.Parse(txtQtd.Text)},
                {"precoUnitario", double.Parse(txtPreco.Text)},
                {"precoTotal", double.Parse(txrPrecoTotal.Text)},

            };

            DocumentSnapshot snap = await docref.GetSnapshotAsync();
            if (snap.Exists)
            {
                await docref.UpdateAsync(data);
            }
            MessageBox.Show("Alteração realizada com sucesso!","Procoservice", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void Alterarmaterial_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Update_especifico();
        }

        private void txtPreco_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPreco_Leave(object sender, EventArgs e)
        {
            try
            {
                double precoUnit = double.Parse(txtPreco.Text);
                int qtd = Int32.Parse(txtQtd.Text);

                double precoTotal = precoUnit * qtd;

                txrPrecoTotal.Text = precoTotal.ToString();
            }

            catch(Exception ex)
            {
                MessageBox.Show("Preencha o campo de quantidade e valor!", "Procoservice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
