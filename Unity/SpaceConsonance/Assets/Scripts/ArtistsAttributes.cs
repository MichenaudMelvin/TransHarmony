using UnityEngine;

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

}
