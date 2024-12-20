using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class PlaySteps : MonoBehaviour
{

    // Creates a singleton
    public static PlaySteps Instance;

    [Tooltip ("When enabled, a step will not play if the step before it has not played")]
    public bool sequenced = true;

    PlayableDirector director;

    [Tooltip("A list of times on the timeline")]
    public List<Step> steps;

    public int currentStep = 0;

    public bool playStepIndexSuccess = false;


    // Start is called before the first frame update
    void Start()
    {
        // creates a singleton
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        director = GetComponent<PlayableDirector>();
        //PlayStepIndex(currentStep);
        
    }

    [System.Serializable]
    public class Step
    {
        public string name;
        public float time;
        public bool hasPlayed = false;
    }

    [Tooltip("Plays the timeline from the given step, step indexes start at 0")]
    public void PlayStepIndex(int index)
    {
        if (sequenced && index != 0 && !steps[index - 1].hasPlayed) return;


        
        Step step = steps[index];

        if (!step.hasPlayed)
        {
            
            step.hasPlayed = true;

            director.Pause();
            director.time = step.time + 0.01f;

            director.Play();
            Debug.Log(director.time);
            playStepIndexSuccess = true;
        }
    }

    public void PlayNextStep()
    {
        PlayStepIndex(currentStep);
        currentStep++;
    }

}
