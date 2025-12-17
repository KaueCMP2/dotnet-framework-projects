using homeAdminUser_prova2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homeAdminUser_prova2
{
    public partial class FormCadastro : Parent
    {
        Form _formAnterior;
        public FormCadastro(Form formAnterior)
        {
            InitializeComponent();
            _formAnterior = formAnterior;
            dateTimePicker1.MaxDate = DateTime.Today.AddYears(-18);
        }

        private void FormCadastro_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formAnterior.Show();
        }

        byte[] fotoBytes;
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Filtro | *.png; *.jpg;";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackgroundImage = Image.FromFile(openFileDialog.FileName);
                fotoBytes = File.ReadAllBytes(openFileDialog.FileName);
            }
        }
        string sexo;

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Enabled = false;
                sexo = "M";
            }

            sexo = "";
            checkBox2.Enabled = true;
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Enabled = false;
                sexo = "F";
            }

            sexo = "";
            checkBox1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || sexo == null)
            {
                "Todos os campos são obrigatórios, exceto o Time Favorito e a Foto.".Alert();
                return;
            }
            
            var usas = ctx.Usuarios.FirstOrDefault(u => u.Email == textBox2.Text);
            if (usas != null)
            {
                "Email em uso".Alert();
            }

            Usuarios usu = new Usuarios();
            usu.Nome = textBox1.Text;
            usu.Email = textBox2.Text;
            usu.Nascimento = dateTimePicker1.Value;
            usu.Sexo = sexo;

            if (comboBox1.SelectedItem != null)
            {
                var timeF = ctx.Selecoes.FirstOrDefault(t => t.Nome == comboBox1.Text);
                usu.TimeFavoritoId = timeF.Id;
            }            
            if(fotoBytes != null)
            {
                usu.Foto = fotoBytes;
            }

            ctx.Usuarios.Add(usu);
            ctx.SaveChanges();
            Close();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox2.Text, @"^[a-zA-Z][a-z0-9]{3,}([-_.][a-zA-Z0-9]{4,})?@[a-zA-Z][2,]$"))
            {
                "\"Email precisa começar com uma letra, ter no mínimo 4 caracteres antes do @, pode conter números, pode ter opcionalmente um dos caracteres especiais (-, _, .) com pelo menos 4 caracteres antes e depois, o domínio após o @ deve ter pelo menos 3 letras, e o TLD no mínimo 2 letras.\"".Alert();
                textBox2.Focus();
                return;
            }

            bool emailJaUsado = ctx.Usuarios.Any(em => em.Email == textBox2.Text);
            if(emailJaUsado == true)
            {
                panel1.Visible = true;
                panel1.BackColor = Color.Red;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void FormCadastro_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(ctx.Selecoes.Select(s => s.Nome).ToArray());
        }
    }
}
