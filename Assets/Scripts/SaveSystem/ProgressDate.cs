[System.Serializable]
public class ProgressDate
{
    public int Coins;
    public int Level;
    public bool IsMusicOn;

    public ProgressDate(Progress progress)
    {
        Coins = progress.Coins;
        Level = progress.Level;
        IsMusicOn = progress.IsMusicOn; 
    }
}
