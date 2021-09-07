using System;
using PlatformerTP.UI;

namespace PlatformerTP.Boutique
{
    public class ChanceCrit : Talent
    {
        private void Update()
        {
            if (InfosJoueur.ChanceCrit >= 1) bouton.interactable = false;
        }

        private void Start()
        {
            titre.text = $"Chance Crit ({InfosJoueur.ChanceCrit * 100}%)";
        }

        public void Acheter()
        {
            if (Score.score < prix) return;
            Score.score -= prix;
            InfosJoueur.ChanceCrit += valeur;
            titre.text = $"Chance Crit ({InfosJoueur.ChanceCrit * 100}%)";
        }
    }
}
