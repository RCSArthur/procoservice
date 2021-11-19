using form_procoservice.Domain.Servico;
using form_procoservice.Utils;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace form_procoservice
{
    public partial class Historico : Form
    {
        public Historico() => InitializeComponent();

        private static readonly FirestoreDb _firestoreDb = FirebaseService.Conectar();
        private FirestoreDb _database = _firestoreDb;

        private void Historico_Load(object sender, EventArgs e)
        {
            _database = _firestoreDb;
            txtNome.Focus();
            //string path = AppDomain.CurrentDomain.BaseDirectory + "@cloudfire.json";
            //Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Listar_Servicos();
        }

        private async System.Threading.Tasks.Task<object> Listar_Servicos()
        {
            Query query = _database.Collection("servicos");
            QuerySnapshot snapquery = await query.GetSnapshotAsync();

            DataTable servicos = new();
            servicos.Columns.Add("descricao");
            servicos.Columns.Add("cliente");
            servicos.Columns.Add("data_inicio");
            servicos.Columns.Add("data_termino");
            servicos.Columns.Add("forma_pagamento");
            servicos.Columns.Add("material");
            servicos.Columns.Add("prazo_entrada");
            servicos.Columns.Add("prazo_pagamento");
            servicos.Columns.Add("valor");

            try
            {
                foreach (DocumentSnapshot docsnap in snapquery.Documents)
                {
                    Servico docs = docsnap.ConvertTo<Servico>();
                    if (docsnap.Exists && docs.descricao.Contains(txtNome.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        servicos.Rows.Add(docs.descricao, docs.cliente, docs.data_inicio, docs.data_termino, docs.forma_pagamento, docs.material, docs.prazo_entrada, docs.prazo_pagamento, docs.valor);
                    }
                }
                return dgDados.DataSource = servicos;
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

            var dialogResult = MessageBox.Show("Deseja excluir o serviço?", "Aviso", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    var query = _database.Collection("servicos");
                    var snapquery = await query.GetSnapshotAsync();

                    foreach (var docsnap in snapquery.Documents)
                    {
                        var docs = docsnap.ConvertTo<Servico>();
                        if (docsnap.Exists && docs.descricao.Contains(txtNome.Text, StringComparison.OrdinalIgnoreCase))
                        {

                            var docref = _database.Collection("servicos").Document(docsnap.Id);
                            await docref.DeleteAsync();

                            MessageBox.Show("Servico " + docs.descricao + " excluído!");
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

            Query cityque = _database.Collection("servicos");
            QuerySnapshot snape = await cityque.GetSnapshotAsync();

            try
            {
                foreach (DocumentSnapshot docsnap in snape.Documents)
                {
                    Servico docs = docsnap.ConvertTo<Servico>();
                    if (valorGet.ToString().Equals(docs.descricao, StringComparison.OrdinalIgnoreCase))
                    {
                        documento = docsnap.Id;
                    }
                }

                DocumentReference docref = _database.Collection("servicos").Document(documento.ToString());
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
