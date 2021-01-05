using InterviewProblem.Model;
using Mvvm;

namespace InterviewProblem.ViewModel
{
    public class TemperatureViewModel : ViewModelBase
    {
        public TemperatureViewModel(Temperature temperature)
        {
            Temperature = temperature;
        }

        public Temperature Temperature { get; set; }

    }
}
