using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ButtonMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void Click()
    {
        _audio.Play();
    }
}
