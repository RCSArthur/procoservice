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
            servicos.Columns.Add("cpfCnpj");
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
                    if (docsnap.Exists && docs.cliente.Contains(txtNome.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        servicos.Rows.Add(docs.descricao, docs.cliente, docs.cpfCnpj, docs.data_inicio, docs.data_termino, docs.forma_pagamento, docs.material, docs.prazo_entrada, docs.prazo_pagamento, docs.valor);
                    }
                }
                return dgDados.DataSource = servicos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro\n" + ex, "Procoservice", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            int i = dgDados.CurrentRow.Index;
            int col = dgDados.CurrentCell.ColumnIndex;
            String nomeCol = dgDados.CurrentCell.OwningColumn.Name;
            object valor = "";
            object valorGet = dgDados.Rows[i].Cells[0].Value;
            object valorCliente = dgDados.Rows[i].Cells[1].Value;


            var dialogResult = MessageBox.Show("Deseja excluir o serviço "+valorGet.ToString()+ " do cliente "+ valorCliente.ToString()+"?", "Procoserivce", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    var query = _database.Collection("servicos");
                    var snapquery = await query.GetSnapshotAsync();

                    foreach (var docsnap in snapquery.Documents)
                    {
                        var docs = docsnap.ConvertTo<Servico>();
                        if (docsnap.Exists && docs.descricao.Contains(valorGet.ToString(), StringComparison.OrdinalIgnoreCase))
                        {

                            var docref = _database.Collection("servicos").Document(docsnap.Id);
                            await docref.DeleteAsync();

                            MessageBox.Show("Servico " + valorGet.ToString() + " excluído!", "Procoservice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgDados.DataSource = null;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro\n" + ex, "Procoservice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        string descricao;
        string cliente;
        string cpfCnpj;
        string material;
        double valor2;
        string data_inicio;
        string data_termino;
        string prazo_entrada;
        string prazo_pagamento;
        string forma_pagamento;

        async void LevaInformacao()
        {


            int i = dgDados.CurrentRow.Index;
            int col = dgDados.CurrentCell.ColumnIndex;
            String nomeCol = dgDados.CurrentCell.OwningColumn.Name;
            object valor = "";
            object valorGet = dgDados.Rows[i].Cells[2].Value;
            object documento = "";

            Query cityque = _database.Collection("servicos");
            QuerySnapshot snape = await cityque.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snape.Documents)
            {
                Servico docs = docsnap.ConvertTo<Servico>();
                if (valorGet.ToString() == docs.cpfCnpj)
                {

                    descricao = docs.descricao;
                    cliente= docs.cliente;
                    cpfCnpj = docs.cpfCnpj;
                    material = docs.material;
                    valor2 = docs.valor;
                    data_inicio = docs.data_inicio;
                    data_termino = docs.data_termino;
                    prazo_entrada = docs.prazo_entrada;
                    prazo_pagamento = docs.prazo_pagamento;
                    forma_pagamento = docs.forma_pagamento;
                    
                }

            }

            Interfaces.Alterarservico alterar = new Interfaces.Alterarservico(descricao, cliente, cpfCnpj, material, valor2, data_inicio, data_termino, prazo_entrada, prazo_pagamento, forma_pagamento);
            alterar.Show();



        }



        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {

            LevaInformacao();
        }

        private void dgDados_Click(object sender, EventArgs e)
        {
        }

        private void dgDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAlterar.Enabled = true;

        }
    }
}
