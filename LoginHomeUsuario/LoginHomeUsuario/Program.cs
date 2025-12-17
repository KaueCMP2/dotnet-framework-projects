using LoginHomeUsuario.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginHomeUsuario
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
            if(Properties.Settings.Default.id > 0)
            {
                var usas = ctx.Usuarios.FirstOrDefault(u => u.Id == Properties.Settings.Default.id);
                UsuarioLogado.Id = usas.Id;
                UsuarioLogado.Email = usas.Email;
                UsuarioLogado.Nome = usas.Nome;
                Application.Run(new FormHomeUsuario());
            }
            Application.Run(new Form1());
        }
    }
}
