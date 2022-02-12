using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour{

    [Header("Timer")]
    [SerializeField]
    [Range(1f, 300f)]
    [Tooltip("Temps d'une journée au festival // 60 = 1min, 300 = 5min, (temps en secondes)")]
    private float _timeOfADay = 300f;

    [Tooltip("Temps restant de la journée")]
    private float _timeLeft = 0f;

    [Tooltip("Nombre de jours écoulés")]
    private int _day = 0;

    [SerializeField]
    [Tooltip("Temps restant de la journée (Visuel)")]
    private Text _timerText;
    [Tooltip("Préfix du temps restant (Visuel)")]
    private string _timerTextPrefix;

    [SerializeField]
    [Tooltip("Nombre de jours écoulés (Visuel)")]
    private Text _dayText;

    [Tooltip("Préfix des jours restant (Visuel)")]
    private string _dayTextPrefix;

    [SerializeField]
    [Tooltip("Timer sous forme de slider")]
    private Slider _sliderTimer;

    [SerializeField]
    [Tooltip("Dégradé du Slider Timer")]
    private Gradient _sliderGradient;

    [Space(10)]

    [Header("UI Results")]
    [SerializeField]
    [Tooltip("Image qui donne les resultas")]
    private RawImage _resultImage;

    [SerializeField]
    [Tooltip("Texte qui donne les resultas")]
    private Text _resultText;

    [Space(10)]

    [Header("Halls")]

    [SerializeField]
    [Tooltip("Parent qui contient tous les halls")]
    private Transform _hallsContainer;

    [Tooltip("Liste contenant tous les halls")]
    private List<HallsAttributes> _listHalls = new List<HallsAttributes>{};

    [Space(10)]

    [Header("Artists")]
    [SerializeField]
    [Tooltip("Parent qui contient tous les artistes")]
    private Transform _artistContainers;

    [Tooltip("Si il est encore possible de changer les artistes ou non")]
    private bool _canChangeArtist = true;

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

    private void Start(){
        _timeLeft = _timeOfADay;

        // initialisation du timer
        _timerTextPrefix = _timerText.text;
        _dayTextPrefix = _dayText.text;
        _dayText.text = _dayTextPrefix + _day.ToString();
        _sliderTimer.maxValue = _timeOfADay;
        _sliderTimer.value = _sliderTimer.maxValue;

        for(int i = 0; i < _hallsContainer.childCount; i++){
            _listHalls.Add(_hallsContainer.GetChild(i).GetComponent<HallsAttributes>());
        }

        _commandGenerator.CreateCommands();
    }

    private void Update(){
        this.ManageTimer();
    }

    // gère de le timer du festival entre chaque journées
    private void ManageTimer(){
        if(!_resultImage.gameObject.activeInHierarchy && _canChangeArtist){
            _timeLeft -= Time.deltaTime;

            // ajouter la fonction qui créer les commandes ici
            _timerText.text = _timerTextPrefix + ((int)_timeLeft).ToString() + "s";
            _sliderTimer.value = _timeLeft;
            _sliderTimer.transform.Find("Fill").GetComponent<Image>().color = _sliderGradient.Evaluate(_sliderTimer.normalizedValue);

            if(_timeLeft <= 0f){
                this.EndDay();
            }
        }
    }

    // ce qu'il se passe à la fin de charque jours
    private void EndDay(){
        // augmente de compteur de jours
        _day += 1;
        _dayText.text = _dayTextPrefix + _day.ToString();

        // affichage des resultats
        _resultText.text = "Votre score : " + _playerPoints;
        _resultImage.gameObject.SetActive(true);

        // préparation pour le prochain jours (destructions des commandes restantes, check les artistes encore disponibles)
        _canChangeArtist = this.CheckArtistStatus();
        _commandGenerator.DestroyCommands();
    }

    // regarde parmis tous les artistes si certains non pas encore fait un concert
    private bool CheckArtistStatus(){
        int artistAvailable = 0;
        for(int i = 0; i < _artistContainers.childCount; i++){
            if(!_artistContainers.GetChild(i).GetComponent<ArtistsAttributes>().GetStatus()){
                artistAvailable += 1;
            }
        }

        if(artistAvailable > _hallsContainer.childCount){return true;}
        else{return false;}
    }

    private void ManagePoints(){
        // on verra
    }

    // public functions
    public float GetTime(){return _timeLeft;}

    public float GetTimeOfADay(){return _timeOfADay;}

    public int GetDay(){return _day;}

    public Gradient GetTimerGradient(){return _sliderGradient;}

    public bool GetCanChangeArtist(){return _canChangeArtist;}

    // recommence une journée
    public void RestartDay(){
        _timeLeft = _timeOfADay;

        _resultImage.gameObject.SetActive(false);

        for(int i = 0; i < _listHalls.Count; i++){
            _listHalls[i].ChangeArtist();
        }

        _commandGenerator.CreateCommands();
        _musicManager.SetAudioClip();
    }

    // pour ajouter ou enlever des points
    public void UpdatePoints(float amount){
        _playerPoints += amount;
    }

}
