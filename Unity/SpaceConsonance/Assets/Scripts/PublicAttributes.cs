using UnityEngine;
using UnityEngine.UI;

public class PublicAttributes : MonoBehaviour{

    public string preferencesName0;
    public string preferencesName1;
    public string preferencesName2;
    public RawImage preferences;

    public GameManager gameManager;
    private int randomPositionMusicStyle0;
    private int randomPositionMusicStyle1;
    private int randomPositionMusicStyle2;

    private void Start(){
        this.SetupPreference();
    }

    private void SetupPreference(){
        randomPositionMusicStyle0 = Random.Range(0, gameManager.musicStyles.Count);
        randomPositionMusicStyle1 = Random.Range(0, gameManager.musicStyles.Count);
        randomPositionMusicStyle2 = Random.Range(0, gameManager.musicStyles.Count);
        preferencesName0 = gameManager.musicStyles[randomPositionMusicStyle0];
        preferencesName1 = gameManager.musicStyles[randomPositionMusicStyle1];
        preferencesName2 = gameManager.musicStyles[randomPositionMusicStyle2];
        
        while(preferencesName0 == preferencesName1){
            randomPositionMusicStyle1 = Random.Range(0, gameManager.musicStyles.Count);
            preferencesName1 = gameManager.musicStyles[randomPositionMusicStyle1];
        }

        while(preferencesName0 == preferencesName2 || preferencesName1 == preferencesName2){
            randomPositionMusicStyle2 = Random.Range(0, gameManager.musicStyles.Count);
            preferencesName2 = gameManager.musicStyles[randomPositionMusicStyle2];
        }

        gameManager.changePrefrenceColor(preferences, randomPositionMusicStyle0, randomPositionMusicStyle1, randomPositionMusicStyle2);
    }
}
