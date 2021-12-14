using UnityEngine;
using UnityEngine.UI;

public class PlayMusic : MonoBehaviour{

    public Button btn;
    private AudioSource musicToPlay;

    private void Start(){
        // musicToPlay = this.transform.parent.GetComponent<ArtistsAttributes>().musicToPlay;
        btn.onClick.AddListener(Play);
    }

    private void Play(){
        // musicToPlay.Play();
    }
}
