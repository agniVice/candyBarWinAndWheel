using System;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    public static Audio Instance { get; private set; }

    public bool Sound {  get; private set; }
    public bool Music { get; private set; }

    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private GameObject _soundPrefab;

    [Header("Audio Clips")]
    public AudioClip Spin;
    public AudioClip StartSpin;
    public AudioClip Correct;
    public AudioClip SpinAdded;
    public AudioClip ItemSelected;
    public AudioClip ItemSetted;
    public AudioClip ItemBought;
    public AudioClip LastSpin;

    private void Awake()
    {
        if(Instance != this && Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        Sound = Convert.ToBoolean(PlayerPrefs.GetInt("Sound", 1));
        Music = Convert.ToBoolean(PlayerPrefs.GetInt("Music", 0));

        UpdateAll();
    }
    private void UpdateAll()
    {
        if (Sound)
            _audioMixer.SetFloat("Sound", 0f);
        else
            _audioMixer.SetFloat("Sound", -80f);
        if (Music)
            _audioMixer.SetFloat("Music", 0f);
        else
            _audioMixer.SetFloat("Music", -80f);
    }
    public void SoundToggle()
    {
        Sound = !Sound;
        UpdateAll();
        SaveAll();
    }
    public void MusicToggle()
    {
        Music = !Music;
        UpdateAll();
        SaveAll();
    }
    public void PlaySFX(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        if (Sound)
            Instantiate(_soundPrefab).GetComponent<Sound>().PlaySFX(clip, volume, pitch);
    }
    public void SaveAll()
    {
        PlayerPrefs.SetInt("Sound", Convert.ToInt32(Sound));
        PlayerPrefs.SetInt("Music", Convert.ToInt32(Music));
    }
}
