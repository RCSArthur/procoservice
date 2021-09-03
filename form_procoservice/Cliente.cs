using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace form_procoservice
{
    [FirestoreData]
    public class Cliente
    {
        [FirestoreProperty]
        public string UF { get; set; }

        [FirestoreProperty]
        public string bairro { get; set; }

        [FirestoreProperty]
        public string cep { get; set; }

        [FirestoreProperty]
        public string cidade { get; set; }

        [FirestoreProperty]
        public string nome { get; set; }

        [FirestoreProperty]
        public string numero { get; set; }

        [FirestoreProperty]
        public string rua { get; set; }

        [FirestoreProperty]
        public string telefone { get; set; }

    }
}
