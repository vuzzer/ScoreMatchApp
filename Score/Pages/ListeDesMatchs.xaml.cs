using Score.Models;
using Score.Services;

namespace Score.Pages;

public partial class ListeDesMatchs : ContentPage
{
    List<Match> matches;
    public ListeDesMatchs()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        matches = ServiceDB.ConnexionBD.Table<Match>().OrderBy(m => m.DateDuMatch ).Reverse().ToList();
        lesMatchs.ItemsSource = matches;
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NouveauMatch());
    }
}