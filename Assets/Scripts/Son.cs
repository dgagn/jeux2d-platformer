using System;
using UnityEngine;
using UnityEngine.Audio;

namespace PlatformerTP
{
    [Serializable]
    public class Son
    {
        public string nom;
        
        public AudioClip clip;
        
        [Range(0f, 1f)]
        public float volume;

        public bool loop;
        
        [HideInInspector]
        public AudioSource source;

        public AudioMixerGroup mixerGroup;
    }
}