using UnityEngine;
using UnityEngine.UI;

public class SetLevelText : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Text _levelText;
    void Start()
    {
        _levelText.text = "Level " + Progress.Instance.Level;
    }

   
}
