using UnityEngine;
using UnityEngine.UI;

public class StopMusic : MonoBehaviour{

    public Button btn;
    public Button playBtn;

    public AudioSource musicToStop;

    public GameManager gameManager;

    private void Start(){
        btn.onClick.AddListener(Stop);
    }

    private void Update(){
        if(!musicToStop.isPlaying){
            // stop la musique si elle s'arrette
            this.gameObject.SetActive(false);
            playBtn.gameObject.SetActive(true);
        }
    }

    private void Stop(){
        if(musicToStop.clip != null && !gameManager.isFinished){
            musicToStop.Stop();
        }
    }
}
