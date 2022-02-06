using UnityEngine;
using System.Collections.Generic;

public class CommandGenerator : MonoBehaviour{

    [Header("Commands")]
    [SerializeField]
    [Tooltip("Prefab de la commande")]
    private CommandAttributes _commandToGenerate;

    [SerializeField]
    [Tooltip("Référence au GameManager")]
    private GameManager _gameManager;

    [SerializeField]
    [Range(1, 10)]
    [Tooltip("Nombre de commande maximales")]
    private int _maxCommandNumber;

    [SerializeField]
    [Range(10f, 100f)]
    [Tooltip("Espace entre les commandes (UI)")]
    private float _spaceBetweenCommands;

    [Header("Halls")]
    [SerializeField]
    [Tooltip("Parent qui contient tous les halls")]
    private Transform _hallsContainer;

    [Tooltip("Liste contenant tous les halls")]
    private List<HallsAttributes> _listHalls = new List<HallsAttributes>{};

    [Tooltip("Commande demandé")]
    private List<string> _commandForHalls;

    private void Start(){
        for(int i = 0; i < _hallsContainer.childCount; i++){
            _listHalls.Add(_hallsContainer.GetChild(i).GetComponent<HallsAttributes>());
        }
    }

    private void Update(){
        this.ManageCommands();
    }

    private void ManageCommands(){
        this.CreateCommands();
        this.DestroyCommands();
    }

    private void CreateCommands(){
        if(this.transform.childCount < _maxCommandNumber && _gameManager.GetTime() > 0 && _gameManager.GetCanChangeArtist()){
            CommandAttributes newCommand = Instantiate(_commandToGenerate, this.transform);
            newCommand.SetPosition(new Vector2((newCommand.GetComponent<RectTransform>().sizeDelta.x * (this.transform.childCount-1)) + (_spaceBetweenCommands * this.transform.childCount) + ((newCommand.GetComponent<RectTransform>().sizeDelta.x/2) - _spaceBetweenCommands), -(newCommand.GetComponent<RectTransform>().sizeDelta.y/2)));

            int hallIndex = Random.Range(0, _listHalls.Count-1);

            newCommand.SetHallToGo(_listHalls[hallIndex].transform.Find("HallName").GetComponent<TextMesh>().text);
        }
    }

    private void DestroyCommands(){
        if(this.transform.childCount > 0 && _gameManager.GetTime() <= 0){
            for(int i = 0; i < this.transform.childCount; i++){
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }
    }

    // public functions
    public int GetMaxCommandNumber(){return _maxCommandNumber;}

}