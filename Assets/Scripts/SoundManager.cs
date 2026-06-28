using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("SFX")]
    public AudioClip jumpClip;
    public AudioClip deathClip;
    public AudioClip coinClip;
    public AudioClip reviveClip;
    public AudioClip swipeClip;
    public AudioClip slideClip;

    [Header("Music")]
    public AudioClip[] worldMusics;

    private AudioSource sfx;
    private AudioSource music;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        AudioSource[] sources = GetComponents<AudioSource>();
        sfx = sources[0];
        music = sources[1];
    }

    private void Start()
    {
        PlayWorldMusic(0);
    }

    public void PlayWorldMusic(int worldIndex)
    {
        if (worldIndex >= worldMusics.Length) return;
        music.clip = worldMusics[worldIndex];
        music.loop = true;
        music.Play();
    }

    public void PlayJump()  => sfx.PlayOneShot(jumpClip);
    public void PlayDeath() => sfx.PlayOneShot(deathClip);
    public void PlayCoin()  => sfx.PlayOneShot(coinClip);
    public void PlayRevive()=> sfx.PlayOneShot(reviveClip);
    public void PlaySwipe() => sfx.PlayOneShot(swipeClip);
    public void PlaySlide() => sfx.PlayOneShot(slideClip);
}
