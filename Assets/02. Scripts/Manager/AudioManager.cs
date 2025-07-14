using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance {  get { return instance; } }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }
}
