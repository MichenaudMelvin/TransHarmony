using UnityEngine;
using UnityEngine.UI;

public class PublicAttributes : MonoBehaviour{

    public RawImage preferences;

    public GameManager gameManager;

    public string preferencesName0;
    public string preferencesName1;
    public string preferencesName2;
    public string preferencesName3;

    private int randomPositionMusicStyle0;
    private int randomPositionMusicStyle1;
    private int randomPositionMusicStyle2;
    private int randomPositionMusicStyle3;

    private void Start(){
        this.SetupPublicPreference();

        // c'est du bidouillage mais ça marche
        // (modification du prefab plutot que d'un object dans la scene)
        // (impossible de modifier le prefab GameManager avec sa propre fonction start : modifie uniquement l'instance)
        gameManager.isFinished = false;
        gameManager.satisfactionPoints = 0;
    }

    private void SetupPublicPreference(){
        randomPositionMusicStyle0 = Random.Range(0, gameManager.musicStyles.Count);
        randomPositionMusicStyle1 = Random.Range(0, gameManager.musicStyles.Count);
        randomPositionMusicStyle2 = Random.Range(0, gameManager.musicStyles.Count);
        randomPositionMusicStyle3 = Random.Range(0, gameManager.musicStyles.Count);
        preferencesName0 = gameManager.musicStyles[randomPositionMusicStyle0];
        preferencesName1 = gameManager.musicStyles[randomPositionMusicStyle1];
        preferencesName2 = gameManager.musicStyles[randomPositionMusicStyle2];
        preferencesName3 = gameManager.musicStyles[randomPositionMusicStyle3];
        
        while(preferencesName0 == preferencesName1){
            randomPositionMusicStyle1 = Random.Range(0, gameManager.musicStyles.Count);
            preferencesName1 = gameManager.musicStyles[randomPositionMusicStyle1];
        }

        while(preferencesName0 == preferencesName1 || preferencesName0 == preferencesName2 || preferencesName1 == preferencesName2){
            randomPositionMusicStyle2 = Random.Range(0, gameManager.musicStyles.Count);
            preferencesName2 = gameManager.musicStyles[randomPositionMusicStyle2];
        }

        while(preferencesName0 == preferencesName1 || preferencesName0 == preferencesName2 || preferencesName1 == preferencesName2 || preferencesName0 == preferencesName3 || preferencesName1 == preferencesName3 || preferencesName2 == preferencesName3){
            randomPositionMusicStyle3 = Random.Range(0, gameManager.musicStyles.Count);
            preferencesName3 = gameManager.musicStyles[randomPositionMusicStyle3];
        }

        gameManager.changePrefrenceColor(preferences, randomPositionMusicStyle0, randomPositionMusicStyle1, randomPositionMusicStyle2, randomPositionMusicStyle3);
    }
}
