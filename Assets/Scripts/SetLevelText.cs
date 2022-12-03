using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetLevelText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    void Start()
    {
        _levelText.text = "Level " + Progress.Instance.Level;
    }

   
}
