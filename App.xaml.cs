using InterviewProblem.View;
using InterviewProblem.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InterviewProblem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainViewModel vm = new MainViewModel();
            MainView view = new MainView() { DataContext = vm };
            view.Show();
        }
    }
}
