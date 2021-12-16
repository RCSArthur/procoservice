using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace form_procoservice.Utils
{
    internal static class FirebaseService
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
                MessageBox.Show("Erro ao conectar com o Firebase\n" + e, "Procoservice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }
    }
}
