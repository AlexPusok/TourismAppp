using System;
using System.IO;
using Microsoft.Maui.Controls;
using TourismAppp.Data;
using TourismAppp.Models;

namespace TourismAppp;

public partial class BookingDetailsPage : ContentPage
{
    private readonly TourismDatabase _database;
    private readonly Booking _booking;
    private readonly User _currentUser;
    public BookingDetailsPage(Booking booking, User user)
	{
		InitializeComponent();
        _booking = booking;
        _currentUser = user;
        _database = new TourismDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tourism.db3"));

        LoadBookingDetails();

        if (_currentUser.Role == "Agent")
        {
            ConfirmBookingButton.IsVisible = true;  // Show the button only for agents
        }
    }

    private async void LoadBookingDetails()
    {
        Vacation vacation = await _database.GetVacationAsync(_booking.VacationID);
        if (vacation != null)
        {
            TitleLabel.Text = vacation.Title;
            LocationLabel.Text = vacation.Location;
            StartDateLabel.Text = _booking.StartDate.ToString("MMM dd, yyyy");
            EndDateLabel.Text = _booking.EndDate.ToString("MMM dd, yyyy");
            NumberOfPeopleLabel.Text = _booking.NumberOfPeople.ToString();
            StatusLabel.Text = _booking.Status;
        }
    }

    private async void OnCancelBookingClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Cancel Booking", "Are you sure you want to cancel this booking?", "Yes", "No");
        if (confirm)
        {
            _booking.Status = "Cancelled";
            await _database.SaveBookingAsync(_booking);
            await DisplayAlert("Cancelled", "Your booking has been cancelled.", "OK");
            await Navigation.PopAsync();
        }
    }

    private async void OnConfirmBookingClicked(object sender, EventArgs e)
    {
        _booking.Status = "Confirmed";
        await _database.UpdateBookingAsync(_booking);
        StatusLabel.Text = $"Status: {_booking.Status}";

        await DisplayAlert("Success", "Booking has been confirmed!", "OK");
    }
}