using System.Reflection;
using System.Windows;

namespace Crap
{
    /// <summary>
    /// Interaction logic for AboutBox.xaml
    /// </summary>
    public partial class AboutBox : Window
    {
        public AboutBox()
        {
            InitializeComponent();
            labelVersion.Content = "v" + Assembly.GetExecutingAssembly().GetName().Version;
        }

        private void btnCloseAbout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
