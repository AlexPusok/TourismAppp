using TourismAppp.Data;
using TourismAppp.Models;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;


namespace TourismAppp;

public partial class HomePage : ContentPage
{
    private User _currentUser;
    private readonly TourismDatabase _database;
    public HomePage(User currentUser)
	{
		InitializeComponent();

        _currentUser = currentUser;

        _database = new TourismDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tourism.db3"));
        
        LoadVacations();

        if (_currentUser.Role == "Agent")
        {
            AddVacationButton.IsVisible = true;
        }
    }

    private async void LoadVacations()
    {
        List<Vacation> vacations = await _database.GetVacationsAsync();
        VacationsListView.ItemsSource = vacations;
    }

    private async void OnVacationSelected(object sender, SelectedItemChangedEventArgs e)
    {
        Vacation vacation = e.SelectedItem as Vacation;
        await Navigation.PushAsync(new VacationDetailsPage(vacation, _currentUser));
    }

    private async void OnAddVacationClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddVacationPage());
    }

}