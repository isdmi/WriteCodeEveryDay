using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPractice3
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _searchName;
        public string SearchName
        {
            get => _searchName;
            set
            {
                _searchName = value;
                OnPropertyChanged(nameof(SearchName));
                FilterEmployees();
            }
        }

        private ObservableCollection<EmployeeList> _employees;
        public ObservableCollection<EmployeeList> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        // 元データ（検索用）
        private ObservableCollection<EmployeeList> _allEmployees;

        public MainViewModel()
        {
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            // 本来は DB から取得する
            _allEmployees = new ObservableCollection<EmployeeList>
        {
            new EmployeeList { Id=1, Name="田中太郎", Department="総務", Email="tanaka@example.com" },
            new EmployeeList { Id=2, Name="山田花子", Department="営業", Email="yamada@example.com" },
            new EmployeeList { Id=3, Name="鈴木一郎", Department="開発", Email="suzuki@example.com" }
        };

            Employees = new ObservableCollection<EmployeeList>(_allEmployees);
        }

        private void FilterEmployees()
        {
            if (string.IsNullOrWhiteSpace(SearchName))
            {
                Employees = new ObservableCollection<EmployeeList>(_allEmployees);
            }
            else
            {
                Employees = new ObservableCollection<EmployeeList>(
                    _allEmployees.Where(e => e.Name.Contains(SearchName))
                );
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
