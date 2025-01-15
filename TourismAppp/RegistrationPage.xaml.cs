using Microsoft.Maui.Storage;
using System;
using Microsoft.Maui.Controls;
using TourismAppp.Data;

namespace TourismAppp;

public partial class RegistrationPage : ContentPage
{
    private readonly TourismDatabase _database;
    public RegistrationPage()
	{
		InitializeComponent();
        _database = new TourismDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tourism.db3"));
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        // Retrieve values from the entries and picker
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;
        string email = EmailEntry.Text;
        string role = RolePicker.SelectedItem.ToString();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(role))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        bool userExists = await _database.RegisterUserAsync(username, password, email, role);

        if (userExists)
        {
            await DisplayAlert("Error", "User already exists", "OK");
            return;
        }
        else
        {
            await DisplayAlert("Success", "User registered", "OK");
            await Navigation.PushAsync(new LoginPage());
        }
    }
}