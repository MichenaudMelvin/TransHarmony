using UnityEngine;
using UnityEngine.Color;
using UnityEditor;

[CustomEditor(typeof(EmisiveSopt))]

public class EmisiveSopt : MonoBehaviour
{
   public Material spot_material;
  

   public void Color () {

      float h = Random.Range(0f, 1f);
      float s = Random.Range(0f, 1f);
      float v = Random.Range(0f, 1f);


      spot_material.color = new Color (h,s,v);
      
   }
   


}
