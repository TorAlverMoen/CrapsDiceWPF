using System.Windows;

namespace Crap
{
    /// <summary>
    /// Interaction logic for HowToPlay.xaml
    /// </summary>
    public partial class HowToPlay : Window
    {
        public HowToPlay()
        {
            InitializeComponent();
        }

        private void btnCloseHowTo_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
