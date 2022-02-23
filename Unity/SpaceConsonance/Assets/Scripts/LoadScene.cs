using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour{

    [SerializeField]
    [Tooltip("Référence aux settings du jeu")]
    private Settings _settings;

    [SerializeField]
    [Tooltip("La scène qui va être lancé")]
    private string _sceneName;

    public void ChangeScene(){
        // DontDestroyOnLoad(_settings);
        if(_sceneName == "self" || _sceneName == "this"){
            // reload current scene
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        } else{
            SceneManager.LoadScene(_sceneName);
        }
    }

}
