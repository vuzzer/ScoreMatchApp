using Score.DTO;
using Score.Models;
using Score.Services;

namespace Score.Pages;

public partial class ListeDesEquipes : ContentPage
{

    List<EquipeDTO> equipes;
    List<Match> matchs;

    public ListeDesEquipes()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        matchs = ServiceDB.ConnexionBD.Table<Match>().OrderBy(x => x.DateDuMatch).ToList();
        equipes = ServiceDB.ConnexionBD.Table<Equipe>().Select(e => new EquipeDTO
        {
            Id = e.Id,
            Nom = e.Nom,
            Description = e.Description,
            NombreDeMatchJoue = ServiceDB.ConnexionBD.Table<Match>().OrderBy(x => x.DateDuMatch).ToList()
          .FindAll(x => x.EquipeDomicile == e.Nom || x.EquipeExterieure == e.Nom).Count,
            ButMarque = CalculeButs(e.Nom, But.marque ),
            ButRecu = CalculeButs(e.Nom, But.encaisse),
            NombreDePoints = CalculerPointTotal(e.Nom)
        }).OrderByDescending(equipe => equipe.NombreDePoints).ToList();

        lesEquipes.ItemsSource = equipes;
    }

    private int CalculeButs(string equipe, But but)
    {
        int butsTotal = 0;
        var matchs = ServiceDB.ConnexionBD.Table<Match>().ToList()
          .FindAll(e => e.EquipeDomicile == equipe || e.EquipeExterieure == equipe);

        switch (but)
        {
            case But.marque:
                foreach (var match in matchs)
                {
                    if (match.EquipeDomicile == equipe)
                    {
                        butsTotal += match.ScoreEquipeDomicile;
                        continue;
                    }
                    butsTotal += match.ScoreEquipeExterieure;
                }
                break;
            case But.encaisse:
                foreach (var match in matchs)
                {
                    if (match.EquipeDomicile == equipe)
                    {
                        butsTotal += match.ScoreEquipeExterieure;
                        continue;
                    }
                    butsTotal += match.ScoreEquipeDomicile;
                }
                break;
        }

        return butsTotal;
    }

    private int CalculerPointTotal(string equipe)
    {
        int points = 0;
        var matchs = ServiceDB.ConnexionBD.Table<Match>().ToList()
          .FindAll(x => x.EquipeDomicile == equipe || x.EquipeExterieure == equipe);

        foreach (var match in matchs)
        {
            if (match.EquipeDomicile == equipe)
            {
                if (match.ScoreEquipeDomicile > match.ScoreEquipeExterieure)
                {
                    points += 3;
                }
                else if (match.ScoreEquipeDomicile == match.ScoreEquipeExterieure)
                {
                    points += 1;
                }
            }
            else
            {
                if (match.ScoreEquipeExterieure > match.ScoreEquipeDomicile)
                {
                    points += 3;
                }
                else if (match.ScoreEquipeExterieure == match.ScoreEquipeDomicile)
                {
                    points += 1;
                }
            }
        }
        return points;
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        if (matchs.Count > 0)
        {
            await DisplayAlert("Message", "La saison a commencée, aucune équipe ne peut être ajoutée.", "Ok");
            return;
        }
        await Navigation.PushAsync(new NouvelleEquiqe());
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var label = sender as Label;
        var equipe = label?.BindingContext as EquipeDTO;
        await Navigation.PushAsync(new DetailsEquipe(equipe));
    }
}

enum But { encaisse, marque }