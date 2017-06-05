using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows;

namespace Signaturing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string path;
        private Structure signature = new Structure();

        public MainWindow()
        {
            InitializeComponent();
            GetSignature.IsEnabled = false;
        }

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog() { Filter = "DLL files | *.dll" };
            if (dlg.ShowDialog() == true)
            {
                path = dlg.FileName;
                signature.Hash = GetHash(path);
                signature.Date = File.GetCreationTime(path);
                GetSignature.IsEnabled = true;
            }
        }

        private byte[] GetHash(string path)
        {
            FileStream stream = File.OpenRead(path);
            SHA256Managed sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(stream);
            return hash;
        }

        private void GetSignature_Click(object sender, RoutedEventArgs e)
        {
            FileStream file = File.Open(Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".mys", FileMode.Create);
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine(JsonConvert.SerializeObject(signature));
            writer.Dispose();
            writer.Close();
            GetSignature.IsEnabled = false;
        }
    }
}
