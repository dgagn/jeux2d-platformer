using System;
using PlatformerTP.UI;

namespace PlatformerTP.Boutique
{
    public class Vie : Talent
    {
        private void Update()
        {
            bouton.interactable = !(InfosJoueur.Vie >= 300f);
            titre.text = $"Vie ({Math.Round(InfosJoueur.Vie)})";
        }

        public void Acheter()
        {
            if (Score.score < prix) return;
            Score.score -= prix;
            
            InfosJoueur.Vie += valeur;
        }
    }
}
