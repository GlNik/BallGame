using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    private bool _pauseGame;
    [SerializeField] private GameObject _pauseGameMenu;
    [SerializeField] MonoBehaviour[] _componentsToDisable;
  
    private void Start()
    {
        Time.timeScale = 1f;
    }
    //void Update()
    //{
    //    //if (Input.GetKeyDown(KeyCode.Escape))
    //    //{
    //    //    if (_pauseGame)
    //    //    {
    //    //        Resume();
    //    //    }
    //    //    else
    //    //    {
    //    //        Pause();
    //    //    }
    //    //}
       
    //}

    public void Resume()
    {
        _pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        _pauseGame = false;
        for (int i = 0; i < _componentsToDisable.Length; i++)
        {
            _componentsToDisable[i].enabled = true;
        }
    }

    public void Pause()
    {
        _pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        _pauseGame = true;
        for (int i = 0; i < _componentsToDisable.Length; i++)
        {
            _componentsToDisable[i].enabled = false;
        }
    } 

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
