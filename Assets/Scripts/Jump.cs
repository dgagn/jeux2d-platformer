using PlatformerTP.Interfaces;
using UnityEngine;

namespace PlatformerTP
{
    public class Jump : MonoBehaviour, IJump
    {
        //public float forceJump = 8f;
        [SerializeField] private LayerMask plateforme;
        private BoxCollider2D _boxCollider;
        private Rigidbody2D _rigidbody;
        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public bool SurPlateforme()
        {
            const float hauteur = .3f;
            var bounds = _boxCollider.bounds;
            
            var boxCast = Physics2D.BoxCast(bounds.center, bounds.size, 0f, Vector2.down,
                hauteur, plateforme);

            return boxCast.collider;
        }
        
        public void Saute()
        {
            if (!SurPlateforme()) return;
            _rigidbody.velocity = Vector2.up * InfosJoueur.ForceSaut;
        }

        private void FixedUpdate()
        {
            var velocityY = _rigidbody.velocity.y;
            
            const float tomberVitesse = 2.5f;
            const float gravityVitesse = 2f;
            
            if (velocityY < 0)
                _rigidbody.velocity += Vector2.up * (Physics2D.gravity.y * (tomberVitesse - 1) * Time.deltaTime);
            else if (velocityY > 0 && !Input.GetButton("Jump"))
                _rigidbody.velocity += Vector2.up * (Physics2D.gravity.y * (gravityVitesse - 1) * Time.deltaTime);
        }

        
    }
}