using form_procoservice.Utils;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace form_procoservice.Interfaces.Clientes
{
    public partial class Clientes : Form
    {

        private readonly FirestoreDb _fireDb = FirebaseService.Conectar();

        //Construtor para o Banco de Dados
        public Clientes(FirestoreDb firestoreDb)
        {
            _fireDb = firestoreDb;
        }

        public Clientes() => InitializeComponent();

        private void Clientes_Load(object sender, EventArgs e)
        {
            txtNome.Focus();
        }

        private void button1_ClickAsync(object sender, EventArgs e)
        {
            Listar_Clientes();
        }

        //Função para pegar dados de cliente e retornar como tabela
        private async System.Threading.Tasks.Task<object> Listar_Clientes()
        {
            //Conexão com o Banco de dados
            Query query = _fireDb.Collection("clientes");
            QuerySnapshot snapquery = await query.GetSnapshotAsync();

            //criação de um objeto do tipo Tabela de dados e adicionando colunas conforme a classe Cliente.cs
            DataTable clientes = new();
            clientes.Columns.Add("Nome");
            clientes.Columns.Add("CpfCnpj");
            clientes.Columns.Add("Telefone");
            clientes.Columns.Add("Rua");
            clientes.Columns.Add("Número");
            clientes.Columns.Add("Bairro");
            clientes.Columns.Add("Cidade");
            clientes.Columns.Add("UF");
            clientes.Columns.Add("CEP");
            clientes.Columns.Add("Excluido");

            foreach (DocumentSnapshot docsnap in snapquery.Documents)
            {
                Cliente docs = docsnap.ConvertTo<Cliente>();
                if (docsnap.Exists && docs.nome.Contains(txtNome.Text, StringComparison.OrdinalIgnoreCase))
                {
                    clientes.Rows.Add(docs.nome, docs.cpfCnpj, docs.telefone, docs.rua, docs.numero, docs.bairro, docs.cidade, docs.UF, docs.cep, docs.isExcluido);
                }
            }
            //Retorna atribuição de dados do objeto clientes(DataTable) para o dgDados(DataGridView)
            return dgDados.DataSource = clientes;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private async void dgDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dialogResult = MessageBox.Show("Deseja setar o cliente como excluído?", "Aviso", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    Query query = _fireDb.Collection("clientes").WhereEqualTo("isExcluido", false);
                    QuerySnapshot snapquery = await query.GetSnapshotAsync();
                    string cpfCnpj = dgDados.Rows[e.RowIndex].Cells["cpfCnpj"].Value.ToString();
                    foreach (DocumentSnapshot docsnap in snapquery.Documents)
                    {
                        Cliente docs = docsnap.ConvertTo<Cliente>();
                        if (docsnap.Exists && docs.cpfCnpj.Contains(cpfCnpj, StringComparison.OrdinalIgnoreCase))
                        {
                            Dictionary<string, object> data = new Dictionary<string, object>()
                            {
                                { "isExcluido", true }
                            };
                            DocumentReference docref = _fireDb.Collection("clientes").Document(docsnap.Id);
                            await docref.UpdateAsync(data);

                            MessageBox.Show("Cliente " + docs.nome + " excluído!");
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
    }
}
