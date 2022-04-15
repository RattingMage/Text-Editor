using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace IDE
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt|RichText Files (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (sfd.ShowDialog() == true)
            {
                TextRange doc = new TextRange(docBox.Document.ContentStart, docBox.Document.ContentEnd);
                using (FileStream fs = File.Create(sfd.FileName))
                {
                    if (Path.GetExtension(sfd.FileName).ToLower() == ".rtf")
                        doc.Save(fs, DataFormats.Rtf);
                    else if (Path.GetExtension(sfd.FileName).ToLower() == ".txt")
                        doc.Save(fs, DataFormats.Text);
                }
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt|RichText Files (*.rtf)|*.rtf|All files (*.*)|*.*";

            if (ofd.ShowDialog() == true)
            {
                TextRange doc = new TextRange(docBox.Document.ContentStart, docBox.Document.ContentEnd);
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open))
                {
                    if (Path.GetExtension(ofd.FileName).ToLower() == ".rtf")
                        doc.Load(fs, DataFormats.Rtf);
                    else if (Path.GetExtension(ofd.FileName).ToLower() == ".txt")
                        doc.Load(fs, DataFormats.Text);
                }
            }
        }
        private void Help_Executed(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Справка по приложению");
        }
        private void Change_Fonts(object sender, RoutedEventArgs e)
        {
            docBox.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, (sender as MenuItem).Header.ToString());
        }
        private void Change_Size(object sender, RoutedEventArgs e)
        {
            docBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, (sender as MenuItem).Header.ToString());
        }
        private void Change_Color(object sender, RoutedEventArgs e)
        {
            docBox.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, (sender as MenuItem).Header.ToString());
        }
        private void Italic(object sender, RoutedEventArgs e)
        {
            docBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, "Italic");
        }
        private void Bold(object sender, RoutedEventArgs e)
        {
            docBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, "Bold");
        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            docBox.Selection.ClearAllProperties();
        }
    }
}
