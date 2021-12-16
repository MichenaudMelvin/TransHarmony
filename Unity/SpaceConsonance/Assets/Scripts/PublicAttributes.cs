using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PublicAttributes : MonoBehaviour{

    public RawImage preferences;

    public GameManager gameManager;
    public Text debugText;

    public string preferencesName0;
    public string preferencesName1;

    private int randomPositionMusicStyle0;
    private int randomPositionMusicStyle1;

    public List<string> publicPreferences;

    private void Start(){
        this.SetupPublicPreference();
        this.FixGameManagerPrefab();
    }

    private void SetupPublicPreference(){
        randomPositionMusicStyle0 = Random.Range(0, gameManager.musicStyles.Count);
        randomPositionMusicStyle1 = Random.Range(0, gameManager.musicStyles.Count);
        preferencesName0 = gameManager.musicStyles[randomPositionMusicStyle0];
        preferencesName1 = gameManager.musicStyles[randomPositionMusicStyle1];
        
        while(preferencesName0 == preferencesName1){
            randomPositionMusicStyle1 = Random.Range(0, gameManager.musicStyles.Count);
            preferencesName1 = gameManager.musicStyles[randomPositionMusicStyle1];
        }

        gameManager.changePrefrenceColor(preferences, randomPositionMusicStyle0, randomPositionMusicStyle1);
        debugText.text = preferencesName0 + "\n" + preferencesName1;

        publicPreferences.Add(preferencesName0);
        publicPreferences.Add(preferencesName1);
    }

    private void FixGameManagerPrefab(){
        // c'est du bidouillage mais ça marche
        // (modification du prefab plutot que d'un object dans la scene)
        // (impossible de modifier le prefab GameManager avec sa propre fonction start : modifie uniquement l'instance)
        gameManager.isFinished = false;
        gameManager.satisfactionPoints = 0;
        gameManager.finalMusicList.Clear();
        gameManager.correctMusicStyles = 0;
        gameManager.index0 = -1;
        gameManager.index1 = -1;
        gameManager.index2 = -1;
        gameManager.index3 = -1;
    }
}
