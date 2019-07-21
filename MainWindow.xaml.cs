using System.Globalization;
using System.Windows;

using WPFLocalizeExtension.Engine;

namespace LTFGameLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LocalizeDictionary.Instance.SetCurrentThreadCulture = true;

            //Set Extension Culture to System culture:
            LocalizeDictionary.Instance.Culture = new CultureInfo(CultureInfo.CurrentUICulture.Name);
        }
    }
}