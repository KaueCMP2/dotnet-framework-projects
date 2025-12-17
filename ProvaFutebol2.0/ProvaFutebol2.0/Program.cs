using ProvaFutebol2._0.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProvaFutebol2._0
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Sessao5Entities ctx = new Sessao5Entities();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Properties.Settings.Default.id > 0)
            {
                var usas = ctx.Usuarios.FirstOrDefault(u => u.Id == Properties.Settings.Default.id);

                string perfilAdm = 0.ToString();
                if (usas.perfil == perfilAdm)
                {
                    Application.Run(new Form1());
                }

                Application.Run(new FormHomeUsu());
                return;
            }
            Application.Run(new FormLogin());
        }
    }

    public static class me
    {
        public static DialogResult Alert(this string mensagem)
        {
            return MessageBox.Show(mensagem, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static DialogResult Conf(this string mensagem)
        {
            return MessageBox.Show(mensagem, "Confirmacao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public static DialogResult Info(this string mensagem)
        {
            return MessageBox.Show(mensagem, "Informacao", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
