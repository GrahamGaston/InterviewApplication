using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InterviewProblem.DatabaseLayer;
using InterviewProblem.Model;
using Mvvm;
using Mvvm.Commands;

namespace InterviewProblem.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private const string DatabasePath = @"TempLogger.db";

        public MainViewModel()
        {
            // Fill users from database       

            OnPropertyChanged("Temperatures");
            _dl = new TempLoggerDal(DatabasePath);
            Users = new ObservableCollection<User>(_dl.GetAllUsers().ToList());
            CurrentTemperature = new Temperature();

        }

        public ObservableCollection<User> Users { get; set; }

        private User _selectedUser;
        public User SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
                if (value is null) return;
                PopulateMeasurements();

            }
        }

        private Temperature _currentTemperature;
        public Temperature CurrentTemperature
        {
            get => _currentTemperature;
            set
            {
                _currentTemperature = value;
                OnPropertyChanged();
            }
        }

        public string SelectedUserName { get; set; }

        public bool CanLogTemperature => !(SelectedUserName is null);
        public ObservableCollection<TemperatureViewModel> Temperatures { get; set; } = new ObservableCollection<TemperatureViewModel>();

        private readonly TempLoggerDal _dl;
        private List<Temperature> _temperatureCollection;
        private RelayCommand _captureTemperature;

        public RelayCommand CaptureTemperature
                => _captureTemperature ?? (_captureTemperature = new RelayCommand(param => LogTemperature(),
                                                                                  param => CanLogTemperature));
        private void LogTemperature()
        {
            AddUserIfNew();
            CurrentTemperature.TimeStamp = DateTime.Now;
            _dl.AddMeasurement(SelectedUser, CurrentTemperature);
            Temperatures.Add(new TemperatureViewModel(CurrentTemperature));
            CurrentTemperature = new Temperature();

        }

        private void AddUserIfNew()
        {
            if (!Users.Any(u => string.Equals(u.UserName, SelectedUserName, StringComparison.OrdinalIgnoreCase)))
            {
                Users.Add(_dl.AddUser(SelectedUserName));
                SelectedUser = Users.Single(u => u.UserName == SelectedUserName);

            }
        }

        private void PopulateMeasurements()
        {
            _temperatureCollection = _dl.GetMeasurementsForUser(SelectedUser).ToList();
            Temperatures.Clear();
            foreach (var temp in _temperatureCollection)
            {
                Temperatures.Add(new TemperatureViewModel(temp));
            }
        }
    }
}
