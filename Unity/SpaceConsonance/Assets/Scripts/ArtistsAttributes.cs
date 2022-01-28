using UnityEngine;

public class ArtistsAttributes : MonoBehaviour{

    [SerializeField]
    private string _name;

    [SerializeField]
    private AudioSource _music;

    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private string needs;

    private bool _alreadyPerform;

    [Space(10)]
    [Header("Affichage texte")]
    [SerializeField]
    private TextMesh _textName;

    private void Start(){
        this.BillboardText();
    }

    private void Update(){
        
    }

    private void BillboardText(){
        _textName.text = _name;
        _textName.transform.rotation = Quaternion.LookRotation(_textName.transform.position - Camera.main.transform.position);
    }

}
