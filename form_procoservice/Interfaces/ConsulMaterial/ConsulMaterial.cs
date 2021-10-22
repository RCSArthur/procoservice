using System;
using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using form_procoservice.Utils;
using form_procoservice.Domain.Material;

namespace form_procoservice.interfaces.ConsulMaterial
{
    public partial class ConsulMaterial : Form
    {
        private readonly FirestoreDb _fireDb = FirebaseService.Conectar();

        public ConsulMaterial(FirestoreDb firestoreDb)
        {
            _fireDb = firestoreDb;
        }

        private static readonly FirestoreDb firestoreDb = FirebaseService.Conectar();
        private FirestoreDb database = firestoreDb;
        public ConsulMaterial() => InitializeComponent();


        private void ConsulMaterial_Load(object sender, EventArgs e)
        {
            txtNome.Focus();
            string path = AppDomain.CurrentDomain.BaseDirectory + "@cloudfire.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = firestoreDb;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Listar_Materiais();
        }

        private async System.Threading.Tasks.Task<object> Listar_Materiais()
        {
            Query query = _fireDb.Collection("materiais");
            QuerySnapshot snapquery = await query.GetSnapshotAsync();

            DataTable materiais = new();
            materiais.Columns.Add("descricao");
            materiais.Columns.Add("quantidade");
            materiais.Columns.Add("precoUnitario");
            materiais.Columns.Add("precoTotal");

            try
            {
                foreach (DocumentSnapshot docsnap in snapquery.Documents)
                {
                    Material docs = docsnap.ConvertTo<Material>();
                    if (docsnap.Exists && docs.descricao.Contains(txtNome.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        materiais.Rows.Add(docs.descricao, docs.quantidade, docs.precoUnitario, docs.precoTotal);
                    }
                }

                return dgDados.DataSource = materiais;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro\n" + ex);
            }
            return null;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            deletar_selecionado();
        }

        private async void deletar_selecionado()
        {

            var dialogResult = MessageBox.Show("Deseja excluir o material?", "Aviso", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    var query = _fireDb.Collection("materiais");
                    var snapquery = await query.GetSnapshotAsync();

                    foreach (var docsnap in snapquery.Documents)
                    {
                        var docs = docsnap.ConvertTo<Material>();
                        if (docsnap.Exists && docs.descricao.Contains(txtNome.Text, StringComparison.OrdinalIgnoreCase))
                        {

                            var docref = _fireDb.Collection("materiais").Document(docsnap.Id);
                            await docref.DeleteAsync();

                            MessageBox.Show("Material " + docs.descricao + " excluído!");
                            dgDados.DataSource = null;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro\n" + ex);
                }
            }
        }

        async void Update_especifico()
        {
            int i = dgDados.CurrentRow.Index;
            int col = dgDados.CurrentCell.ColumnIndex;
            string nomeCol = dgDados.CurrentCell.OwningColumn.Name;
            object valorGet = dgDados.Rows[i].Cells[0].Value;
            object valor = txtAlterar.Text;
            if (!nomeCol.Equals("descricao"))
            {
                valor = int.Parse(valor.ToString());
            }
            object documento = "";

            Query cityque = database.Collection("materiais");
            QuerySnapshot snape = await cityque.GetSnapshotAsync();

            try
            {
                foreach (DocumentSnapshot docsnap in snape.Documents)
                {
                    Material docs = docsnap.ConvertTo<Material>();
                    if (valorGet.ToString().Equals(docs.descricao, StringComparison.OrdinalIgnoreCase))
                    {
                        documento = docsnap.Id;
                    }
                }

                DocumentReference docref = database.Collection("materiais").Document(documento.ToString());
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    { nomeCol,  valor }
                };

                DocumentSnapshot snap = await docref.GetSnapshotAsync();
                if (snap.Exists)
                {
                    await docref.UpdateAsync(data);
                    txtAlterar.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro\n" + ex);
            }

        }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            Update_especifico();
        }
    }
}
