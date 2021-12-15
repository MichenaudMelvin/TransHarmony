using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour{

    public Button btn;

    private void Start(){
        btn.onClick.AddListener(StartGame);
    }

    private void StartGame(){
        SceneManager.LoadScene("ParcExpo");
    }
}
