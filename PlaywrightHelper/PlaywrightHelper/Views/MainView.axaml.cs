using Avalonia.Controls;

namespace PlaywrightHelper.Views
{
    public partial class MainView : UserControl
    {
        
        public MainView()
        {
            InitializeComponent();
            TextEditor.Text = "THIS IS A TEST!!";
        }

    }
}