using Score.Models;
using Score.Services;

namespace Score.Pages;

public partial class NouvelleEquiqe : ContentPage
{
    private readonly Equipe equipeCourant;
    public NouvelleEquiqe()
	{
		InitializeComponent();
	}

    public NouvelleEquiqe(Equipe equipe)
    {
        InitializeComponent();
        equipeCourant = equipe;

        txtNom.Text = equipeCourant.Nom;
        txtDescription.Text = equipeCourant.Description;
    }

    private async void btnEnregistrer_Clicked(object sender, EventArgs e)
    {
        if (equipeCourant == null)
        {
            if (string.IsNullOrEmpty(txtNom.Text))
            {
                await DisplayAlert("Erreur", "Veuillez entrer un nom pour l'�quipe", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                await DisplayAlert("Erreur", "Veuillez entrer une description", "Ok");
                return;
            }
            if (ServiceDB.ConnexionBD.Table<Equipe>().Any(e => e.Nom == txtNom.Text))
            {
                await DisplayAlert("Erreur", "Une equipe avec le m�me nom existe d�j�.", "Ok");
                return;
            }

            var equipe = new Equipe()
            {
                Nom = txtNom.Text,
                Description = txtDescription.Text,
            };

            ServiceDB.ConnexionBD.Insert(equipe);

            await DisplayAlert("Message", "L'�quipe a �t� ajout�e avec succ�s", "Ok");

            await Navigation.PopAsync();
        }
        else
        {
            if (string.IsNullOrEmpty(txtNom.Text))
            {
                await DisplayAlert("Erreur", "Veuillez entrer un nom pour l'�quipe", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                await DisplayAlert("Erreur", "Veuillez entrer une description", "Ok");
                return;
            }

            equipeCourant.Nom = txtNom.Text;
            equipeCourant.Description = txtDescription.Text;

            ServiceDB.ConnexionBD.Update(equipeCourant);

            await DisplayAlert("Message", "L'�quipe a �t� modifi� avec succ�s", "Ok");
            await Navigation.PopAsync();
        }

    }
}