using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlatformerTP
{
    public class Pic : MonoBehaviour
    {
        public float minDamage = 100;
        public float maxDamage = 125;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.CompareTag("Player")) return;
            var damage = Random.Range(minDamage, maxDamage);
            other.collider.GetComponent<Joueur>().PrendreDegat(damage);
        }
    }
}
