using System;
using System.Collections;
using PlatformerTP.Interfaces;
using PlatformerTP.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlatformerTP
{
    public class Joueur : MonoBehaviour
    {
        //public float vie = 100;
        
        private Jump _jump;
        private IDeplacement _deplacement;
        private IAttaque _attaque;
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private static readonly int Speed = Animator.StringToHash("Vitesse");
        private static readonly int VSpeed = Animator.StringToHash("VVitesse");
        private static readonly int SurPlancher = Animator.StringToHash("SurPlancher");
        
        public ParticleSystem particles;
        private GameObject _talents;
        private GameObject _canvas;
        private GameObject _gameOver;
        private GameObject _esc;

        public HpBar hpBar;

        private bool _estGameOver;
        private bool _estBoutique;
        private bool _estEsc;
        private void Awake()
        {
            _jump = GetComponent<Jump>();
            _deplacement = GetComponent<IDeplacement>();
            _attaque = GetComponent<IAttaque>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _talents = GameObject.Find("Talents");
            hpBar = FindObjectOfType<HpBar>();
            _canvas = GameObject.Find("UIHud");
            _esc = GameObject.Find("Esc");
            
            _gameOver = FindObjectOfType<GameOver>()?.gameObject;

            if (hpBar == null) return;
            hpBar.SetVie(InfosJoueur.Vie);
        }

        private void Start()
        {
            StartCoroutine(ResetGame(2f));
        }

        public void PrendreDegat(float dmg)
        {
            AudioManager.Instance.Jouer("ZombieHit");
            InfosJoueur.Vie -= dmg;
        }

        private IEnumerator ResetGame(float temps)
        {
            yield return new WaitUntil(() => _estGameOver);
            yield return new WaitForSecondsRealtime(temps);
            InfosJoueur.Vie = 100;
            Time.timeScale = 1;
            SceneManager.LoadScene("SampleScene");
        }

        private void Update()
        {
            if (InfosJoueur.Vie <= 0 && !_estGameOver)
            {
                if (_gameOver == null) return;
                _estGameOver = true;
                AudioManager.Instance.Jouer("GameOver");
                _talents.SetActive(false);
                _canvas.SetActive(false);
                _gameOver.SetActive(true);
                Score.score = 0;
                
                return;
            }

            if (InfosJoueur.Vie <= 0)
            {
                Time.timeScale = 0;
                return;
            }

            if (_gameOver != null)
            {
                _gameOver.SetActive(false);
            }

            if (_esc != null)
            {
                if (!_estEsc)
                {
                    _estEsc = true;
                    _esc.SetActive(false);
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Time.timeScale = _esc.activeSelf ? 1 : 0;
                    _esc.SetActive(!_esc.activeSelf);
                }
            }

            var boutiqueBouton = Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.P) ||
                                 Input.GetKeyDown(KeyCode.T);

            if (hpBar != null)
            {
                hpBar.SetVie(InfosJoueur.Vie);
            }

            if (_talents != null)
            {
                if (!_estBoutique)
                {
                    _estBoutique = true;
                    _talents.SetActive(false);
                }
                
                if (_talents.activeSelf && Input.GetKeyDown(KeyCode.Escape)) _talents.SetActive(false);
                
                _canvas.SetActive(!_talents.activeSelf);

                if (boutiqueBouton && !_esc.activeSelf)
                {
                    Time.timeScale = _talents.activeSelf ? 1 : 0;
                    _talents.SetActive(!_talents.activeSelf);
                }

            }
            
            if (Input.GetButtonDown("Jump")) _jump.Saute();
            

            _deplacement?.Bouge(Input.GetAxis("Horizontal"));
            _attaque?.Attaquer();
            _animator.SetFloat(Speed, Mathf.Abs(_rigidbody.velocity.x));
            _animator.SetFloat(VSpeed, _rigidbody.velocity.y);
        
            _animator.SetBool(SurPlancher, _jump.SurPlateforme());
        }

        private void CreerParticules()
        {
            particles.Play();
        }

        private void JouerSonPas() => AudioManager.Instance.Jouer("JoueurPas");

        private void JouerSonSaut() => AudioManager.Instance.Jouer("JoueurSaut");
    }
}