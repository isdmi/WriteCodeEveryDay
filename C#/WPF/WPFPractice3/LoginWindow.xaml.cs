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

namespace WPFPractice3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            if (DataContext is LoginViewModel vm)
            {
                vm.LoginSucceeded += OnLoginSucceeded;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is LoginViewModel vm)
            {
                vm.Password = ((PasswordBox)sender).Password;
            }
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (DataContext is LoginViewModel vm)
                {
                    if (vm.LoginCommand.CanExecute(null))
                        vm.LoginCommand.Execute(null);
                }
            }
        }

        private void OnLoginSucceeded(Employee emp)
        {
            // メイン画面を開く
            var mainWindow = new MainWindow(emp);
            mainWindow.Show();

            // このログイン画面を閉じる
            this.Close();
        }
    }
}