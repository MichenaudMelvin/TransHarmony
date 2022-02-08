using UnityEngine;
using System.Collections;
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
    [Range(1, 5)]
    [Tooltip("Temps d'attente entre deux commandes")]
    private float _maxWaitingTime;

    [SerializeField]
    [Tooltip("Parent qui contient tous les positions des commandes")]
    private Transform _commandsPositions;

    // [SerializeField]
    // [Range(10f, 100f)]
    // [Tooltip("Espace entre les commandes (UI)")]
    // private float _spaceBetweenCommands;

    [Header("Artists")]
    [SerializeField]
    [Tooltip("Parent qui contient tous les artistes")]
    private Transform _artistContainers;

    [Tooltip("Liste contenant les artistes présents dans les halls")]
    private List<ArtistsAttributes> _listArtistesInHalls;

    [Tooltip("Commande demandé")]
    private List<string> _commandForHalls;

    private void Update(){
        this.ManageCommands();
    }

    private void ManageCommands(){
        this.CreateCommands();
        this.DestroyCommands();
    }

    // temps d'attente entre deux commande
    private IEnumerator WaitingTimeBeforeNewCommand(float time){
        yield return new WaitForSeconds(time);

        this.CreateCommands();
    }

    // créé les commandes
    public void CreateCommands(){
        if(this.transform.childCount < _maxCommandNumber && _gameManager.GetTime() > 0 && _gameManager.GetCanChangeArtist()){
            if(this.transform.childCount == 0){
                print("jupdate la liste");
                this.UpdateArtistList();
            }

            CommandAttributes newCommand = Instantiate(_commandToGenerate, this.transform);

            newCommand.SetGameManager(_gameManager);
            newCommand.SetPosition(_commandsPositions.GetChild(this.transform.childCount-1).GetComponent<RectTransform>().anchoredPosition);

            // la commande va être attribué à un artise aléatoire
            int artistIndex = Random.Range(0, _listArtistesInHalls.Count-1);

            newCommand.SetArtisteWhoNeedIt(_listArtistesInHalls[artistIndex].GetComponent<ArtistsAttributes>());

            StartCoroutine(this.WaitingTimeBeforeNewCommand(Random.Range(0, _maxWaitingTime)));
        }

    }

    // ajoute à la liste les artistes présent dans les halls
    private void UpdateArtistList(){
        _listArtistesInHalls = new List<ArtistsAttributes>{};

        for(int i = 0; i < _artistContainers.childCount; i++){
            if(_artistContainers.GetChild(i).gameObject.activeInHierarchy){
                _listArtistesInHalls.Add(_artistContainers.GetChild(i).GetComponent<ArtistsAttributes>());
            }
        }
    }

    // détruits les commandes
    private void DestroyCommands(){
        if(this.transform.childCount > 0 && _gameManager.GetTime() <= 0){
            for(int i = 0; i < this.transform.childCount; i++){
                Destroy(this.transform.GetChild(i).gameObject);
            }

            _listArtistesInHalls.Clear();
        }
    }

    // public functions
    public int GetMaxCommandNumber(){return _maxCommandNumber;}

}