using InterviewProblem.Model;
using Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
