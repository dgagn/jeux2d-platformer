using UnityEngine;

namespace PlatformerTP
{
    public class SuitJoueur : MonoBehaviour
    {
        private Joueur _joueur;
        public float offsetX;
        public float offsetY;
        private void Awake()
        {
            _joueur = FindObjectOfType<Joueur>();
        }

        private void Update()
        {
            Vector3 position;
            transform.position = new Vector3((position = _joueur.transform.position).x + offsetX, position.y + offsetY, _joueur.transform.position.z);
        }
    }
}
