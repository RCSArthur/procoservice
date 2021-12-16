using form_procoservice.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace form_procoservice
{
    internal static class TratarCampos
    {
        public static bool Validar(this Control control)
        {
            foreach (Control item in control.Controls)
            {
                if (item.Enabled)
                {
                    switch (item)
                    {
                        case TextBox when String.IsNullOrWhiteSpace(item.Text.Trim()):
                            ExibirErro(item);
                            return false;
                        case MaskedTextBox when String.IsNullOrWhiteSpace(item.Text.ReplaceNumeros()):
                            ExibirErro(item);
                            return false;
                    }
                }
            }
            return true;
        }

        public static void Limpar(this Control control)
        {
            foreach (Control item in control.Controls)
            {
                if (item is TextBox || item is MaskedTextBox)
                {
                    item.Text = "";
                    if (item.Name.Equals("txtNome")) item.Focus();
                }
            }
        }

        public static string ReplaceNumeros(this string texto)
        {
            return RegexConstants.Numeros.Replace(texto, "").Strip();
        }

        private static void ExibirErro(Control item)
        {
            MessageBox.Show("O campo " + item.Name + " é de preenchimento obrigatório!", "Procoservice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            item.Focus();
        }
    }
}
