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
        rawImage.color = musicColors[index0] + ((musicColors[index1] - musicColors[index0])/2);
        Color currColor = rawImage.color;
        currColor.a = 1f;
        rawImage.color = currColor;
    }

    public void ConnectArtist(){
        // ArtistsAttributes artistA, ArtistsAttributes artistB <-- parameters
        // est-ce que le public doit forcément etre la meme couleur que les artistes ou plutot choisir des nuances ?
        Color newColor = ya1.preferences.color + ya2.preferences.color;
        imageTest.color = newColor;
    }

    private void Update(){
        if(Input.GetKey("space")){
            this.ConnectArtist();
        }
    }
}
