using System;
using PlatformerTP.UI;

namespace PlatformerTP.Boutique
{
    public class ChanceArgent: Talent
    {
        private void Update()
        {
            if (InfosJoueur.MaxArgent >= 8) bouton.interactable = false;
        }

        private void Start()
        {
            titre.text = $"Chance Argent ({Math.Round((InfosJoueur.MaxArgent / 8) * 100)}%)";
        }

        public void Acheter()
        {
            if (Score.score < prix) return;
            Score.score -= prix;
            InfosJoueur.MinArgent += valeur;
            InfosJoueur.MaxArgent += valeur;
            titre.text = $"Chance Argent ({Math.Round((InfosJoueur.MaxArgent / 8) * 100)}%)";
        }
    }
}