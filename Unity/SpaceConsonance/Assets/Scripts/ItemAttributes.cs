using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ItemAttributes : MonoBehaviour{

    [Header("Visuel")]
    [Tooltip("Nom de l'item")]
    private string _name;

    [SerializeField]
    [Tooltip("Sprite de l'item")]
    private Image _sprite;

    [Tooltip("Position initiale de l'item")]
    private Vector3 _initialPosition;

    // public functions
    public void SetItem(string itemType){
        // set et affiche le nom de l'item
        _name = itemType;
        // set le sprite de l'item
        for(int i = 0; i < this.GetComponentInParent<ItemGenerator>().GetSpriteList().Count; i++){
            if(this.GetComponentInParent<ItemGenerator>().GetSpriteList()[i].name == _name){
                _sprite.sprite = this.GetComponentInParent<ItemGenerator>().GetSpriteList()[i];
                if(_name == "Speaker"){_sprite.SetNativeSize();}
                else{_sprite.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);}
            }
        }
    }

    // set de la position, va être considéré comme la position initiale
    public void SetPosition(Vector2 newAnchorMin, Vector2 newAnchorMax, Vector2 newPivot, Vector2 newPosition){
        this.GetComponent<RectTransform>().anchorMin = newAnchorMin;
        this.GetComponent<RectTransform>().anchorMax = newAnchorMax;
        this.GetComponent<RectTransform>().pivot = newPivot;
        this.GetComponent<RectTransform>().anchoredPosition = newPosition;
        _initialPosition = this.transform.position;
    }

    public string GetItemName(){return _name;}

    public void RestartPosition(){this.transform.position = _initialPosition;}
}