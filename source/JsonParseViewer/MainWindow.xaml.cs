using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace JsonParseViewer
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    partial class MainWindow
    {
        string message;

        public MainWindow()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.FirstChanceException += First_Change_Exception;
        }

        private void First_Change_Exception(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            string eMember = e.Exception.TargetSite.Name;
            string eMessage = e.Exception.Message;
            message = string.Format(
                "例外が{0}で発生。プログラムは継続します。\n{1}",
                eMember,
                eMessage);
            /*
            MessageBox.Show(
                message,
                "FirstChanceException",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            */
        }

        private void Parse(object sender, RoutedEventArgs e)
        {
            var target = this.TextBox.Text;

            if (!string.IsNullOrEmpty(target))
            {
                try
                {
                    this.TextBox.Text = format_json(target);
                }
                catch
                {
                    this.TextBox.Text = message;
                }
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Json Files(*.json)|*.json|Text Files(*.txt)|*.txt|すべてのファイル(*.*)| *.*";

            if (sfd.ShowDialog() == true)
            {
                File.WriteAllText(sfd.FileName, this.TextBox.Text);
            }
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            this.TextBox.Text = null;
        }

        private static string format_json(string json)
        {
            //dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(JsonConvert.DeserializeObject(json), Formatting.Indented);
        }
    }
}
