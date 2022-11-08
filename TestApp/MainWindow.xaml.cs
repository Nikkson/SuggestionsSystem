using System;
using System.Collections.Generic;
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

namespace TestApp
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

        private void openSearchDataWindow(object sender, RoutedEventArgs e)
        {
            SearchDataWindow sdw = new SearchDataWindow();
            sdw.Show();
        }

        private void openEnterDataWindow(object sender, RoutedEventArgs e)
        {
            EnterDataWindow edw = new EnterDataWindow();
            edw.Show();
        }
    }
}
