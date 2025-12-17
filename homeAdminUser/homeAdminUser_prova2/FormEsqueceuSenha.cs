using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homeAdminUser_prova2
{
    public partial class FormEsqueceuSenha : Parent
    {
        Form _formAnterior;
        int _id;
        public FormEsqueceuSenha(Form formAnterior, int id)
        {
            InitializeComponent();
            _id = id;
            _formAnterior = formAnterior;
            dateTimePicker1.MaxDate = DateTime.Today.AddYears(-18);
        }

        private void FormEsqueceuSenha_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(ctx.Selecoes.Select(s => s.Nome).ToArray());
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var usas = ctx.Usuarios.FirstOrDefault(u => u.Id == _id);

            if (usas == null)
            {
                "Error".Alert();
                Close();
                return;
            }
            if (checarDados() == true)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var usas = ctx.Usuarios.FirstOrDefault(u => u.Id == _id);
            if (usas == null)
            {
                "Error".Alert();
                Close();
                return;
            }

            if (checarDados() == true)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var timeFav = ctx.Selecoes.FirstOrDefault(t => t.Nome == comboBox1.SelectedText);
            var usas = ctx.Usuarios.FirstOrDefault(u => u.Id == _id);

            if (usas.Nascimento != dateTimePicker1.Value || usas.TimeFavoritoId != timeFav.Id)
            {
                "Dados incorretos".Alert();
            }

            if (textBox1.Text != textBox2.Text)
            {
                "Senhas não coincidem".Alert();
                return;
            }


            if (Regex.IsMatch(textBox1.Text, @"^(?=.*\d)[a-d\d]{8,15}$"))
            {
                "8-15 caracteres, letras minúsculas e números. Não pode existir letra maiúscula nem caracteres especiais. Tem que ter pelo menos 1 número".Info();
                return;
            }

            usas.Senha = textBox1.Text;
            ctx.SaveChanges();
            Close();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text)) { return; }

            label5.Visible = true;
            panel1.Visible = true;
            label5.Text = verificarNivelSenha();

            if (verificarNivelSenha() == "Fraca")
            {
                panel1.BackColor = Color.Red;
            }
            else if (verificarNivelSenha() == "Forte")
            {
                panel1.BackColor = Color.Green;
            }
            else
            {
                panel1.BackColor = Color.Yellow;
            }

            if (textBox1.Text != textBox2.Text)
            {
                label6.Text = "Senhas não correspondem";
                panel2.BackColor = Color.Red;
                return;
            }
            label6.Text = "Senhas identicas";
            panel2.BackColor = Color.Green;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) { return; }
            if (textBox1.Text != textBox2.Text)
            {
                label6.Text = "Senhas não correspondem";
                panel2.BackColor = Color.Red;
                return;
            }

            label6.Text = "Senhas identicas";
            panel2.BackColor = Color.Green;
            return;
        }
        public string verificarNivelSenha()
        {
            var contagem = textBox1.Text.Trim().GroupBy(c => c)
                .Select(g => g.Count())
                .ToList();

            if (contagem.Any(c => c >= 3))
            {
                return "Fraca";
            }

            if (contagem.Any(c => c == 1))
            {
                return "Forte";
            }

            return "Senha Média";
        }

        private void FormEsqueceuSenha_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formAnterior.Show();
        }

        public bool checarDados()
        {
            var usas = ctx.Usuarios.FirstOrDefault(u => u.Id == _id);
            var timeFav = ctx.Selecoes.FirstOrDefault(t => t.Nome == comboBox1.Text);
            if (dateTimePicker1.Value != usas.Nascimento)
            {
                return false;
            }

            if (timeFav == null) { return false; }

            if (timeFav.Id != usas.TimeFavoritoId)
            {
                return false;
            }
            return true;
        }
    }
}
