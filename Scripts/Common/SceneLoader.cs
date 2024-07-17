using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private int _id;
    private float _delay;

    private float _delayClose;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(this);
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    public void ChangeScene(int id, float delay = 0)
    {
        _id = id;
        _delay = delay;

        StartCoroutine("LoadSceneWithDelay");
    }
    private IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(_id);
    }
    public void CloseApp(float delay)
    { 
        _delayClose = delay;
        StartCoroutine("CloseAppWithDelay");
    }
    private IEnumerator CloseAppWithDelay()
    {
        yield return new WaitForSeconds(_delayClose);
        Application.Quit();
    }
}
