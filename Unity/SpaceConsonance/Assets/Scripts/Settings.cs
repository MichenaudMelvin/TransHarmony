using UnityEngine;

public class Settings : MonoBehaviour{

    [Tooltip("Mode du jeu")]
    private string _gameMode;

    [Tooltip("Volume du jeu")]
    private float _masterVolume;

    // public functions
    public string GetGameMode(){return _gameMode;}

    public void SetGameMode(string newGameMode){_gameMode = newGameMode;}

    public float GetMasterVolume(){return _masterVolume;}

    public void SetMasterVolume(float value){
        _masterVolume = value;
        AudioListener.volume = _masterVolume;
    }

}
