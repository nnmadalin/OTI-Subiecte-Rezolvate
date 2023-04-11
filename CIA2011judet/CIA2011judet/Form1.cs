using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CIA2011judet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        int lenght = 0;

        private void button3_Click(object sender, EventArgs e)
        {
            lenght = richTextBox1.Text.Length;
            if(lenght != 0)
            {
                richTextBox1.AppendText("\r\n");
            }

            richTextBox1.AppendText("Ionel: " + richTextBox2.Text);
            richTextBox1.Select(lenght, 6);
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.Select(lenght, 8 + richTextBox2.Text.Length);
            richTextBox1.SelectionColor = Color.Blue;

            richTextBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lenght = richTextBox1.Text.Length;
            if (lenght != 0)
            {
                richTextBox1.AppendText("\r\n");
            }

            richTextBox1.AppendText("Maria: " + richTextBox2.Text);
            richTextBox1.Select(lenght, 6);
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.Select(lenght, 8 + richTextBox2.Text.Length);
            richTextBox1.SelectionColor = Color.Red;

            richTextBox2.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            DialogResult dialog = MessageBox.Show("Esti sigur ca vrei sa stergi fereastra?", "Sterge", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(dialog == DialogResult.Yes)
            {
                richTextBox1.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog1.FileName = "";
            DialogResult dialog = saveFileDialog1.ShowDialog();
            if(dialog == DialogResult.OK)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName);
                MessageBox.Show("Fisier salvat cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog1.FileName = "";
            DialogResult dialog = openFileDialog1.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                richTextBox1.LoadFile(openFileDialog1.FileName);
                MessageBox.Show("Fisier incarcat cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
