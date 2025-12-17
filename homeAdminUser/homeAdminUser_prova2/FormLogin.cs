using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace homeAdminUser_prova2
{
    public partial class FormLogin : Parent
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormCadastro fmCadastro = new FormCadastro(this);
            fmCadastro.Show();
            Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                "Preencha o campo correspondente a: \"Email\"".Alert();
                return;
            }

            var usas = ctx.Usuarios.FirstOrDefault(u => u.Email == textBox1.Text);
            if (usas == null)
            {
                "Dados incorretos".Alert();
                return;
            }

            FormEsqueceuSenha frmSenha = new FormEsqueceuSenha(this, usas.Id);
            frmSenha.Show();
            Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var usas = ctx.Usuarios.FirstOrDefault(u => u.Email == textBox1.Text);
            if (usas == null)
            {
                "Dados invalidos".Alert();
                checkBox1.Checked = false;
                return;
            }

            if (usas.Senha != textBox2.Text)
            {
                "Dados invalidos".Alert();
                checkBox1.Checked = false;
                return;
            }

            if (usas.Email == "administrador@email.com")
            {
                "Admins nao podem usar esta opção".Alert();
                checkBox1.Checked = false;
                return;
            }

            if (checkBox1.Checked)
            {
                Properties.Settings.Default.id = usas.Id;
                Properties.Settings.Default.Save();
                return;
            }

            Properties.Settings.Default.id = 0;
            Properties.Settings.Default.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var usas = ctx.Usuarios.FirstOrDefault(u => u.Email == textBox1.Text);
            if (usas == null)
            {
                "Dados invalidos".Alert();
                return;
            }

            if (usas.Senha != textBox2.Text)
            {
                "Dados invalidos".Alert();
                return;
            }


            if (usas.Email == "administrador@email.com")
            {
                Form1 frmAdmin = new Form1(this);
                frmAdmin.Show();
                Hide();
                return;
            }

            if (usas.Senha == "Admin123" && usas.Email != "administrador@email.com")
            {
                FormCadastro frmCadastro = new FormCadastro(this);
                frmCadastro.Show();
                Hide();
                return;
            }

            FormHomeUsu formHomeUsu = new FormHomeUsu(usas.Id);
            formHomeUsu.Show();
            Hide();
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "administrador@email.com")
            {
                checkBox1.Visible = false;
                return;
            }

            checkBox1.Visible = true;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "administrador@email.com")
            {
                checkBox1.Visible = false;
                return;
            }

            checkBox1.Visible = true;
        }

        private void FormLogin_Activated(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }
    }
}
