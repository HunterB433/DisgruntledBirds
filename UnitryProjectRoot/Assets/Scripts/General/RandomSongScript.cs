using UnityEngine;

public class RandomSongScript : MonoBehaviour
{
    public AudioClip[] audioClips; // Array to hold your audio clips
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioClips.Length > 0)
        {
            PlayRandomSong();
        }
    }

    void PlayRandomSong()
    {
        int randomIndex = Random.Range(0, audioClips.Length);
        audioSource.clip = audioClips[randomIndex];
        audioSource.Play();
    }
}
