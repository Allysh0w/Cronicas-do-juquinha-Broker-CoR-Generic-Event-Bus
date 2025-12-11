using UnityEngine;

public class ChangeSong : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip song;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BossBattle"))
        {
            audioSource.clip = song;
            audioSource.Play();
        }

    }

}
