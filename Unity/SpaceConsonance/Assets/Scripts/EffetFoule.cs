using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class EffetFoule : MonoBehaviour{

    [SerializeField]
    [Tooltip("Audio source du jeu")]
    private AudioSource _audioSource;

    [SerializeField]
    [Tooltip("Référence au GameManager")]
    private GameManager _gameManager;

    [SerializeField]
    [Tooltip("A quel hall appartient la foule")]
    private int currentHallFoule;

    [Space(5)]

    [Tooltip("Hauteur originale de l'objet")]
    private float _originalHeight;



    private IEnumerator Start(){
        _originalHeight = this.transform.localScale.y;
        yield return new WaitForSeconds(2f);
        StartCoroutine(this.FouleMovement());
    }

    // public functions
    // fait bouger le publique en fonction des actions du joueurs
    public IEnumerator FouleMovement(){
        while(_gameManager.currentPhase == 2){

            float ecart = _audioSource.volume/2.5f;
            float newHeightScale = Random.Range(_originalHeight-ecart, _originalHeight+ecart);
            transform.localScale = new Vector3(this.transform.localScale.x, newHeightScale, this.transform.localScale.z);

            yield return new WaitForSeconds(0.03f);
        }

        yield return null;

    }

    // reset la hauteur de la foule
    public void ResetFoule(){
        this.transform.localScale = new Vector3(this.transform.localScale.x, _originalHeight, this.transform.localScale.z);
    }
}
