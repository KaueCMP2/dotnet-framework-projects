using LoginHomeUsuario.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginHomeUsuario
{
    public partial class Form1 : Parent
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text)) return;

            if (usas() == null) return;

            if (usas().Senha != textBox2.Text) return;

            if (usas().Email == "administrador@email.com")
            {
                return;
            }

            if (usas().Senha == "admin123")
            {
                new FormSenha(this).Show();
                return;
            }
            UsuarioLogado.Id = usas().Id;
            UsuarioLogado.Email = usas().Email;
            UsuarioLogado.Nome = usas().Nome;
            new FormHomeUsuario(this).Show();
            Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (usas() == null) return;
            UsuarioLogado.Email = usas().Email;
            new FormSenha(this).Show();
            Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new FormCadastro(this, 1).Show();
            Hide();
        }

        private Usuarios? usas()
        {
            return ctx.Usuarios.FirstOrDefault(u => u.Email == textBox1.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    checkBox1.Checked = false;
                    return;
                }
                var usas = ctx.Usuarios.FirstOrDefault(u => u.Email == textBox1.Text);

                if (usas == null)
                {
                    checkBox1.Checked = false;
                    return;
                }
                if (usas.Senha != textBox2.Text)
                {
                    checkBox1.Checked = false;
                    return;
                }

                Properties.Settings.Default.id = usas.Id;
                Properties.Settings.Default.Save();

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                return;

            if (textBox1.Text == "administrador@email.com")
            {
                checkBox1.Visible = false;
                return;
            }
            checkBox1.Visible = true;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                return;

            if (textBox1.Text == "administrador@email.com")
            {
                checkBox1.Visible = false;
                return;
            }
            checkBox1.Visible = true;
        }
    }
}
