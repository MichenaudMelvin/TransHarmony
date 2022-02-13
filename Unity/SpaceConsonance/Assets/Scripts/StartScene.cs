using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour{

    [SerializeField]
    [Tooltip("Référence aux settings du jeu")]
    private Settings _settings;

    [Tooltip("La scène qui va être lancé")]
    private string _sceneName;

    public void ChangeScene(){
        SceneManager.LoadScene(_sceneName);
        DontDestroyOnLoad(_settings);
    }

}
