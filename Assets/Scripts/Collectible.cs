using System;
using UnityEngine;

namespace PlatformerTP
{
    public class Collectible : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float height = 0.5f;
        [SerializeField] private GameObject particulesArgent;
        private Camera _camera;

        public static event Action PrisCollectible;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private Vector3 _pos;

        private void Start()
        {
            _pos = transform.position;
        }

        private void Update()
        {
            var newY = Mathf.Sin(Time.time * speed) * height + _pos.y;
            var pos = new Vector3(transform.position.x, newY, transform.position.z);
            transform.position = pos;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            AudioManager.Instance.Jouer("Collectible");
            PrisCollectible?.Invoke();
            var particules = Instantiate(particulesArgent, other.transform.position, Quaternion.identity);
            Destroy(particules, 2f);
            _camera.GetComponent<Animator>().SetTrigger("Shake");
            Destroy(gameObject);
        }
    }
}
