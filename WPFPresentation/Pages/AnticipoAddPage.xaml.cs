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

namespace WPFPresentation.Pages
{
    /// <summary>
    /// Interaction logic for AnticipoAddPage.xaml
    /// </summary>
    public partial class AnticipoAddPage : UserControl
    {
        public AnticipoAddPage()
        {
            InitializeComponent();
        }

        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
            var textbox = (TextBox)sender;
            textbox.Select(0, textbox.Text.Length);
        }
    }
}
