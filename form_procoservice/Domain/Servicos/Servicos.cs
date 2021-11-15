using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace form_procoservice.Domain.Servicos
{
    class Servicos
    {
        [FirestoreProperty]
        public string descricao { get; set; }

        [FirestoreProperty]
        public string Cliente { get; set; }

        [FirestoreProperty]
        public double valor { get; set; }

        [FirestoreProperty]
        public string material { get; set; }

        [FirestoreProperty]
        public string data_inicio { get; set; }

        [FirestoreProperty]
        public string data_termino { get; set; }

        [FirestoreProperty]
        public string prazo_pagamento { get; set; }

        [FirestoreProperty]
        public string prazo_entrada { get; set; }

        [FirestoreProperty]
        public string forma_pagamento { get; set; }

/*
        [FirestoreProperty]
        public bool notaGerada { get; set; }
*/

    }
}
