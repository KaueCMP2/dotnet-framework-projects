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
    public partial class Parent : Form
    {
        public Sessao5Entities ctx = new Sessao5Entities();
        public Parent()
        {
            InitializeComponent();

                this.StartPosition = FormStartPosition.CenterScreen;            
        }
    }
}
