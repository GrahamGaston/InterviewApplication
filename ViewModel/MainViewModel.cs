using InterviewProblem.Model;
using Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblem.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            // Fill users from database       

            Temperatures = new ObservableCollection<TemperatureViewModel>();
            Temperatures.Add(new TemperatureViewModel(new Temperature() { TimeStamp = DateTime.Now }));
            OnPropertyChanged("Temperatures");
        }

        public List<User> Users { get; set; }

        private User _selectedUser;
        public User SelectedUser { 
            get
            {
                return _selectedUser;
            } 
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
                // Pull Temperature history and fill Temperatures

            }
        }

        public ObservableCollection<TemperatureViewModel> Temperatures { get; set; }
    }
}
