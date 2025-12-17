using homeAdminUser_prova2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homeAdminUser_prova2
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
            Sessao5Entities ctx = new Sessao5Entities();
            if (Properties.Settings.Default.id > 0)
            {
                var usuLog = ctx.Usuarios.FirstOrDefault(u => u.Id == Properties.Settings.Default.id);
                if (usuLog != null)
                {
                    Application.Run(new FormHomeUsu(usuLog.Id));
                }
            }
            Application.Run(new FormLogin());
        }
        public static DialogResult Alert(this string message)
        {
            return MessageBox.Show(message, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult Info(this string message)
        {
            return MessageBox.Show(message, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Confi(this string message)
        {
            return MessageBox.Show(message, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
