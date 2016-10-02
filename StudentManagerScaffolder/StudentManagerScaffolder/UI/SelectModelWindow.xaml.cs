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

namespace StudentManagerScaffolder.UI
{
    /// <summary>
    /// Interaction logic for SelectModelWindow.xaml
    /// </summary>
    public partial class SelectModelWindow : Window
    {
        public Model _viewModel { get; set; }
        public SelectModelWindow(Model viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

            DataContext = viewModel;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            _viewModel.Scaffold = true;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Scaffold = false;
        }
    }
}
