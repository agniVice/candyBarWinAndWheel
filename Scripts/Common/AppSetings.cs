using UnityEngine;

public class AppSetings : MonoBehaviour
{
    public static AppSetings Instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        if (Instance != null && Instance != this)
            Destroy(gameObject);
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
