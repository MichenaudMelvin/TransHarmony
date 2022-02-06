using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Button))]
public class DragNDrop : MonoBehaviour{

    [SerializeField]
    [Tooltip("Element auquel s'accrocher")]
    private ArtistsAttributes _objectToSnap;

    [Tooltip("Référence au GameManager")]
    private GameManager _gameManager;

    [SerializeField]
    [Tooltip("Bouton qu'il y a sur l'élément à drag n drop (self)")]
    private Button _btn;

    [Tooltip("Si l'élément est en train de bouger ou non")]
    private bool _moving = false;

    private void Start(){
        _btn.onClick.AddListener(MoveElement);

        // juste pour les différencier
        // this.GetComponent<RawImage>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
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
        print("ya");
        _moving = _moving == true ? false : true;
    }

    private void StopElement(){
        _objectToSnap.PiecePlaced(this);
    }

    // public functions
    // donne la référence GameManager quand l'élément est créé
    public void SetGamemanager(GameManager gameManagerVariable){_gameManager = gameManagerVariable;}

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