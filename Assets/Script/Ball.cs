using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public bool IsSelected { get; set; } =true;
    private void OnCollisionEnter(Collision collision)
    {
        if (!_audioSource.isPlaying && !IsSelected)
        {
            _audioSource.Play();
        }
    }
}
