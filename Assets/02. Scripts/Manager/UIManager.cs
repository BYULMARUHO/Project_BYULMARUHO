using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

    private GameObject mainCanvas;
    public GameObject menuBoard;
    private Slider experienceSlider;
    private TextMeshProUGUI goldText;
    private TextMeshProUGUI timerText;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    private void Start()
    {
        mainCanvas = GameObject.Find("MainCanvas").gameObject;
        menuBoard = mainCanvas.transform.GetChild(2).gameObject;
        experienceSlider = mainCanvas.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Slider>();
        goldText = mainCanvas.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        timerText = mainCanvas.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuBoard.activeSelf)
                menuBoard.SetActive(false);
        }
    }

    public void ChangeCoinText(int _coin)
    {
        goldText.text = _coin.ToString();
    }

    public void ChangeExperienceSlider()
    {
        
    }

    public void ChangeTimerText(float _time)
    {
        timerText.text = string.Format("{0:00}:{1:00}", (int)(_time / 60), (int)(_time % 60));
    }
}