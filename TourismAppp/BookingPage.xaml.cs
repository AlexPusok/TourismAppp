using System;
using Microsoft.Maui.Controls;
using TourismAppp.Models;
using TourismAppp.Data;
using System.IO;

namespace TourismAppp;

public partial class BookingPage : ContentPage
{

	private readonly TourismDatabase _database;
    private readonly Vacation _selectedVacation;
    private readonly User _currentUser;
    public BookingPage(User user, Vacation vacation)
	{
		InitializeComponent();
		_currentUser = user;
		_selectedVacation = vacation;

		_database = new TourismDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tourism.db3"));

		StartDatePicker.Date = vacation.StartDate;
        EndDatePicker.Date = vacation.EndDate;
    }

    private async void OnBookVacationClicked(object sender, EventArgs e)
	{
        if (!int.TryParse(PeopleEntry.Text, out int numberOfPeople) || numberOfPeople <= 0)
        {
            await DisplayAlert("Error", "Please enter a valid number of people.", "OK");
            return;
        }

        Booking newBooking = new Booking
        {
            UserID = _currentUser.userID,
            VacationID = _selectedVacation.VacationID,
            BookingDate = DateTime.Now,
            StartDate = StartDatePicker.Date,
            EndDate = EndDatePicker.Date,
            NumberOfPeople = numberOfPeople,
            Status = "Pending"
        };

        await _database.SaveBookingAsync(newBooking);
        await DisplayAlert("Success", "Vacation booked successfully!", "OK");
        await Navigation.PopAsync();
    }
}