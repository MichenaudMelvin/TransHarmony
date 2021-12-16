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

                // nearHalls[0].nearArtists.RemoveAt(0); // problème ici

                // possibilité de kill cette fonctionnalité (retirer l'artiste d'un hall, si la personne s'est trompé de hall, pas censé etre pénalisant)
                // veut dire qu'une fois qu'un artiste est placé dans un hall, impossible de le bouger
                // si c'est le cas --> l'indiquer au debut du jeu

                // marche a peus pres sauf pour le hall 4
                // s'update si on l'enleve puis le remet
                element = null;
                artistInHall.validConstraint = false;
                artistInHall = null;
                artistPlaced = false;
                nearArtists.Clear();
            }
        }

        if(!artistPlaced){
            for(int i = 0; i < nearHalls.Count; i++){
                if(element != null && nearHalls[i] != null && nearHalls[i].artistInHall != null){
                    nearArtists.Add(nearHalls[i].artistInHall);

                    // check si l'artiste est pas deja dans la liste
                    // pose problème
                    // print(i);
                    // print(nearHalls[i]);
                    // print(nearHalls[i].artistInHall);
                    // print(nearArtists[i]);

                    // pas parfait, marche quand le hall est près d'un seul autre hall
                    // si le hall est près de plusieurs autres halls (uniquement le Hall 4) marche pas, rempli la liste nearArtists avec le meme artiste
                    if(nearHalls[i].artistInHall == nearArtists[i]){
                        artistPlaced = true;
                    } else{
                        artistPlaced = false;
                    }

                    // if(nearHalls.Count == nearArtists.Count){
                    //     artistPlaced = true;
                    // }

                    if(artistInHall != null && !artistInHall.validConstraint){
                        this.CheckArtistConstraint(i);
                    }
                }
            }
        }
    }

    private void CheckArtistConstraint(int index){
        // en gros ça, pour multiplier ça [Shift+Alt+Up/Down] et remplacer le style de musique
        // 3 familles de musiques
        // 9 styles de musiques
        // normalement c'est bon
        // téma la taille du code

        // famille de musique 1
        if(artistInHall.constraint == "LikePlayNearTechno"){
            if(nearArtists[index].preferencesName == "Techno"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearTechno"){
            if(nearArtists[index].preferencesName != "Techno"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }

        else if(artistInHall.constraint == "LikePlayNearHouse"){
            if(nearArtists[index].preferencesName == "House"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearHouse"){
            if(nearArtists[index].preferencesName != "House"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }

        else if(artistInHall.constraint == "LikePlayNearElectro"){
            if(nearArtists[index].preferencesName == "Electro"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearElectro"){
            if(nearArtists[index].preferencesName != "Electro"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }

        // famille de musique 2
        else if(artistInHall.constraint == "LikePlayNearFunk"){
            if(nearArtists[index].preferencesName == "Funk"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearFunk"){
            if(nearArtists[index].preferencesName != "Funk"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }

        else if(artistInHall.constraint == "LikePlayNearRap"){
            if(nearArtists[index].preferencesName == "Rap"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearRap"){
            if(nearArtists[index].preferencesName != "Rap"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }

        else if(artistInHall.constraint == "LikePlayNearJazz"){
            if(nearArtists[index].preferencesName == "Jazz"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearJazz"){
            if(nearArtists[index].preferencesName != "Jazz"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }

        // famille de musique 3
        else if(artistInHall.constraint == "LikePlayNearRock"){
            if(nearArtists[index].preferencesName == "Rock"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearRock"){
            if(nearArtists[index].preferencesName != "Rock"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }

        else if(artistInHall.constraint == "LikePlayNearPunk"){
            if(nearArtists[index].preferencesName == "Punk"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearPunk"){
            if(nearArtists[index].preferencesName != "Punk"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }

        else if(artistInHall.constraint == "LikePlayNearMetal"){
            if(nearArtists[index].preferencesName == "Metal"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        } else if(artistInHall.constraint == "DislikePlayNearMetal"){
            if(nearArtists[index].preferencesName != "Metal"){
                artistInHall.validConstraint = true;
            } else{
                artistInHall.validConstraint = false;
            }
        }
    }
}
