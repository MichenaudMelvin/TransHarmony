using UnityEngine;


public class HallsAttributes : MonoBehaviour{

    [Tooltip("GameObject empty où sont placés tout les artistes créés")]
    [SerializeField]
    private Transform _artistContainers;

    private ArtistsAttributes _artistInHall;

    private Transform _artistPlacement;

    [SerializeField]
    private GameManager _gameManager;

    private void Start(){
        _artistPlacement = this.transform.Find("ArtistPlacement");

        Transform newArtist = _artistContainers.GetChild(Random.Range(1, _artistContainers.childCount));
        _artistInHall = newArtist.GetComponent<ArtistsAttributes>();
        this.SetupArtist();
    }

    public void ChangeArtist(){
        _artistInHall.gameObject.SetActive(false);
        _artistInHall.transform.SetPositionAndRotation(new Vector3(), new Quaternion());

        if(_gameManager.GetCanChangeArtist()){
            while(_artistInHall.GetStatus()){
                Transform newArtist = _artistContainers.GetChild(Random.Range(1, _artistContainers.childCount));
                _artistInHall = newArtist.GetComponent<ArtistsAttributes>();
            }

            this.SetupArtist();

        } else if(!_gameManager.GetCanChangeArtist()){
            // this.EndFestival();
            print("fin du festival");
        }
    }

    private void SetupArtist(){
        _artistInHall.gameObject.SetActive(true);
        _artistInHall.SetStatus(true);
        _artistInHall.transform.SetPositionAndRotation(_artistPlacement.position, new Quaternion());
    }

}
