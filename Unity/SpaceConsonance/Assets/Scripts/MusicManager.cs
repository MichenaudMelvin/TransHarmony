using System.Collections.Specialized;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class MusicManager : MonoBehaviour{

    [Header("Audio")]
    [SerializeField]
    [Tooltip("Source audio (musique du festival)")]
    private AudioSource _audioSource;

    [Tooltip("Volume de la musique joué")]
    private float _volume = -0.0001f;

    [Space(10)]

    [Header("Display music")]
    [SerializeField]
    [Tooltip("Nom de la musique")]
    private TextMeshProUGUI _musicName;

    [SerializeField]
    [Tooltip("Nom de l'artiste")]
    private Text _artistName;

    [Tooltip("Vitesse à laquel va défiler le nom de la musique si le texte est trop grand")]
    private float _musicNameScrollSpeed = 5;

    private TextMeshProUGUI _cloneTextObject;

    [SerializeField]
    [Tooltip("Rect transform du nom de la music")]
    private RectTransform _musicNameRectTransform;

    private string _sourceText;
    private string _tempText;

    [Space(10)]

    [Header("Others")]
    [SerializeField]
    [Tooltip("Référence au GameManager")]
    private GameManager _gameManager;

    [SerializeField]
    [Tooltip("Parent qui contient tous les artistes")]
    private Transform _artistContainers;

    [Tooltip("Artiste qui joue la musique entendu par le joueur")]
    private ArtistsAttributes _actualArtist;

    [SerializeField]
    [Tooltip("Parent qui contient tous les halls")]
    private Transform _hallsContainer;

    private void Start(){
        // pour laisser le temps à Unity d'instancier les artistes // sinon marche pas
        StartCoroutine(this.LateStart(0.001f));
    }

    private IEnumerator LateStart(float time){
        yield return new WaitForSeconds(time);

        this.SetAudioClip();
    }

    private void Update(){
        this.ManageVolume();
    }

    // gère le volume de la musique en fonciton des actions du joueur
    private void ManageVolume(){
        if(_gameManager.GetTime() > 0){
            // ptet pas la meilleur méthode
            // marche pas avec un Lerp car impossible d'augmenter ou diminuer du volume
            _audioSource.volume += _volume;
        } else if(_gameManager.GetTime() <= 0){
            _audioSource.volume = 1;
        }
    }

    private IEnumerator DisplayMusicName(){
        // scroll du texte si trop long : https://youtu.be/AuZNU7JTeWQ
        // also : https://youtu.be/ToVL_f9G9Yk

        // pas parfait

        _cloneTextObject = Instantiate(_musicName) as TextMeshProUGUI;
        RectTransform cloneRectTransform = _cloneTextObject.GetComponent<RectTransform>();
        cloneRectTransform.SetParent(_musicNameRectTransform);
        cloneRectTransform.anchorMin = new Vector2(1, 0.5f);
        cloneRectTransform.localScale = Vector3.one;

        float width = _musicName.preferredWidth;
        Vector3 startPosition = _musicNameRectTransform.position;

        float scrollPosition = 0;
        while(true){
            _musicNameRectTransform.position = new Vector3 (-scrollPosition % width, startPosition.y, startPosition.z);

            scrollPosition += _musicNameScrollSpeed * 20 * Time.deltaTime;

            yield return null;
        }
    }

    // affiche le nom de la musique et de l'artiste
    private void SetMusicName(){
        // split le nom de la musique
        // passe de "MonTexteTropCool" à "Mon Texte Trop Cool "
        string[] splitedString = Regex.Split(_audioSource.clip.name, @"(?<!^)(?=[A-Z])");
        string result = "";
        for(int i = 0; i < splitedString.Length; i++){
            result += splitedString[i] + " ";
            // result += splitedString.Length != i ? splitedString[i] + " " : splitedString[i];
        }

        _musicName.text = result;
        _artistName.text = _actualArtist.GetName();
    }

    // public functions
    public void SetAudioClip(){
        List<ArtistsAttributes> listArtistInHalls = new List<ArtistsAttributes> {};
        int artistInHalls = 0;

        // prend un des artistes présent dans les halls au hasard pour y jouer sa musique
        for(int i = 0; i < _artistContainers.childCount; i++){
            if(_artistContainers.GetChild(i).gameObject.activeInHierarchy){
                listArtistInHalls.Add(_artistContainers.GetChild(i).GetComponent<ArtistsAttributes>());
                artistInHalls += 1;
            }
        }

        ArtistsAttributes actualArtist = listArtistInHalls[Random.Range(0, listArtistInHalls.Count)];
        _audioSource.clip = actualArtist.GetMusic();
        _actualArtist = actualArtist;

        this.SetMusicName();
        StartCoroutine(this.DisplayMusicName());
        _audioSource.Play();
    }

    // pour augmenter ou diminuer le volume de la musique
    public void UpdateVolume(float value){_audioSource.volume += value;}
}