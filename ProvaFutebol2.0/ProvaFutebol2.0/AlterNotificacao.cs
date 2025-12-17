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
    public partial class AlterNotificacao : Form
    {
        Sessao5Entities ctx = new Sessao5Entities();
        int _id;
        public AlterNotificacao(int id)
        {
            InitializeComponent();
            var notificaoEditada = ctx.Notificacoes.Find(id);        
            _id = id;

            textBox1.Text = notificaoEditada.Titulo;
            textBox2.Text = notificaoEditada.Descricao;
            dateTimePicker2.Value = notificaoEditada.DataHoraEnvio.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(comboBox1.Text))
            {
                "Preencha todos os campos".Alert();
                return;
            }

            var notificacaoEditada = ctx.Notificacoes.Find(_id);
            notificacaoEditada.Titulo = textBox1.Text;
            notificacaoEditada.Descricao = textBox2.Text;
            notificacaoEditada.DataHoraCadastro = DateTime.Now;
            notificacaoEditada.DataHoraEnvio = dateTimePicker2.Value;
            notificacaoEditada.Importancia = comboBox1.Text;
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }
    }
}
