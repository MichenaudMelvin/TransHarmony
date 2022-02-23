using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollingMenu : MonoBehaviour{

    [Header("Scrolling button")]
    [Tooltip("Si le menu est scrollé ou non")]
    private bool _hasScroll = false;

    [SerializeField]
    [Tooltip("Bouton qui permet le scroll")]
    private Button _scrollButton;

    [SerializeField]
    [Tooltip("Scrolling menu")]
    private Image _scrollingMenu;

    [SerializeField]
    [Tooltip("Fade out image")]
    private Image _fadeOutImage;

    [SerializeField]
    [Tooltip("Position jusqu'à laquelle le menu va se déplacer")]
    private Vector2 _scrolledPosition;

    [Tooltip("Position initiale")]
    private Vector2 _initialPosition;

    [Tooltip("Pour savoir si la coroutine est lancé")]
    private bool _hasScrollCoroutineStart = false;

    [SerializeField]
    [Tooltip("Référence au GameManager")]
    private GameManager _gameManager;

    private void Start(){
        _scrollButton.onClick.AddListener(TaskOnClickScrollButton);
        _initialPosition = _scrollButton.GetComponent<RectTransform>().anchoredPosition;
    }

    private void TaskOnClickScrollButton(){
        if(!_hasScrollCoroutineStart){
            StartCoroutine(this.Scroll());
        }
    }

    // animation de scroll du menu
    private IEnumerator Scroll(){
        int sens = 1;
        Vector2 positionToGo;
        Color tempColor;
        _hasScrollCoroutineStart = true;
        bool hasFadeImage = false;

        // need to set game pause
        // also mask command et item

        if(!_hasScroll){
            sens = 1;
            positionToGo = _scrolledPosition;
            _fadeOutImage.gameObject.SetActive(true);
            tempColor = _fadeOutImage.color;
            _gameManager.PauseGame(true);
        } else{
            sens = -1;
            positionToGo = _initialPosition;
            tempColor = _fadeOutImage.color;
            _gameManager.PauseGame(false);
        }

        while(_scrollButton.GetComponent<RectTransform>().anchoredPosition != positionToGo){
            _scrollButton.GetComponent<RectTransform>().anchoredPosition += (sens * new Vector2(0, -1f));
            _scrollingMenu.GetComponent<RectTransform>().anchoredPosition += (sens * new Vector2(0, -1f));

            if(!hasFadeImage){
                hasFadeImage = true;
                StartCoroutine(this.Fade(tempColor, sens));
            }

            if(!_hasScroll && -_scrollButton.GetComponent<RectTransform>().anchoredPosition.y >= positionToGo.y){
                _scrollButton.GetComponent<RectTransform>().anchoredPosition = -_scrolledPosition;
                _scrollingMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(_scrollingMenu.GetComponent<RectTransform>().anchoredPosition.x, _scrollButton.GetComponent<RectTransform>().anchoredPosition.y + _scrollButton.GetComponent<RectTransform>().sizeDelta.y);
                _hasScroll = true;
                _hasScrollCoroutineStart = false;
                yield break;
            } else if(_hasScroll && _scrollButton.GetComponent<RectTransform>().anchoredPosition.y >= positionToGo.y){
                _scrollButton.GetComponent<RectTransform>().anchoredPosition = _initialPosition;
                _scrollingMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(_scrollingMenu.GetComponent<RectTransform>().anchoredPosition.x, _scrollButton.GetComponent<RectTransform>().anchoredPosition.y + _scrollButton.GetComponent<RectTransform>().sizeDelta.y);
                _hasScroll = false;
                _fadeOutImage.gameObject.SetActive(false);
                _hasScrollCoroutineStart = false;
                yield break;
            }

            yield return new WaitForSeconds(0.001f);
        }
    }

    private IEnumerator Fade(Color tempColor, int sens){
        if(sens == 1){
            while(_fadeOutImage.color.a < 0.5f){
                tempColor.a += sens * 0.01f;
                _fadeOutImage.color = tempColor;
                yield return new WaitForSeconds(0.005f);
            }
        } else if(sens == -1){
            while(_fadeOutImage.color.a > 0f){
                tempColor.a += sens * 0.01f;
                _fadeOutImage.color = tempColor;
                yield return new WaitForSeconds(0.005f);
            }
        }
    }

    public bool GetHasScroll(){return _hasScroll;}
}
