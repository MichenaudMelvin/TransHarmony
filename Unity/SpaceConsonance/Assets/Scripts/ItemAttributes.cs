using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ItemAttributes : MonoBehaviour{

    [Header("Visuel")]
    [SerializeField]
    [Tooltip("Nom de l'item")]
    private string _name;

    [Tooltip("Position initiale de l'item")]
    private Vector3 _initialPosition;

    private void Start(){
        this.SetPosition();
    }

    // set de la position, va être considéré comme la position initiale
    public void SetPosition(){
        _initialPosition = this.transform.position;
    }

    public string GetItemName(){return _name;}

    public void RestartPosition(){this.transform.position = _initialPosition;}
}