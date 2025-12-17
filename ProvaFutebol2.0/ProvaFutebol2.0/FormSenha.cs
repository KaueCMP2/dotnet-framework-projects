using ProvaFutebol2._0.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProvaFutebol2._0
{
    public partial class FormSenha : Parent
    {
        string _email;
        Form _formAnterior;
        bool senhavalida;
        public FormSenha(Form formAnterior, string email)
        {
            InitializeComponent();
            _email = email;
            _formAnterior = formAnterior;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_email != null)
            {
                var usas = ctx.Usuarios.FirstOrDefault(u => u.Email == _email);
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
                {
                    if (textBox1.Text == textBox2.Text)
                    {
                        if (usas.Nascimento == dateTimePicker1.Value)
                        {
                            if (usas.Senha != textBox2.Text)
                            {
                                usas.Senha = textBox1.Text;
                                ctx.SaveChanges(); return;
                            }

                            else
                            {
                                "Senha igual a antiga".Info(); return;
                            }
                        }
                        else
                        {
                            "Dados nao coincidem".Alert(); return;
                        }
                    }
                }
                else
                {
                    "Preencha todos os campos obrigatorios".Alert();
                    return;
                }
            }
        }

        private void FormSenha_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formAnterior.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox1 != null)
            {
                if (textBox1.Text == textBox2.Text)
                {
                    panel2.Visible = true;
                    panel2.BackColor = Color.Green;
                    label6.Text = "Senhas Coincidem";
                    label6.BackColor = Color.Green;
                    label6.ForeColor = Color.White;
                    return;
                }

                else
                {
                    panel2.Visible = true;
                    panel2.BackColor = Color.Red;
                    label6.Text = "Nao Coincidem";
                    label6.BackColor = Color.Red;
                    label6.ForeColor = Color.White;
                    return;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {            
            string nivel = GetNivelSenha(textBox1.Text);
            panel1.Visible = true;
            label5.Visible = true;

            if (nivel == "Fraca")
            {
                panel1.BackColor = Color.Red;
                label5.Text = nivel;
                return;
            }

            if (nivel == "Medio")
            {
                panel1.BackColor = Color.Yellow;
                label5.Text = nivel;
                return;
            }            

            panel1.BackColor = Color.Green;
            label5.Text = nivel;
            return;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBox1.Text, @"^[?=*\d][a-z0-9][8,15]$"))
            {
                senhavalida = false;
            }
        }

        public string GetNivelSenha(string s)
        {
            var repetSenha = s
                .GroupBy(c => c)
                .Select(g => g.Count())
                .ToList();

            if (repetSenha.Any(r => r > 2))
            {
                return "Fraca";
            }
            if (repetSenha.Any(r => r == 2))
            {
                return "Media";
            }

            return "Forte";
        }
    }
}
