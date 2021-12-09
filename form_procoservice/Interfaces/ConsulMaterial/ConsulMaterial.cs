using form_procoservice.Domain.Material;
using form_procoservice.Utils;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

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

     

        string descricao;
        string qtd;
        string precoUnit;
        string precoTotal;


        async void LevaInformacao()
        {

            try
            {
                int i = dgDados.CurrentRow.Index;
                int col = dgDados.CurrentCell.ColumnIndex;
                String nomeCol = dgDados.CurrentCell.OwningColumn.Name;
                object valor = "";
                object valorGet = dgDados.Rows[i].Cells[0].Value;
                object documento = "";

                Query cityque = database.Collection("materiais");
                QuerySnapshot snape = await cityque.GetSnapshotAsync();
                foreach (DocumentSnapshot docsnap in snape.Documents)
                {
                    Material docs = docsnap.ConvertTo<Material>();
                    if (valorGet.ToString() == docs.descricao)
                    {
                        descricao = docs.descricao;
                        qtd = docs.quantidade.ToString();
                        precoUnit = docs.precoUnitario.ToString();
                        precoTotal = docs.precoTotal.ToString();

                    }

                }

                Interfaces.Alterarmaterial alteraMaterial = new Interfaces.Alterarmaterial(descricao, qtd, precoUnit, precoTotal);
                alteraMaterial.Show();
            }

            catch(Exception ex)
            {
                MessageBox.Show("Por favor, selecione o material que deseja alterar.");
            }

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            button3.Enabled = true;
        }
       
        private void btnAlterar_Click(object sender, EventArgs e)
        {
           
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                LevaInformacao();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
