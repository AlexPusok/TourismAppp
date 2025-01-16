using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourismAppp.Data;
using TourismAppp.Models;
using System.IO;

namespace TourismAppp;

public partial class AllBookingsPage : ContentPage
{
	private readonly TourismDatabase _database;
    private readonly User _currentUser;
    public AllBookingsPage(User user)
	{
		InitializeComponent();
        _database = new TourismDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tourism.db3"));
        _currentUser = user;

        LoadAllBookings();
    }

    private async void LoadAllBookings()
    {
        List<Booking> allBookings = await _database.GetAllBookingsAsync();
        List<AllBookingsViewModel> displayList = new List<AllBookingsViewModel>();

        foreach (var booking in allBookings)
        {
            var user = await _database.GetUserAsync(booking.UserID);
            var vacation = await _database.GetVacationAsync(booking.VacationID);

            displayList.Add(new AllBookingsViewModel
            {
                VacationTitle = vacation?.Title ?? "Unknown",
                Username = user?.Username ?? "Unknown",
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Status = booking.Status
            });
        }

        AllBookingsListView.ItemsSource = displayList;
    }

    private async void OnBookingSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is AllBookingsViewModel selectedBooking)
        {
            List<Booking> allBookings = await _database.GetAllBookingsAsync();
            Booking booking = allBookings.FirstOrDefault(b =>
                b.StartDate == selectedBooking.StartDate &&
                b.EndDate == selectedBooking.EndDate &&
                b.Status == selectedBooking.Status);

            if (booking != null)
            {
                await Navigation.PushAsync(new BookingDetailsPage(booking, _currentUser));
            }
        }
    }

    public class AllBookingsViewModel
    {
        public string VacationTitle { get; set; }
        public string Username { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}