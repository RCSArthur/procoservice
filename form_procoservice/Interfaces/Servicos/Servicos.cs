using System;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using form_procoservice.Utils;
using System.Collections.Generic;
using System.Data;
using form_procoservice.Domain.Material;

namespace form_procoservice
{
    public partial class Servicos : Form
    {
        private readonly FirestoreDb _fireDb = FirebaseService.Conectar();


        public Servicos(FirestoreDb firestoreDb)
        {
            _fireDb = firestoreDb;
        }

        private static readonly FirestoreDb firestoreDb = FirebaseService.Conectar();
        private FirestoreDb database = firestoreDb;

        public Servicos() => InitializeComponent();

        private async System.Threading.Tasks.Task<object> Listar_Clientes()
        {
            Query query = _fireDb.Collection("clientes");
            QuerySnapshot snapquery = await query.GetSnapshotAsync();

            DataTable clientes = new();
            clientes.Columns.Add("nome");
            cmbCliente.ValueMember = "nome";
            cmbCliente.DataSource = clientes;

            foreach (DocumentSnapshot docsnap in snapquery.Documents)
            {
                Cliente docs = docsnap.ConvertTo<Cliente>();
                if (docsnap.Exists)
                {
                    clientes.Rows.Add(docs.nome);
                }
            }
            return cmbCliente.DataSource = clientes; ;
        }

        private async System.Threading.Tasks.Task<object> Listar_Materiais()
        {

            Query query = _fireDb.Collection("materiais");
            QuerySnapshot snapquery = await query.GetSnapshotAsync();

            DataTable materiais = new();
            materiais.Columns.Add("descricao");
            chkListMaterial.ValueMember = "descricao";
            chkListMaterial.DataSource = materiais;

            foreach (DocumentSnapshot docsnap in snapquery.Documents)
            {
                Material docs = docsnap.ConvertTo<Material>();
                if (docsnap.Exists)
                {
                    materiais.Rows.Add(docs.descricao);
                }
            }
            return chkListMaterial.DataSource = materiais;

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Servicos_Load(object sender, EventArgs e)
        {
            Listar_Clientes();
            Listar_Materiais();
            dtTermino.MinDate = dtInicio.Value;
            dtPagamento.MinDate = dtEntrega.Value;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            int resul = DateTime.Compare(dtInicio.Value, dtTermino.Value);
            int pagMenorTermino = DateTime.Compare(dtEntrega.Value, dtTermino.Value);

            if (resul > 0)
            {
                MessageBox.Show("Data de inicio/termino inválida.", "Procoservice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else if (pagMenorTermino < 0)
            {
                MessageBox.Show("Data de entrega inválida.", "Procoservice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else if (!(dtPagamento.Value < dtEntrega.Value && dtPagamento.Value > dtInicio.Value))
            {
                MessageBox.Show("Data de pagamento inválida.", "Procoservice", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

            else
            {
                if ((this).Validar())
                {
                    try
                    {
                        List<string> termsList = new List<string>();
                        foreach (var item in chkListMaterial.CheckedItems)
                            termsList.Add(((DataRowView)item)["descricao"].ToString());
                        var result = String.Join(", ", termsList.ToArray());
                        CollectionReference coll = database.Collection("servicos");
                        Dictionary<string, object> data = new()
                        {
                            { "descricao", txtDescricao.Text },
                            { "cliente", cmbCliente.Text },
                            { "cpfCnpj", textBox1.Text },
                            { "material", result },
                            { "valor", double.Parse(txtValor.Text) },
                            { "data_inicio", dtInicio.Value.ToString("dd/MM/yyyy") },
                            { "data_termino", dtTermino.Value.ToString("dd/MM/yyyy") },
                            { "prazo_entrada", dtEntrega.Value.ToString("dd/MM/yyyy") },
                            { "prazo_pagamento", dtPagamento.Value.ToString("dd/MM/yyyy") },
                            { "forma_pagamento", cmbFormaPagamento.Text },
                        };
                        coll.AddAsync(data);
                        MessageBox.Show("Serviço cadastrado com sucesso!", "Procoservice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        (this).Limpar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao cadastrar cliente!\n" + ex, "Procoservice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                    (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void cmbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dtTermino_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtPagamento_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtInicio_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtValor_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbMateriais_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtEntrega_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbFormaPagamento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtDescricao_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private async void Listar_CpfCnpj()
        {
            Query query = _fireDb.Collection("clientes");
            QuerySnapshot snapquery = await query.GetSnapshotAsync();


            string item = cmbCliente.SelectedValue.ToString();
            foreach (DocumentSnapshot docsnap in snapquery.Documents)
            {
                Cliente docs = docsnap.ConvertTo<Cliente>();
                if (item == docs.nome)
                {
                    textBox1.Text = docs.cpfCnpj.ToString();
                }
            }

        }
        private void cmbCliente_Leave(object sender, EventArgs e)
        {
            Listar_CpfCnpj();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkListMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
