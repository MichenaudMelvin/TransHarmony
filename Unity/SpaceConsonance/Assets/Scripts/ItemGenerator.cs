using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemGenerator : MonoBehaviour{

    [Header("Items")]
    [SerializeField]
    [Tooltip("Prefab de l'item")]
    private ItemAttributes _itemToGenerate;

    [Tooltip("Nombre d'items maximal")]
    private int _maxItemNumber;

    [SerializeField]
    [Tooltip("Liste de besoins des artistes (items)")]
    private List<string> _listNeeds;

    [Tooltip("Booléen pour savoir si le nombre d'items demmandé à été créé")]
    private bool _hasFinishedCreateItems = false;

    [SerializeField]
    [Tooltip("Parent qui contient tous les positions des items")]
    private Transform _itemsPositions;

    [SerializeField]
    [Tooltip("Liste contenant les sprites des items")]
    private List<Sprite> _spriteList;

    [Space(10)]

    [Header("Others")]
    [SerializeField]
    [Tooltip("GameObject qui génère les commandes")]
    private CommandGenerator _commandGenerator;

    [SerializeField]
    [Tooltip("Référence au GameManager")]
    private GameManager _gameManager;

    [SerializeField]
    [Tooltip("Parent qui contient tous les artistes")]
    private Transform _artistContainers;

    private void Start(){
        // génère 1.5 fois plus d'items que de commande
        _maxItemNumber = (int)(_commandGenerator.GetMaxCommandNumber()*1.5);

        for(int i = 0; i < _itemsPositions.childCount; i++){
            _itemsPositions.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Update(){
        this.CreateItems();
        this.DestroyItems();
    }

    // créé les items
    // ptet à mettre en public comme pour CommandGenerator pour éviter une Update();
    private void CreateItems(){
        if(this.transform.childCount < _maxItemNumber && !_hasFinishedCreateItems && _gameManager.GetTime() > 0){
            if(_listNeeds.Count > 0){
                ItemAttributes newItem = Instantiate(_itemToGenerate, this.transform);
                newItem.SetItem(_listNeeds[Random.Range(0, _listNeeds.Count)]);
                newItem.GetComponent<DragNDrop>().SetupVariables(_gameManager, _artistContainers);
                newItem.SetPosition(_itemsPositions.GetChild(this.transform.childCount-1).GetComponent<RectTransform>().anchoredPosition);
            }
        }

        if(this.transform.childCount >= _maxItemNumber){
            _hasFinishedCreateItems = true;
        }
    }

    // détruits les items (à la fin de chaque journée)
    // ptet à mettre en public comme pour CommandGenerator pour éviter une Update();
    private void DestroyItems(){
        // détruits tous les items encore restants
        if(this.transform.childCount > 0 && _gameManager.GetTime() <= 0){
            for(int i = 0; i < this.transform.childCount; i++){
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }

        // reset pour recréer des items
        if(_gameManager.GetTime() <= 0){
            _hasFinishedCreateItems = false;
            _listNeeds.Clear();
        }
    }

    // public functions
    public List<Sprite> GetSpriteList(){return _spriteList;}

    public List<string> GetListNeeds(){return _listNeeds;}
}