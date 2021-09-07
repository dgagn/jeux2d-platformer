using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace PlatformerTP
{
    public class AudioManager: MonoBehaviour
    {
        public Son[] audios;

        public static AudioManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            
            foreach (var son in audios)
            {
                son.source = gameObject.AddComponent<AudioSource>();
                son.source.clip = son.clip;
                son.source.volume = son.volume;
                son.source.loop = son.loop;
                if (son.mixerGroup != null)
                {
                    son.source.outputAudioMixerGroup = son.mixerGroup;
                }
            }
        }

        private void Start()
        {
            Jouer("Musique");
        }

        public void ChangerMusiqueVictoire()
        {
            var s = Array.Find(audios, son => son.nom == "Musique");
            s?.source.Stop();
            
            var s2 = Array.Find(audios, son => son.nom == "MusiqueVictoire");

            if (s2.source.isPlaying) return;
            s2?.source.Play();
        }
        
        public void Jouer(string nom, bool noPitch = false)
        {
            var s = Array.Find(audios, son => son.nom == nom);
            if(!noPitch) s.source.pitch = Random.Range(0.5f, 1f);
            
            s?.source.Play();
        }
    }
}