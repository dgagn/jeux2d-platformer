using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace PlatformerTP.UI
{
    public class Score: MonoBehaviour
    {
        public static int score;
        private void OnEnable() => Collectible.PrisCollectible += OnCollectible;
        private void OnDisable() => Collectible.PrisCollectible -= OnCollectible;

        private Text _text;

        private void Update()
        {
            _text.text = $"{score:C0}";
        }

        private void Awake()
        {
            _text = GetComponent<Text>();
            _text.text = $"{score:C0}";
        }

        private void OnCollectible() => score += Random.Range(100, 125);
    }
}