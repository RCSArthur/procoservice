using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace form_procoservice.Domain.Servico
{
    [FirestoreData]
    class Servico
    {
        [FirestoreProperty]
        public string cliente { get; set; }

        [FirestoreProperty]
        public string cpfCnpj { get; set; }

        [FirestoreProperty]
        public string data_inicio { get; set; }

        [FirestoreProperty]
        public string data_termino { get; set; }

        [FirestoreProperty]
        public string descricao { get; set; }

        [FirestoreProperty]
        public string forma_pagamento { get; set; }

        [FirestoreProperty]
        public string material { get; set; }

        [FirestoreProperty]
        public string prazo_entrada { get; set; }

        [FirestoreProperty]
        public string prazo_pagamento { get; set; }

        [FirestoreProperty]
        public double valor { get; set; }
    }
}
