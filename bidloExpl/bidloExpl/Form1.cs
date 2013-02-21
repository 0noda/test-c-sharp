using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace bidloExpl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string[] getDir(string path)
        {
            string[] dir = Directory.GetDirectories(path);
            return dir;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) //НАЖАТИЕ НА ЭЛЕМЕНТ
        {
            string path = listBox1.SelectedItem.ToString();
            listBox1.Items.Clear();
            string[] dirList = new string[20];
            dirList = getDir(path);
            for (int i = 0; i < dirList.Count(); i++)
            {
                listBox1.Items.Add(dirList[i]);
            }
        }

        private void button1_Click_1(object sender, EventArgs e) //С:\
        {
            string path = @"C:\";
            listBox1.Items.Add(path);
        }

        private void button2_Click(object sender, EventArgs e) //ОЧИСТКА
        {
            listBox1.Items.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = @"C:\";
            listBox1.Items.Add(path);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

    }
}
