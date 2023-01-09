using Assets.SimpleLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLocalization : MonoBehaviour
{

    //public static LevelLocalization Instance { get; private set; }

    //[SerializeField] private Text _levelText;
    //public void ChangeLevelText() => _levelText.text = LocalizationManager.Localize("Game.LevelText", (Progress.Instance.Level).ToString("0"));

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(this);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //private void Start()
    //{  
    //    ChangeLevelText();
    //    LocalizationManager.LocalizationChanged += ChangeLevelText;
    //}

    //private void OnDestroy()
    //{
    //    if (Instance == this)
    //    {
    //        Instance = null;
    //        LocalizationManager.LocalizationChanged -= ChangeLevelText;
    //    }
    //}
}
