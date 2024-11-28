using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PokeLike.MVVM.ViewModel;


namespace PokeLike
{
    public partial class MainWindow : Window
    {
        public MainViewVM MainViewVM { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // Créer et initialiser le ViewModel principal
            MainViewVM = new MainViewVM();

            // Assigner le DataContext à MainViewVM
            DataContext = MainViewVM;
        }
    }
}