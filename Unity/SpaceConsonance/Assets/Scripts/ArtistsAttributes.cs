using UnityEngine;
using UnityEngine.UI;

public class ArtistsAttributes : MonoBehaviour{

    public string artistName;
    public Text artistNameToChange;
    public Text musicStyle;
    public Text constraintText;

    public string preferencesName;
    public RawImage preferences;

    public AudioSource musicToPlay;

    public GameManager gameManager;
    private int randomPositionMusicStyle;

    public string constraint;
    public float rewardPoints;
    public float pointMultiplier;
    public bool validConstraint = false;

    private void Start(){
        this.FixNameReferences();
        randomPositionMusicStyle = gameManager.musicStyles.IndexOf(preferencesName);
        gameManager.changePrefrenceColor(preferences, randomPositionMusicStyle);
    }

    private void FixNameReferences(){
        this.name = artistName;
        musicStyle.text = preferencesName;
        artistNameToChange.text = artistName;
        this.changeConstraintText();
    }

    private void changeConstraintText(){
        if(constraint == "LikePlayNearTechno"){
            constraintText.text = "Like to play next to a Techno group";
        } else if(constraint == "DislikePlayNearTechno"){
            constraintText.text = "Doesn't like to play next to a Techno group";
        }

        else if(constraint == "LikePlayNearHouse"){
            constraintText.text = "Like to play next to a House group";
        } else if(constraint == "DislikePlayNearHouse"){
            constraintText.text = "Doesn't like to play next to a House group";
        }

        else if(constraint == "LikePlayNearElectro"){
            constraintText.text = "Like to play next to a Electro group";
        } else if(constraint == "DislikePlayNearElectro"){
            constraintText.text = "Doesn't like to play next to a Electro group";
        }

        // famille de musique 2
        else if(constraint == "LikePlayNearFunk"){
            constraintText.text = "Like to play next to a Funk group";
        } else if(constraint == "DislikePlayNearFunk"){
            constraintText.text = "Doesn't like to play next to a Funk group";
        }

        else if(constraint == "LikePlayNearRap"){
            constraintText.text = "Like to play next to a Rap group";
        } else if(constraint == "DislikePlayNearRap"){
            constraintText.text = "Doesn't like to play next to a Rap group";
        }

        else if(constraint == "LikePlayNearJazz"){
            constraintText.text = "Like to play next to a Jazz group";
        } else if(constraint == "DislikePlayNearJazz"){
            constraintText.text = "Doesn't like to play next to a Jazz group";
        }

        // famille de musique 3
        else if(constraint == "LikePlayNearRock"){
            constraintText.text = "Like to play next to a Rock group";
        } else if(constraint == "DislikePlayNearRock"){
            constraintText.text = "Doesn't like to play next to a Rock group";
        }

        else if(constraint == "LikePlayNearPunk"){
            constraintText.text = "Like to play next to a Punk group";
        } else if(constraint == "DislikePlayNearPunk"){
            constraintText.text = "Doesn't like to play next to a Punk group";
        }

        else if(constraint == "LikePlayNearMetal"){
            constraintText.text = "Like to play next to a Metal group";
        } else if(constraint == "DislikePlayNearMetal"){
            constraintText.text = "Doesn't like to play next to a Metal group";
        }
    }
}
