using System;
using TourismAppp.Models;

namespace TourismAppp;

public partial class VacationDetailsPage : ContentPage
{
	public VacationDetailsPage(Vacation vacation)
	{
		InitializeComponent();

        TitleLabel.Text = vacation.Title;
        DescriptionLabel.Text = vacation.Description;
        LocationLabel.Text = vacation.Location;
        PriceLabel.Text = $"${vacation.Price:F2}";
        DurationLabel.Text = $"{vacation.DurationDays} days";
        StartDateLabel.Text = vacation.StartDate.ToString("MMMM dd, yyyy");
        EndDateLabel.Text = vacation.EndDate.ToString("MMMM dd, yyyy");
    }

    private async void OnGoBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}