using System;
using TourismAppp.Data;
using TourismAppp.Models;
using System.IO;
using System.Threading.Tasks;

namespace TourismAppp;

public partial class EditVacationPage : ContentPage
{
	public Vacation _vacation;
	private readonly TourismDatabase _database;
	public EditVacationPage(Vacation vacation)
	{
		InitializeComponent();
        _vacation = vacation;

        _database = new TourismDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tourism.db3"));

        TitleEntry.Text = _vacation.Title;
        DescriptionEntry.Text = _vacation.Description;
        LocationEntry.Text = _vacation.Location;
        PriceEntry.Text = _vacation.Price.ToString();
        DurationDaysEntry.Text = _vacation.DurationDays.ToString();
        StartDatePicker.Date = _vacation.StartDate;
        EndDatePicker.Date = _vacation.EndDate;

    }

    private async void OnSaveChangesClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text) || string.IsNullOrWhiteSpace(DescriptionEntry.Text) || string.IsNullOrWhiteSpace(LocationEntry.Text) || string.IsNullOrWhiteSpace(PriceEntry.Text) || string.IsNullOrWhiteSpace(DurationDaysEntry.Text))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        _vacation.Title = TitleEntry.Text;
        _vacation.Description = DescriptionEntry.Text;
        _vacation.Location = LocationEntry.Text;
        _vacation.Price = decimal.Parse(PriceEntry.Text);
        _vacation.DurationDays = int.Parse(DurationDaysEntry.Text);
        _vacation.StartDate = StartDatePicker.Date;
        _vacation.EndDate = EndDatePicker.Date;

        await _database.SaveVacationAsync(_vacation);
        await DisplayAlert("Success", "Vacation saved successfully", "OK");
        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}