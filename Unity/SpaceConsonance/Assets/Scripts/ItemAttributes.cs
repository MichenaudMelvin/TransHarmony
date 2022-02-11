using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ItemAttributes : MonoBehaviour{

    [Header("Visuel")]
    [Tooltip("Nom de l'item")]
    private string _name;

    [SerializeField]
    [Tooltip("Nom de l'item (visuel)")]
    private Text _textName;

    [SerializeField]
    [Tooltip("Sprite de l'item")]
    private Image _sprite;

    // public functions
    public void SetItem(string itemType){
        // set et affiche le nom de l'item
        _name = itemType;
        _textName.text = _name;
        // set le sprite de l'item
        for(int i = 0; i < this.GetComponentInParent<ItemGenerator>().GetSpriteList().Count; i++){
            if(this.GetComponentInParent<ItemGenerator>().GetSpriteList()[i].name == _name){
                _sprite.sprite = this.GetComponentInParent<ItemGenerator>().GetSpriteList()[i];
            }
        }
    }

    public void SetPosition(Vector2 newPosition){this.GetComponent<RectTransform>().anchoredPosition = newPosition;}

    public string GetItemName(){return _name;}
}