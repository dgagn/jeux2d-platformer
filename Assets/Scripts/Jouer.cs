using System;
using PlatformerTP.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace PlatformerTP
{
    public class Jouer : MonoBehaviour
    {
        public void JouerJeux()
        {
            SceneManager.LoadScene("Scenes/SampleScene");
        }

        public void QuitterJeux()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        public void AllerOptions()
        {
            SceneManager.LoadScene("Scenes/Options");
        }

        public void Retour()
        {
            Score.score = 0;
            SceneManager.LoadScene("Scenes/MenuPrincipal");
        }

        public AudioMixer audioMixer;
        public void MettreVolume(float volume)
        {
            audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        }

        public void Reprendre()
        {
            Time.timeScale = 1;
        }

        public void Difficulte(float difficulte)
        {
            InfosJoueur.Difficulte = (int)difficulte;
        }
    }
}
