using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] public string ID { get; private set; }
    [SerializeField] private float volume;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;

    private void Start()
    {
        source.volume = volume;
    }

    public void PlaySound()
    {
        GetNewPitch();
        source.Play();
    }

    private void GetNewPitch()
    {
        if (minPitch != maxPitch)
        {
            float newPitch = Random.Range(minPitch, maxPitch);
            source.pitch = newPitch;
        }
    }
}
