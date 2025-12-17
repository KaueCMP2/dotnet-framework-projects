using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginHomeUsuario
{
    public partial class FormHomeUsuario : Parent
    {
        Form _formAnterior;
        public FormHomeUsuario(Form? formAnterior = null)
        {
            InitializeComponent();
            _formAnterior = formAnterior;

            var usas = ctx.Usuarios.FirstOrDefault(u => u.Id == UsuarioLogado.Id);
            if (usas == null)
                return;

            if (usas.Foto == null)
                return;

            using (MemoryStream ms = new MemoryStream(usas.Foto))
            {
                button1.BackgroundImage = Image.FromStream(ms);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UsuarioLogado.Email == null)
            {
                return;
            }

            new FormCadastro(this, 2).ShowDialog();
        }

        private void FormHomeUsuario_Load(object sender, EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (contagemExt == 3)
            {
                Properties.Settings.Default.id = 0;
                Properties.Settings.Default.Save();
                Close();
                return;
            }
            Close();
        }

        private void FormHomeUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_formAnterior != null)
            {
                _formAnterior.Show();
                return;
            }
            new Form1().Show();
        }

        int contagemExt;
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            contagemExt++;
            MessageBox.Show($"{contagemExt}");
        }
    }
}
