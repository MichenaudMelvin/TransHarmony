using UnityEngine;
using UnityEngine.UI;

public class PlayMusic : MonoBehaviour{

    [SerializeField]
    private Button _btn;

    [SerializeField]
    private AudioSource _musicToPlay;

    private void Start(){
        _btn.onClick.AddListener(Play);
    }

    private void Play(){
        if(_musicToPlay.clip != null){
            _musicToPlay.Play();
        }
    }
}
