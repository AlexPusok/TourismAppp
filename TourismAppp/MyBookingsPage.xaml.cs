using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using TourismAppp.Data;
using TourismAppp.Models;

namespace TourismAppp;

public partial class MyBookingsPage : ContentPage
{
	private readonly TourismDatabase _database;
    private readonly User _currentUser;
    public MyBookingsPage(User currentUser)
	{
		InitializeComponent();
        _currentUser = currentUser;
        _database = new TourismDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tourism.db3"));
        LoadBookings();
    }

    private async void LoadBookings()
    {
        List<Booking> bookings = await _database.GetBookingsByUserAsync(_currentUser.userID);

        // Fetch vacation details for each booking
        List<MyBookingViewModel> bookingDetails = new List<MyBookingViewModel>();

        foreach (var booking in bookings)
        {
            Vacation vacation = await _database.GetVacationAsync(booking.VacationID);
            if (vacation != null)
            {
                bookingDetails.Add(new MyBookingViewModel
                {
                    Title = vacation.Title,
                    Location = vacation.Location,
                    StartDate = booking.StartDate,
                    EndDate = booking.EndDate,
                    NumberOfPeople = booking.NumberOfPeople,
                    Status = booking.Status
                });
            }
        }

        BookingsListView.ItemsSource = bookingDetails;
    }

    public class MyBookingViewModel
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfPeople { get; set; }
        public string Status { get; set; }
    }

    private async void OnBookingSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is MyBookingViewModel selectedBooking)
        {
            // Find the actual booking in the database
            List<Booking> userBookings = await _database.GetBookingsByUserAsync(_currentUser.userID);
            Booking booking = userBookings.Find(b =>
                b.StartDate == selectedBooking.StartDate &&
                b.EndDate == selectedBooking.EndDate &&
                b.NumberOfPeople == selectedBooking.NumberOfPeople &&
                b.Status == selectedBooking.Status);

            if (booking != null)
            {
                await Navigation.PushAsync(new BookingDetailsPage(booking, _currentUser));
            }
        }
    }
}