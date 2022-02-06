using UnityEngine;

public class ArtistsAttributes : MonoBehaviour{

    [Header("Basics")]
    [SerializeField]
    [Tooltip("Nom de l'artiste")]
    private string _name; // https://www.lestrans.com/trans-2021/

    [SerializeField]
    [Tooltip("Style de musique de l'artiste")]
    private string _style; // https://www.lestrans.com/trans-2021/

    [SerializeField]
    [Tooltip("Musique que l'artiste joue")]
    private AudioClip _music;

    [Tooltip("Si l'artiste a déjà fait un concert")]
    private bool _alreadyPerform;

    [Space(10)]

    [Header("Affichage texte")]
    [SerializeField]
    [Tooltip("Affichage de son nom au dessus de son modèle")]
    private TextMesh _textName;

    private void Start(){
        this.BillboardText();
    }

    // permet au texte d'être toujours face à la camera
    // probablement à changer
    private void BillboardText(){
        _textName.text = _name;
        _textName.transform.rotation = Quaternion.LookRotation(_textName.transform.position - Camera.main.transform.position);
    }

    // public functions
    public string Getname(){return _name;}

    public AudioClip GetMusic(){return _music;}

    public bool GetStatus(){return _alreadyPerform;}

    public void SetStatus(bool boolean){_alreadyPerform = boolean;}

    // place l'item sur l'artiste et permet le snap
    public void PiecePlaced(DragNDrop piece){
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        print(piece.transform.position);

        if(Mathf.Abs(piece.transform.position.x - screenPos.x) <= 100f && Mathf.Abs(piece.transform.position.y - screenPos.y) <= 100f && this.transform.eulerAngles.y ==0){
            piece.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
            Destroy(piece);
            // ajouter des points ici toussa toussa
        }
    }
}
