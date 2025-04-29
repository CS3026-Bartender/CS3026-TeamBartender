using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PersistentManager<AudioManager>
{
    private Dictionary<string, Sound> sounds;
    [SerializeField] private float masterVolume = 1f;
    public float MasterVolume { get => masterVolume; set => masterVolume = value; }

    private void Start()
    {
        sounds = new Dictionary<string, Sound>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject sourceObj = transform.GetChild(i).gameObject;
            Sound sound = sourceObj.GetComponent<Sound>();
            if (sound != null)
            {
                sounds.Add(sound.ID, sound);
            }
        }
    }

    public void PlaySound(string id)
    {
        Sound sound = sounds[id];
        if (sound != null)
        {
            sound.PlaySound();
        }
    }
}
