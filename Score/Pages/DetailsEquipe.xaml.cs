using Score.DTO;
using Score.Models;
using Score.Services;


namespace Score.Pages;

public partial class DetailsEquipe : ContentPage
{
    List<Match> _matches;
    private EquipeDTO _equipe;
    public DetailsEquipe(EquipeDTO equipe)
	{
        InitializeComponent();
		_equipe = equipe;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var equipe = ServiceDB.ConnexionBD.Find<Equipe>(_equipe.Id);
        _matches = ServiceDB.ConnexionBD.Table<Match>().OrderBy(m => m.DateDuMatch).ToList();

        lbDesc.Text = equipe.Description;
        this.Title = equipe.Nom;

        var tousLesMatches = _matches.FindAll(m => m.EquipeDomicile == equipe.Nom || m.EquipeExterieure == equipe.Nom);
        lesMatchs.ItemsSource = tousLesMatches;
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        if (_matches.Count > 0)
        {
            await DisplayAlert("Message", "La saison a commencée, l'équipe ne peut pas être modifiée.", "Ok");
            return;
        }
        var equipe = ServiceDB.ConnexionBD.Find<Equipe>(_equipe.Id);
        await Navigation.PushAsync(new NouvelleEquiqe(equipe));
    }
}