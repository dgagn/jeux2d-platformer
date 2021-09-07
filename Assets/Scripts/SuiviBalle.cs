using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerTP
{
    public class SuiviBalle : MonoBehaviour
    {
        [SerializeField] private float vitesse = 230;
        void Update()
        {
            transform.Translate(Vector3.right * (Time.deltaTime * vitesse));
            Destroy(gameObject, 1f);
        }
    }
}
