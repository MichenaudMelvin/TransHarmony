using UnityEngine;

public class ArtistsAttributes : MonoBehaviour{

    [SerializeField]
    [Tooltip("Scriptable object de l'artiste")]
    private ArtistAsset _artistAsset;

    // quand l'artiste quitte un hall
    private void OnDestroy(){_artistAsset._hasPlayMusic = false;}

    // public functions
    // quand l'artiste est instantié
    public void SetArtistAsset(ArtistAsset newArtistAsset){_artistAsset = newArtistAsset;}

    public string GetName(){return _artistAsset._name;}

    public int GetHallNumber(){return _artistAsset.currentHall;}

    public void SetCurrentHall(int value){_artistAsset.currentHall = value;}

    public AudioClip GetMusic(){return _artistAsset._music;}

    public bool GetStatus(){return _artistAsset._alreadyPerform;}

    public void SetStatus(bool boolean){_artistAsset._alreadyPerform = boolean;}

    public bool HasPlayMusic(){return _artistAsset._hasPlayMusic;}

    public void SetHasPlayMusic(bool boolean){_artistAsset._hasPlayMusic = boolean;}
}