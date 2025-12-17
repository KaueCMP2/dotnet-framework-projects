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
    public partial class FormHomeUsu : Parent
    {
        Form1 _formAnterior;
        private int id;

        public FormHomeUsu(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        public FormHomeUsu(Form1 formAnterior, int id)
        {
            InitializeComponent();
        }

        private void FormHomeUsu_FormClosing(object sender, FormClosingEventArgs e)
        {
             new Form1().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.id = 0;
            Properties.Settings.Default.Save();

            Close();
        }
    }
}
