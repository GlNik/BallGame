using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    private CanvasGroup _fade;
    private Tween _tween;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;

        _fade = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _fade.alpha = 1;
        HideFade();
        if (_tween != null)
        {
            _tween.Play();
        }
    }

    private void OnDisable()
    {
        if (_tween != null)
        {
            _tween.Pause();
        }
    }

    public void ShowFade()
    {
        _fade.DOFade(1, 1);
    }

    public void HideFade()
    {
        _fade.DOFade(0, 1).SetDelay(1);
    }

    public void LoadGameScene()
    {
        _tween = _fade.DOFade(1, 1);
        _tween.onComplete += () =>
        {
            //???? ??????? +1 ??? ?????? ? ????
            if (Progress.Instance.Level==52)
            {
                LoadMainMenu();
            }
            else
            {
                SceneManager.LoadScene(Progress.Instance.Level);
            }
        };
    }

    public void LoadMainMenu()
    {
        _tween = _fade.DOFade(1, 1);
        _tween.onComplete += () =>
        {
            SceneManager.LoadScene(0);
        };
    }

    public void Restart()
    {
        _tween = _fade.DOFade(1, 1);
        _tween.onComplete += () =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        };
    }

    private void OnDestroy()
    {
        DOTween.Kill(_tween);
    }
}
