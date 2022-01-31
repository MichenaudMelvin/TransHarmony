using UnityEngine;
using UnityEngine.UI;

public class CommandAttributes : MonoBehaviour{

    [Header("Halls")]
    private string _hallToGo;

    [SerializeField]
    private Text _hallName;

    private Vector2 _positionToSpawn;

    [SerializeField]
    [Range(10f, 25f)]
    private float _availableTime;

    private void Start(){
        _hallName.text = _hallToGo;

        // fait spawn chaque élement en haut à gauche du canvas
        this.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        this.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        // this.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x/2, -(this.GetComponent<RectTransform>().sizeDelta.y/2));

        this.SetPosition();
    }

    private void Update(){
        this.ManageTimer();

    }

    private void ManageTimer(){
        _availableTime -= Time.deltaTime;

        if(_availableTime <= 0){Destroy(this.gameObject);}
    }

    private void SetPosition(){this.GetComponent<RectTransform>().anchoredPosition = _positionToSpawn;}

    // public functions
    public string GetHallToGo(){return _hallToGo;}

    public void SetHallToGo(string newHall){_hallToGo = newHall;}

    public void SetPosition(Vector2 newPosition){_positionToSpawn = newPosition;}


}