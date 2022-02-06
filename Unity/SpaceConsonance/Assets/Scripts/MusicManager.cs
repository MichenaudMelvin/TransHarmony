using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour{

    [Header("Audio")]
    [SerializeField]
    [Tooltip("Source audio (musique du festival)")]
    private AudioSource _audioSource;

    [Tooltip("Volume de la musique joué")]
    private float _volume;

    [Space(10)]

    [Header("Display music")]
    [SerializeField]
    [Tooltip("Nom de la musique")]
    private Text _musicName;

    [SerializeField]
    [Tooltip("Nom de l'artiste")]
    private Text _artistName;

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
        StartCoroutine(LateStart(0.5f));
    }

    private IEnumerator LateStart(float time){
        yield return new WaitForSeconds(time);

        this.SetAudioClip();
    }

    private void Update(){
        _audioSource.volume = _volume;
        // Penser à afficher la musique joué et de quel artiste
        // this.DisplayMusicName();
    }

    private void ManageVolume(){
        if(_gameManager.GetTime() > 0){

        }
            // float time = _gameManager.GetTime();
        // time = Mathf.Lerp()
    }

    private void DisplayMusicName(){
        // if(){
        //     scroll du texte si trop long : https://youtu.be/AuZNU7JTeWQ
        // }
    }

    private void SetMusicName(){
        _musicName.text = _audioSource.clip.name;
        _artistName.text = _actualArtist.Getname();
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
    }
}
