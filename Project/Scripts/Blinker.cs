using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Blinker : MonoBehaviour
{

    Transform child;
    [SerializeField]
    private int blinks = 3;
    [SerializeField]
    private float timeBetweenBlinks = 0.3f;
    [SerializeField]
    private float initialDelay = 1;
    [SerializeField]
    private bool destroyOnEnd = false;
    [SerializeField]
    private float hideDelay = 1;

    void Start()
    {
        // Ensure there's only one child, then get its transform
        if (transform.childCount == 1)
        {
            child = transform.GetChild(0);
        }
        else
        {
            Debug.LogError("This script should be attached to a GameObject with exactly one child!");
            enabled = false; // Disable the script if there's not exactly one child
        }
    }

    public void Blink()
    {
        StartCoroutine(BlinkCoroutine());
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    private IEnumerator BlinkCoroutine()
    {
        yield return new WaitForSeconds(initialDelay);

        for (int i = 1; i < blinks; i++)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            child.gameObject.SetActive(false);
            yield return new WaitForSeconds(timeBetweenBlinks);
        }

        child.gameObject.SetActive(true);
        yield return new WaitForSeconds(hideDelay);
        child.gameObject.SetActive(false);

        if (destroyOnEnd)
        {
            Destroy();
        }
    }


}
