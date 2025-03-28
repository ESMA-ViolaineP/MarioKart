using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("AudioSources")]
    [SerializeField] private AudioSource _musicSource;

    private void Start()
    {
        _musicSource.Play();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus && !_musicSource.isPlaying)
        {
            _musicSource.Play();
        }
    }
}
