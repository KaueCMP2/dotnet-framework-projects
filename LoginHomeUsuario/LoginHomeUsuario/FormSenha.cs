using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginHomeUsuario
{
    public partial class FormSenha : Form
    {
        Form formAnterior;
        public FormSenha(Form1 form1)
        {
            InitializeComponent();
            formAnterior = form1;
        }
    }
}
