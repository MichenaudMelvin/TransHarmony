using UnityEngine;

public class HallsAttributes : MonoBehaviour{

    public bool isBusy = false;

    public DragNDrop element;

    private void Update() {
        if(element != null && !element.inHall){
            isBusy = false;
        }
    }
}
