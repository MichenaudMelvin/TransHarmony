using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Validation : MonoBehaviour{

    public Button btn;
    public RawImage resultsImage;
    public GameManager gameManager;

    public List<ArtistsAttributes> artistList;

    public Halls halls;

    private string endMessage;

    private bool canValidate;

    private void Start(){
        btn.onClick.AddListener(FinishedFestival);
    }

    private void FinishedFestival(){
        if(!gameManager.isFinished){
            canValidate = true;
            for(int i = 0; i < halls.hallList.Count; i++){
                if(halls.hallList[i].isBusy == false){
                    canValidate = false;
                }
            }

            if(canValidate){
                gameManager.isFinished = true;

                gameManager.ManagePoints(artistList);

                endMessage = gameManager.satisfactionPoints >= gameManager.enoughPoints ? "Bravo !" : "Le public n'a pas été convaincu par le festival";

                resultsImage.GetComponentInChildren<Text>().text = "Points de satisfactions accumulés " + gameManager.satisfactionPoints.ToString() + ".\n\n" + endMessage;
                resultsImage.gameObject.SetActive(true);
            }
        }
    }
}
