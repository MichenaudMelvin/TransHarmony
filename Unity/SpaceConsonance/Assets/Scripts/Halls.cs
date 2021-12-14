using UnityEngine;
using System.Collections.Generic;

public class Halls : MonoBehaviour{

    public List<HallsAttributes> hallList;

    public void PiecePlaced(DragNDrop piece){
        for(int i = 0; i < hallList.Count; i++){
            if(!hallList[i].isBusy){
                if(Mathf.Abs(piece.transform.position.x - hallList[i].transform.position.x) <= 50f && Mathf.Abs(piece.transform.position.y - hallList[i].transform.position.y) <= 50f && hallList[i].transform.eulerAngles.y ==0){
                    piece.transform.position = new Vector3(hallList[i].transform.position.x, hallList[i].transform.position.y, hallList[i].transform.position.z);
                    hallList[i].isBusy = true;
                    hallList[i].element = piece;
                    piece.inHall = true;
                }
            }
        }
    }
}
