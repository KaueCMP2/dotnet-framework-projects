using ProvaFutebol2._0.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProvaFutebol2._0
{
    public partial class FormHomeAdm : Form
    {
        Form _formAnterior;
        Sessao5Entities ctx = new Sessao5Entities();
        public FormHomeAdm(Form formAnterior)
        {
            InitializeComponent();
            _formAnterior = formAnterior;
            flowLayoutPanel1.AutoScroll = true;
            LoadNotificacoes();
        }

        private void FormHomeAdm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formAnterior.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.BackColor = Color.Gray;
            button1.BackColor = Color.Transparent;
            button3.Visible = true;

            groupBox1.Visible = true;
            flowLayoutPanel1.Visible = true;
            dataGridView1.Visible = false;
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button1.BackColor = Color.Gray;
            button2.BackColor = Color.Transparent;

            groupBox1.Visible = true;
            dataGridView1.Visible = true;
            flowLayoutPanel1.Visible = false;
            return;
        }

        private void LoadNotificacoes()
        {
            flowLayoutPanel1.Controls.Clear();
            var notificacao = ctx.Notificacoes.ToList();
            foreach (var item in notificacao)
            {
                flowLayoutPanel1.Controls.Add(new NotificacaoControl(item));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdcionarNova adNova = new AdcionarNova();
            adNova.ShowDialog();
        }
    }
}
