using System.Collections;
using UnityEngine;

public class Spot_material : MonoBehaviour
{
    [SerializeField] Material m_Material;
    
    void Start()
    {
        m_Material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        
        float _r = Random.Range(0f, 10f);
        float _g = Random.Range(0f, 10f);

        m_Material.SetFloat("_seed_x", _r);
        m_Material.SetFloat("_seed_y", _g);
       
    }

    //private IEnumerator  
}
