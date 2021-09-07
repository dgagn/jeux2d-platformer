using System;
using PlatformerTP.UI;

namespace PlatformerTP.Boutique
{
    public class MultipleCrit: Talent
    {
        private void Update()
        {
            if (InfosJoueur.MultipleCrit >= 4) bouton.interactable = false;
        }

        private void Start()
        {
            titre.text = $"Multiple Crit ({InfosJoueur.MultipleCrit * 100}%)";
        }

        public void Acheter()
        {
            if (Score.score < prix) return;
            Score.score -= prix;
            InfosJoueur.MultipleCrit += valeur;
            titre.text = $"Multiple Crit ({InfosJoueur.MultipleCrit * 100}%)";
        }
    }
}