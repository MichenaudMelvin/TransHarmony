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
    [Range(1, 8)]
    [Tooltip("Nombre de commande maximales")]
    private int _maxCommandNumber;

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
    private List<ArtistsAttributes> _listArtistesInHalls;

    [Tooltip("Commande demandé")]
    private List<string> _commandForHalls;

    // ajoute à la liste les artistes présent dans les halls
    private void UpdateArtistList(){
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
    private void CheckAvailability(CommandAttributes command){
        int artistIndex = Random.Range(0, _listArtistesInHalls.Count);
        print(_listArtistesInHalls);

        command.SetArtisteWhoNeedIt(_listArtistesInHalls[artistIndex].GetComponent<ArtistsAttributes>());

        // fait crash
        // for(int i = 0; i < this.transform.childCount; i++){
        //     print("je passe dans la boucle for");
        //     if(command.GetArtisteWhoNeedIt() == this.transform.GetChild(i).GetComponent<CommandAttributes>().GetArtisteWhoNeedIt() && command != this.transform.GetChild(i).GetComponent<CommandAttributes>()){
        //         print("je passe dans la boucle if");
        //         this.CheckAvailability(command);
        //     }
        // }
    }

    // temps d'attente entre deux commande
    private IEnumerator WaitingTimeBeforeNewCommand(float time){
        yield return new WaitForSeconds(time);

        this.CreateCommands();
    }

    // public functions
    public int GetMaxCommandNumber(){return _maxCommandNumber;}

    // créé les commandes
    public void CreateCommands(){
        if(this.transform.childCount < _maxCommandNumber && _gameManager.GetTime() > 0 && _gameManager.GetCanChangeArtist()){
            if(this.transform.childCount == 0){
                this.UpdateArtistList();
            }

            // à faire en fonction du nombre d'artistes

            CommandAttributes newCommand = Instantiate(_commandToGenerate, this.transform);

            newCommand.SetGameManager(_gameManager);
            newCommand.SetPosition(_commandsPositions.GetChild(this.transform.childCount-1).GetComponent<RectTransform>().anchoredPosition);

            // if(this.transform.childCount == 4){return;}
            this.CheckAvailability(newCommand);

            StartCoroutine(this.WaitingTimeBeforeNewCommand(Random.Range(0, _maxWaitingTime)));
        }
    }

    // détruits les commandes
    public void DestroyCommands(){
        if(this.transform.childCount > 0 && _gameManager.GetTime() <= 0){
            for(int i = 0; i < this.transform.childCount; i++){
                Destroy(this.transform.GetChild(i).gameObject);
            }

            _listArtistesInHalls.Clear();
        }
    }
}