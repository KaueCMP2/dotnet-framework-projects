using ProvaFutebol2._0.Model;
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

namespace ProvaFutebol2._0
{
    public partial class NotificacaoControl : UserControl
    {
        Notificacoes _notificacao;


        public NotificacaoControl(Notificacoes notificacao)
        {
            InitializeComponent();
            _notificacao = notificacao;

            label1.Text = notificacao.Titulo;
            label2.Text = notificacao.Descricao;
            label3.Text = notificacao.DataHoraEnvio?.ToString("dd/MM/yyyy HH:mm");
        }

        private void NotificacaoControl_Load(object sender, EventArgs e)
        {            

            if (_notificacao.DataHoraEnvio < DateTime.Now || _notificacao.DataHoraEnvio == null)
            {
                this.BackColor = Color.Orange;
            }

            else
            {
                if(_notificacao.Importancia == "Baixa")
                {
                    this.BackColor = Color.Blue;
                    return;
                }
                
                if(_notificacao.Importancia == "Alta")
                {
                    this.BackColor = Color.Red;
                    return;
                }
            }
        }

        Sessao5Entities ctx = new Sessao5Entities();
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var notificacaApagada = ctx.Notificacoes.Find(this._notificacao.Id);

            ctx.Notificacoes.Remove(notificacaApagada);
            ctx.SaveChanges();

            this.Parent.Controls.Remove(this);
            this.Dispose();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AlterNotificacao alter = new AlterNotificacao(this._notificacao.Id);
            alter.ShowDialog();  
        }
    }
}
