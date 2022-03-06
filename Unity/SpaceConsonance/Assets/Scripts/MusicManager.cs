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

    [SerializeField]
    [Tooltip("Vitesse à laquelle la musique se fade out (à la fin de chaque journée)")]
    [Range(0.01f, 0.1f)]
    private float _fadeOutSpeed;

    [Space(10)]

    [Header("Display music")]
    [SerializeField]
    [Tooltip("Nom de la musique")]
    private TextMeshProUGUI _musicName;

    [SerializeField]
    [Tooltip("Enfant de \"_musicName\" qui permet de faire défiller le texte")]
    private TextMeshProUGUI _childMusicName;

    [SerializeField]
    [Tooltip("Nom de l'artiste")]
    private Text _artistName;

    [SerializeField]
    [Range(0.01f, 1f)]
    [Tooltip("Vitesse à laquel va défiler le nom de la musique si le texte est trop grand")]
    private float _musicNameScrollSpeed;

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

    [SerializeField]
    [Tooltip("Sécurité pour pas que la couroutine fonctionne plusieurs fois en meme temps")]
    private bool _displayMusicNameCoroutineIsRunning = false;

    [Tooltip("Bool de merde qui sert juste pour pas call un fonction plusieurs fois")]
    private bool _hasCallNewAudioClip = false;

    private void Start(){
        // pour laisser le temps à Unity d'instancier les artistes // sinon marche pas
        StartCoroutine(this.LateStart(0.001f));
    }

    private IEnumerator LateStart(float time){
        yield return new WaitForSeconds(time);

        this.SetAudioClip();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(this.ManageVolume());
    }

    // gère le volume de la musique en fonciton des actions du joueur
    private IEnumerator ManageVolume(){
        while(_gameManager.GetTime() > 0){

            // changement de musique si la musique actuelle est finie
            if(_audioSource.time == 0 && _hasCallNewAudioClip == false){
                // rejoue la meme musique si le jeu est en pause
                if(_gameManager.GetIsGamePause()){
                    _audioSource.Play();
                } else{
                    _hasCallNewAudioClip = true;
                    StopCoroutine(this.DisplayMusicName());
                    this.SetAudioClip();
                    yield return new WaitForSeconds(0.5f);
                }

            }

            yield return null;
        }
    }


    private IEnumerator DisplayMusicName(){
        if(!_displayMusicNameCoroutineIsRunning){
            _displayMusicNameCoroutineIsRunning = true;

            // à voir si en fonction de la taille du texte
            // faudrait pas changer la largeur de TextMeshPro component

            bool canScroll = true;

            // fait encore des trucs chelou mais à peu pres bon
            while(canScroll){
                _musicName.transform.Translate(new Vector3(-1, 0, 0) * _musicNameScrollSpeed);
                if(_musicName.GetComponent<RectTransform>().anchoredPosition.x <= -_musicName.GetComponent<RectTransform>().sizeDelta.x){
                    _musicName.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    if(_gameManager.GetTime() <= 0){
                        canScroll = false;
                    }
                }

                yield return null;
            }
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
        _childMusicName.text = _musicName.text;
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

        int playCount = 0;

        // pour éviter un crash
        for(int i = 0; i < listArtistInHalls.Count; i++){
            if(listArtistInHalls[i].HasPlayMusic()){
                playCount += 1;
            }
        }

        if(playCount >= artistInHalls){
            for(int i = 0; i < listArtistInHalls.Count; i++){
                listArtistInHalls[i].SetHasPlayMusic(false);
            }
        }

        ArtistsAttributes actualArtist = null;
        actualArtist = listArtistInHalls[Random.Range(0, listArtistInHalls.Count)];

        while(actualArtist.HasPlayMusic()){
            actualArtist = listArtistInHalls[Random.Range(0, listArtistInHalls.Count)];
        }

        _audioSource.clip = actualArtist.GetMusic();
        _actualArtist = actualArtist;
        actualArtist = null;

        _actualArtist.SetHasPlayMusic(true);

        this.SetMusicName();
        StartCoroutine(this.DisplayMusicName());
        _audioSource.Play();
        _hasCallNewAudioClip = false;
    }

    // se délenche à la fin de chaque journée
    // Unused
    public IEnumerator EndPhase(){
        StopCoroutine(this.DisplayMusicName());
        _displayMusicNameCoroutineIsRunning = false;

        while(_audioSource.volume > 0){
            _audioSource.volume -= _fadeOutSpeed;
            yield return new WaitForSeconds(0.1f);
        }
    }

    // pour augmenter ou diminuer le volume de la musique
    public void UpdateVolume(float value){_audioSource.volume += value;}
}