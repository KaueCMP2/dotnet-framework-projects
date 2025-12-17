using homeAdminUser_prova2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homeAdminUser_prova2
{
    public partial class NotificaControls : UserControl
    {
        Sessao5Entities ctx = new Sessao5Entities();
        Notificacoes _notif;
        Form1 _formAnterior;
        public NotificaControls(Notificacoes notifica, Form1 formAnterior)
        {
            InitializeComponent();
            _notif = notifica;
            _formAnterior = formAnterior;
        }

        private void NotificaControls_Load(object sender, EventArgs e)
        {
            var noti = ctx.Notificacoes.FirstOrDefault(n => n.Id == _notif.Id);
            label1.Text = noti.Titulo;
            label2.Text = noti.Descricao;
            label3.Text = noti.DataHoraEnvio?.ToString("dd/MM/yyyy HH:mm");

            if (noti.DataHoraEnvio < DateTime.Now)
            {
                this.BackColor = Color.Orange;
                return;
            }

            else
            {
                if(noti.Importancia == "Padrão")
                {
                    this.BackColor = Color.Blue;
                }
                
                if(noti.Importancia.Trim() == "Urgente")
                {
                    this.BackColor = Color.Red;
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var notiApagada = ctx.Notificacoes.FirstOrDefault(n => n.Id == _notif.Id);
            ctx.Notificacoes.Remove(notiApagada);
            ctx.SaveChanges();

            Parent.Controls.Remove(this);
            this.Dispose();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormAlterarNotificacao formAlterarNotificacao = new FormAlterarNotificacao(this._notif.Id, _formAnterior);
            formAlterarNotificacao.ShowDialog();
        }
    }
}
