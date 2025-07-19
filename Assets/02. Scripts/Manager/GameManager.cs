using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }

    private int level = 1;
    private int coin = 100;
    private int experience = 0;
    private int maxExperience = 100;

    private bool isGameOver = false;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance);
        else
            instance = this;

        Init();
    }

    private void Init()
    {
        Screen.SetResolution(1920, 1080, true);
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

    // 경험치 변동
    public void ExperienceHandler(int _exp)
    {
        if(experience + _exp >= maxExperience)
        {
            experience = maxExperience;
        }
    }
}