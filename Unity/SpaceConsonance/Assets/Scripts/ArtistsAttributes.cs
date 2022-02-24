using UnityEngine;
using System.Collections.Generic;

public class ArtistsAttributes : MonoBehaviour{

    [SerializeField]
    [Tooltip("Liste des mesh dispo")]
    private List<Mesh> _meshList;

    [SerializeField]
    [Tooltip("Model du perso")]
    private MeshFilter _model;

    [SerializeField]
    [Tooltip("Scriptable object de l'artiste")]
    private ArtistAsset _artistAsset;

    private void Start(){
        _model.mesh = _meshList[Random.Range(0, _meshList.Count)];
    }

    // quand l'artiste quitte un hall
    // pas sur que ce soit utile
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

    // retourne le hall dans lequel l'artiste est placé
    public HallsAttributes GetHall(){return _artistAsset._hall;}

    public void SetHall(HallsAttributes newHall){_artistAsset._hall = newHall;}


}