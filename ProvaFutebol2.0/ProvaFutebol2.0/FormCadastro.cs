using ProvaFutebol2._0.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProvaFutebol2._0
{
    public partial class FormCadastro : Parent
    {
        Sessao5Entities ctx = new Sessao5Entities();
        Form? _formAnterior;
        byte[] imagemBytes;
        string sexo;
        public FormCadastro(Form? formAnterior = null)
        {
            InitializeComponent();
            _formAnterior = formAnterior;
        }

        private void FormCadastro_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formAnterior?.Show();
        }

        private void FormCadastro_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sessao5DataSet2.Selecoes' table. You can move, or remove it, as needed.
            this.selecoesTableAdapter2.Fill(this.sessao5DataSet2.Selecoes);
            // TODO: This line of code loads data into the 'sessao5DataSet1.Selecoes' table. You can move, or remove it, as needed.

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Filtro | *.jpg; *.png;";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackgroundImage = Image.FromFile(ofd.FileName);
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                imagemBytes = File.ReadAllBytes(ofd.FileName);
                return;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DateTime.Now.Year - dateTimePicker1.Value.Year < 18 || string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(sexo))
            {
                "Preencha todos os campos".Alert();
                return;
            }

            var usas = ctx.Usuarios.FirstOrDefault(u => u.Email == textBox2.Text);
            if (usas != null)
            {
                panel1.Visible = true;
                label5.Visible = true;
                panel1.BackColor = Color.Red;
                label5.ForeColor = Color.White;
                return;
            }

            Usuarios usu = new Usuarios();
            usu.Nome = textBox1.Text;
            usu.Email = textBox2.Text;
            usu.Senha = "admin123";
            usu.Nascimento = dateTimePicker1.Value;
            usu.Sexo = sexo;
            usu.perfil = "1";

            if (imagemBytes != null)
                usu.Foto = imagemBytes;

            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                var time = ctx.Selecoes.FirstOrDefault(t => t.Nome == comboBox1.Text);
                usu.TimeFavoritoId = time.Id;
            }

            ctx.Usuarios.Add(usu);
            ctx.SaveChanges();
            "Salvo com sucesso".Info();
            Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                sexo = "F";
                checkBox1.Enabled = false;
                return;
            }

            if (!checkBox2.Checked)
            {
                sexo = "";
                checkBox1.Enabled = true;
                return;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                sexo = "M";
                checkBox2.Enabled = false;
                return;
            }

            if (!checkBox1.Checked)
            {
                sexo = "";
                checkBox2.Enabled = true;
                return;
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            checkBox1.Focus();
        }
    }
}

