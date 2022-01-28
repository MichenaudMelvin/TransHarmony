using UnityEngine;


public class HallsAttributes : MonoBehaviour{

    [SerializeField]
    private GameManager _gameManager;

    [Tooltip("GameObject empty où sont placés tout les artistes créés")]
    [SerializeField]
    private GameObject _artistContainers;

    [Tooltip("GameObject empty où sont placés tout les halls")]
    [SerializeField]
    private GameObject _hallsContainers;

    private void Start(){
        // _hallsContainers.GetComponentInChildren<ArtistsAttributes>()
        this.ChangeArtist();
    }

    private void Update(){
        if(_gameManager.GetTime() <= 0f){
            this.ChangeArtist();
        }
    }

    private void ChangeArtist(){

    }

}
