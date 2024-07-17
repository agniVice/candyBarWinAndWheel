using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    public void PlaySFX
        (AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        _source.clip = clip;
        _source.volume = volume;
        _source.pitch = pitch;
        _source.Play();

        Destroy(gameObject, clip.length + 1f);
    }
}
