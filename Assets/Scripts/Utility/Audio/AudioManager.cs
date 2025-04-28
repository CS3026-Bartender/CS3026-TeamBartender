using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private Dictionary<string, Sound> sounds;

    private void Start()
    {
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
