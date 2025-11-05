using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;

namespace WPFPractice
{
    public partial class CounterViewModel : ObservableValidator
    {
        // ▼ 入力値（データアノテーションでバリデーション）
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Range(0, 100, ErrorMessage = "0〜100の間で入力してください")]
        private int? userInput;

        // ▼ エラーテキスト（画面表示用）
        public string ErrorsText
        {
            get
            {
                var errors = GetErrors(nameof(UserInput))?.Cast<ValidationResult>();
                if (errors == null || !errors.Any()) return string.Empty;

                return string.Join("\n", errors.Select(e => e.ErrorMessage));
            }
        }

        // ▼ ボタン
        [RelayCommand(CanExecute = nameof(CanSubmit))]
        private void Submit()
        {
        }

        private bool CanSubmit() => !HasErrors;

        // ▼ 入力値が変わったとき
        partial void OnUserInputChanged(int? value)
        {
            // バリデーション
            ValidateProperty(value, nameof(UserInput));

            // ErrorsText 更新
            OnPropertyChanged(nameof(ErrorsText));

            // ボタン有効/無効更新
            SubmitCommand.NotifyCanExecuteChanged();
        }


        [ObservableProperty]
        private int count;

        [RelayCommand]
        private void Increment()
        {
            Count++;
        }

        [RelayCommand]
        private void Decrement()
        {
            Count--;
        }
    }
}