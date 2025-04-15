using TMPro;
using UnityEngine;

public class EmptyBoxCounter : MonoBehaviour
{
    [SerializeField] private LayerMask _ballMask;

    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    [SerializeField] private AudioClip _scoreIncrease, _scoreDecrease;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (((1 << collider.gameObject.layer) & _ballMask) != 0)
        {
            int.TryParse(_textMeshProUGUI.text, out var currentScore);
            _textMeshProUGUI.text = (++currentScore).ToString();
            if (!_audioSource.isPlaying || !_audioSource.clip == _scoreIncrease)
            {
                _audioSource.clip = _scoreIncrease;
                _audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (((1 << collider.gameObject.layer) & _ballMask) != 0)
        {
            int.TryParse(_textMeshProUGUI.text, out var currentScore);
            _textMeshProUGUI.text = (--currentScore).ToString();
            if (!_audioSource.isPlaying || !_audioSource.clip == _scoreDecrease)
            {
                _audioSource.clip = _scoreDecrease;
                _audioSource.Play();
            }
        }
    }
}
