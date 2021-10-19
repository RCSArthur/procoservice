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

        private void button1_Click(object sender, EventArgs e)
        {
            Listar_Materiais();
        }

        private async System.Threading.Tasks.Task<object> Listar_Materiais()
        {
            Query query = _fireDb.Collection("materiais");
            QuerySnapshot snapquery = await query.GetSnapshotAsync();

            DataTable materiais = new();
            materiais.Columns.Add("Descrição");
            materiais.Columns.Add("Quantidade");
            materiais.Columns.Add("Preço unitário");
            materiais.Columns.Add("Preço total");

            foreach (DocumentSnapshot docsnap in snapquery.Documents)
            {
                Material docs = docsnap.ConvertTo<Material>();
                if(docsnap.Exists && docs.descricao.Contains(txtNome.Text, StringComparison.OrdinalIgnoreCase))
                {
                   materiais.Rows.Add(docs.descricao, docs.quantidade, docs.precoUnitario, docs.precoTotal);
                }
            }

            return dgDados.DataSource = materiais;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
