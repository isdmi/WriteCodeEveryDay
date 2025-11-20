using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WPFPractice3
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly EmployeeRepository _repository = new EmployeeRepository();

        public event Action LoginSucceeded;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// ユーザーID
        /// </summary>
        private string _userId = "";
        public string UserId
        {
            get { return _userId; }

            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }

        /// <summary>
        /// パスワード
        /// </summary>
        private string _password = "";
        public string Password
        {
            get { return _password; }

            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        private string _errorMessage = "aaaaa";

        public string ErrorMessage
        {
            get { return _errorMessage; }

            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(async () => await Login());
        }

        private async Task Login()
        {
            ErrorMessage = "";

            var employee = await _repository.GetEmployeeAsync(UserId, Password);

            if (employee != null)
            {
                // ログイン成功時の処理
                MessageBox.Show("Login successful!");

                LoginSucceeded?.Invoke();
            }
            else
            {
                // ログイン失敗時の処理
                ErrorMessage = "Invalid username or password.";
            }
        }
    }
}