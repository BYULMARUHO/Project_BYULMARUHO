using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

    public TextMeshProUGUI goldText;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    private void Start()
    {
        
    }

    public void ChangeCoinText(int _coin)
    {
        goldText.text = _coin.ToString();
    }
}