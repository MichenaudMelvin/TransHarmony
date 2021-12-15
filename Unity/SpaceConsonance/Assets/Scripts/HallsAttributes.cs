using UnityEngine;
using System.Collections.Generic;

public class HallsAttributes : MonoBehaviour{

    public bool isBusy = false;
    private bool artistPlaced = false;

    public DragNDrop element;
    public ArtistsAttributes artistInHall;

    public List<HallsAttributes> nearHalls;
    public List<ArtistsAttributes> nearArtists;

    private void Update(){
        if(element != null){
            artistInHall = element.GetComponent<ArtistsAttributes>();
            if(!element.inHall && isBusy){
                isBusy = false;
                // for(int i = 0; i < nearHalls.Count; i++){
                //     for(int j = 0; j < nearHalls[i].nearArtists.Count; j++){
                //         if(artistInHall == nearHalls[i].nearArtists[j]){
                //             nearHalls[i].nearArtists[j] = null;
                //         }
                //     }
                // }
                // nearHalls[0].nearArtists.Remove(nearHalls[0].nearArtists[0]);

                nearHalls[0].nearArtists.RemoveAt(0);

                // ici enlever la validation des contraintes des artistes

                artistInHall.validConstraint = false;
                artistInHall = null;
                artistPlaced = false;
            }
        }

        if(!artistPlaced){
            for(int i = 0; i < nearHalls.Count; i++){
                if(element != null && nearHalls[i] != null && nearHalls[i].artistInHall != null){
                    // nearArtists[i] = nearHalls[i].artistInHall;

                    // check si l'artiste est pas deja dans la liste
                    if(nearHalls[i].artistInHall == nearArtists[i]){
                        nearArtists.Add(nearHalls[i].artistInHall);
                        print("pas deja pris");
                    } else{
                        print("deja pris");
                    }

                    // }

                    // for(int j = 0; j < nearHalls.Count; j++){
                    //     if(nearHalls[j].nearArtists[i] == nearHalls[i].artistInHall){
                    // }

                    // for(int j = 0; j < nearHalls.Count; j++){
                    //     for(int k = 0; k < nearHalls[k].nearArtists.Count; k++){
                    //         if(artistInHall == nearHalls[i].nearArtists[j]){
                    //             nearHalls[i].nearArtists[j] = null;
                    //         }
                    //     }
                    // }

                    if(nearHalls.Count == nearArtists.Count){
                        artistPlaced = true;
                    }

                    this.CheckArtistConstraint(i);



                    if(artistInHall.preferencesName == nearArtists[i].preferencesName){
                        // is ok
                    } else{
                        // enlever des pts du coup ?
                    }
                }
            }
        }
    }


    private void CheckArtistConstraint(int index){
        // en gros ça, pour multiplier ça [Shift+Alt+Up/Down] et remplacer "Techno"
        // 9 styles de musiques
        // normalement c'est bon
        // téma la taille du code

        // famille de musique 1
        if(artistInHall.constraint == "LikePlayNearTechno"){
            if(nearHalls[index].nearArtists[index].preferencesName == "Techno"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearTechno"){
            if(nearHalls[index].nearArtists[index].preferencesName != "Techno"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }

        if(artistInHall.constraint == "LikePlayNearHouse"){
            if(nearHalls[index].nearArtists[index].preferencesName == "House"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearHouse"){
            if(nearHalls[index].nearArtists[index].preferencesName != "House"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }

        if(artistInHall.constraint == "LikePlayNearElectro"){
            if(nearHalls[index].nearArtists[index].preferencesName == "Electro"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearElectro"){
            if(nearHalls[index].nearArtists[index].preferencesName != "Electro"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }

        // famille de musique 2
        if(artistInHall.constraint == "LikePlayNearFunk"){
            if(nearHalls[index].nearArtists[index].preferencesName == "Funk"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearFunk"){
            if(nearHalls[index].nearArtists[index].preferencesName != "Funk"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }

        if(artistInHall.constraint == "LikePlayNearRap"){
            if(nearHalls[index].nearArtists[index].preferencesName == "Rap"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearRap"){
            if(nearHalls[index].nearArtists[index].preferencesName != "Rap"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }

        if(artistInHall.constraint == "LikePlayNearJazz"){
            if(nearHalls[index].nearArtists[index].preferencesName == "Jazz"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearJazz"){
            if(nearHalls[index].nearArtists[index].preferencesName != "Jazz"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }

        // famille de musique 3
        if(artistInHall.constraint == "LikePlayNearRock"){
            if(nearHalls[index].nearArtists[index].preferencesName == "Rock"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearRock"){
            if(nearHalls[index].nearArtists[index].preferencesName != "Rock"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }

        if(artistInHall.constraint == "LikePlayNearPunk"){
            if(nearHalls[index].nearArtists[index].preferencesName == "Punk"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearPunk"){
            if(nearHalls[index].nearArtists[index].preferencesName != "Punk"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }

        if(artistInHall.constraint == "LikePlayNearMetal"){
            if(nearHalls[index].nearArtists[index].preferencesName == "Metal"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearMetal"){
            if(nearHalls[index].nearArtists[index].preferencesName != "Metal"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }

    }
}
