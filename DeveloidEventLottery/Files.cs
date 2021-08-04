using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace DeveloidEventLottery
{
    class Files
    {
        // 저장 경로 가져오기
        public static string GetSaveFilePath(string filename)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFile.Title = "저장 경로를 선택해주세요.";
            saveFile.OverwritePrompt = true;
            saveFile.FileName = filename;
            saveFile.DefaultExt = "csv";
            saveFile.Filter = "CSV file (*.csv)|*.csv";

            if (saveFile.ShowDialog() == true) return saveFile.FileName;
            else return null;
        }

        // 저장 폴더 경로 가져오기
        public static string GetSaveFolderPath()
        {
            System.Windows.Forms.FolderBrowserDialog saveFolder = new System.Windows.Forms.FolderBrowserDialog();

            if (saveFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK) return saveFolder.SelectedPath;
            else return null;
        }

        // 여는 파일 경로 가져오기
        public static string GetOpenFilePath()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "불러올 파일을 선택해주세요.";
            openFile.DefaultExt = "csv";
            openFile.Filter = "CSV file (*.csv)|*.csv";

            if (openFile.ShowDialog() == true) return openFile.FileName;
            else return null;
        }
    }
}
