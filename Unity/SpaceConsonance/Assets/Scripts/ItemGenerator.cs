using UnityEngine;
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

    private bool hasFinishedCreateItem = false;

    [Space(10)]

    [Header("Others")]
    [SerializeField]
    [Tooltip("GameObject qui génère les commandes")]
    private CommandGenerator _commandGenerator;

    [SerializeField]
    [Tooltip("Référence au GameManager")]
    private GameManager _gameManager;

    private void Start(){
        // génère 2 fois plus d'item que de commande (pour l'instant)
        _maxItemNumber = _commandGenerator.GetMaxCommandNumber()*2;
    }

    private void Update(){
        this.CreateItems();
        this.DestroyItems();
    }

    // créé les items
    private void CreateItems(){
        if(this.transform.childCount < _maxItemNumber && !hasFinishedCreateItem && _gameManager.GetTime() > 0){
            ItemAttributes newItem = Instantiate(_itemToGenerate, this.transform);
            newItem.SetItem(_listNeeds[Random.Range(0, _listNeeds.Count-1)]);
            // newItem.GetComponent<DragNDrop>().SetGamemanager(_gameManager);
        }

        if(this.transform.childCount >= _maxItemNumber){
            hasFinishedCreateItem = true;
        }
    }

    // détruits les items (à la fin de chaque journée)
    private void DestroyItems(){
        if(this.transform.childCount > 0 && _gameManager.GetTime() <= 0){
            for(int i = 0; i < this.transform.childCount; i++){
                Destroy(this.transform.GetChild(i).gameObject);
            }

            hasFinishedCreateItem = false;
        }
    }
}