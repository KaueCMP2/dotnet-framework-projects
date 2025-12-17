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
    public partial class FormAlterarNotificacao : Parent
    {
        int _id;
        Form1 _formAnterior;
        public FormAlterarNotificacao(int id, Form1 formAnterior)
        {
            InitializeComponent();
            _id = id;
            _formAnterior = formAnterior;
        }

        private void FormAlterarNotificacao_Load(object sender, EventArgs e)
        {
            maskedTextBox1.Mask = "00:00";
            comboBox1.Items.AddRange(ctx.Selecoes.Select(t => t.Nome).ToArray());

            var notif = ctx.Notificacoes.FirstOrDefault(n => n.Id == _id);
            if (notif == null)
            {
                "Notificacao nao encontrada".Alert();
                Close();
            }

            textBox1.Text = notif.Titulo;
            textBox2.Text = notif.Descricao;
            if (notif.DataHoraEnvio == null)
            {
                return;
            }
            else
            {
                dateTimePicker1.Value = notif.DataHoraEnvio.Value;
                maskedTextBox1.Text = notif.DataHoraEnvio?.ToString("HH:mm");
            }
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
            var not = ctx.Notificacoes.FirstOrDefault(n => n.Id == _id);
            not.Importancia = comboBox2.Text;
            if (not == null)
            {
                "Erro inesperado".Alert();
                Close();
            }

            if (!TimeSpan.TryParse(maskedTextBox1.Text, out _))
            {
                "Hora invalida".Alert();
                return;
            }

            not.Titulo = textBox1.Text;
            not.Descricao = textBox2.Text;
            not.SelecaoId = time.Id;

            not.DataHoraEnvio = DateTime.Today.Add(TimeSpan.Parse(maskedTextBox1.Text.Trim()));
            MessageBox.Show($"{DateTime.Today.Add(TimeSpan.Parse(maskedTextBox1.Text.Trim()))}");
            ctx.SaveChanges();
            Close();

            _formAnterior?.atualizarNotifiicacoes();
        }

        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {
            MessageBox.Show(maskedTextBox1.Text);
        }
    }
}
