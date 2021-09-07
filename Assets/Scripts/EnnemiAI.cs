using System;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlatformerTP
{
    public class EnnemiAI : MonoBehaviour
    {
        [Header("Pathfinding")]
        public Transform target;
        public float activateDistance = 50f;
        public float pathUpdateSeconds = 0.5f;

        [Header("Physics")]
        public float speed = 200f;
        public float nextWaypointDistance = 3f;
        public float jumpNodeHeightRequirement = 0.8f;
        public float jumpModifier = 0.3f;
        public float jumpCheckOffset = 0.1f;

        [Header("Custom Behavior")]
        public bool followEnabled = true;
        public bool jumpEnabled = true;
        public bool directionLookEnabled = true;

        public GameObject texteFlotantPrefab;
        public GameObject particules;
        
        public float minDamage = 30;
        public float maxDamage = 65;
        
        private Path path;
        private int currentWaypoint = 0;
        RaycastHit2D isGrounded;
        Seeker seeker;
        Rigidbody2D rb;
        public float tempsFrapper = 1f;
        private float _tempsAttente;
        
        private Animator _animator;
        private Camera _camera;
        private static readonly int Vitesse = Animator.StringToHash("Vitesse");
        private static readonly int Attaque = Animator.StringToHash("Attaque");
        private static readonly int EstCritique = Animator.StringToHash("EstCritique");
        private static readonly int ShakeCrit = Animator.StringToHash("ShakeCrit");

        public void Start()
        {
            seeker = GetComponent<Seeker>();
            rb = GetComponent<Rigidbody2D>();

            if (target == null) target = FindObjectOfType<Joueur>().gameObject.transform;

            if (InfosJoueur.Difficulte == 1)
            {
                minDamage /= 1.5f;
                maxDamage /= 1.5f;
                tempsFrapper *= 2;
            }
            else if (InfosJoueur.Difficulte == 3)
            {
                minDamage *= 1.3f;
                maxDamage *= 1.3f;
            }
            else if (InfosJoueur.Difficulte == 4)
            {
                minDamage *= 1.6f;
                maxDamage *= 1.6f;
            }

            InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _camera = Camera.main;
        }

        private void Update()
        {
            _animator.SetFloat(Vitesse, Mathf.Abs(rb.velocity.x));
        }

        private void FixedUpdate()
        {
            if (TargetInDistance() && followEnabled)
            {
                PathFollow();
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.CompareTag("Player")) return;
            if (GetComponent<Zombie>().estCollecter) return;
            if (!(Time.time > _tempsAttente)) return;
            
            _tempsAttente = Time.time + 1 / tempsFrapper;
            _animator.SetTrigger(Attaque);

            var calculerDamage = Random.Range(minDamage, maxDamage);
            other.collider.GetComponent<Joueur>().PrendreDegat(calculerDamage);

            var position = other.collider.transform.position;
            var prefab = Instantiate(texteFlotantPrefab, position, Quaternion.identity);
            prefab.GetComponentInChildren<Animator>().SetFloat(EstCritique, 1f);
            prefab.GetComponentInChildren<TextMesh>().text = $"{Mathf.Round(calculerDamage)}";
            _camera.GetComponent<Animator>().SetTrigger(ShakeCrit);

            var instantiate = Instantiate(particules, new Vector3(position.x, position.y, -8f), Quaternion.identity);
            
            Destroy(instantiate, 5f);
            Destroy(prefab, 1f);
        }

        private void UpdatePath()
        {
            if (followEnabled && TargetInDistance() && seeker.IsDone())
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
        }

        private void PathFollow()
        {
            if (path == null)
            {
                return;
            }

            if (currentWaypoint >= path.vectorPath.Count)
            {
                return;
            }

            Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
            isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);
        
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            if (jumpEnabled && isGrounded)
            {
                if (direction.y > jumpNodeHeightRequirement)
                {
                    rb.AddForce(Vector2.up * speed * jumpModifier);
                }
            }

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            if (directionLookEnabled)
            {
                if (rb.velocity.x > 0.05f)
                {
                    transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else if (rb.velocity.x < -0.05f)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
            }
        }

        private bool TargetInDistance()
        {
            return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
        }
    }
}
