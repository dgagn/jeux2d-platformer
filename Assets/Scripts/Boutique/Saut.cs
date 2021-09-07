using System;
using PlatformerTP.UI;

namespace PlatformerTP.Boutique
{
    public class Saut: Talent
    {
        private void Update()
        {
            if (InfosJoueur.ForceSaut >= 12f) bouton.interactable = false;
        }

        private void Start()
        {
            titre.text = $"Saut ({Math.Round(((InfosJoueur.ForceSaut - 8) / 6) * 100)}%)";
        }

        public void Acheter()
        {
            if (Score.score < prix) return;
            Score.score -= prix;
            InfosJoueur.ForceSaut += valeur;
            titre.text = $"Saut ({Math.Round(((InfosJoueur.ForceSaut - 8) / 6) * 100)}%)";
        }
    }
}