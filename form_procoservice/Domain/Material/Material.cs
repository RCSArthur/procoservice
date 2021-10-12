using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace form_procoservice.Domain.Material
{
    [FirestoreData]

    class Material
    {

        [FirestoreProperty]
        public string descricao { get; set; }

        [FirestoreProperty]
        public int quantidade { get; set; }


        [FirestoreProperty]
        public double precoUnitario { get; set; }


    }
}
