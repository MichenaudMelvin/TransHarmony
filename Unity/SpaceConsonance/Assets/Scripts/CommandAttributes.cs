using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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

    [Header("Feedback")]
    [SerializeField]
    [Tooltip("Image de la commande (est utilisé quand le joueur réussi ou rate)")]
    private Image _image;

    [Tooltip("Couleur initiale de cette image")]
    private Color _initialColor;

    [SerializeField]
    [Tooltip("Couleur quand le joueur réussi")]
    private Color _succeedColor;

    [SerializeField]
    [Tooltip("Couleur quand le joueur rate")]
    private Color _failureColor;

    [Tooltip("Booléan pour pas que la fin de timer soit faite en meme temps qu'une réussite")]
    private bool _hasSucced = false;

    [Space(10)]

    [Header("Appearance")]
    [SerializeField]
    [Range(0.5f, 2.5f)]
    [Tooltip("Temps du movement d'apparition")]
    private float _movementDuration;

    [SerializeField]
    [Tooltip("Position finale (après un lerp)")]
    private Vector3 _finalPosition;

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

    [Tooltip("Référence au Music Manager")]
    private MusicManager _musicManager;

    private void Start(){
        _initialColor = _image.color;
    }

    private void Update(){
        this.ManageTimer();
    }

    private void SetupCommand(List<string> listNeed){
        _availableTimeInitial = _availableTime;

        _artistName.text = _artistWhoNeedIt.GetName();

        // génère un besoin aléatoire parmis les besoins de l'artiste
        _artistNeed = listNeed[Random.Range(0, listNeed.Count)];
        _visualNeed.text = _artistNeed;

        this.SetPosition();
    }


    // detruit les commande à partir d'un temps donné
    private void ManageTimer(){
        if(!_hasSucced){
            _availableTime -= Time.deltaTime;

            _circleTimer.fillAmount = Mathf.InverseLerp(0, _availableTimeInitial, _availableTime);

            _circleTimer.color = _gameManager.GetTimerGradient().Evaluate(_circleTimer.fillAmount);
        }

        if(_availableTime <= 0 && !_hasSucced){
            this.trucTimer();
        }
    }

    private void trucTimer(){
        _gameManager.UpdatePoints(-50);
        _musicManager.UpdateVolume(-0.05f);
        // faire une animation de fin de timer
        Destroy(this.gameObject);
    }

    // gère la position de la commande
    private void SetPosition(){
        Vector2 artistScreenPos = Camera.main.WorldToScreenPoint(_artistWhoNeedIt.transform.position);
        this.transform.position = artistScreenPos;

        this.Appearance();
    }

    // fait apparaitre la commande en fade in
    private void Appearance(){
        this.GetComponent<Image>().canvasRenderer.SetAlpha(0f);
        this.GetComponent<Image>().CrossFadeAlpha(1f, _movementDuration, false);
        StartCoroutine(this.UpMovement());
    }

    // fait monter la commande quand elle apparait
    // pas parfait car différent en fonction de la taille de l'écran
    // à voir avec les anchoredPosition car plus modulable
    private IEnumerator UpMovement(){
        float time = 0f;
        float normalizedValue;

        Vector3 startPosition = this.transform.position;
        // this._finalPosition.y += Screen.height/10;
        this._finalPosition += startPosition;

        while(time <= _movementDuration){
            time += Time.deltaTime; 
            normalizedValue = time / _movementDuration;

            this.transform.position = Vector3.Lerp(startPosition, _finalPosition, normalizedValue);
            yield return null;
        }
    }

    // shake la commande quand le joueur fait une erreurs
    private IEnumerator FailureMovement(){
        float time = 0f;
        float duration= 0.025f;
        float normalizedValue;

        Vector3 midPosition = this.transform.position;
        Vector3 leftPositon = midPosition + new Vector3(5, 0, 0);
        Vector3 rightPosition = midPosition + new Vector3(-5, 0, 0);

        Vector3 startPosition = this.transform.position;

        while(time <= duration){
            time += Time.deltaTime; 
            normalizedValue = time / duration;

            this.transform.position = Vector3.Lerp(startPosition, leftPositon, normalizedValue);
            yield return new WaitForSeconds(0.01f);

            this.transform.position = Vector3.Lerp(startPosition, midPosition, normalizedValue);
            yield return new WaitForSeconds(0.01f);

            this.transform.position = Vector3.Lerp(startPosition, rightPosition, normalizedValue);
            yield return new WaitForSeconds(0.01f);

            this.transform.position = Vector3.Lerp(startPosition, midPosition, normalizedValue);
        }

    }

    // change la couleur puis revient à sa couleur initiale
    private IEnumerator ChangeColor(Color newColor){
        _image.color = newColor;
        yield return new WaitForSeconds(0.5f);
        _image.color = _initialColor;
    }

    // public functions
    public ArtistsAttributes GetArtisteWhoNeedIt(){return _artistWhoNeedIt;}

    public string GetArtistNeed(){return _artistNeed;}

    public void SetVariablesCommand(ArtistsAttributes newArtiste, GameManager gameManagerReference, MusicManager musicManagerReference, List<string> listNeed){
        _artistWhoNeedIt = newArtiste;
        _gameManager = gameManagerReference;
        _musicManager = musicManagerReference;
        this.SetupCommand(listNeed);
    }

    // quand le joueur réussi a bien drag n dropé son item
    public IEnumerator Succeed(){
        StartCoroutine(this.ChangeColor(_succeedColor));
        _musicManager.UpdateVolume(0.05f);
        _hasSucced = true;
        // ajouter un son
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    // quand le joueur rate
    public void Failure(){
        StartCoroutine(this.ChangeColor(_failureColor));
        _musicManager.UpdateVolume(-0.05f);
        // ajouter un son
        _finalPosition = this.transform.position;
        StartCoroutine(this.FailureMovement());

    }
}