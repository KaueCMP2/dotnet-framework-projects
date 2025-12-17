using homeAdminUser_prova2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace homeAdminUser_prova2
{
    public partial class Form1 : Parent
    {
        Form _formAnterior;
        public Form1(Form formAnterior = null)
        {
            InitializeComponent();
            _formAnterior = formAnterior;
            flowLayoutPanel1.AutoScroll = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formAnterior.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = SystemColors.Window;
            button2.BackColor = Color.Gray;
            button1.Visible = true;
            flowLayoutPanel1.Visible = true;
            flowLayoutPanel1.Controls.Clear();
            dataGridView1.Visible = false;

            var Notificacoes = ctx.Notificacoes.ToList();
            foreach (var item in Notificacoes)
            {
                flowLayoutPanel1.Controls.Add(new NotificaControls(item, this));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Gray;
            button2.BackColor = SystemColors.Window;
            button1.Visible = false;

            flowLayoutPanel1.Visible = false;
            dataGridView1.Visible = true;

            ListarJogos();
        }

        public void atualizarNotifiicacoes()
        {
            button3.PerformClick();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAdicionarNotifi formAdicionarNotifi = new FormAdicionarNotifi(this);
            formAdicionarNotifi.ShowDialog();
        }

        private string verificarStatusJogo()
        {
            int rodadaAtual = 1;
            var rodadas = ctx.Rodadas.FirstOrDefault(r => r.Id == rodadaAtual);
            if (rodadas.DataInicio < DateTime.Now)
            {
                return "Finalizado";
            }
            else if (rodadas.DataInicio == DateTime.Now)
            {
                return "Em andamento";
            }
            else if (rodadas.DataInicio > DateTime.Now)
            {
                return "Não iniciado";
            }
            return "null";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
        }

        public void ListarJogos()
        {
            dataGridView1.Rows.Clear();

            var JogosLista = ctx.Jogos.ToList();
            foreach (var jogo in JogosLista)
            {
                var selecoesVisitanteNome = ctx.Selecoes.FirstOrDefault(s => s.Id == jogo.SelecaoVisitanteId);
                var selecoesCasaNome = ctx.Selecoes.FirstOrDefault(s => s.Id == jogo.SelecaoCasaId);
                dataGridView1.Rows.Add
                (
                    jogo.Data.Value.ToString("dd/MM/yyyy"),
                    jogo.Data.Value.ToString("HH:mm"),
                    selecoesCasaNome.Nome,
                    jogo.PlacarCasa,
                    "X",
                    jogo.PlacarVisitante,
                    selecoesVisitanteNome.Nome,
                    verificarStatusJogo()
                );

                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }
    }
}
