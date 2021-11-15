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
            cmbMateriais.ValueMember = "descricao";
            cmbMateriais.DataSource = materiais;

            foreach (DocumentSnapshot docsnap in snapquery.Documents)
            {
                Material docs = docsnap.ConvertTo<Material>();
                if (docsnap.Exists)
                {
                    materiais.Rows.Add(docs.descricao);
                }
            }
            return cmbMateriais.DataSource = materiais;

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
            if ((this).Validar())
            {
                try
                {
                    CollectionReference coll = database.Collection("servicos");
                    Dictionary<string, object> data = new()
                    {
                        { "descricao", txtDescricao.Text },
                        { "cliente", cmbCliente.Text },
                        { "material", cmbMateriais.Text },
                        { "valor", double.Parse(txtValor.Text) },
                        { "data_inicio", dtInicio.Value.ToString("dd/MM/yyyy") },
                        { "data_termino", dtTermino.Value.ToString("dd/MM/yyyy") },
                        { "prazo_entrada", dtEntrega.Value.ToString("dd/MM/yyyy") }, 
                        { "prazo_pagamento", dtPagamento.Value.ToString("dd/MM/yyyy") },
                        { "forma_pagamento", cmbFormaPagamento.Text },
                    };
                    coll.AddAsync(data);
                    MessageBox.Show("Serviço cadastrado com sucesso!");
                    (this).Limpar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao cadastrar cliente!\n" + ex);
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
    }
}
