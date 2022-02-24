using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour{

    [Tooltip("Mode du jeu")]
    private string _gameMode;

    [Header("Master Volume")]
    [SerializeField]
    [Tooltip("Slider qui gère le master volume")]
    private Slider _masterVolumeSlider;

    [Tooltip("Volume du jeu")]
    private float _masterVolume = 1;

    [Tooltip("Effets sonores activés ou désactivés")]
    private bool _isSoundEnable = true;

    private void Start(){
        _masterVolumeSlider.value = _masterVolume;
    }

    // public functions
    public string GetGameMode(){return _gameMode;}

    public void SetGameMode(string newGameMode){_gameMode = newGameMode;}

    public float GetMasterVolume(){return _masterVolume;}

    // Master volume
    public void SetMasterVolume(){
        _masterVolume = _masterVolumeSlider.value;
        AudioListener.volume = _masterVolume;
    }

    // enable/disable sounds effects
    public void SetSoundEnable(bool boolean){_isSoundEnable = boolean;}

    public bool GetIsSoundEnable(){return _isSoundEnable;}

}
