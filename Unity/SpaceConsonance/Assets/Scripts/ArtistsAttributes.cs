using UnityEngine;
using System.Collections.Generic;

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

    [SerializeField]
    [Tooltip("Référence au GameManager")]
    private GameManager _gameManager;

    [SerializeField]
    [Tooltip("Référence au Music Manager")]
    private MusicManager _musicManager;

    [SerializeField]
    [Tooltip("Liste de besoins des artistes (items)")]
    private List<string> _listNeeds;

    [Header("Commands")]
    [SerializeField]
    [Tooltip("Parent qui contient toutes les commandes")]
    private Transform _commandsContainer;

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

    // check si l'item drag n dropé est bon
    // possède l'erreur "has been destroyed but you are still trying to access it" // pas trop grave normalement
    private void CheckItem(DragNDrop item){
        for(int i = 0; i < _commandsContainer.childCount; i++){
            if(this == _commandsContainer.GetChild(i).GetComponent<CommandAttributes>().GetArtisteWhoNeedIt()){
                if(item.GetComponent<ItemAttributes>().GetItemName() == _commandsContainer.GetChild(i).GetComponent<CommandAttributes>().GetArtistNeed()){
                    // victoire
                    StartCoroutine(_commandsContainer.GetChild(i).GetComponent<CommandAttributes>().Succeed());
                    _gameManager.UpdatePoints(50);
                    return;
                } else if(item.GetComponent<ItemAttributes>().GetItemName() != _commandsContainer.GetChild(i).GetComponent<CommandAttributes>().GetArtistNeed()){
                    // echec
                    _commandsContainer.GetChild(i).GetComponent<CommandAttributes>().Failure();
                } else{
                    // si l'artiste n'avait aucune commande et que un item a été déposé sur lui
                    _musicManager.UpdateVolume(-0.01f);
                }
            }
        }

        _gameManager.UpdatePoints(-10);
    }

    // public functions
    public string GetName(){return _name;}

    public AudioClip GetMusic(){return _music;}

    public bool GetStatus(){return _alreadyPerform;}

    public List<string> GetListNeeds(){return _listNeeds;}

    public void SetStatus(bool boolean){_alreadyPerform = boolean;}

    // place l'item sur l'artiste et permet le snap
    public void PiecePlaced(DragNDrop piece){
        if(this.gameObject.activeInHierarchy){
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);

            if(Mathf.Abs(piece.transform.position.x - screenPos.x) <= 50f && Mathf.Abs(piece.transform.position.y - screenPos.y) <= 50f && this.transform.eulerAngles.y ==0){
                this.CheckItem(piece);

                Destroy(piece.gameObject);
            }
        }
    }
}