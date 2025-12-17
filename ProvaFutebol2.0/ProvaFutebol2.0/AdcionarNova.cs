using ProvaFutebol2._0.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProvaFutebol2._0
{
    public partial class AdcionarNova : Form
    {
        Sessao5Entities ctx = new Sessao5Entities();
        public AdcionarNova()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(comboBox1.Text))
            {
                "Preencha todos os campos".Alert();
                return;
            }

            Notificacoes notificacaoAdcionada = new Notificacoes();
            notificacaoAdcionada.Titulo = textBox1.Text;
            notificacaoAdcionada.Descricao = textBox2.Text;
            notificacaoAdcionada.DataHoraCadastro = DateTime.Now;
            notificacaoAdcionada.DataHoraEnvio = dateTimePicker2.Value;
            notificacaoAdcionada.Importancia = comboBox1.Text;

            ctx.Notificacoes.Add(notificacaoAdcionada);
            ctx.SaveChanges();
        }
    }
}
