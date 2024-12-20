using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screw : MonoBehaviour
{
    public GameObject screwMesh;
    [SerializeField] private float torqueRate = 100f;
    public float currentTorque = 0;
    public float targetTorque = 1000f;
    public bool canTighten = true;
    public Image progressRadialImage;
    public TMP_Text progressText;
    
    public RectTransform canvasTransform;
    public float duration = 1.0f;
    
    [SerializeField] private bool stepOnComplete = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canTighten && currentTorque >= targetTorque)
        {
            canTighten = false;
            currentTorque = targetTorque;
            StartCoroutine(AnimateScaleDown());
            if (stepOnComplete && gameObject.TryGetComponent<Stepper>(out Stepper stepper))
            {
                stepper.TryNextStep();
            }
        }
    }

    public void Tighten()
    {
        if (canTighten)
        {
            screwMesh.transform.Rotate(Vector3.up * (50f * Time.deltaTime));
            currentTorque += (torqueRate * Time.deltaTime);
            progressRadialImage.fillAmount = currentTorque / targetTorque;
            progressText.text = "Current Torque: " + currentTorque.ToString("0.0") + "\nTarget Torque: " + targetTorque.ToString("0.0"); 
        }
    }
    
    private IEnumerator AnimateScaleDown()
    {
        // Set initial scale to 100%
        canvasTransform.localScale = Vector3.one;
        yield return new WaitForSeconds(1f);

        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float scale = Mathf.Lerp(1f, 0f, timeElapsed / duration);
            canvasTransform.localScale = new Vector3(scale, scale, scale);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        
        canvasTransform.localScale = Vector3.zero;
        Destroy(canvasTransform.gameObject, 1f);
        
    }
}
