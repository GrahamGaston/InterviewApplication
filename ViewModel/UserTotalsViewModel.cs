using InterviewProblem.DatabaseLayer;
using Mvvm;

namespace InterviewProblem.ViewModel
{
    public class UserTotalsViewModel : ViewModelBase
    {
        private int _numberPassed;
        public int NumberPassed
        {
            get => _numberPassed;
            set => SetField(ref _numberPassed, value);
        }

        private int _numberFailed;
        public int NumberFailed
        {
            get => _numberFailed;
            set => SetField(ref _numberFailed, value);
        }

        public void LoadUserTotals()
        {
            var dal = new TempLoggerDal("TempLogger.db");
            var totals = dal.GetUserTotals();
            NumberPassed = totals.NumberPassed;
            NumberFailed = totals.NumberFailed;
        }
    }
}
