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

    [Tooltip("Si l'artiste a déjà fait un concert (présent dans un hall)")]
    private bool _alreadyPerform;

    [Tooltip("Si l'artiste a déjà joué sa musique")]
    private bool _hasPlayMusic = false;

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

    // quand l'artiste est allé dans un halls puis reparti
    private void OnDisable(){_hasPlayMusic = false;}

    // public functions
    public string GetName(){return _name;}

    public AudioClip GetMusic(){return _music;}

    public bool GetStatus(){return _alreadyPerform;}

    public void SetStatus(bool boolean){_alreadyPerform = boolean;}

    public bool HasPlayMusic(){return _hasPlayMusic;}

    public void SetHasPlayMusic(bool boolean){_hasPlayMusic = boolean;}
}