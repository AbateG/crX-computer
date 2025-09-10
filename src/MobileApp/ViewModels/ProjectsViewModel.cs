using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using CR_COMPUTER.MobileApp.Services;
using CR_COMPUTER.MobileApp.Models;

namespace CR_COMPUTER.MobileApp.ViewModels
{
    public class ProjectsViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        public ObservableCollection<Project> Projects { get; set; }

        public Command LoadProjectsCommand { get; set; }
        public Command<Project> ProjectSelectedCommand { get; set; }

        public ProjectsViewModel(IApiService apiService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));

            Title = "Projects";
            Projects = new ObservableCollection<Project>();

            LoadProjectsCommand = new Command(async () => await ExecuteLoadProjectsCommand());
            ProjectSelectedCommand = new Command<Project>(OnProjectSelected);
        }

        private async Task ExecuteLoadProjectsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Projects.Clear();
                var projects = await _apiService.GetProjectsAsync();

                foreach (var project in projects)
                {
                    Projects.Add(project);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Unable to load projects: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnProjectSelected(Project project)
        {
            if (project == null)
                return;

            // Navigate to project details
            await Shell.Current.GoToAsync($"{nameof(ProjectDetailPage)}?{nameof(ProjectDetailViewModel.ProjectId)}={project.Id}");
        }
    }
}
