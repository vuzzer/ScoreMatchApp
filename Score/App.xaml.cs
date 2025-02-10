using Score.Services;

namespace Score
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ServiceDB.ConfigurerBD();
            MainPage = new AppShell();
        }
    }
}
