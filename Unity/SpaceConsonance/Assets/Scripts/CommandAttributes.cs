using UnityEngine;
using UnityEngine.UI;

public class CommandAttributes : MonoBehaviour{

    [Header("Artists")]
    [Tooltip("Artiste qui a besoin de la commande")]
    private ArtistsAttributes _artistWhoNeedIt;

    [Tooltip("Besoin de l'artiste")]
    private string _artistNeed;

    [Space(10)]

    [Header("Visual")]
    [SerializeField]
    [Tooltip("Texte qui permet de connnaitre le besoin de l'artiste")]
    private Text _visualNeed;

    [SerializeField]
    [Tooltip("Texte qui permet de connaitre quel artiste à besoin de cette commande")]
    private Text _artistName;

    [Space(10)]

    [Header("Timer")]
    [SerializeField]
    [Tooltip("Timer visuel de la commande")]
    private Image _circleTimer;

    [SerializeField]
    [Range(10f, 25f)]
    [Tooltip("Temps restant avant que la commande disparaisse (lifetime)")]
    private float _availableTime;

    [Tooltip("Lifetime initial")]
    private float _availableTimeInitial;

    [Space(10)]

    [Header("Others")]
    [Tooltip("Référence au GameManager")]
    private GameManager _gameManager;

    [Tooltip("La position où spawn la commande (Position en UI)")]
    private Vector2 _positionToSpawn;

    private void Update(){
        this.ManageTimer();
    }

    private void SetupCommand(){
        _availableTimeInitial = _availableTime;

        _artistName.text = _artistWhoNeedIt.GetName();

        // génère un besoin aléatoire parmis les besoins de l'artiste
        _artistNeed = _artistWhoNeedIt.GetListNeeds()[Random.Range(0, _artistWhoNeedIt.GetListNeeds().Count-1)];
        _visualNeed.text = _artistNeed;
    }


    // detruit les commande à partir d'un temps donné
    private void ManageTimer(){
        _availableTime -= Time.deltaTime;

        _circleTimer.fillAmount = Mathf.InverseLerp(0, _availableTimeInitial, _availableTime);

        _circleTimer.color = _gameManager.GetTimerGradient().Evaluate(_circleTimer.fillAmount);

        if(_availableTime <= 0){
            _gameManager.UpdatePoints(-50);
            Destroy(this.gameObject);
        }
    }

    // public functions
    public ArtistsAttributes GetArtisteWhoNeedIt(){return _artistWhoNeedIt;}

    public string GetArtistNeed(){return _artistNeed;}

    public void SetArtisteWhoNeedIt(ArtistsAttributes newArtiste){
        _artistWhoNeedIt = newArtiste;
        this.SetupCommand();
    }

    // gère la position des commandes
    public void SetPosition(Vector2 newPosition){
        // set les anchors en haut à gauche du canvas
        this.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        this.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);

        this.GetComponent<RectTransform>().anchoredPosition = newPosition;
    }

    public void SetGameManager(GameManager gameManagerReference){_gameManager = gameManagerReference;}


}