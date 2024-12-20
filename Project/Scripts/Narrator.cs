using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using static PlaySteps;

public class Narrator : MonoBehaviour
{
    public PlayableDirector director;
    private double lastCheckpoint = 0;

    private void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    [Tooltip("Stores the current time on the Director's timeline")]
    public void SetCheckpoint()
    {
        lastCheckpoint = director.time;
    }

    [Tooltip("Plays the director's timeline at the most recently stored time")]
    public void PlayAtCheckPoint()
    {
        director.Stop();
        director.time = lastCheckpoint;
        director.Play();
    }

}
