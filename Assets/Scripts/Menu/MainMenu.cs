using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text _coinsText;
    [SerializeField] private Text _levelText;

    [SerializeField] private Button _startButton;

    private void Start()
    {
        _coinsText.text = "Coins: " + Progress.Instance.Coins;
        _levelText.text = "Level: " + Progress.Instance.Level;
        _startButton.onClick.AddListener(StartLevel);
    }

    private void StartLevel()
    {
        FadeManager.Instance.LoadGameScene();
        //SceneManager.LoadScene(Progress.Instance.Level);
    }

}
