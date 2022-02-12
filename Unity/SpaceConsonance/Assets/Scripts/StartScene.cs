using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour{

    [SerializeField]
    [Tooltip("La scène qui va être lancé")]
    private string _sceneName;

    [SerializeField]
    [Tooltip("Bouton qui démarre la scène")]
    private Button _btn;

    private void Start(){
        _btn.onClick.AddListener(ChangeScene);
    }

    private void ChangeScene(){
        SceneManager.LoadScene(_sceneName);
    }

}
