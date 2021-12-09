using form_procoservice.Domain.Material;
using form_procoservice.Domain.Servico;
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
    public partial class Alterarservico : Form
    {
        private readonly FirestoreDb _fireDb = FirebaseService.Conectar();

        public Alterarservico(string descricao, string cliente, string cpfCnpj, string material, double valor2, string data_inicio, string data_termino, string prazo_entrada, string prazo_pagamento, string forma_pagamento)
        {
            InitializeComponent();
            txtDescricao.Text = descricao;
            cmbCliente.Text = cliente;
            textBox1.Text = cpfCnpj;
            textBox2.Text = material;
            txtValor.Text = valor2.ToString();
            dtInicio.Text = data_inicio;
            dtTermino.Text = data_termino;
            dtEntrega.Text = prazo_entrada;
            dtPagamento.Text = prazo_pagamento;
            cmbFormaPagamento.Text = forma_pagamento;
        }

        public Alterarservico(FirestoreDb firestoreDb)
        {
            _fireDb = firestoreDb;
        }
        private static readonly FirestoreDb firestoreDb = FirebaseService.Conectar();
        private FirestoreDb database = firestoreDb;

        List<string> termsList = new List<string>();
        List<string> materiaisTodos = new List<string>();
        private void Alterarservico_Load(object sender, EventArgs e)
        {
            Listar_Materiais();
            Checados();
        }

        async void Update_especifico()
        {
            List<string> termsList = new List<string>();
            foreach (var item in chkListMaterial.CheckedItems)
                termsList.Add(((DataRowView)item)["descricao"].ToString());
            var result = String.Join(", ", termsList.ToArray());
            object documento = "";

            Query cityque = database.Collection("servicos");
            QuerySnapshot snape = await cityque.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snape.Documents)
            {
                Servico docs = docsnap.ConvertTo<Servico>();
                if (textBox1.Text.ToString().Equals(docs.cpfCnpj, StringComparison.OrdinalIgnoreCase))
                {
                    ;
                    documento = docsnap.Id;
                }
            }

            DocumentReference docref = database.Collection("servicos").Document(documento.ToString());
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                   { "descricao", txtDescricao.Text },
                        { "cliente", cmbCliente.Text },
                        { "cpfCnpj", textBox1.Text },
                        { "material", result},
                        { "valor", double.Parse(txtValor.Text) },
                        { "data_inicio", dtInicio.Value.ToString("dd/MM/yyyy") },
                        { "data_termino", dtTermino.Value.ToString("dd/MM/yyyy") },
                        { "prazo_entrada", dtEntrega.Value.ToString("dd/MM/yyyy") },
                        { "prazo_pagamento", dtPagamento.Value.ToString("dd/MM/yyyy") },
                        { "forma_pagamento", cmbFormaPagamento.Text },

            };

            DocumentSnapshot snap = await docref.GetSnapshotAsync();
            if (snap.Exists)
            {
                await docref.UpdateAsync(data);
            }
            MessageBox.Show("Alteração realizada com sucesso!");

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
                    materiaisTodos.Add(docs.descricao.ToString());
                }
            }
            return chkListMaterial.DataSource = materiais;

        }

        private void Checados()
        {
            MessageBox.Show("entei em checados");
            string material = textBox2.Text;
            string[] opcoes = material.Split(',').Select(x => x.Trim()).ToArray();
            int index = -1;
            foreach (var item in materiaisTodos)
            {
                index = index + 1;
                foreach (var item2 in opcoes)
                {

                    if (item == item2)
                        {
                            chkListMaterial.SetItemCheckState(index, CheckState.Checked);
                        }
                    }
                    
                
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //chkListMaterial.SetItemCheckState(index, CheckState.Checked);
            Update_especifico();


        }
    }
}
