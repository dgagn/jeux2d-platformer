using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace PlatformerTP
{
    public class ChargementNiveau : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            SceneManager.LoadScene("Scenes/Niveau2");
        }
    }
}
