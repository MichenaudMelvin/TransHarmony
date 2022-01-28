using UnityEngine;
using UnityEngine.UI;

public class StopMusic : MonoBehaviour{

    [SerializeField]
    private Button _btn;

    [SerializeField]
    private Button _playBtn;

    [SerializeField]
    private AudioSource _musicToStop;

    private void Start(){
        _btn.onClick.AddListener(Stop);
    }

    private void Update(){
        if(!_musicToStop.isPlaying){
            // stop la musique si elle s'arrette
            this.gameObject.SetActive(false);
            _playBtn.gameObject.SetActive(true);
        }
    }

    private void Stop(){
        if(_musicToStop.clip != null){
            _musicToStop.Stop();
        }
    }
}
