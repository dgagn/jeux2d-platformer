using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlatformerTP
{
    public class HpBar : MonoBehaviour
    {
        public Slider slider;
        [SerializeField] private float vitesse = 3f;
        private void Awake()
        {
            slider = GetComponent<Slider>();
        }
        
        public void SetVie(float vie)
        {
            if (vie > 100)
            {
                var image = gameObject.transform.GetChild(1).GetComponent<Image>();
                var currentColor = image.color;

                image.color = Color.Lerp(currentColor, new Color(0.0763617f, 0.4643811f, 0.7169812f, 1f), Time.deltaTime * vitesse);
            }
            else
            {
                var image = gameObject.transform.GetChild(1).GetComponent<Image>();
                var currentColor = image.color;
                var color = new Color(125, 19, 19);
                image.color = Color.Lerp(currentColor, new Color(0.490566f, 0.0763617f, 0.0763617f, 1f), Time.deltaTime * vitesse);
            }
            slider.value = Mathf.Lerp(slider.value, vie, Time.deltaTime * vitesse);
        }
    }
}
