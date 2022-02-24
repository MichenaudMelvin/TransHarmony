using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

    [Header("Timer")]
    [SerializeField]
    [Range(1f, 300f)]
    [Tooltip("Temps de la phase 1 // 60 = 1min, 300 = 5min, (temps en secondes)")]
    private float _timePhase1;

    [SerializeField]
    [Range(1f, 300f)]
    [Tooltip("Temps de la phase 2 // 60 = 1min, 300 = 5min, (temps en secondes)")]
    private float _timePhase2;

    [SerializeField]
    [Range(1, 10)]
    [Tooltip("Nombre de jour que va durer la phase 2")]
    private int _days;

    [Tooltip("Nombre de jours restants")]
    private int _daysRemaining;

    [Tooltip("Temps restant de la journée")]
    private float _timeLeft = 0f;

    [SerializeField]
    [Tooltip("Temps restant de la journée (Visuel)")]
    private Text _timerText;
    [Tooltip("Préfix du temps restant (Visuel)")]
    private string _timerTextPrefix;

    [SerializeField]
    [Tooltip("Phase Actuelle (Visuel)")]
    private Text _currentPhaseText;

    [Tooltip("Préfix des jours restant (Visuel)")]
    private string _currentPhaseTextPrefix;

    [SerializeField]
    [Tooltip("Timer sous forme de slider")]
    private Slider _sliderTimer;

    [SerializeField]
    [Tooltip("Dégradé du Slider Timer")]
    private Gradient _sliderGradient;

    [Space(10)]

    [Header("UI Results")]
    [SerializeField]
    [Tooltip("Image qui s'affiche pour transitionner d'une phase à une autre")]
    private RawImage _transitionImage;

    [SerializeField]
    [Tooltip("Texte qui s'affiche pendant une transition")]
    private Text _transitionText;

    [Space(10)]

    [Header("Halls")]

    [SerializeField]
    [Tooltip("Parent qui contient tous les halls")]
    private Transform _hallsContainer;

    [Tooltip("Nombre de halls")]
    private int _activeHalls = 0;

    [Tooltip("Liste contenant tous les halls")]
    private List<HallsAttributes> _listHalls = new List<HallsAttributes>{};

    [Space(10)]

    [Header("Artists")]
    [SerializeField]
    [Tooltip("Parent qui contient tous les artistes")]
    private Transform _artistContainers;

    [Space(10)]

    [Header("Points")]
    [Tooltip("Les points accumulés par le joueur")]
    private float _playerPoints;

    [Space(10)]

    [Header("Music")]
    [SerializeField]
    [Tooltip("Référence au Music Manager")]
    private MusicManager _musicManager;

    [Space(10)]

    [Header("Others")]
    [SerializeField]
    [Tooltip("GameObject qui créé les commandes")]
    private CommandGenerator _commandGenerator;

    [SerializeField]
    [Tooltip("GameObject qui créé les items")]
    private ItemGenerator _itemGenerator;

    [Tooltip("Référence aux settings du jeu")]
    private Settings _settings;

    [SerializeField]
    [Tooltip("Parent qui contient le public")]
    private List<Transform> _publicContainersList;

    [SerializeField]
    [Tooltip("List des variables des artistes")]
    private List<ArtistAsset> _artistAssetsList;

    [Space(10)]

    [Header("Phases")]
    [Tooltip("Nombre de conditions a completer avant le débloquage du hall")]
    public int[] conditionsLeftBeforeHallComplete = new int[] {4,4,4,4};

    [Tooltip("La valeur de réussite de l'artiste en fonction des events")]
    [Range(0f, 1f)]
    public float[] fouleMovementStrenght = new float[] {1f,1f,1f,1f};

    [Tooltip("Phase Actuelle")]
    private int _currentPhase = 1;

    [SerializeField]
    [Tooltip("Ecran de fin")]
    private RawImage _endingScreen;

    [SerializeField]
    [Tooltip("Autre UI")]
    private GameObject _externalUI;

    [Space(10)]

    [Header("Game pause")]
    [Tooltip("Si le jeu est en pause ou non")]
    private bool _isGamePause = false;

    [Tooltip("Sauvegarde du timer pour la pause")]
    private float _savedTime;

    [Space(10)]

    [Header("Lights")]
    [SerializeField]
    [Tooltip("Liste des spots lights")]
    private List<Light> _lightList;

    private void Start(){
        _timeLeft = _timePhase1;
        _daysRemaining = _days;

        // Settings
        // _settings = FindObjectOfType<Settings>();
        // AudioListener.volume = _settings.GetMasterVolume();

        // initialisation du timer
        _timerTextPrefix = _timerText.text;
        _sliderTimer.maxValue = _timePhase1;
        _sliderTimer.value = _sliderTimer.maxValue;

        for(int i = 0; i < _hallsContainer.childCount; i++){
            _listHalls.Add(_hallsContainer.GetChild(i).GetComponent<HallsAttributes>());
        }

        StartCoroutine(this.LateStart());
    }

    private IEnumerator LateStart(){
        yield return new WaitForSeconds(0.0005f);

        _commandGenerator.CreateCommands();
        StartCoroutine(_itemGenerator.CreateItems());
    }

    private void Update(){
        this.ManageTimer();
    }

    // gère de le timer du festival entre chaque journées
    private void ManageTimer(){
        if(!_transitionImage.gameObject.activeInHierarchy && _timeLeft > 0f && !_isGamePause){
            _timeLeft -= Time.deltaTime;

            if(_currentPhase == 1){
                if(_commandGenerator.GetHallsLeft() == 0){
                    _timeLeft = 0f;
                }
            }

            // ajouter la fonction qui créer les commandes ici
            _timerText.text = _timerTextPrefix + ((int)_timeLeft).ToString() + "s";
            _sliderTimer.value = _timeLeft;
            _sliderTimer.transform.Find("Fill").GetComponent<Image>().color = _sliderGradient.Evaluate(_sliderTimer.normalizedValue);

            if(_timeLeft <= 0f){
                this.PhaseTransition();
            }
        }
    }

    // ce qu'il se passe à la fin de chaque phases
    private void PhaseTransition(){
        if(_currentPhase == 1){
            UnlockHalls();

            if(_activeHalls > 0){
                if(_activeHalls == 1){_transitionText.text = _activeHalls + " hall is ready\nThe festival is about to begin !";}
                else if(_activeHalls > 1){_transitionText.text = _activeHalls + " halls are ready\nThe festival is about to begin !";}
            } else if(_activeHalls == 0){
                _transitionText.text = "No halls are ready\nClick to return to the menu";
            }

            _transitionImage.gameObject.SetActive(true);

            // préparation pour la phase 2 (destructions des commandes restantes, check les artistes encore disponibles)
            // va recréer les items et commandes
            _commandGenerator.DestroyCommands();
            _itemGenerator.DestroyItems();

            // StartCoroutine(_musicManager.EndPhase());

            _currentPhase += 1;

        } else if(_currentPhase == 2){
            if(_daysRemaining <= 0){
                _commandGenerator.DestroyCommands();
                _itemGenerator.DestroyItems();
                _externalUI.SetActive(false);
                _endingScreen.gameObject.SetActive(true);
            } else if(_daysRemaining > 0){
                // ici passe à un jouer suplémentaire
                int showDay = _days - _daysRemaining;
                _transitionText.text = "Day " + showDay + " finished !\nDay " + (showDay + 1) + " is about to start.";
                _transitionImage.gameObject.SetActive(true);

                _commandGenerator.DestroyCommands();
                _itemGenerator.DestroyItems();

                for(int i = 0; i < _publicContainersList.Count; i++){
                    for(int j = 0; j < _publicContainersList[i].childCount; j++){
                        _publicContainersList[i].GetChild(j).GetComponent<EffetFoule>().ResetFoule();
                    }
                }

                // StartCoroutine(_musicManager.EndPhase());
            }
        }
    }

    // pour savoir si un hall a bien reçu les deux commandes nécessaires ou non
    private void UnlockHalls(){
        for(int i = 0; i < conditionsLeftBeforeHallComplete.Length; i++){
            if(conditionsLeftBeforeHallComplete[i] <= 0){
                // voir ici pour set si le hall est actif ou non
                _publicContainersList[i].gameObject.SetActive(true);
                _activeHalls += 1;
            } else{
                _hallsContainer.GetChild(i).GetComponent<HallsAttributes>().Disable();
                conditionsLeftBeforeHallComplete[i] = 100;
                _commandGenerator.SetHallsLeft(-1);
            }
        }

    }

    // public functions
    public float GetTime(){return _timeLeft;}

    public float GetTimeOfADay(){return _timePhase1;}

    public Gradient GetTimerGradient(){return _sliderGradient;}

    // recommence une journée
    // call par le bouton
    public void RestartDay(){
        if(_activeHalls > 0){
            if(_currentPhase == 1){
                _timeLeft = _timePhase1;
                _sliderTimer.maxValue = _timePhase1;
            } else if(_currentPhase == 2){
                _daysRemaining -= 1;
                _timeLeft = _timePhase2/_days;
                _sliderTimer.maxValue = _timeLeft;

                for(int i = 0; i < _publicContainersList.Count; i++){
                    for(int j = 0; j < _publicContainersList[i].childCount; j++){
                        StartCoroutine(_publicContainersList[i].GetChild(j).GetComponent<EffetFoule>().FouleMovement());
                    }
                }

                for(int i = 0; i < _lightList.Count; i++){
                    _lightList[i].enabled = true;
                }

                // faire deux timers du coups
                // un pour le temps du jour
                // un autre pour le temps restant du festival
            }

            if(_daysRemaining != _days - 1){
                for(int i = 0; i < _listHalls.Count; i++){
                    // if(_listHalls[i].GetIsActive()){
                        _listHalls[i].ChangeArtist();
                    // }
                }
            }

            _sliderTimer.value = _sliderTimer.maxValue;

            _transitionImage.gameObject.SetActive(false);

            _commandGenerator.CreateCommands();
            StartCoroutine(_itemGenerator.CreateItems());
            _musicManager.SetAudioClip();
        } else if(_activeHalls <= 0){
            SceneManager.LoadScene("StartScene");
        }
    }

    // pour ajouter ou enlever des points
    public void UpdatePoints(float amount){
        _playerPoints += amount;
    }

    public int GetCurrentPhase(){return _currentPhase;}

    public bool GetIsGamePause(){return _isGamePause;}

    // pause du jeu
    public void PauseGame(bool pauseOrUnPause){
        _isGamePause = pauseOrUnPause;

        // pour mouvement up si la commande l'a pas fait
        // if(!_isGamePause){
        //     for(int i = 0; i < _commandGenerator.transform.childCount; i++){
        //         _commandGenerator.transform.GetChild(i).gameObject.SetActive(!pauseOrUnPause);
        //     }
        // }

        _commandGenerator.gameObject.SetActive(!pauseOrUnPause);
        _itemGenerator.gameObject.SetActive(!pauseOrUnPause);
    }

    public List<ArtistAsset> GetArtistAssetsList(){return _artistAssetsList;}
}
