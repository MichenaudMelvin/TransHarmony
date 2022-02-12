using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform), typeof(Button), typeof(EventTrigger))]
public class DragNDrop : MonoBehaviour{

    [Header("Artists")]
    [Tooltip("Parent qui contient tous les artistes")]
    private Transform _artistContainers;

    [Tooltip("Liste d'elements auquels s'accrocher")]
    private List<ArtistsAttributes> _objectsToSnap = new List<ArtistsAttributes> {};

    [Header("Others")]
    [Tooltip("Référence au GameManager")]
    private GameManager _gameManager;

    [Tooltip("Si l'élément est en train de bouger ou non")]
    private bool _moving = false;

    [Tooltip("Position initial de l'élément")]
    private Vector3 _initialPos;

    private void Start(){
        this.UpdateArtistList();
        _initialPos = this.transform.position;
    }

    private void Update(){
        if(_moving){
            Vector3 mousePos = Input.mousePosition;

            this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, this.gameObject.transform.position.z);

            this.ResetMousePosition(mousePos);
        }

    }

    // met à jour la liste d'objet avec lequel l'item peux snap
    // se fait à chaque instantiation d'items
    // pas besoin de remettre la lise à zero car supprimé après chaque jour
    private void UpdateArtistList(){
        for(int i = 0; i < _artistContainers.childCount; i++){
            if(_artistContainers.GetChild(i).gameObject.activeInHierarchy){
                _objectsToSnap.Add(_artistContainers.GetChild(i).GetComponent<ArtistsAttributes>());
            }
        }
    }

    // pour pas que l'élément se retrouve en dehors de l'écran, marche pas vraiment // pas une priorité
    private void ResetMousePosition(Vector3 mousePositon){
        if(mousePositon.x <= 0 || mousePositon.y <= 0 || mousePositon.x >= Screen.width - 1 || mousePositon.y >= Screen.height - 1){
            this.transform.position = _initialPos;
            _moving = false;
        }
    }

    // public functions
    public void MoveElement(){
        // pour pas que le joueur puisse déplacer plusieurs éléments en meme temps
        for(int i = 0; i < this.transform.parent.childCount; i++){
            if(this.transform.parent.GetChild(i).GetComponent<DragNDrop>().GetIsMoving() && this.transform.parent.GetChild(i).GetComponent<DragNDrop>() != this){
                return;
            }
        }

        _moving = true;
    }

    // arrete l'élément et check si l'item peux se snap
    public void StopElement(){
        _moving = false;

        for(int i = 0; i < _objectsToSnap.Count; i++){
            _objectsToSnap[i].PiecePlaced(this);
        }
    }

    // donne la référence GameManager quand l'élément est créé
    public void SetupVariables(GameManager gameManagerVariable, Transform artistContainersVariable){
        _gameManager = gameManagerVariable;
        _artistContainers = artistContainersVariable;
    }

    public bool GetIsMoving(){return _moving;}
}