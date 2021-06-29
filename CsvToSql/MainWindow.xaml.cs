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
using System.Windows.Shapes;

namespace CsvToSql
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string csvfile;

        private string CsvToSql(string csv)
        {
            string head = csv.Substring(0, csv.IndexOf(Environment.NewLine));
            head = head.Replace(";", ",");
            csv = csv.Substring(csv.IndexOf(Environment.NewLine));
            string[] builder = csv.Split(Environment.NewLine);
            string final = "";
            foreach (string item in builder)
            {
                if (item != "")
                {
                    final = final + "INSERT INTO " + tableBox.Text + " (" + head + ")" + " VALUES('" + item.Replace(";", "','") + "');" + Environment.NewLine;
                }
            }
            return final;
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "(*.csv)|*.csv|Wszystkie (*.*)|*.*";

            if (openFile.ShowDialog() == true)
            {
                csvfile = File.ReadAllText(openFile.FileName);
                textBox.Text = CsvToSql(csvfile);
                CopyToClipboardButton.IsEnabled = true;
            }
        }

        private void CopyToClipboard(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(textBox.Text);
        }

        private void OpenHelpWindow(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Show();
        }
    }
}