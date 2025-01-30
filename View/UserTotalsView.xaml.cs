using System.Windows;

namespace InterviewProblem.View
{
    public partial class UserTotalsView : Window
    {
        public UserTotalsView()
        {
            InitializeComponent();
            DataContext = new ViewModel.UserTotalsViewModel();
        }
    }
}
