using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _winObject;
    [SerializeField] private GameObject _loseObject;

    public UnityEvent OnWin;

    public static GameManager Instance { get; private set; }

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

    public void Win()
    {
        _winObject.SetActive(true);
        OnWin.Invoke();
    }

    public void Lose()
    {
        _loseObject.SetActive(true);
    }


    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
