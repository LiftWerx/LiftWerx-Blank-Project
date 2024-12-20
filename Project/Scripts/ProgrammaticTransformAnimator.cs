using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgrammaticTransformAnimator : MonoBehaviour
{

    private CustomGrabInteractable grabInteractable;
    public bool canDisplace = true;
    public bool canRotate = true;
    public bool reenableInteractable = true;
    public bool destroyOnFinish = false;
    public bool playOnStart = false;
    [System.Serializable]
    public class AnimationStep
    {
        public Vector3 displace;
        public Vector3 targetRotation;
        public float duration;
        public float pauseDurationAfterStep = 0;
        public UnityEvent eventPlayedBeforePause;
    }

    public List<AnimationStep> animationSteps = new List<AnimationStep>();

    private void Start()
    {
        grabInteractable = GetComponent<CustomGrabInteractable>();
        if(playOnStart){
            Play();
         }
    }

    public void Play()
    {
        if (grabInteractable != null) grabInteractable.enabled = false;
        StartCoroutine(PlaySequentialSteps(animationSteps));
        
    }

    private IEnumerator PlaySequentialSteps(List<AnimationStep> steps)
    {

        foreach (AnimationStep step in animationSteps)
        {
            
            float time = 0;

            // Displacement calculations
            Vector3 startPosition = this.transform.position;
            Vector3 destination = new Vector3(startPosition.x + step.displace.x,
                startPosition.y + step.displace.y, startPosition.z + step.displace.z);

            // Rotation Calculations
            Vector3 targetAngles = step.targetRotation;

            Quaternion startRotation = this.transform.rotation;
            Quaternion endRotation = this.transform.rotation * Quaternion.Euler(targetAngles);

            while (time < step.duration)
            {
                // apply displacement
                if (canDisplace) this.transform.position = Vector3.Lerp(startPosition, destination, time / step.duration);
                // apply rotation
                if (canRotate) this.transform.rotation = Quaternion.Lerp(startRotation, endRotation, time / step.duration);

                time += Time.deltaTime;
                yield return null;
            }

            if (canDisplace) transform.position = destination;
            if (canRotate) transform.rotation = endRotation;

            step.eventPlayedBeforePause.Invoke();

            yield return new WaitForSeconds(step.pauseDurationAfterStep);
        }

        if (destroyOnFinish) Destroy(this.gameObject);
        if (grabInteractable != null && reenableInteractable) grabInteractable.enabled = true;
    }

    public void SetCanDisplace(bool can)
    {
        canDisplace = can;
    }

    public void SetCanRotate(bool can)
    {
        canRotate = can;
    }

}
