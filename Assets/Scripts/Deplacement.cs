using PlatformerTP.Interfaces;
using UnityEngine;

namespace PlatformerTP
{
    public class Deplacement : MonoBehaviour, IDeplacement
    {
        private Rigidbody2D _rigidbody;

        private void Awake() 
            => _rigidbody = GetComponent<Rigidbody2D>();

        public void Bouge(float x) 
            => _rigidbody.velocity = new Vector2(x * InfosJoueur.VitesseMouvement, _rigidbody.velocity.y);
    }
}