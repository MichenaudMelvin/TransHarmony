using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{

    public List<string> musicStyles;
    public List<Color> musicColors;

    public RawImage imageTest;

    public ArtistsAttributes ya1;
    public ArtistsAttributes ya2;

    public void changePrefrenceColor(RawImage rawImage, int index){
        rawImage.color = musicColors[index];
        Color currColor = rawImage.color;
        currColor.a = 1f;
        rawImage.color = currColor;
    }

    public void changePrefrenceColor(RawImage rawImage, int index0, int index1, int index2){
        rawImage.color = musicColors[index0] * musicColors[index1] * musicColors[index2];
        Color currColor = rawImage.color;

        // if(currColor.r >= 180f){
        //     currColor.r += 15f;
        // } else if(currColor.r <= 30){
        //     currColor.r += -15f;
        // }

        // if(currColor.g >= 180f){
        //     currColor.g += 15f;
        // } else if(currColor.g <= 30){
        //     currColor.g += -15f;
        // }

        // if(currColor.b >= 180f){
        //     currColor.b += 15f;
        // } else if(currColor.b <= 30){
        //     currColor.b += -15f;
        // }

        currColor.a = 1f;
        rawImage.color = currColor;
    }

    public void ConnectArtist(){
        // ArtistsAttributes artistA, ArtistsAttributes artistB <-- parameters
        Color newColor = ya1.preferences.color + ya2.preferences.color;
        imageTest.color = newColor;
    }

    private void Update(){
        if(Input.GetKey("space")){
            this.ConnectArtist();
        }
    }
}
