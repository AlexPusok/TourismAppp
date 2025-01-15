using System;
using TourismAppp.Data;
using System.IO;

namespace TourismAppp
{
    public partial class App : Application
    {
        static TourismDatabase database;

        public static TourismDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new TourismDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tourism.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
