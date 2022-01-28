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

    [Space(10)]

    [Header("UI Results")]
    [SerializeField]
    private RawImage resultImage;

    [Space(10)]

    [Header("Halls")]
    [SerializeField]
    private List<HallsAttributes> _listHalls;

    private void Start(){
        _timeLeft = _timeOfADay;

        // initialisation du timer
        _timerTextPrefix = _timerText.text;
        _dayTextPrefix = _dayText.text;
        _dayText.text = _dayTextPrefix + _day.ToString();
    }

    private void Update(){
        this.ManageTimer();
    }

    private void ManageTimer(){
        if(!resultImage.gameObject.activeInHierarchy){
            _timeLeft -= Time.deltaTime;

            _timerText.text = _timerTextPrefix + ((int)_timeLeft).ToString() + "s";

            if(_timeLeft <= 0f){
                _day += 1;
                _dayText.text = _dayTextPrefix + _day.ToString();
                resultImage.gameObject.SetActive(true);
            }
        }
    }

    private void ManagePoints(){
        // on verra
    }

    // public functions
    public float GetTime(){return _timeLeft;}

    public int GetDay(){return _day;}

    public void RestartDay(){
        _timeLeft = _timeOfADay;

        resultImage.gameObject.SetActive(false);

        for(int i = 0; i < _listHalls.Count; i++){
            _listHalls[i].ChangeArtist();
        }
    }

}
