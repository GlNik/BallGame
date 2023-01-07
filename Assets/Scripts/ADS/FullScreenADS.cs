using System.Runtime.InteropServices;
using UnityEngine;

public class FullScreenADS : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowFullScreenReclm();

    [DllImport("__Internal")]
    private static extern void ShowRewardedVideoInGame();

    public static FullScreenADS Instance { get; private set; }

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


    void Start()
    {
        ShowFullScreenADS();
    }

    public void ShowFullScreenADS()
    {
        ShowFullScreenReclm();
    }

    public void ShowRewardedVideo()
    {
        SoundSettingGame.Instance.MusicPause();       
        ShowRewardedVideoInGame();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

}
