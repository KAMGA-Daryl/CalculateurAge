using System.Windows.Input;

namespace CalculateurAge.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
    private string _nom = "";
    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);
    private bool _resultatVisible;
    private string _resultat = "";
    private string _statutMajeur = "";

    public string Nom
    {
        get => _nom;
        set
        {
            if (SetField(ref _nom, value))
            {
                (CalculerCommand as RelayCommand)?.Rafraichir();
            }
        }
    }

    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set
        {
            // Activité 6 : Refus d'une date future
            DateTime dateAjustee = value > DateTime.Today ? DateTime.Today : value;
            SetField(ref _dateNaissance, dateAjustee);
        }
    }

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    public string StatutMajeur
    {
        get => _statutMajeur;
        set => SetField(ref _statutMajeur, value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set => SetField(ref _resultatVisible, value);
    }

    public ICommand CalculerCommand { get; }
    public ICommand EffacerCommand { get; } // Activité 6

    public CalculateurViewModel()
    {
        CalculerCommand = new RelayCommand(Calculer, () => !string.IsNullOrWhiteSpace(Nom));
        EffacerCommand = new RelayCommand(Effacer);
    }

    private void Calculer()
    {
        int age = DateTime.Today.Year - DateNaissance.Year;
        if (DateNaissance.Date > DateTime.Today.AddYears(-age)) age--;

        Resultat = $"{Nom}, vous avez {age} ans";

        // Activité 6 : Affichage Majeur ou Mineur
        StatutMajeur = age >= 18 ? "Statut : Majeur(e)" : "Statut : Mineur(e)";
        ResultatVisible = true;
    }

    // Activité 6 : Remise à zéro des champs
    private void Effacer()
    {
        Nom = string.Empty;
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = string.Empty;
        StatutMajeur = string.Empty;
        ResultatVisible = false;
        (CalculerCommand as RelayCommand)?.Rafraichir();
    }
}