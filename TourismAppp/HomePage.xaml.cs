using TourismAppp.Data;
using TourismAppp.Models;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using Plugin.LocalNotification;


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
            ViewAllBookingsButton.IsVisible = true;
        }

        var request = new NotificationRequest
        {
            Title = "Welcome to TourismApp",
            Description = "Welcome to TourismApp, " + _currentUser.Username + ". You are logged in as " + _currentUser.Role,

        };
        LocalNotificationCenter.Current.Show(request);
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

    private async void OnMyBookingsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MyBookingsPage(_currentUser));
    }

    private async void OnViewAllBookingsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AllBookingsPage(_currentUser));
    }


}