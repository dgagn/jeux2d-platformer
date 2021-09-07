using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerTP
{
    public class JouerChansonVictoire : MonoBehaviour
    {
        private void Start() => AudioManager.Instance.ChangerMusiqueVictoire();
    }
}
