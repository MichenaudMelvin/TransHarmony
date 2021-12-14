using UnityEngine;
using UnityEngine.UI;

public class StopMusic : MonoBehaviour{

    public Button btn;
    private AudioSource musicToStop;

    private void Start(){
        // musicToStop = this.transform.parent.GetComponent<ArtistsAttributes>().musicToPlay;
        btn.onClick.AddListener(Stop);
    }

    private void Stop(){
        // musicToStop.Stop();
    }
}
