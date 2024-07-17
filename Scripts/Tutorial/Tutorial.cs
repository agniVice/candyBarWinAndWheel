using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance {get; private set;}

    public GameObject[] Cursors;

    private void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
}
