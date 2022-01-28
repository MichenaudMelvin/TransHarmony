using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour{

    [SerializeField]
    private float _timeLeft = 300f; // 300 = 5min

    private float _time = 0f;

    private int _day = 0;

    private void Start(){
        _time = _timeLeft;
    }

    private void Update(){
        _time -= Time.deltaTime;

        if(_time <= 0f){
            _time = _timeLeft;
            _day += _day;
        }

    }

    private void ManagePoints(){
        // on verra
    }

    // public functions
    public float GetTime(){return _timeLeft;}

    public int GetDay(){return _day;}

}
