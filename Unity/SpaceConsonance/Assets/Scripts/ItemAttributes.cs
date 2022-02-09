using System;
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
    [Tooltip("Image de l'item")]
    private Image _image;

    // public functions
    public void SetItem(string itemType){
        // set et affiche le nom de l'item
        _name = itemType;
        _textName.text = _name;
    }

    public void SetPosition(Vector2 newPosition){this.GetComponent<RectTransform>().anchoredPosition = newPosition;}

    public string GetItemName(){return _name;}

}