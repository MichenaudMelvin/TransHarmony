using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CommandAttributes : MonoBehaviour{

    [Header("Hall")]
    [Tooltip("Savoir a quel hall appartient la commande")]
    private int currentHall;

    [Header("Artists")]
    [Tooltip("Artiste qui a besoin de la commande")]
    private ArtistsAttributes _artistWhoNeedIt;

    [Tooltip("Besoin de l'artiste")]
    private string _artistNeed;

    [Space(10)]

    [Header("Visual")]
    [SerializeField]
    [Tooltip("Sprite de la commande")]
    private Image _spriteNeed;

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

    [Tooltip("Scale initial")]
    private Vector3 _initialScale;

    [SerializeField]
    [Tooltip("Scale quand annimaiton d'aggrandisement")]
    private Vector3 _upperScale;

    [SerializeField]
    [Tooltip("Sprite de bulle calme")]
    private Sprite _bulleCalmeSprite;

    [SerializeField]
    [Tooltip("Sprite de bulle action")]
    private Sprite _bulleActionSprite;

    [Space(10)]

    [Header("Timer")]
    // [SerializeField]
    // [Tooltip("Timer visuel de la commande")]
    // private Image _circleTimer;

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

    [Tooltip("Référence au Settings")]
    private Settings _settings;

    [Tooltip("Référence au Music Manager")]
    private MusicManager _musicManager;

    [Tooltip("Booléen qui permet de savoir si le joueur passe son curseur/item dessus la commande")]
    private bool _isItemOnIt = false;

    [Tooltip("Référence au CommandGenerator")]
    private CommandGenerator _commandGenerator;

    [SerializeField] public ParticleSystem _paticulesTrue;
    [SerializeField] public ParticleSystem _paticulesFalse;

    [SerializeField]
    [Tooltip("Points gagnés quand la commande est réussie")]
    private float _succesPoints;

    [Space(10)]

    [Header("Sounds")]
    [SerializeField]
    [Tooltip("Audio source de la commande")]
    private AudioSource _audioSource;

    [SerializeField]
    [Tooltip("Son quand valide une commande")]
    private AudioClip _validRequest;

    [SerializeField]
    [Tooltip("Son quand fail une commande")]
    private AudioClip _failRequest;

    private void Start(){
        _initialColor = _image.color;
        _initialScale = this.transform.localScale;
    }

    private void Update(){
        if(_gameManager.GetCurrentPhase() == 2){this.ManageTimer();}
        this.ScaleAnimation();
    }

    private void SetupCommand(List<string> listNeed){
        _availableTimeInitial = _availableTime;

        currentHall = _artistWhoNeedIt.GetHallNumber();

        // génère un besoin aléatoire parmis les besoins de l'artiste
        _artistNeed = listNeed[Random.Range(0, listNeed.Count)];

        // set le sprite de la commande
        for(int i = 0; i < _commandGenerator.GetCommandGenerator().GetSpriteList().Count; i++){
            if(_commandGenerator.GetCommandGenerator().GetSpriteList()[i].name == _artistNeed){
                _spriteNeed.sprite = _commandGenerator.GetCommandGenerator().GetSpriteList()[i];
                _spriteNeed.SetNativeSize();
            }
        }

        if(_gameManager.GetCurrentPhase() == 1){
            _image.sprite = _bulleCalmeSprite;
        } else if(_gameManager.GetCurrentPhase() == 2){
            _image.sprite = _bulleActionSprite;
            _image.SetNativeSize();
            _image.GetComponent<RectTransform>().sizeDelta = new Vector2(_image.GetComponent<RectTransform>().sizeDelta.x/2, _image.GetComponent<RectTransform>().sizeDelta.y/2);
        }

        this.SetPosition();
    }


    // detruit les commande à partir d'un temps donné
    private void ManageTimer(){
        if(!_hasSucced){
            _availableTime -= Time.deltaTime;

            // _circleTimer.fillAmount = Mathf.InverseLerp(0, _availableTimeInitial, _availableTime);

            // _circleTimer.color = _gameManager.GetTimerGradient().Evaluate(_circleTimer.fillAmount);
        }

        if(_availableTime <= 0 && !_hasSucced){
            this.EndTimer();
        }
    }

    private void EndTimer(){
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

        if(!_gameManager.GetIsGamePause()){
            this.GetComponent<Image>().CrossFadeAlpha(1f, _movementDuration, false);
        }

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

    // quand un item entre en collison avec une commande
    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Item"){
            _isItemOnIt = true;
        }
    }

    // quand un item sort de la collison avec la commande
    private void OnTriggerExit2D(Collider2D collider){
        if(collider.tag == "Item"){
            _isItemOnIt = false;
        }
    }

    // animation scale quand la souris passe dessus
    private void ScaleAnimation(){
        if(_isItemOnIt){
            while(this.transform.localScale.x <= _upperScale.x && this.transform.localScale.y <= _upperScale.y){
                this.transform.localScale += new Vector3(0.01f, 0.01f, 0f);
            }
        } else if(!_isItemOnIt){
            while(this.transform.localScale.x >= _initialScale.x && this.transform.localScale.y >= _initialScale.y){
                this.transform.localScale -= new Vector3(0.01f, 0.01f, 0f);
            }
        }
    }

    // public functions
    public ArtistsAttributes GetArtisteWhoNeedIt(){return _artistWhoNeedIt;}

    public string GetArtistNeed(){return _artistNeed;}

    public void SetVariablesCommand(ArtistsAttributes newArtiste, GameManager gameManagerReference, Settings settingsReference, MusicManager musicManagerReference, CommandGenerator commandGenerator, List<string> listNeed){
        _artistWhoNeedIt = newArtiste;
        _gameManager = gameManagerReference;
        _musicManager = musicManagerReference;
        _commandGenerator = commandGenerator;
        _settings = settingsReference;
        this.SetupCommand(listNeed);
    }

    // quand le joueur réussi a bien drag n dropé son item
    public IEnumerator Succeed(){
        StartCoroutine(this.ChangeColor(_succeedColor));
        _hasSucced = true;
        _paticulesTrue.Play();

        if(_settings.GetIsSoundEnable()){
            _audioSource.clip = _validRequest;
            _audioSource.Play();
        }

        yield return new WaitForSeconds(0.5f);

        if(_gameManager.GetCurrentPhase() == 1){
            _gameManager.conditionsLeftBeforeHallComplete[currentHall] -= 1;
            if(_gameManager.conditionsLeftBeforeHallComplete[currentHall] <= 0){
                _commandGenerator.SetHallsLeft(-1);
                _commandGenerator.SetMaxCommandAtATime(-1);
            }
        } else if(_gameManager.GetCurrentPhase() == 2){
            _artistWhoNeedIt.GetHall().UpdatePoints(_succesPoints);
        }

        Destroy(this.gameObject);
    }

    // quand le joueur rate
    public void Failure(){
        StartCoroutine(this.ChangeColor(_failureColor));

        if(_settings.GetIsSoundEnable()){
            _audioSource.clip = _failRequest;
            _audioSource.Play();
        }

        _finalPosition = this.transform.position;
        _paticulesFalse.Play();
        StartCoroutine(this.FailureMovement());
    }
}