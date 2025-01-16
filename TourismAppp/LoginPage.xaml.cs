using System;
using Microsoft.Maui.Controls;
using TourismAppp.Data;
using TourismAppp.Models;

namespace TourismAppp;

public partial class LoginPage : ContentPage
{
    private readonly TourismDatabase _database;
    public LoginPage()
	{
		InitializeComponent();
        _database = new TourismDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tourism.db3"));
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        User user = await _database.GetUserByUsernameAsync(username);

        if (user != null && user.Password == password)
        {
            await DisplayAlert("Success", "Login successful", "OK");
            await Navigation.PushAsync(new HomePage(user));
        }
        else
        {
            await DisplayAlert("Error", "Invalid credentials", "OK");
        }

        
    }
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistrationPage());
    }
}