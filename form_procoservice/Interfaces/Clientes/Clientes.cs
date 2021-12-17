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

        private static readonly FirestoreDb firestoreDb = FirebaseService.Conectar();
        private FirestoreDb database = firestoreDb;

        public Clientes() => InitializeComponent();

        private void Clientes_Load(object sender, EventArgs e)
        {
            txtNome.Focus();
            string path = AppDomain.CurrentDomain.BaseDirectory + "@cloudfire.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = firestoreDb;
        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            await Listar_Clientes();
        }

        //Função para pegar dados de cliente e retornar como tabela
        private async System.Threading.Tasks.Task<object> Listar_Clientes()
        {
            //Conexão com o Banco de dados
            Query query = _fireDb.Collection("clientes");
            QuerySnapshot snapquery = await query.GetSnapshotAsync();

            //criação de um objeto do tipo Tabela de dados e adicionando colunas conforme a classe Cliente.cs
            DataTable clientes = new();
            clientes.Columns.Add("nome");
            clientes.Columns.Add("cpfCnpj");
            clientes.Columns.Add("telefone");
            clientes.Columns.Add("rua");
            clientes.Columns.Add("numero");
            clientes.Columns.Add("bairro");
            clientes.Columns.Add("cidade");
            clientes.Columns.Add("Uf");
            clientes.Columns.Add("cep");
            clientes.Columns.Add("ClienteAtivo");

            foreach (DocumentSnapshot docsnap in snapquery.Documents)
            {
                Cliente docs = docsnap.ConvertTo<Cliente>();
                if (docsnap.Exists && docs.nome.Contains(txtNome.Text, StringComparison.OrdinalIgnoreCase))
                {
                    clientes.Rows.Add(docs.nome, docs.cpfCnpj, docs.telefone, docs.rua, docs.numero, docs.bairro, docs.cidade, docs.UF, docs.cep, docs.ClienteAtivo);
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
            button2.Enabled = true;
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private async void dgDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dialogResult = MessageBox.Show("Deseja atribuir para que o cliente seja inativo?", "Procoservice", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            int i = dgDados.CurrentRow.Index;
            int col = dgDados.CurrentCell.ColumnIndex;
            String nomeCol = dgDados.CurrentCell.OwningColumn.Name;
            object valor = "";
            object valorGet = dgDados.Rows[i].Cells[1].Value;
            object documento = "";
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    Query query = _fireDb.Collection("clientes");
                    QuerySnapshot snapquery = await query.GetSnapshotAsync();
                    string cpfCnpj = dgDados.Rows[e.RowIndex].Cells["cpfCnpj"].Value.ToString();
                    foreach (DocumentSnapshot docsnap in snapquery.Documents)
                    {
                        Cliente docs = docsnap.ConvertTo<Cliente>();
                        if (docsnap.Exists && docs.cpfCnpj.Contains(valorGet.ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            Dictionary<string, object> data = new Dictionary<string, object>()
                            {
                                { "ClienteAtivo", "Falso" }
                            };
                            DocumentReference docref = _fireDb.Collection("clientes").Document(docsnap.Id);
                            await docref.UpdateAsync(data);

                            MessageBox.Show("Cliente " + docs.nome + " inativado!", "Procoservice", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dgDados_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        String cpfCnpj;
        String nome;
        String telefone;
        String rua;
        String numero;
        String bairro;
        String cidade;
        String Uf;
        String cep;
        String excluido;
        async void LevaInformacao()
        {
           
            
            int i = dgDados.CurrentRow.Index;
            int col = dgDados.CurrentCell.ColumnIndex;
            String nomeCol = dgDados.CurrentCell.OwningColumn.Name;
            object valor = "";
            object valorGet = dgDados.Rows[i].Cells[1].Value;
            object documento = "";

            Query cityque = database.Collection("clientes");
            QuerySnapshot snape = await cityque.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snape.Documents)
            {
                Cliente docs = docsnap.ConvertTo<Cliente>();
                if (valorGet.ToString() == docs.cpfCnpj)
                {

                    cpfCnpj = docs.cpfCnpj;
                    nome = docs.nome;
                    telefone = docs.telefone;
                    rua = docs.rua;
                    numero = docs.numero;
                    bairro = docs.bairro;
                    cidade = docs.cidade;
                    Uf = docs.UF;
                    cep = docs.cep;
                }

            }

            Alteracao alterao = new Alteracao(cpfCnpj, nome, telefone, rua, numero, bairro, cidade, Uf, cep);
            alterao.Show();

            

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                LevaInformacao();
            }

            catch(Exception ex)
            {
                MessageBox.Show("Selecione o cliente que deseja alterar.","Procoservice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Clientes_Load_1(object sender, EventArgs e)
        {

        }

        private async void deletar_selecionado()
        {
            int i = dgDados.CurrentRow.Index;
            int col = dgDados.CurrentCell.ColumnIndex;
            String nomeCol = dgDados.CurrentCell.OwningColumn.Name;
            object valor = "";
            object valorGet = dgDados.Rows[i].Cells[0].Value;
            object valorCpfCnpj = dgDados.Rows[i].Cells[1].Value;


            var dialogResult = MessageBox.Show("Deseja excluir o cliente "+ valorGet.ToString()+"?", "Procoservice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    var query = database.Collection("clientes");
                    var snapquery = await query.GetSnapshotAsync();

                    foreach (var docsnap in snapquery.Documents)
                    {
                        var docs = docsnap.ConvertTo<Cliente>();
                        if (docsnap.Exists && docs.cpfCnpj.Contains(valorCpfCnpj.ToString(), StringComparison.OrdinalIgnoreCase))
                        {

                            var docref = database.Collection("clientes").Document(docsnap.Id);
                            await docref.DeleteAsync();

                            MessageBox.Show("Cliente " + valorGet.ToString() + " excluído!", "Procoservice", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void button3_Click(object sender, EventArgs e)
        {
            deletar_selecionado();
        }
    }
}
