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
        //フィールド
        String path = @"C:\ue8d";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Pathの表示
            Label1(this.path);
            label3.Text = "拡張子";
            textBox1.Text = "jpg";
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
                this.path = folderBrowserDialog1.SelectedPath;
            }

            //Pathの表示
            Label1(this.path);

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

        private void 配布サイトToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ue8d/imagelister");
        }

        private void 製作者TwitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitter.com/ue8d_");
        }

        /*
         * 
         * 
         * ここまでメニュー欄
         * 
         */

        private void button1_Click(object sender, EventArgs e)
        {
            //path以下のファイルをすべて取得する
            //ワイルドカード"*"は、すべてのファイルを意味する
            string[] files = System.IO.Directory.GetFiles(
                this.path, "*", System.IO.SearchOption.AllDirectories);

            //ListBox1に結果を表示する
            listBox1.Items.Clear();
            listBox1.Items.AddRange(files);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string extension = "*." + textBox1.Text;
            string imageDir = this.path; // 画像ディレクトリ
            string[] jpgFiles = Directory.GetFiles(imageDir, extension, SearchOption.AllDirectories);

            int width = 100;
            int height = 80;

            imageList1.ImageSize = new Size(width, height);
            listView1.LargeImageList = imageList1;

            for (int i = 0; i < jpgFiles.Length; i++)
            {
                Image original = Bitmap.FromFile(jpgFiles[i]);
                Image thumbnail = createThumbnail(original, width, height);

                imageList1.Images.Add(thumbnail);
                listView1.Items.Add(jpgFiles[i], i);

                original.Dispose();
                thumbnail.Dispose();
            }
        }

        // 幅w、高さhのImageオブジェクトを作成
        Image createThumbnail(Image image, int w, int h)
        {
            Bitmap canvas = new Bitmap(w, h);

            Graphics g = Graphics.FromImage(canvas);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, w, h);

            float fw = (float)w / (float)image.Width;
            float fh = (float)h / (float)image.Height;

            float scale = Math.Min(fw, fh);
            fw = image.Width * scale;
            fh = image.Height * scale;

            g.DrawImage(image, (w - fw) / 2, (h - fh) / 2, fw, fh);
            g.Dispose();

            return canvas;
        }

        //label1管理
        private void Label1(String path)
        {
            label1.Text = "検索ファイル：" + path;
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
