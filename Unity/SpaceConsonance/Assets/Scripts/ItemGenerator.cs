using UnityEngine;
using UnityEngine.UI;

public class ItemGenerator : MonoBehaviour{

    [Header("Items")]
    [SerializeField]
    private ItemAttributes _itemToGenerate;

    private int _maxItemNumber;

    [Space(10)]

    [Header("Others")]
    [SerializeField]
    private CommandGenerator _commandGenerator;

    [SerializeField]
    private GameManager _gameManager;

    private void Start(){
        _maxItemNumber = _commandGenerator.GetMaxCommandNumber()*2;
    }

    private void Update(){
        this.CreateItems();
        this.DestroyItems();
    }

    private void CreateItems(){
        if(this.transform.childCount < _maxItemNumber && _gameManager.GetTime() > 0){
            ItemAttributes newItem = Instantiate(_itemToGenerate, this.transform);
            newItem.GetComponent<DragNDrop>().SetGamemanager(_gameManager);

        }
    }

    private void DestroyItems(){
        if(this.transform.childCount > 0 && _gameManager.GetTime() <= 0){
            for(int i = 0; i < this.transform.childCount; i++){
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }
    }
}