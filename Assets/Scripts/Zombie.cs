using System;
using UnityEngine;

namespace PlatformerTP
{
    public class Zombie : MonoBehaviour
    {
        public float vie = 100;
        private static readonly int EstMort = Animator.StringToHash("EstMort");
        public bool estCollecter;

        private void Start()
        {
            if (InfosJoueur.Difficulte == 1)
                vie /= 1.5f;

            else if (InfosJoueur.Difficulte == 3)
                vie *= 1.3f;

            else if (InfosJoueur.Difficulte == 4) vie *= 1.6f;
        }

        private void Update()
        {
            if (vie <= 0) Mort();
        }

        private void Mort()
        {
            estCollecter = true;
            GetComponent<Animator>().SetBool(EstMort, true);
            GetComponent<Rigidbody2D>().velocity = Vector2.down * 10f;
            Destroy(gameObject, 2f);
        }

        public void PrendreDegat(float dmg)
        {
            vie -= dmg;
        }
    }
}
