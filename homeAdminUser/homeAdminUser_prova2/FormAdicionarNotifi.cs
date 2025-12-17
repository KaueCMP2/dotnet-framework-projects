using homeAdminUser_prova2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homeAdminUser_prova2
{
    public partial class FormAdicionarNotifi : Parent
    {
        Sessao5Entities ctx = new Sessao5Entities();
        Form1 _formAnterior;
        public FormAdicionarNotifi(Form1 formAnterior)
        {
            InitializeComponent();
            _formAnterior = formAnterior;
        }

        private void FormAdicionarNotifi_Load(object sender, EventArgs e)
        {
            maskedTextBox1.Mask = "00:00";
            comboBox1.Items.AddRange(ctx.Selecoes.Select(t => t.Nome).ToArray());
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(maskedTextBox1.Text)
    || string.IsNullOrEmpty(comboBox2.Text))
            {
                "Preencha todos os campos".Alert();
                return;
            }
            var time = ctx.Selecoes.FirstOrDefault(s => s.Nome == comboBox1.Text);

            Notificacoes not = new Notificacoes();

            not.Titulo = textBox1.Text;
            not.Descricao = textBox2.Text;
            not.DataHoraCadastro = DateTime.Now;
            not.SelecaoId = time.Id;
            not.Importancia = comboBox2.Text;

            DateTime data = dateTimePicker1.Value;
            TimeSpan hora;
            if (TimeSpan.TryParse(maskedTextBox1.Text.Replace("_", "").Trim(), out hora))
            {
                not.DataHoraEnvio = data.Add(hora);
                ctx.Notificacoes.Add(not);
                ctx.SaveChanges();

                _formAnterior?.atualizarNotifiicacoes();

                Close();
            }

            else
            {
                "Hora invalida".Alert();
                return;
            }
        }
    }
}
