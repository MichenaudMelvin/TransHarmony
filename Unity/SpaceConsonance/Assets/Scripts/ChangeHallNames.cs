using UnityEngine;

public class ChangeHallNames : MonoBehaviour{

    [SerializeField]
    private HallsAttributes _hall;

    [SerializeField]
    private TextMesh _textToChange;

    private void Start(){
        // écrit le nom du hall au sol
        _textToChange.text = _hall.name;
    }
}
