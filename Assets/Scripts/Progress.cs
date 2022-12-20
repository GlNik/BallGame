using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public int Coins;
    public int Level;
    public bool IsMusicOn;
    public static Progress Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        Load();
    }

    public void SetLevel(int level)
    {
        Level = level;
        Save();
    }

    public void AddCoins(int value)
    {
        Coins += value;
        Save();
    }

    [ContextMenu("Save")]
    public void Save()
    {
        SaveSystem.Save(this);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        ProgressDate progressDate = SaveSystem.Load();
        if (progressDate != null)
        {
            Coins = progressDate.Coins;
            Level = progressDate.Level;

            IsMusicOn = progressDate.IsMusicOn;
        }
        else
        {
            Coins = 0;
            Level = 1;
            IsMusicOn = true;
        }
    }

    [ContextMenu("Reset")]
    public void DeleteFile()
    {
        Coins = 0;
        Level = 1;
        IsMusicOn = true;
        Save();
        // SaveSystem.DeleteFile();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
