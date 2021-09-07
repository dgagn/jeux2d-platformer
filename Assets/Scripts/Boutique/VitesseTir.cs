using System;
using PlatformerTP.UI;

namespace PlatformerTP.Boutique
{
    public class VitesseTir: Talent
    {
        private void Update()
        {
            if (InfosJoueur.TempsTirer >= 3) bouton.interactable = false;
        }

        private void Start()
        {
            titre.text = $"Vitesse de tir ({InfosJoueur.TempsTirer}/s)";
        }

        public void Acheter()
        {
            if (Score.score < prix) return;
            Score.score -= prix;
            InfosJoueur.TempsTirer += valeur;
            titre.text = $"Vitesse de tir ({InfosJoueur.TempsTirer}/s)";
        }
    }
}