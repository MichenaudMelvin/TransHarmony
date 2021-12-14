using UnityEngine;
using UnityEngine.UI;

public class ChangeHallNames : MonoBehaviour{

    public HallsAttributes hall;
    public Text textToChange;

    private void Start(){
        textToChange.text = hall.name;
    }
}
