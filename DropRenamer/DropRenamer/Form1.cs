using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DropRenamer
{
    public partial class Form1 : Form
    {
        // 
        // Properties
        //
        #region Properties


        #endregion Properties


        public Form1()
        {
            InitializeComponent();

            Initialize();
        }

        //
        // Methods
        //
        #region Methods
        private void Initialize()
        {

        }

        private void Rename(string filePath)
        {

        }

        #endregion Methods


        //
        // Events
        //
        #region Events

        private void label3_DragEnter(object sender, DragEventArgs e)
        {
            // ファイルをドロップできるようにする
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        // ファイルドロップ時の処理
        private void label3_DragDrop(object sender, DragEventArgs e)
        {
            // ファイルに付加する文字列
            string appendStr = textBox1.Text;

            // 連番の桁数            
            int numberOf = Int32.Parse( textBox2.Text);

            
            // ドロップしたファイルを取得
            string[] filePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            int count = 1;

            foreach (var filePath in filePaths)
            {
                if (File.Exists(filePath) == false)
                {
                    // フォルダは除外
                    continue;
                }
                // フォルダパス
                string dirPath = Path.GetDirectoryName(filePath);

                // フォルダ名
                string dirName = Path.GetFileName(dirPath);

                // リネーム先のファイル名を作成
                string fileName = string.Format("{0}{1}{2:D3}{3}", dirName, appendStr, count, Path.GetExtension(filePath));
                string newFilePath = Path.Combine(dirPath, fileName);

                File.Move(filePath, newFilePath);
                Console.WriteLine(newFilePath);

                count++;
            }

            if (count > 1)
            {
                MessageBox.Show(String.Format("{0}個のファイルをリネームしました", count - 1), "complete");
            }
            else
            {
                MessageBox.Show("リネームするファイルが見つかりませんでした。");
            }
        }

        #endregion Events
    }
}
