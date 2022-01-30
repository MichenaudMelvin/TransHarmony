using UnityEngine;
using System.Collections.Generic;

public class ArtistsAttributes : MonoBehaviour{

    [Header("Basics")]
    [SerializeField]
    [Tooltip("https://www.lestrans.com/trans-2021/")]
    private string _name;

    [SerializeField]
    [Tooltip("https://www.lestrans.com/trans-2021/")]
    private string _style;

    [SerializeField]
    private string needs;

    [SerializeField]
    private AudioSource _music;

    [SerializeField]
    private GameManager _gameManager;

    private bool _alreadyPerform;

    [Space(10)]

    [Header("Affichage texte")]
    [SerializeField]
    private TextMesh _textName;

    private void Start(){
        this.BillboardText();
    }

    private void BillboardText(){
        _textName.text = _name;
        _textName.transform.rotation = Quaternion.LookRotation(_textName.transform.position - Camera.main.transform.position);
    }

    // public functions
    public bool GetStatus(){return _alreadyPerform;}

    public void SetStatus(bool boolean){_alreadyPerform = boolean;}

    public void PiecePlaced(DragNDrop piece){
        if(Mathf.Abs(piece.transform.position.x - this.transform.position.x) <= 100f && Mathf.Abs(piece.transform.position.y - this.transform.position.y) <= 100f && this.transform.eulerAngles.y ==0){
            piece.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            Destroy(piece);
            // ajouter des points ici toussa toussa
        }
    }
}
