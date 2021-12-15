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
            constraintText.text = "Aime jouer à coté d'un groupe de Techno";
        } else if(constraint == "DislikePlayNearTechno"){
            constraintText.text = "N'aime jouer à coté d'un groupe de Techno";
        }

        else if(constraint == "LikePlayNearHouse"){
            constraintText.text = "Aime jouer à coté d'un groupe de House";
        } else if(constraint == "DislikePlayNearHouse"){
            constraintText.text = "N'aime jouer à coté d'un groupe de House";
        }

        else if(constraint == "LikePlayNearElectro"){
            constraintText.text = "Aime jouer à coté d'un groupe d'Electro";
        } else if(constraint == "DislikePlayNearElectro"){
            constraintText.text = "N'aime jouer à coté d'un groupe d'Electro";
        }

        // famille de musique 2
        else if(constraint == "LikePlayNearFunk"){
            constraintText.text = "Aime jouer à coté d'un groupe de Funk";
        } else if(constraint == "DislikePlayNearFunk"){
            constraintText.text = "N'aime jouer à coté d'un groupe de Funk";
        }

        else if(constraint == "LikePlayNearRap"){
            constraintText.text = "Aime jouer à coté d'un groupe de Rap";
        } else if(constraint == "DislikePlayNearRap"){
            constraintText.text = "N'aime jouer à coté d'un groupe de Rap";
        }

        else if(constraint == "LikePlayNearJazz"){
            constraintText.text = "Aime jouer à coté d'un groupe de Jazz";
        } else if(constraint == "DislikePlayNearJazz"){
            constraintText.text = "N'aime jouer à coté d'un groupe de Jazz";
        }

        // famille de musique 3
        else if(constraint == "LikePlayNearRock"){
            constraintText.text = "Aime jouer à coté d'un groupe de Rock";
        } else if(constraint == "DislikePlayNearRock"){
            constraintText.text = "N'aime jouer à coté d'un groupe de Rock";
        }

        else if(constraint == "LikePlayNearPunk"){
            constraintText.text = "Aime jouer à coté d'un groupe de Punk";
        } else if(constraint == "DislikePlayNearPunk"){
            constraintText.text = "N'aime jouer à coté d'un groupe de Punk";
        }

        else if(constraint == "LikePlayNearMetal"){
            constraintText.text = "Aime jouer à coté d'un groupe de Metal";
        } else if(constraint == "DislikePlayNearMetal"){
            constraintText.text = "N'aime jouer à coté d'un groupe de Metal";
        }
    }
}
