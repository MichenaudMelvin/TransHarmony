using UnityEngine;
using UnityEngine.UI;

public class DragNDrop : MonoBehaviour{

    public Halls objectToSnap;
    public Button btn;

    public GameManager gameManager;

    public bool inHall = false;

    private bool moving = false;

    void Start(){
        btn.onClick.AddListener(MoveElement);
    }

    void Update(){
        if(moving && !gameManager.isFinished){
            Vector3 mousePos = Input.mousePosition;

            this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, this.gameObject.transform.position.z);
            // this.ResetMousePosition(mousePos);
        } else if(!moving){
            this.StopElement();
        }
    }

    private void MoveElement(){
        inHall = false;
        moving = moving == true ? false : true;
    }

    private void StopElement(){
        objectToSnap.PiecePlaced(this);
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