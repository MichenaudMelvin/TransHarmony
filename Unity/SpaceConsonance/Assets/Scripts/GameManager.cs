using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour{

    [Header("Timer")]
    [SerializeField]
    [Range(1f, 300f)]
    [Tooltip("60 = 1min, 300 = 5min, (temps en secondes)")]
    private float _timeOfADay = 300f;

    private float _timeLeft = 0f;

    private int _day = 0;

    [SerializeField]
    private Text _timerText;
    private string _timerTextPrefix;

    [SerializeField]
    private Text _dayText;
    private string _dayTextPrefix;

    [SerializeField]
    private Slider _sliderTimer;

    [SerializeField]
    private Gradient _sliderGradient;

    [Space(10)]

    [Header("UI Results")]
    [SerializeField]
    private RawImage _resultImage;

    [Space(10)]

    [Header("Halls")]

    [SerializeField]
    private Transform _hallsContainer;

    private List<HallsAttributes> _listHalls = new List<HallsAttributes>{};

    [Space(10)]

    [Header("Artists")]
    [Tooltip("GameObject empty où sont placés tout les artistes créés")]
    [SerializeField]
    private Transform _artistContainers;

    private bool _canChangeArtist = true;

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
    }

    private void Update(){
        this.ManageTimer();
    }

    private void ManageTimer(){
        if(!_resultImage.gameObject.activeInHierarchy && _canChangeArtist){
            _timeLeft -= Time.deltaTime;

            _timerText.text = _timerTextPrefix + ((int)_timeLeft).ToString() + "s";
            _sliderTimer.value = _timeLeft;
            _sliderTimer.transform.Find("Fill").GetComponent<Image>().color = _sliderGradient.Evaluate(_sliderTimer.normalizedValue);

            if(_timeLeft <= 0f){
                _day += 1;
                _dayText.text = _dayTextPrefix + _day.ToString();
                _resultImage.gameObject.SetActive(true);
                _canChangeArtist = this.CheckArtistStatus();
            }
        }
    }

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

    public int GetDay(){return _day;}

    public bool GetCanChangeArtist(){return _canChangeArtist;}

    public void RestartDay(){
        _timeLeft = _timeOfADay;

        _resultImage.gameObject.SetActive(false);

        for(int i = 0; i < _listHalls.Count; i++){
            _listHalls[i].ChangeArtist();
        }
    }

}
