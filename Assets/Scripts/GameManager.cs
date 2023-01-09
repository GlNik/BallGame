using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _winObject;
    [SerializeField] private GameObject _loseObject;

    public UnityEvent OnWin;

    public static GameManager Instance { get; private set; }
    [SerializeField] private Text _levelText;
    public void ChangeLevelText() => _levelText.text = LocalizationManager.Localize("Game.LevelText", (Progress.Instance.Level).ToString("0"));

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeLevelText();
        LocalizationManager.LocalizationChanged += ChangeLevelText;
    }

    public void Win()
    {
        _winObject.SetActive(true);
        OnWin.Invoke();
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        Progress.Instance.SetLevel(currentLevelIndex + 1);
        ChangeLevelText();
        Progress.Instance.AddCoins(50);
    }

    public void Lose()
    {
        _loseObject.SetActive(true);
    }

    public void NextLevel()
    {
        FadeManager.Instance.LoadGameScene();
        // SceneManager.LoadScene(Progress.Instance.Level);
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        // SceneManager.LoadScene(0);
        FadeManager.Instance.LoadMainMenu();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        FadeManager.Instance.Restart();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
            LocalizationManager.LocalizationChanged -= ChangeLevelText;
        }
    }
}
