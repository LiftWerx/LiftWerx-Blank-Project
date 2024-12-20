using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour//Script should be attached to it's own Empty
    // This scrip is used to organize & manage audio files and allow for easy management of sound profiles
{

    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) //Checks if there is AudioManager already in the scene 
            instance = this;
        else
        {
            Destroy(gameObject); //Removes AudioManager if there is an alternative AudioManager 
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
        foreach (Sound s in sounds) //Copies sound profiles and applies it to clip 
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }    
    }

    public void Play (string name)//For referencing and typos 
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " Not found");
            return;
        }

        s.source.Play();

    }
}
