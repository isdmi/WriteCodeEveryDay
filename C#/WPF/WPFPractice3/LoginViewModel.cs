using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WPFPractice3
{
    public class LoginViewModel : INotifyPropertyChanged
    {
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
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object parameter)
        {
            // ここでユーザー名とパスワードを検証するロジックを実装する

            if (UserId == "user" && Password == "password")
            {
                // ログイン成功時の処理
                MessageBox.Show("Login successful!");

                ErrorMessage = "";
                LoginSucceeded?.Invoke();   // ← ログイン成功を通知
            }
            else
            {
                // ログイン失敗時の処理
                ErrorMessage = "Invalid username or password.";
            }
        }
    }
}