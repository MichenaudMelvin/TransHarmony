using UnityEngine;
using UnityEngine.UI;

public class PlayMusic : MonoBehaviour{

    public Button btn;
    public AudioSource musicToPlay;

    private void Start(){
        btn.onClick.AddListener(Play);
    }

    private void Play(){
        if(musicToPlay.clip != null){
            musicToPlay.Play();
        }
    }
}
