using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour{

    [SerializeField]
    private string sceneName;

    [SerializeField]
    private Button btn;

    private void Start(){
        btn.onClick.AddListener(ChangeScene);
    }

    private void ChangeScene(){
        SceneManager.LoadScene(sceneName);
    }

}
