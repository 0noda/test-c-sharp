﻿using System;
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

        public string[] getDir(string path) //Получить список директорий
        {
            string[] dir = Directory.GetDirectories(path);
            return dir;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) //НАЖАТИЕ НА ЭЛЕМЕНТ
        {
        }

        private void button2_Click(object sender, EventArgs e) //Очистка
        {
            listBox1.Items.Clear();
            listBox1.Items.Add(@"C:\");
            listBox2.Items.Clear();
            listBox2.Items.Add(@"C:\");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = @"C:\";
            listBox1.Items.Add(path);
            listBox2.Items.Add(path);
        } //События при создании форми

        public string retDir(string path, int index)
        {
            int m = path.Count() - index;
            for (; index < path.Count(); index++)
            {
                path = path.Remove(index, m);
            }
            return path;
        } // C:\dir1\dir2 -> C:\dir1

        public void updateListBox1(string[] dirList, string path)//обновить листбокс1
        { 
                listBox1.Items.Clear();
                dirList = getDir(path);
                for (int i = 0; i < dirList.Count(); i++)
                {
                    listBox1.Items.Add(dirList[i]);
                }
                dirList = Directory.GetFiles(path);
                for (int i = 0; i < dirList.Count(); i++)
                {
                    listBox1.Items.Add(dirList[i]);
                }
        }

        public void updateListBox2(string[] dirList, string path)//обновить листбокс2
        {
            listBox2.Items.Clear();
            dirList = getDir(path);
            for (int i = 0; i < dirList.Count(); i++)
            {
                listBox2.Items.Add(dirList[i]);
            }
            dirList = Directory.GetFiles(path);
            for (int i = 0; i < dirList.Count(); i++)
            {
                listBox2.Items.Add(dirList[i]);
            }
        }

        private void button3_Click(object sender, EventArgs e) //предыдущая директория листбокс1
        {
            string path = textBox1.Text.ToString();
            if (path != @"C:")
            {
                string slash = "\\";
                int index = path.LastIndexOf(slash);
                path = retDir(path, index); // C:\dir1\dir2 -> C:\dir1
                if (path == @"C:")
                    path += "\\";
                textBox1.Text = path;
                //MessageBox.Show(path,i.ToString());
                string[] dirList = new string[20];
                updateListBox1(dirList,path);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        } //Нажатие на элимент второго листбокса

        private void button1_Click(object sender, EventArgs e)
        {
            string path = textBox2.Text.ToString();
            if (path != @"C:")
            {
                string slash = "\\";
                int index = path.LastIndexOf(slash);
                int k = path.Count();
                int m = k - index;
                for (; index < path.Count(); index++)
                {
                    path = path.Remove(index, m);
                }
                if (path == @"C:")
                    path += "\\";
                textBox2.Text = path;
                string[] dirList = new string[20];
                updateListBox2(dirList,path);
            }
        } //return list2

        private void button5_Click(object sender, EventArgs e)//Удалить файл
        {
            string path = listBox1.SelectedItem.ToString();
            File.Delete(path);
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string path = listBox2.SelectedItem.ToString();
            if (path.Contains(".") == false)
            {
                textBox2.Text = path;
                string[] dirList = new string[20];
                updateListBox2(dirList,path);
            }

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            string path = listBox1.SelectedItem.ToString();
            if (path.Contains(".") == false)
            {
                textBox1.Text = path;
                string[] dirList = new string[20];
                updateListBox1(dirList,path);//обновить листбокс1
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string fromPath =listBox1.SelectedItem.ToString();
            string toPath = textBox2.Text.ToString();
            File.Copy(fromPath,toPath);
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
