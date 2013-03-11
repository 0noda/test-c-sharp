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
using System.Diagnostics;

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

        public bool fileOrDir(string path) //Если файл директоря - тру, если файл - фолс
        {
            if(Directory.Exists(path) == true)
                return true;
            if(File.Exists(path) == true)
                return  false;
            MessageBox.Show("ЕГГОГ");
            return true;
        }

        public string shortDir(string path)//"C:\dir1\file.ext" -> "file.ext"
        {
            int index = path.LastIndexOf("\\");
            path = path.Remove(0,index+1);
            return path;
        }

        private void button2_Click(object sender, EventArgs e) //Clear
        {
            string path = @"C:\";
            string[] dir = Directory.GetDirectories(path);
            updateListBox1(dir, path);
            updateListBox2(dir, path);
        }

        private void Form1_Load(object sender, EventArgs e) 
        {
            string path = @"C:\";
            string[] dir = Directory.GetDirectories(path);
            updateListBox1(dir, path);
            updateListBox2(dir, path);
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

        public void updateListBox1(string[] dirList, string path)//Вывод содержимого директории в листбокс1
        { 
                listBox1.Items.Clear();
                dirList = getDir(path);
                string fromTextBox = textBox1.Text.ToString();
                for (int i = 0; i < dirList.Count(); i++)
                {
                    dirList[i] = shortDir(dirList[i]); //######
                    listBox1.Items.Add(dirList[i]);
                    //dirList[i] = fromTextBox + dirList[i];
                }
                dirList = Directory.GetFiles(path);
                for (int i = 0; i < dirList.Count(); i++)
                {
                    dirList[i] = shortDir(dirList[i]); //######
                    listBox1.Items.Add(dirList[i]);
                    //dirList[i] = fromTextBox + dirList[i];
                }
        }

        public void updateListBox2(string[] dirList, string path)//Вывод содержимого директории в листбокс2
        {
            listBox2.Items.Clear();
            dirList = getDir(path);
            for (int i = 0; i < dirList.Count(); i++)
            {
                dirList[i] = shortDir(dirList[i]);
                listBox2.Items.Add(dirList[i]);
            }
            dirList = Directory.GetFiles(path);
            for (int i = 0; i < dirList.Count(); i++)
            {
                dirList[i] = shortDir(dirList[i]);
                listBox2.Items.Add(dirList[i]);
            }
        }

        private void button3_Click(object sender, EventArgs e) //Предыдущая директория листбокс1
        {
            string path = textBox1.Text.ToString();
            if (path.Count() > 2)
            {
                string slash = "\\";
                int index = path.LastIndexOf(slash);
                path = retDir(path, index); // C:\dir1\dir2 -> C:\dir1
                if (path.Count() == 2)
                    path += "\\";
                textBox1.Text = path;
                //MessageBox.Show(path,i.ToString());
                string[] dirList = new string[20];
                updateListBox1(dirList,path);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = textBox2.Text.ToString();
            if (path.Count() > 2)
            {
                string slash = "\\";
                int index = path.LastIndexOf(slash);
                path = retDir(path, index); // C:\dir1\dir2 -> C:\dir1
                if (path.Count() == 2)
                    path += "\\";
                textBox2.Text = path;
                //MessageBox.Show(path,i.ToString());
                string[] dirList = new string[20];
                updateListBox2(dirList, path);
            }
        } //Предыдущая директория листбокс2

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)//Нажатие на элимент листбокса1
        {
            string path = textBox1.Text.ToString()+ "//" + listBox1.SelectedItem.ToString();
            DateTime time = Directory.GetCreationTime(path);
            textBox3.Text = time.ToLongDateString();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)//Нажатие на элимент листбокса2
        {
            string path = textBox2.Text.ToString() + "//" + listBox2.SelectedItem.ToString();
            DateTime time = Directory.GetCreationTime(path);
            textBox3.Text = time.ToLongDateString();
        } 

        private void button5_Click(object sender, EventArgs e)//Кнопка Delete1
        {
            string path = textBox1.Text.ToString() + "\\" + listBox1.SelectedItem.ToString();
            if (fileOrDir(path) == true)
                Directory.Delete(path);
            else
                File.Delete(path);
            Directory.Delete(path, true);
            path = textBox1.Text.ToString();
            string[] dir = Directory.GetDirectories(path);
            updateListBox1(dir,path);
        }

        private void button6_Click(object sender, EventArgs e)//Кнопка Delete2
        {
            string path = textBox2.Text.ToString() + "\\" + listBox2.SelectedItem.ToString();
            if (fileOrDir(path) == true)
                Directory.Delete(path,true);
            else
                File.Delete(path);
            path = textBox2.Text.ToString();
            string[] dir = Directory.GetDirectories(path);
            updateListBox2(dir, path);
        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string path = textBox2.Text.ToString();
            if (fileOrDir(path) == true)
            {
                if (path != "C:\\")
                {
                    if (fileOrDir(path + "\\" + listBox2.SelectedItem.ToString()) == true)
                    {
                        textBox2.Text = path + "\\" + listBox2.SelectedItem.ToString();
                        path = path + "\\" + listBox2.SelectedItem.ToString();
                    }
                    else
                        Process.Start(path + "\\" + listBox2.SelectedItem.ToString());
                }
                else
                {
                    if (fileOrDir(path + "\\" + listBox2.SelectedItem.ToString()) == true)
                    {
                        textBox2.Text = path + listBox2.SelectedItem.ToString();
                        path = path + listBox2.SelectedItem.ToString();
                    }
                    else
                        Process.Start(path + "\\" + listBox2.SelectedItem.ToString());
                }
                string[] dirList = new string[20];
                if (fileOrDir(path) == true)
                    updateListBox2(dirList, path);//обновить листбокс2
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string path = textBox1.Text.ToString();
            if (fileOrDir(path) == true)
            {
                if (path != "C:\\")
                {
                    if (fileOrDir(path + "\\" + listBox1.SelectedItem.ToString()) == true)
                    {
                        textBox1.Text = path + "\\" + listBox1.SelectedItem.ToString();
                        path = path + "\\" + listBox1.SelectedItem.ToString();
                    }
                    else
                        Process.Start(path + "\\" + listBox1.SelectedItem.ToString());
                }
                else
                {
                    if (fileOrDir(path + "\\" + listBox1.SelectedItem.ToString()) == true)
                    {
                        textBox1.Text = path + listBox1.SelectedItem.ToString();
                        path = path + listBox1.SelectedItem.ToString();
                    }
                    else
                        Process.Start(path + "\\" + listBox1.SelectedItem.ToString());
                }
                string[] dirList = new string[20];
                if (fileOrDir(path) == true)
                    updateListBox1(dirList, path);//обновить листбокс2
            }
        }

        private void button4_Click(object sender, EventArgs e)//Кнопка Move1
        {
            string fromPath = textBox1.Text.ToString() + "\\" + listBox1.SelectedItem.ToString();
            string toPath = textBox2.Text.ToString() + "\\" + shortDir(fromPath);
            if((fileOrDir(fromPath) == true))
                Directory.Move(fromPath, toPath);
            else
                File.Copy(fromPath, toPath, true);
            string path = textBox2.Text.ToString();
            string[] dir = Directory.GetDirectories(path);
            updateListBox2(dir, path);
        }

        private void button7_Click(object sender, EventArgs e)//Кнопка Move2
        {
            string fromPath = textBox2.Text.ToString() + "\\" + listBox2.SelectedItem.ToString();
            string toPath = textBox1.Text.ToString() + "\\" + shortDir(fromPath);
            if((fileOrDir(fromPath) == true))
                Directory.Move(fromPath, toPath);
            else
                File.Copy(fromPath, toPath, true);
            string path = textBox1.Text.ToString();
            string[] dir = Directory.GetDirectories(path);
            updateListBox1(dir, path);
        }



        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string path = textBox2.Text.ToString();
                DirectoryInfo source = new DirectoryInfo(path);
                if (source.Exists == true)
                {
                    string[] dir = Directory.GetDirectories(path);
                    updateListBox2(dir, path);
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string path = textBox1.Text.ToString();
                DirectoryInfo source = new DirectoryInfo(path);
                if (source.Exists == true)
                {
                    string[] dir = Directory.GetDirectories(path);
                    updateListBox1(dir, path);
                }
            }
        }
    }
}