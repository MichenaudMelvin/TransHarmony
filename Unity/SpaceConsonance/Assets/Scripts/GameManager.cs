using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour{

    public List<string> musicStyles;
    public List<Color> musicColors;

    public float satisfactionPoints;
    public int enoughPoints;

    public bool isFinished;

    public List<string> finalMusicList;
    public int correctMusicStyles;

    public Color mixedColor;

    public int index0;
    public int index1;
    public int index2;
    public int index3;

    public void changePrefrenceColor(RawImage rawImage, int index){
        rawImage.color = musicColors[index];
        Color currColor = rawImage.color;
        currColor.a = 1f;
        rawImage.color = currColor;
    }

    public void changePrefrenceColor(RawImage rawImage, int index0, int index1){
        // pas clair au niveau de la couleur
        rawImage.color = musicColors[index0] + ((musicColors[index1] - musicColors[index0])/2);
        Color currColor = rawImage.color;
        currColor.a = 1f;
        rawImage.color = currColor;
    }

    public void ManagePoints(List<ArtistsAttributes> artistList, List<string> publicPreferences, RawImage preferences){
        for(int i = 0; i < artistList.Count; i++){
            // check tous les artistes qui sont dans un hall
            // et check si la contrainte de l'artiste est valide
            if(artistList[i].GetComponent<DragNDrop>().inHall){
                if(artistList[i].validConstraint){
                    satisfactionPoints += artistList[i].rewardPoints * artistList[i].pointMultiplier;
                } else{
                    satisfactionPoints += artistList[i].rewardPoints;
                }

                if(index0 == -1){index0 = i;}
                else if(index1 == -1){index1 = i;}
                else if(index2 == -1){index2 = i;}
                else if(index3 == -1){index3 = i;}

                finalMusicList.Add(artistList[i].preferencesName);
            }
        }


        // à faire : calcul des points du public
        // pour l'instant ça
        for(int i = 0; i < publicPreferences.Count; i++){
            for(int j = 0; j < finalMusicList.Count; j++){
                if(publicPreferences[i] == finalMusicList[j]){
                    correctMusicStyles += 1;
                }
            }
        }

        if(correctMusicStyles > 0){
            satisfactionPoints *= correctMusicStyles;
        }

        // Comparer ces deux la
        mixedColor = ((artistList[index1].preferences.color - artistList[index0].preferences.color)/2) + ((artistList[index3].preferences.color - artistList[index2].preferences.color)/2);
        print(preferences.color);
        print(mixedColor);
        // convertir la couleur en décimal
        // et faire un rapprochement entre les deux nombres
    }
}
