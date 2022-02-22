using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemGenerator : MonoBehaviour{

    [Header("Items")]
    [SerializeField]
    [Tooltip("Prefab de l'item")]
    private ItemAttributes _itemToGenerate;

    [SerializeField]
    [Range(1, 10)]
    [Tooltip("Nombre d'items maximal")]
    private int _maxItemNumber;

    [SerializeField]
    [Tooltip("Liste de besoins des artistes (items)")]
    private List<string> _listNeeds;

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

    [Space(10)]
    [SerializeField]
    [Tooltip("Liste de besoins des besoins pour les events")]
    private List<string> _listEventItems;

    private void Start(){
        for(int i = 0; i < _itemsPositions.childCount; i++){
            _itemsPositions.GetChild(i).gameObject.SetActive(false);
        }
    }

    // public functions
    // créé les items
    public IEnumerator CreateItems(){
        int index = 0;
        while(this.transform.childCount < _maxItemNumber){
            if(_listNeeds.Count > 0){
                ItemAttributes newItem = Instantiate(_itemToGenerate, this.transform);
                newItem.SetItem(_listNeeds[index]);
                index += 1;

                newItem.SetPosition(_itemsPositions.GetChild(this.transform.childCount-1).GetComponent<RectTransform>().anchoredPosition);
            }

            yield return null;
        }
    }


    // détruits les items (à la fin de chaque journée)
    public void DestroyItems(){
        // détruits tous les items encore restants
        if(this.transform.childCount > 0){
            for(int i = 0; i < this.transform.childCount; i++){
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }
    }

    public List<Sprite> GetSpriteList(){return _spriteList;}

    public List<string> GetListNeeds(){return _listNeeds;}
    public List<string> GetListEventsItems(){return _listEventItems;}
}