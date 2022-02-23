using UnityEngine;

public class HallsAttributes : MonoBehaviour{

    [SerializeField]
    [Tooltip("Parent qui contient tous les artistes")]
    private Transform _artistContainers;

    [Tooltip("Artiste actuellement dans le hall")]
    private ArtistsAttributes _artistInHall;
    
    [SerializeField]
    [Tooltip("Numero de Hall")]
    private int hallNumber;

    [Tooltip("Position où l'artiste va être placé dans le hall")]
    private Transform _artistPlacement;

    [SerializeField]
    [Tooltip("Référence au GameManager")]
    private GameManager _gameManager;

    private void Start(){
        _artistPlacement = this.transform.Find("ArtistPlacement");

        Transform newArtist = _artistContainers.GetChild(Random.Range(1, _artistContainers.childCount));
        _artistInHall = newArtist.GetComponent<ArtistsAttributes>();
        this.SetupArtist();
    }

    // permet le changement d'artiste après une journée passé dans le festival
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

    // Setup le nouvel artiste qui entre dans le hall
    private void SetupArtist(){
        _artistInHall.currentHall = hallNumber;
        _artistInHall.gameObject.SetActive(true);
        _artistInHall.SetStatus(true);
        _artistInHall.transform.SetPositionAndRotation(_artistPlacement.position, new Quaternion());
    }

}
