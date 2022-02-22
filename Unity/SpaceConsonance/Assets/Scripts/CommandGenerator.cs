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
    [Tooltip("Référence au Music Manager")]
    private MusicManager _musicManager;

    [SerializeField]
    [Tooltip("GameObject qui contient le générateur d'items")]
    private ItemGenerator _itemGenerator;

    [SerializeField]
    [Range(1, 20)]
    [Tooltip("Nombre de commande maximales au total")]
    private int _maxCommandNumber;

    [SerializeField]
    [Range(1, 4)]
    [Tooltip("Nombre de commande maximales à l'écran")]
    private int _maxCommandNumberAtTime;

    [Tooltip("Nombre de commande générés au total pendant une journée")]
    private int _commandGeneratedNbr = 0;

    [SerializeField]
    [Range(0.1f, 5f)]
    [Tooltip("Temps d'attente entre deux commandes")]
    private float _maxWaitingTime;

    [Header("Artists")]
    [SerializeField]
    [Tooltip("Parent qui contient tous les artistes")]
    private Transform _artistContainers;

    [Tooltip("Liste contenant les artistes présents dans les halls")]
    private List<ArtistsAttributes> _listArtistesInHalls = new List<ArtistsAttributes>{};

    [Tooltip("Nombre d'artistes par jours")]
    private int _nbrOfArtistePerDays = 4; // à set manuellement, pas trouvé d'autres moyens

    [Tooltip("Nombre d'artistes restant a répondre aux commandes")]
    private int artistsLeft;

    [Tooltip("Commande demandé")]
    private List<string> _commandForHalls;

    private void Start(){
        if(_maxCommandNumberAtTime > _artistContainers.childCount){
            _maxCommandNumberAtTime = _artistContainers.childCount;
        }
        artistsLeft=_nbrOfArtistePerDays;
    }

    // ajoute à la liste les artistes présent dans les halls
    // micro delay sinon marche pas
    private IEnumerator UpdateArtistList(float time){
        if(_gameManager.GetCurrentPhase() == 1)
        {
            yield return new WaitForSeconds(time);

            _listArtistesInHalls = new List<ArtistsAttributes>{};

            for(int i = 0; i < _artistContainers.childCount; i++){
                if(_artistContainers.GetChild(i).gameObject.activeInHierarchy){
                    _listArtistesInHalls.Add(_artistContainers.GetChild(i).GetComponent<ArtistsAttributes>());
                }
            }  
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
                //check if artist commands are over
            if(_gameManager.conditionsLeftBeforeHallComplete[_listArtistesInHalls[artistIndex].GetComponent<ArtistsAttributes>().GetHallNumber()] <= 0 && _gameManager.conditionsLeftBeforeHallComplete[_listArtistesInHalls[artistIndex].GetComponent<ArtistsAttributes>().GetHallNumber()] <= 0 || _gameManager.conditionsLeftBeforeHallComplete[_listArtistesInHalls[artistIndex].GetComponent<ArtistsAttributes>().GetHallNumber()] >= 100)
            {
                this.CheckArtistAvailability(command);
                return;
            }
        }

        command.SetVariablesCommand(_listArtistesInHalls[artistIndex].GetComponent<ArtistsAttributes>(), _gameManager, _musicManager, this, _itemGenerator.GetListNeeds());
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
        float delay = Random.Range(0, _maxWaitingTime);
        if(_commandGeneratedNbr < _maxCommandNumber && this.transform.childCount < _maxCommandNumberAtTime && _gameManager.GetTime() > 0 && _gameManager.GetCanChangeArtist()){
            if(_commandGeneratedNbr == 0){
                StartCoroutine(this.UpdateArtistList(0.0001f));
            }

            if(_listArtistesInHalls.Count == _nbrOfArtistePerDays){

                // à faire en fonction du nombre d'artistes

                CommandAttributes newCommand = Instantiate(_commandToGenerate, this.transform);

                this.CheckArtistAvailability(newCommand);
                _commandGeneratedNbr += 1;

            } else{
                delay = 0f;
            }
        }

        StartCoroutine(this.WaitingTimeBeforeNewCommand(delay));
    }

    // détruits les commandes
    public void DestroyCommands(){
        if(this.transform.childCount > 0){
            for(int i = 0; i < this.transform.childCount; i++){
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }

        _listArtistesInHalls.Clear();
        _commandGeneratedNbr = 0;
    }

    public void SetArtistLeft(int newValue){artistsLeft += newValue;}
    public void SetMaxCommandAtATime(int newValue){_maxCommandNumberAtTime += newValue;}
    public void SetArtistsLeft(int newValue){artistsLeft += newValue;}
}