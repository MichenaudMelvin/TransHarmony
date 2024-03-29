using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour{

    [SerializeField]
    [Tooltip("La scène qui va être lancé")]
    private string _sceneName;

    private void Start(){
        if(SceneManager.GetActiveScene().name == "LoadingScene"){
            StartCoroutine(this.LoadMainScene());
        }
    }

    private IEnumerator LoadMainScene(){
        yield return new WaitForSeconds(0.01f);
        SceneManager.LoadScene(_sceneName);
    }

    public void ChangeScene(){
        if(_sceneName == "self" || _sceneName == "this"){
            // reload current scene
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        } else{
            SceneManager.LoadScene(_sceneName);
        }
    }

}
