using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ItemAttributes : MonoBehaviour{

    private string _name;

    private Text _textName;

    private Image _image;

    private void Start(){
        _textName.text = _name;
    }

}