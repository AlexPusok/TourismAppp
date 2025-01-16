using System;
using TourismAppp.Data;
using TourismAppp.Models;

namespace TourismAppp;

public partial class VacationDetailsPage : ContentPage
{

    private Vacation _vacation;
    private User _user;
    private readonly TourismDatabase _database;
	public VacationDetailsPage(Vacation vacation, User user)
	{
		InitializeComponent();
        _vacation = vacation;
        _user = user;

        _database = new TourismDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tourism.db3"));

       

        TitleLabel.Text = _vacation.Title;
        DescriptionLabel.Text = _vacation.Description;
        LocationLabel.Text = _vacation.Location;
        PriceLabel.Text = $"${_vacation.Price:F2}";
        DurationLabel.Text = $"{_vacation.DurationDays} days";
        StartDateLabel.Text = _vacation.StartDate.ToString("MMMM dd, yyyy");
        EndDateLabel.Text = _vacation.EndDate.ToString("MMMM dd, yyyy");

        if (_user.Role == "Agent")
        {
            EditButton.IsVisible = true;
            DeleteButton.IsVisible = true;
        }
    }

    private async void OnGoBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }


    private async void OnEditVacationClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditVacationPage(_vacation));
    }

    private async void OnDeleteVacationClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Delete", "Are you sure you want to delete this vacation?", "Yes", "No");
        if (confirm)
        {
            await _database.DeleteVacationAsync(_vacation);
            await DisplayAlert("Success", "Vacation deleted successfully.", "OK");
            await Navigation.PopAsync();
        }
    }

    private async void OnBookNowClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BookingPage(_user, _vacation));
    }
}