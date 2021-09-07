using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlatformerTP
{
    public class ChauveSouris : MonoBehaviour
    {
        public float vie = 50;
        public bool estMort;
        public float tempsFrapper = 1f;
        private float _tempsAttente;
        public float minDamage = 30;
        public float maxDamage = 65;
        public GameObject texteFlotantPrefab;
        public GameObject particules;
        
        private static readonly int EstCritique = Animator.StringToHash("EstCritique");
        private static readonly int ShakeCrit = Animator.StringToHash("ShakeCrit");
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            if (InfosJoueur.Difficulte == 1)
            {
                vie /= 1.5f;
                minDamage /= 1.5f;
                maxDamage /= 1.5f;
                tempsFrapper *= 2;
            }
            else if (InfosJoueur.Difficulte == 3)
            {
                vie *= 1.5f;
                minDamage *= 1.3f;
                maxDamage *= 1.3f;
            }
            else if (InfosJoueur.Difficulte == 4)
            {
                vie *= 2;
                minDamage *= 1.6f;
                maxDamage *= 1.6f;
            }
        }

        public void PrendreDegat(float dmg)
        {
            vie -= dmg;
        }
        private void Update()
        {
            if (vie <= 0) Mort();
        }

        private void Mort()
        {
            estMort = true;
            GetComponent<Animator>().SetBool("EstMort", true);
            Destroy(gameObject, .2f);
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.CompareTag("Player")) return;
            if (GetComponent<ChauveSouris>().estMort) return;
            if (!(Time.time > _tempsAttente)) return;
            
            _tempsAttente = Time.time + 1 / tempsFrapper;
            //_animator.SetTrigger(Attaque);

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
    }
}
