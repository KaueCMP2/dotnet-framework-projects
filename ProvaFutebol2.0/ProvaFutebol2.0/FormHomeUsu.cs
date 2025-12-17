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
    public partial class FormHomeUsu : Form
    {
        Form? _formAnterior;
        public FormHomeUsu(Form? formAnterior = null)
        {
            InitializeComponent();
            _formAnterior = formAnterior;
        }

        private void FormHomeUsu_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formAnterior?.Show();
        }
    }
}
