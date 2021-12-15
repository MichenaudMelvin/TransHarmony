using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{

    public List<string> musicStyles;
    public List<Color> musicColors;

    public float satisfactionPoints;
    public int enoughPoints;

    public bool isFinished;

    public List<ArtistsAttributes> artistList;

    public void changePrefrenceColor(RawImage rawImage, int index){
        rawImage.color = musicColors[index];
        Color currColor = rawImage.color;
        currColor.a = 1f;
        rawImage.color = currColor;
    }

    public void changePrefrenceColor(RawImage rawImage, int index0, int index1, int index2, int index3){
        rawImage.color = musicColors[index0] + (((musicColors[index1] - musicColors[index0])/2) + ((musicColors[index3] - musicColors[index2])/2));
        print(musicStyles[index0]);
        print(musicStyles[index1]);
        print(musicStyles[index2]);
        print(musicStyles[index3]);
        Color currColor = rawImage.color;
        currColor.a = 1f;
        rawImage.color = currColor;
    }

    public void ManagePoints(){
        for(int i = 0; i < artistList.Count; i++){
            if(artistList[i].validConstraint){
                satisfactionPoints += artistList[i].rewardPoints * artistList[i].pointMultiplier;
            } else{
                satisfactionPoints += artistList[i].rewardPoints;
            }
            print(satisfactionPoints);
        }
    }
}
