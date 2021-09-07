using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlatformerTP.Boutique
{
    public abstract class Talent: MonoBehaviour
    {
        protected TextMeshProUGUI titre;
        protected TextMeshProUGUI uiPrix;
        protected Button bouton;
        
        [SerializeField] protected int prix = 2000;
        [SerializeField] protected float valeur = 0.2f;
        
        private void Awake()
        {
            bouton = GetComponent<Button>();
            titre = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            uiPrix = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            uiPrix.text = $"{prix:C0}";
        }
    }
}