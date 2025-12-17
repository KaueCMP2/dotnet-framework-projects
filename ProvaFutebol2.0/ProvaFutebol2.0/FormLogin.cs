using ProvaFutebol2._0.Model;
using ProvaFutebol2._0.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProvaFutebol2._0
{
    public partial class FormLogin : Parent
    {
        Sessao5Entities ctx = new Sessao5Entities();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                "Prencha todos os campos".Alert();
                return;
            }

            var usas = ctx.Usuarios.FirstOrDefault(u => u.Email == textBox1.Text);
            if (usas == null)
            {
                "Dados invalidos".Alert();
                return;
            }

            if (usas.Senha == textBox2.Text)
            {
                if (textBox1.Text == "administrador@email.com")
                {
                    FormHomeAdm frmHomeAdm = new FormHomeAdm(this);
                    frmHomeAdm.Show();
                    Hide();
                    return;
                }

                if (usas.perfil == "1" && usas.Senha == "admin123")
                {
                    if (usas.Email == "administrador@email.com")
                    {
                        return;
                    }

                    FormSenha frmSenha = new FormSenha(this, textBox1.Text);
                    frmSenha.Show();
                    Hide();
                    return;
                }
                FormHomeUsu frmHomeusu = new FormHomeUsu(this);
                frmHomeusu.Show();
                Hide();
                return;
            }
            "Dadados invalidos".Alert();
            return;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormCadastro fmCadastro = new FormCadastro(this);
            fmCadastro.Show();
            Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                var usas = ctx.Usuarios.FirstOrDefault(u => u.Email == textBox1.Text);
                if (usas != null)
                {
                    FormSenha fmSenha = new FormSenha(this, textBox1.Text);
                    fmSenha.Show();
                    Hide();
                    return;
                }

                "Dados invalidos".Alert();
            }

            "Preencha o campo de email para prosseguir".Info();
            return;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var usas = ctx.Usuarios.FirstOrDefault(u => u.Email == textBox1.Text);
            if (ValidarUsu() == 1 || ValidarUsu() == 2)
            {
                Properties.Settings.Default.id = usas.Id;
                Properties.Settings.Default.Save();
                return;
            }

            Properties.Settings.Default.id = 0;
            Properties.Settings.Default.Save();
        }

        public int ValidarUsu()
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                "Prencha todos os campos".Alert();
                return 0;
            }

            var usas = ctx.Usuarios.FirstOrDefault(u => u.Email == textBox1.Text);
            if (usas != null)
            {
                if (!(usas.Senha == textBox2.Text))
                {
                    "Dadados invalidos".Alert();
                    return 0;
                }

                if (usas.perfil == "0")
                    return 1;

                return 2;
            }

            "Algo deu errado".Alert();
            return 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.ToLower() == "administrador@email.com")
            {
                checkBox1.Visible = false;
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
