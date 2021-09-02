using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace form_procoservice.Utils
{
    internal static class Firebase
    {
        public static FirestoreDb Conectar()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + @"procoservice-firebase.json";
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
                return FirestoreDb.Create("procoservice-d428b");
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Erro ao conectar com o Firebase\n" + e);
            }
            return null;
        }
    }
}
