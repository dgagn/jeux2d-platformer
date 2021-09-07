using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Random = UnityEngine.Random;

namespace PlatformerTP
{
    public class Tirer : MonoBehaviour
    {
        private Camera _camera;
        [SerializeField] private Transform positionTir;
        [SerializeField] private LayerMask layerMask;
        public Transform suiviBalle;
        public Transform explosionPrefab;
        [SerializeField] private Animator animatorWeapon;

        public GameObject particules;
        
        //public float tempsTirer = 1;
        private float _tempsAttente;

        
        public float recul = 900;
        //public float reculZombie = 300;
        
        
        [Header("Damage Critique")] 
        //public float critiqueChance = 0.2f;
        //public float critiqueMultiplier = 3f;
        
        [Header("Damage")]
        public float dmgMin = 20;
        public float dmgMax = 40;
        private static readonly int Shoot1 = Animator.StringToHash("Shoot");
        private SpriteRenderer _sprite;
        [SerializeField] private SpriteRenderer spriteWeapon;

        public GameObject argent;
        //public float minArgent = 1;
        //public float maxArgent = 4;


        [Header("Texte flotant")] [SerializeField]
        private GameObject textFlotantPrefab;

        private static readonly int Shake = Animator.StringToHash("Shake");

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _camera = Camera.main;
        }

        private float CalculerDamage(out bool estCritique)
        {
            var damageInitial = Random.Range(dmgMin, dmgMax);
            var damageFinal = damageInitial;
            estCritique = false;

            if (!(Random.value > (1 - InfosJoueur.ChanceCrit))) return damageFinal;
            
            estCritique = true;
            damageFinal *= InfosJoueur.MultipleCrit;

            if (InfosJoueur.Difficulte == 1)
                damageFinal *= 1.3f;
            else if (InfosJoueur.Difficulte == 3)
                damageFinal /= 1.3f;
            else if (InfosJoueur.Difficulte == 4) damageFinal /= 1.6f;

            return damageFinal;
        }

        private void Update()
        {
            if (Time.timeScale == 0) return;
            if (!Input.GetMouseButtonDown(0) || !(Time.time > _tempsAttente)) return;
            
            _tempsAttente = Time.time + 1 / InfosJoueur.TempsTirer;
            Shoot();
        }

 
        private void Shoot()
        {
            var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            var tirerVecteur = positionTir.position;
            AudioManager.Instance.Jouer("JoueurTire");
            var hit = Physics2D.Raycast(tirerVecteur, mousePos - tirerVecteur, 100, layerMask);
            Effet();
            var isFlipped = GetComponent<Flipper>().flip;
            GetComponent<Rigidbody2D>()?.AddRelativeForce((isFlipped ? Vector2.right : Vector2.left) * recul);
            Debug.DrawLine(tirerVecteur, (mousePos-tirerVecteur) * 500);
            if (hit.collider == null) return;

            if (!hit.collider.CompareTag("Ennemi")) return;
            
            var damage = CalculerDamage(out var estCritique);
            MontrerTextDamage(hit.collider.transform, damage, estCritique);
            AudioManager.Instance.Jouer("ZombieHit");

            if (hit.collider.GetComponent<Zombie>() != null)
            {
                hit.collider.GetComponent<Rigidbody2D>().AddRelativeForce((!isFlipped ? Vector2.right : Vector2.left) * InfosJoueur.ReculZombie);
                hit.collider.GetComponent<Zombie>().PrendreDegat(damage);
                var zombie = hit.collider.GetComponent<Zombie>();
                if (zombie.vie <= 0 && !zombie.estCollecter)
                {
                    var nombre = Math.Round(Random.Range(InfosJoueur.MinArgent, InfosJoueur.MaxArgent));
                
                    hit.collider.GetComponent<CapsuleCollider2D>().enabled = false;
                    hit.collider.GetComponent<BoxCollider2D>().enabled = true;

                    hit.collider.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                
                
                    for (var i = 0; i <= nombre; i++)
                    {
                        var position = hit.collider.transform.position;
                        var newPos = new Vector3(position.x + Random.Range(0.2f, 0.8f), position.y + Random.Range(0.2f, 0.8f), position.z);
                    
                        var instance = Instantiate(argent, newPos, Quaternion.identity);
                        Destroy(instance, 10f);
                    }
                }
            }

            if (hit.collider.GetComponent<ChauveSouris>() != null)
            {
                hit.collider.GetComponent<ChauveSouris>().PrendreDegat(damage);
                var chauve = hit.collider.GetComponent<ChauveSouris>();

                if (chauve.vie <= 0 && !chauve.estMort)
                {
                    var nombre = Math.Round(Random.Range(InfosJoueur.MaxArgent, InfosJoueur.MaxArgent));
                    for (var i = 0; i <= nombre; i++)
                    {
                        var position = hit.collider.transform.position;
                        var newPos = new Vector3(position.x + Random.Range(0.2f, 0.8f), position.y + Random.Range(0.2f, 0.8f), position.z);
                    
                        var instance = Instantiate(argent, newPos, Quaternion.identity);
                        Destroy(instance, 10f);
                    }
                }
            }

            Debug.Log(hit.collider.name + " damage: " + damage + " et est " + (estCritique ? "critique" : "non-critique"));
            Debug.DrawLine(tirerVecteur, hit.point, Color.red);
        }

        private void MontrerTextDamage(Transform ennemi, float damage, bool estCritique)
        {
            if (textFlotantPrefab)
            {
                var position = ennemi.position;
                var prefab = Instantiate(textFlotantPrefab, position, Quaternion.identity);
                prefab.GetComponentInChildren<Animator>().SetFloat("EstCritique", (estCritique ? 1f : 0f));
                prefab.GetComponentInChildren<TextMesh>().text = $"{Mathf.Round(damage)}";
                
                _camera.GetComponent<Animator>().SetTrigger(estCritique ? "ShakeCrit" : "Shake");

                var instantiate = Instantiate(particules, new Vector3(position.x, position.y, -8f), Quaternion.identity);
                
                Destroy(instantiate, 5f);
                Destroy(prefab, 1f);
            }

            var spriteRenderer = ennemi.GetComponent<SpriteRenderer>();

            if (!spriteRenderer) return;
            
            StartCoroutine(ChangementCouleur(spriteRenderer, Color.red));
        }

        private void Effet()
        {
            Instantiate(suiviBalle, positionTir.position, positionTir.rotation);
            var cloneExplosion = Instantiate(explosionPrefab, positionTir.position, positionTir.rotation);
            cloneExplosion.parent = positionTir;
            var size = Random.Range(.4f, .6f);
            
            _camera.GetComponent<Animator>().SetTrigger(Shake);
            
            cloneExplosion.localScale = new Vector3(size, size, size);
            animatorWeapon.SetTrigger(Shoot1);
            var couleur = new Color(189, 185, 145);
            StartCoroutine(ChangementCouleur(_sprite, couleur));
            StartCoroutine(ChangementCouleur(spriteWeapon, couleur));

            Destroy(cloneExplosion.gameObject, .5f);
        }

        private IEnumerator ChangementCouleur(SpriteRenderer spriteRenderer, Color color, float temps = 0.2f)
        {
            if (spriteRenderer == null) yield break;
            
            var spriteColor = spriteRenderer.color;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(temps);
            
            if (spriteRenderer == null) yield break;
            spriteRenderer.color = spriteColor;
        }
    }
}
