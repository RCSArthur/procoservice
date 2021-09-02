using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace form_procoservice.Utils
{
    internal static class AjustarCursor
    {
        public static void Telefone(this MaskedTextBox mtxtTelefone, string telefone)
        {
            mtxtTelefone.SelectionStart = telefone.Length + 1;
            if (telefone.Length > 2) mtxtTelefone.SelectionStart = telefone.Length + 3;
            if (telefone.Length > 6) mtxtTelefone.SelectionStart = telefone.Length + 4;
        }

        public static void Cep(this MaskedTextBox mtxtCEP, string cep)
        {
            mtxtCEP.SelectionStart = cep.Length;
        }
    }
}
