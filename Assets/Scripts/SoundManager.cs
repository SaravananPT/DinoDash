using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Clips")]
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip coinSound;
    public AudioClip reviveSound;
    public AudioClip backgroundMusic;

    private AudioSource sfxSource;
    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        AudioSource[] sources = GetComponents<AudioSource>();
        sfxSource = sources[0];
        musicSource = sources[1];
    }

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayJump() => sfxSource.PlayOneShot(jumpSound);
    public void PlayDeath() => sfxSource.PlayOneShot(deathSound);
    public void PlayCoin() => sfxSource.PlayOneShot(coinSound);
    public void PlayRevive() => sfxSource.PlayOneShot(reviveSound);
}
