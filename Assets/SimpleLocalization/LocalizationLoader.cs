using Assets.SimpleLocalization;
using System;
//using UnityEditor.Localization.Editor;
using UnityEngine;

public class LocalizationLoader : MonoBehaviour {
    public const string EN = "English";
    public const string RU = "Russian";

    public void Awake() {
        DontDestroyOnLoad(this);

        LocalizationManager.Read();

        switch (PlayerPrefs.GetString("Lang", RU)) {
            case EN:
                SetLocalization(EN);
                break;
            default:
                SetLocalization(RU);
                break;
        }
    }

    public void SetLocalization(string localization) {
        PlayerPrefs.SetString("Lang", localization);

        LocalizationManager.Language = localization;
    }
}
