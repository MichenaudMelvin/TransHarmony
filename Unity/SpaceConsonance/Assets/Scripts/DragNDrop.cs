using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform), typeof(Button), typeof(EventTrigger))]
public class DragNDrop : MonoBehaviour{

    [Header("Items")]
    [Tooltip("Commande avec laquelle va collide l'item drag n dropé")]
    private CommandAttributes _collideCommand;

    [Tooltip("Si l'item est en collision avec un commande")]
    private bool _onCommand = false;

    [Header("Others")]
    [Tooltip("Si l'élément est en train de bouger ou non")]
    private bool _moving = false;

    [Tooltip("Position initial de l'élément")]
    private Vector3 _initialPos;

    private void Start(){
        _initialPos = this.transform.position;
    }

    private void Update(){
        if(_moving){
            Vector3 mousePos = Input.mousePosition;

            this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, this.gameObject.transform.position.z);

            this.ResetMousePosition(mousePos);
        }

    }

    // pour pas que l'élément se retrouve en dehors de l'écran, marche pas vraiment // pas une priorité
    private void ResetMousePosition(Vector3 mousePositon){
        if(mousePositon.x <= 0 || mousePositon.y <= 0 || mousePositon.x >= Screen.width - 1 || mousePositon.y >= Screen.height - 1){
            this.transform.position = _initialPos;
            _moving = false;
        }
    }

    // quand l'item drag n dropé rentre en collision avec une commande
    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Command"){
            _onCommand = true;
            _collideCommand = collider.GetComponent<CommandAttributes>();
        }
    }

    // quand l'item drag n dropé sort de la collision avec une commande
    private void OnTriggerExit2D(Collider2D collider){
        if(collider.tag == "Command"){
            _onCommand = false;
            _collideCommand = null;
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

        if(_onCommand){
            if(this.GetComponent<ItemAttributes>().GetItemName() == _collideCommand.GetComponent<CommandAttributes>().GetArtistNeed()){
                StartCoroutine(_collideCommand.GetComponent<CommandAttributes>().Succeed());
            }
            else{_collideCommand.GetComponent<CommandAttributes>().Failure();}
        }

        this.GetComponent<ItemAttributes>().RestartPosition();
    }

    public bool GetIsMoving(){return _moving;}
}