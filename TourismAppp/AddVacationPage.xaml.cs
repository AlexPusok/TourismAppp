using System;
using TourismAppp.Data;
using TourismAppp.Models;

namespace TourismAppp;

public partial class AddVacationPage : ContentPage
{
	private readonly TourismDatabase _database;
	public AddVacationPage()
	{
		InitializeComponent();

        _database = new TourismDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tourism.db3"));

    }
    private async void OnAddVacationClicked(object sender, EventArgs e)
	{
        if (string.IsNullOrWhiteSpace(TitleEntry.Text) ||
            string.IsNullOrWhiteSpace(DescriptionEntry.Text) ||
            string.IsNullOrWhiteSpace(LocationEntry.Text) ||
            string.IsNullOrWhiteSpace(PriceEntry.Text) ||
            string.IsNullOrWhiteSpace(DurationDaysEntry.Text) ||
            !decimal.TryParse(PriceEntry.Text, out decimal price) ||
            !int.TryParse(DurationDaysEntry.Text, out int durationDays) ||
            StartDatePicker.Date > EndDatePicker.Date)
        {
            await DisplayAlert("Error", "Please fill in all fields correctly and ensure start date is before the end date.", "OK");
            return;
        }

        var vacation = new Vacation
        {
            Title = TitleEntry.Text,
            Description = DescriptionEntry.Text,
            Location = LocationEntry.Text,
            Price = price,
            DurationDays = durationDays,
            StartDate = StartDatePicker.Date,
            EndDate = EndDatePicker.Date
        };

        await _database.SaveVacationAsync(vacation);

        await DisplayAlert("Success", "Vacation added successfully", "OK");
        await Navigation.PopAsync();
    }


}