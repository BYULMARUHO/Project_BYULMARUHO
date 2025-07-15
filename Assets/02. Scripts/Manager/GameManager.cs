using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }

    private int level = 1;
    private int coin = 100;
    private int experience = 0;

    private bool isGameOver = false;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        else
            instance = this;

        Application.targetFrameRate = 65;
    }

    private void Start()
    {
        
    }

    // 재화 변동
    public bool CoinHandler(int _gold)
    {
        if(coin + _gold < 0)
        {
            Debug.Log("::: 재화 부족 :::");
            return false;
        }

        coin += _gold;
        UIManager.Instance.ChangeCoinText(coin);
        return true;
    }
}