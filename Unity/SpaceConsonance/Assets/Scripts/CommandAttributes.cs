using UnityEngine;
using UnityEngine.UI;

public class CommandAttributes : MonoBehaviour{

    [Header("Halls")]
    [Tooltip("Hall dans lequel la commande doit aller")]
    private string _hallToGo;

    [SerializeField]
    [Tooltip("Texte qui permet de connaitre le hall dans lequel la commande doit aller")]
    private Text _hallName;

    [Tooltip("La position où spawn la commande (Position en UI)")]
    private Vector2 _positionToSpawn;

    [SerializeField]
    [Range(10f, 25f)]
    [Tooltip("Temps restant avant que la commande disparaisse (lifetime)")]
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

    // detruit les commande à partir d'un temps donné
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