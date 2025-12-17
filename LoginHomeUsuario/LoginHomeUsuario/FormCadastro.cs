using LoginHomeUsuario.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginHomeUsuario
{
    public partial class FormCadastro : Parent
    {
        Form _formAnterior;
        int _numPai;
        byte[] fotoBytes;

        string sexo;
        public FormCadastro(Form formAnterior, int nuPai)
        {
            InitializeComponent();
            _formAnterior = formAnterior;

            _numPai = nuPai;
        }

        public FormHomeUsuario FormHomeUsuario { get; }


        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Filtro | *.jpg; *.png;";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackgroundImage = Image.FromFile(openFileDialog.FileName);
                pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                fotoBytes = File.ReadAllBytes(openFileDialog.FileName);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void FormCadastro_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_numPai == 1)
            {
                _formAnterior.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_numPai == 1)
            {
                Usuarios usus = new Usuarios();
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
                    return;

                usus.Nome = textBox1.Text;
                usus.Email = textBox2.Text;
                usus.Nascimento = dateTimePicker1.Value;
                usus.Sexo = sexo;

                if (fotoBytes.Length > 0)
                {
                    usus.Foto = fotoBytes;
                }

                ctx.Usuarios.Add(usus);
                ctx.SaveChanges();
                Close();
                return;
            }

            var usas = ctx.Usuarios.FirstOrDefault(u => u.Email == textBox2.Text);
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
                return;
            if (usas == null) return;

            var usu = ctx.Usuarios.FirstOrDefault(u => u.Email == UsuarioLogado.Email);
            usu.Nome = textBox1.Text;
            usu.Email = textBox2.Text;
            usu.Nascimento = dateTimePicker1.Value;
            usu.Sexo = sexo;
            usas.Foto = fotoBytes;
            ctx.SaveChanges();
            Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) || _numPai == 2)
            {
                return;
            }

            var usas = ctx.Usuarios.FirstOrDefault(u => u.Email == textBox2.Text);
            if (usas == null)
            {
                panel1.Visible = false;
                return;
            }

            panel1.Visible = true;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) || _numPai == 2)
            {
                return;
            }

            var usas = ctx.Usuarios.FirstOrDefault(u => u.Email == textBox2.Text);
            if (usas == null)
            {
                panel1.Visible = false;
                return;
            }

            panel1.Visible = true;

        }

        private void FormCadastro_Load(object sender, EventArgs e)
        {
            if (_numPai == 1 || _numPai == 0)
            {
                button1.Text = "Cadastrar";
                return;
            }
            var usas = ctx.Usuarios.FirstOrDefault(u => u.Email == UsuarioLogado.Email);

            label4.Text = "Alterar Perfil";
            textBox1.Text = UsuarioLogado.Nome;
            textBox2.Text = UsuarioLogado.Email;
            dateTimePicker1.Value = usas.Nascimento;

            if (usas.Sexo == "M")
                checkBox1.Checked = true;

            if (usas.Sexo == "F")
                checkBox2.Checked = true;

            if (usas.TimeFavoritoId != 0)
            {
                var selecoes = ctx.Selecoes.FirstOrDefault(s => s.Id == usas.TimeFavoritoId);
                comboBox1.Text = selecoes.Nome;
            }

            if (usas.Foto != null)
            {
                byte[] fotoBanco = usas.Foto;
                using (MemoryStream ms = new MemoryStream(fotoBanco))
                {
                    pictureBox1.BackgroundImage = Image.FromStream(ms);
                    pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            comboBox1.Items.AddRange(ctx.Selecoes.Select(s => s.Nome).ToArray());
            button1.Text = "Salvar";
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Enabled = false;
                sexo = "F";
                return;
            }
            checkBox1.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Enabled = false;
                sexo = "M";
                return;
            }
            checkBox2.Enabled = true;
        }
    }
}

