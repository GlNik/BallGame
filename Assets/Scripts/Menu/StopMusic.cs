using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusic : MonoBehaviour
{
    public void MusicPause()
    {
        SoundSettingGame.Instance.MusicPause();
    }
}
