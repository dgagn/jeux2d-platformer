using System;
using PlatformerTP.UI;

namespace PlatformerTP.Boutique
{
    public class Vitesse : Talent
    {
        private void Update()
        {
            if (InfosJoueur.VitesseMouvement >= 15f) bouton.interactable = false;
        }

        private void Start()
        {
            titre.text = $"Vitesse ({Math.Round(((InfosJoueur.VitesseMouvement - 5) / 10) * 100)}%)";
        }

        public void Acheter()
        {
            if (Score.score < prix) return;
            Score.score -= prix;
            InfosJoueur.VitesseMouvement += valeur;
            titre.text = $"Vitesse ({Math.Round(((InfosJoueur.VitesseMouvement - 5) / 10) * 100)}%)";
        }
    }
}
