using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using GradedAssignments.Model;
using Microsoft.Win32;

namespace GradedAssignments
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<User> _users = new ObservableCollection<User>();
        public MainWindow()
        {
            InitializeComponent();

        }

        private void MenuFileOpen_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "C:\\",
                Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() != true) return;
            _users = User.ReadCsvFile(openFileDialog.FileName);
            ListBox.ItemsSource = _users;
            StatusBarTime.Content = "Last time loaded: " + DateTime.Now.ToShortTimeString();
            StatusBarNumber.Content = "Number of items in listbox: " + _users.Count;
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var i = ((ListBox) sender).SelectedIndex;
            DataContext = _users[i];
        }

        private void TextBoxChanged(object sender, TextChangedEventArgs e)
        {
            var index = ListBox.SelectedIndex;
            if (index < 0) return;

            var tb = (TextBox) sender;
            var user = _users[index];
            var tbName = tb.Name.Replace("Text", "");
            try
            {
                switch (tbName)
                {
                    case "Name":
                        user.Name = tb.Text;
                        break;
                    case "Score":
                        user.Score = int.Parse(tb.Text);
                        break;
                    case "Age":
                        user.Age = int.Parse(tb.Text);
                        break;
                    case "Id":
                        user.Id = tb.Text;
                        break;
                    default:
                        throw new Exception("User field not found");
                }
                ListBox.Items.Refresh();
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception.Message);
            }
            
        }
    }
}
