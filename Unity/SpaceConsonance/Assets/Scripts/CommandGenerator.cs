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
    [Range(1, 4)]
    [Tooltip("Nombre de commande maximales")]
    private int _maxCommandNumberAtTime;

    [SerializeField]
    [Range(1f, 5f)]
    [Tooltip("Temps d'attente entre deux commandes")]
    private float _maxWaitingTime;

    [SerializeField]
    [Tooltip("Parent qui contient tous les positions des commandes")]
    private Transform _commandsPositions;

    [Header("Artists")]
    [SerializeField]
    [Tooltip("Parent qui contient tous les artistes")]
    private Transform _artistContainers;

    [Tooltip("Liste contenant les artistes présents dans les halls")]
    private List<ArtistsAttributes> _listArtistesInHalls = new List<ArtistsAttributes>{};

    [Tooltip("Nombre d'artistes par jours")]
    private int _nbrOfArtistePerDays = 4; // à set manuellement, pas trouvé d'autres moyens

    [Tooltip("Commande demandé")]
    private List<string> _commandForHalls;

    private void Start(){
        if(_maxCommandNumberAtTime > _artistContainers.childCount){
            _maxCommandNumberAtTime = _artistContainers.childCount;
        }

        for(int i = 0; i < _commandsPositions.childCount; i++){
            _commandsPositions.GetChild(i).gameObject.SetActive(false);
        }

    }

    // ajoute à la liste les artistes présent dans les halls
    // micro delay sinon marche pas
    private IEnumerator UpdateArtistList(float time){
        yield return new WaitForSeconds(time);

        _listArtistesInHalls = new List<ArtistsAttributes>{};

        for(int i = 0; i < _artistContainers.childCount; i++){
            if(_artistContainers.GetChild(i).gameObject.activeInHierarchy){
                _listArtistesInHalls.Add(_artistContainers.GetChild(i).GetComponent<ArtistsAttributes>());
            }
        }

        for(int i = 0; i < _commandsPositions.childCount; i++){
            _commandsPositions.GetChild(i).gameObject.SetActive(false);
        }
    }

    // la commande va être attribué à un artise aléatoire si l'artiste n'a pas deja une commande
    private void CheckArtistAvailability(CommandAttributes command){
        int artistIndex = Random.Range(0, _listArtistesInHalls.Count);

        for(int i = 0; i < this.transform.childCount; i++){
            if(this.transform.GetChild(i).GetComponent<CommandAttributes>().GetArtisteWhoNeedIt() == _listArtistesInHalls[artistIndex].GetComponent<ArtistsAttributes>()){
                this.CheckArtistAvailability(command);
                return;
            }
        }

        command.SetArtisteWhoNeedIt(_listArtistesInHalls[artistIndex].GetComponent<ArtistsAttributes>());
        command.SetPosition(_commandsPositions.GetChild(this.transform.childCount-1).GetComponent<RectTransform>().anchoredPosition);

    }

    // temps d'attente entre deux commande
    private IEnumerator WaitingTimeBeforeNewCommand(float time){
        yield return new WaitForSeconds(time);

        this.CreateCommands();
    }

    // public functions
    public int GetMaxCommandNumber(){return _maxCommandNumberAtTime;}

    // créé les commandes
    public void CreateCommands(){
        if(this.transform.childCount < _maxCommandNumberAtTime && _gameManager.GetTime() > 0 && _gameManager.GetCanChangeArtist()){
            if(this.transform.childCount == 0){
                StartCoroutine(this.UpdateArtistList(0.0001f));
            }

            float delay;
            if(_listArtistesInHalls.Count == _nbrOfArtistePerDays){

                // à faire en fonction du nombre d'artistes

                CommandAttributes newCommand = Instantiate(_commandToGenerate, this.transform);

                newCommand.SetGameManager(_gameManager);
                this.CheckArtistAvailability(newCommand);

                delay = Random.Range(0, _maxWaitingTime);
            } else{
                delay = 0f;
            }

            StartCoroutine(this.WaitingTimeBeforeNewCommand(delay));
        }
    }

    // détruits les commandes
    public void DestroyCommands(){
        if(this.transform.childCount > 0){
            for(int i = 0; i < this.transform.childCount; i++){
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }

        _listArtistesInHalls.Clear();
    }
}