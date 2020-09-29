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

namespace imagelister
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
            //上部に表示する説明テキストを指定する
            folderBrowserDialog1.Description = "フォルダを指定してください";
            //ルートフォルダを指定する
            //デフォルトでDesktop
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
            //最初に選択するフォルダを指定する
            //RootFolder以下にあるフォルダである必要がある
            folderBrowserDialog1.SelectedPath = @"C:";
            //ユーザーが新しいフォルダを作成できるようにする
            //デフォルトでTrue
            folderBrowserDialog1.ShowNewFolderButton = true;

            //ダイアログを表示する
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                //選択されたフォルダを表示する
                listBox1.Items.Add(folderBrowserDialog1.SelectedPath);
            }
            
        }

        /*
         * 
         *  メニュー欄
         *
         */

        private void 新規NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1_HelpRequest(sender,e);
        }

        private void 開くOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1_HelpRequest(sender, e);
        }

        private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
