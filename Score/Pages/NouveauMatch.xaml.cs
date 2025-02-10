using Score.Models;
using Score.Services;

namespace Score.Pages;

public partial class NouveauMatch : ContentPage
{
	public NouveauMatch()
	{
		InitializeComponent();
        equipeDomicile.ItemsSource = ServiceDB.ConnexionBD.Table<Equipe>().OrderBy(e => e.Nom).ToList();
        equipeExterieure.ItemsSource = ServiceDB.ConnexionBD.Table<Equipe>().OrderBy(e => e.Nom).ToList();
    }

    private void equipeDomicile_SelectedIndexChanged(object sender, EventArgs e)
    {
        var equipe = equipeDomicile.SelectedItem as Equipe;
        txtEquipeDomicile.Path = equipe.Nom;
    }

    private void equipeExterieure_SelectedIndexChanged(object sender, EventArgs e)
    {
        var equipe = equipeExterieure.SelectedItem as Equipe;
        txtEquipeExterieure.Path = equipe.Nom;
    }

    private async void btnEnregistrer_Clicked(object sender, EventArgs e)
    {
        int scoreEquipeDomicile;
        int scoreEquipeExt�rieure;
        if (txtEquipeDomicile.Path == "Nom" || txtEquipeExterieure.Path == "Nom" || txtScoreEquipeDomicile.Text == null || txtScoreEquipeExterieure.Text == null)
        {
            await DisplayAlert("Erreur", "Vous devriez renseigner tout les champs", "Ok");
            return;
        }
        if (txtEquipeDomicile.Path == txtEquipeExterieure.Path)
        {
            await DisplayAlert("Erreur", "Il est impossible d'avoir le m�me nom d'�quipe � domicile qu'a l'ext�rieur", "Ok");
            return;
        }
        bool isNumberDom = int.TryParse(txtScoreEquipeDomicile.Text, out scoreEquipeDomicile);
        bool isNumberExt = int.TryParse(txtScoreEquipeExterieure.Text, out scoreEquipeExt�rieure);

        if (isNumberDom == false)
        {
            await DisplayAlert("Erreur", "Le score de l'�quipe � domicile n'est pas un nombre.", "Ok");
            return;
        }
        if (isNumberExt == false)
        {
            await DisplayAlert("Erreur", "Le score de l'�quipe � l'ext�rieur n'est pas un nombre.", "Ok");
            return;
        }

        if (dateDuMatch.Date > DateTime.Now.Date)
        {
            await DisplayAlert("Erreur", "Un match ne peut pas �tre dans le futur.", "Ok");
            return;
        }


        var match = new Match
        {
            EquipeDomicile = txtEquipeDomicile.Path,
            EquipeExterieure = txtEquipeExterieure.Path,
            ScoreEquipeDomicile = scoreEquipeDomicile,
            ScoreEquipeExterieure = scoreEquipeExt�rieure,
            DateDuMatch = dateDuMatch.Date.ToString("yyyy-MM-dd"),
        };
        List<Match> matches = ServiceDB.ConnexionBD.Table<Match>().OrderBy(m => m.DateDuMatch).ToList();
        List<Match> matchsDeLaJournee = matches.FindAll(m => m.DateDuMatch == match.DateDuMatch);

        if (matchsDeLaJournee.Any(m => m.EquipeDomicile == match.EquipeDomicile || m.EquipeDomicile == match.EquipeExterieure
         || m.EquipeExterieure == match.EquipeDomicile || m.EquipeExterieure == match.EquipeExterieure))
        {
            await DisplayAlert("Erreur", "Une �quipe ne peut pas avoir deux matchs dans une m�me journ�e.", "Ok");
            return;
        }

        ServiceDB.ConnexionBD.Insert(match);

        await DisplayAlert("Info", "Le match a �t� cr�� avec succ�s", "Ok");

        await Navigation.PopAsync();
    }
}