using UnityEngine;
using System.Collections.Generic;

public class ItemGenerator : MonoBehaviour{

    [SerializeField]
    [Tooltip("Liste de besoins des artistes (items)")]
    private List<string> _listNeeds;

    [SerializeField]
    [Tooltip("Liste contenant les sprites des items")]
    private List<Sprite> _spriteList;

    public void ToggleVisibility(bool visibility){
        for(int i = 0; i < this.transform.childCount; i++){
            this.transform.GetChild(i).gameObject.SetActive(visibility);
        }
    }

    public List<Sprite> GetSpriteList(){return _spriteList;}

    public List<string> GetListNeeds(){return _listNeeds;}
}