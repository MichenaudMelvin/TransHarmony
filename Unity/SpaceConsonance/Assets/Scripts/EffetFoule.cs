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
        bool hasChangeScale = false;

        while(_gameManager.GetCurrentPhase() == 2){

            if(!hasChangeScale && _audioSource.volume != 0){
                float ecart = _audioSource.volume/2.5f;
                float newHeightScale = Random.Range(_originalHeight-ecart, _originalHeight+ecart);
                this.transform.localScale = new Vector3(this.transform.localScale.x, newHeightScale, this.transform.localScale.z);
                hasChangeScale = true;
            }

            yield return new WaitForSeconds(0.001f);

            while(hasChangeScale){
                float actualScale = this.transform.localScale.y;
                if(_originalHeight <= actualScale){
                    while(_originalHeight <= this.transform.localScale.y){
                        this.transform.localScale -= new Vector3(0, 0.01f, 0);

                        yield return new WaitForSeconds(0.001f);
                    }

                    hasChangeScale = false;

                } else if(_originalHeight >= actualScale){
                    while(_originalHeight >= this.transform.localScale.y){
                        this.transform.localScale += new Vector3(0, 0.01f, 0);

                        yield return new WaitForSeconds(0.001f);
                    }

                    hasChangeScale = false;

                }
            }

            yield return new WaitForSeconds(0.001f);

        }

        yield return null;

    }

    // reset la hauteur de la foule
    public void ResetFoule(){
        this.transform.localScale = new Vector3(this.transform.localScale.x, _originalHeight, this.transform.localScale.z);
    }
}
