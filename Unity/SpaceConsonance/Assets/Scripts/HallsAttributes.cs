using UnityEngine;
using System.Collections;

public class HallsAttributes : MonoBehaviour{

    [Header("Artists")]
    [SerializeField]
    [Tooltip("Préfab de l'artiste")]
    private ArtistsAttributes _artistsPrefab;

    [SerializeField]
    [Tooltip("Parent qui contient tous les artistes")]
    private Transform _artistContainers;

    [Tooltip("Artiste actuellement dans le hall")]
    private ArtistsAttributes _artistInHall;

    [SerializeField]
    [Tooltip("Pour pas que tous les hall instantie un artiste au meme moment")]
    private float _exePosition;

    [SerializeField]
    [Tooltip("Numero de Hall")]
    private int hallNumber;

    [SerializeField]
    [Tooltip("Position où l'artiste va être placé dans le hall")]
    private Transform _artistPlacement;

    [SerializeField]
    [Tooltip("Référence au GameManager")]
    private GameManager _gameManager;

    [Tooltip("Points générés par le hall")]
    private float _points;

    [Space(10)]

    [Header("Particules")]
    [SerializeField]
    [Tooltip("Si la commande est réussie")]
    private ParticleSystem _paticulesTrue;

    [SerializeField]
    [Tooltip("Si la commande est raté")]
    private ParticleSystem _paticulesFalse;

    [Tooltip("Si il est actif ou non")]
    private bool _canGetArtist = false;

    private void Start(){
        _artistPlacement = this.transform.Find("ArtistPlacement");

        StartCoroutine(this.SpawnArtist());
    }

    private IEnumerator SpawnArtist(){
        yield return new WaitForSeconds(_exePosition);

        _artistInHall = Instantiate(_artistsPrefab, _artistPlacement.position, new Quaternion(), _artistContainers);

        _artistInHall.SetArtistAsset(_gameManager.GetArtistAssetsList()[Random.Range(0, _gameManager.GetArtistAssetsList().Count)]);

        while(_artistInHall.GetStatus()){
            _artistInHall.SetArtistAsset(_gameManager.GetArtistAssetsList()[Random.Range(0, _gameManager.GetArtistAssetsList().Count)]);
        }

        _artistInHall.SetStatus(true);

        this.SetupArtist();
    }

    // permet le changement d'artiste après une journée passé dans le festival
    public void ChangeArtist(){
        _gameManager.UpdatePoints(_points);
        _points = 0;

        _artistInHall.SetHasPlayMusic(false);
        Destroy(_artistInHall.gameObject);

        // à voir pour éviter d'avoir 2 fois de suite le meme artiste dans un hall

        StartCoroutine(this.SpawnArtist());
    }

    // Setup le nouvel artiste qui entre dans le hall
    private void SetupArtist(){
        // pour opti merge ces deux méthodes en une
        _artistInHall.SetCurrentHall(hallNumber);
        _artistInHall.SetHall(this);

        _artistInHall.gameObject.SetActive(true);
        _artistInHall.SetStatus(true);
        _artistInHall.transform.SetPositionAndRotation(_artistPlacement.position, new Quaternion());
    }

    public void Disable(){
        Destroy(_artistInHall.gameObject);
    }

    public float GetPoints(){return _points;}

    public void UpdatePoints(float amount){_points += amount;}

    public ParticleSystem GetGoodParticule(){return _paticulesTrue;}

    public ParticleSystem GetBadParticule(){return _paticulesFalse;}

    public bool GetActivity(){return _canGetArtist;}

    public void SetActivity(bool value){_canGetArtist = value;}

    public void DestroyArtist(){
        if(_artistInHall != null){
            Destroy(_artistInHall.gameObject);
        }
    }
}
