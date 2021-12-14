using UnityEngine;
using UnityEngine.UI;

public class ArtistsAttributes : MonoBehaviour{

    public string artistName;
    public Text artistNameToChange;

    public string preferencesName;
    public RawImage preferences;

    public AudioSource musicToPlay;

    public GameManager gameManager;
    private int randomPositionMusicStyle;

    private void Start(){
        this.FixNameReferences();
        randomPositionMusicStyle = gameManager.musicStyles.IndexOf(preferencesName);
        gameManager.changePrefrenceColor(preferences, randomPositionMusicStyle);
    }

    private void FixNameReferences(){
        this.name = artistName;
        artistNameToChange.text = artistName;
    }
}
