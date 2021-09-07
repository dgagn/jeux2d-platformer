using System;
using PlatformerTP.UI;

namespace PlatformerTP.Boutique
{
    public class Puissance: Talent
    {
        private void Update()
        {
            if (InfosJoueur.ReculZombie >= 800) bouton.interactable = false;
        }

        private void Start()
        {
            titre.text = $"Recul Ennemi ({Math.Round((InfosJoueur.ReculZombie / 800) * 100)}%)";
        }

        public void Acheter()
        {
            if (Score.score < prix) return;
            Score.score -= prix;
            InfosJoueur.ReculZombie += valeur;
            titre.text = $"Recul Ennemi ({Math.Round((InfosJoueur.ReculZombie / 800) * 100)}%)";
        }
    }
}