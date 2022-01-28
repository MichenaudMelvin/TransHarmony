using UnityEngine;
using UnityEngine.UI;

public class DragNDrop : MonoBehaviour{

    [SerializeField]
    private ArtistsAttributes _objectToSnap;

    [SerializeField]
    private Button _btn;

    [SerializeField]
    private GameManager _gameManager;

    // private bool _isPlaced = false;

    private bool _moving = false;

    private void Start(){
        _btn.onClick.AddListener(MoveElement);
    }

    private void Update(){
        if(_moving){
            Vector3 mousePos = Input.mousePosition;

            this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, this.gameObject.transform.position.z);
        } else if(!_moving){
            this.StopElement();
        }
    }

    private void MoveElement(){
        // _isPlaced = false;
        _moving = _moving == true ? false : true;
    }

    private void StopElement(){
        // _objectToSnap.PiecePlaced(this);
    }

    // pour pas que l'élément se retrouve en dehors de l'écran, marche pas vraiment // pas une priorité
    // private void ResetMousePosition(Vector3 mousePositon){
    //     // Debug.Log(mousePositon);
    //     if(mousePositon.x <= 0){
    //         mousePositon.x = 0;
    //         print(mousePositon.x);
    //         // if mouse position is left
    //     } if(mousePositon.y <= 0){
    //         mousePositon.y = 0;
    //         // if mouse position is down
    //     } if(mousePositon.x >= Screen.width - 1){
    //         mousePositon.x = Screen.width;
    //         // if mouse position is right
    //     } if(mousePositon.y >= Screen.height - 1){
    //         mousePositon.y = Screen.height;
    //         // if mouse position is up
    //     }
    // }
}