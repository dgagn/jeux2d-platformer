using System;
using TMPro;
using UnityEngine;

namespace PlatformerTP.UI
{
    public class ArgentBoutique: MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Update()
        {
            OnCollectible();
        }

        private void OnCollectible()
        {
            _text.text = $"{Score.score:C0}";
        }

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            OnCollectible();
        }
    }
}