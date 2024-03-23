using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace Coursework.Helpers
{
    public class WordHelper
    {
        private FileInfo _fileInfo;
        public WordHelper(string fileName)
        {
            if (!File.Exists(fileName))
                MessageBox.Show($"не нашел файл по пути {fileName}");

            _fileInfo = new FileInfo(fileName);
        }

        public bool WriteIntoDocument(Dictionary<string, string> values, string dirName, string productName)
        {
            Word.Application app = null;
            try
            {
                app = new Word.Application();
                Object file = _fileInfo.FullName;

                Object missing = Type.Missing;

                app.Documents.Open(file);

                foreach (var value in values)
                {
                    Word.Find find = app.Selection.Find;
                    find.Text = value.Key;
                    find.Replacement.Text = value.Value;

                    Object wrap = Word.WdFindWrap.wdFindContinue;
                    Object replace = Word.WdReplace.wdReplaceAll;

                    find.Execute(FindText: Type.Missing,
                        MatchCase: false,
                        MatchWholeWord: false,
                        MatchWildcards: false,
                        MatchSoundsLike: missing,
                        MatchAllWordForms: false,
                        Forward: true,
                        Wrap: wrap,
                        Format: false,
                        ReplaceWith: missing,
                        Replace: replace);                   
                }

                var pathToDir = AppDomain.CurrentDomain.BaseDirectory;
                string path = $"{pathToDir}Word\\{dirName}\\{DateTime.Now.ToString("yyyy_MM_dd_H")}-{productName}_{_fileInfo.Name}";

                app.ActiveDocument.SaveAs2(path);
                app.ActiveDocument.Close();
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось сделать гарантию",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return false;
            }
            finally 
            {
                if (app != null)
                    app.Quit();
            }
        }
    }
}
