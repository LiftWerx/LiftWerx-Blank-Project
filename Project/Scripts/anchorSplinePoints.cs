using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Splines;

public class anchorSplinePoints : MonoBehaviour
{
    public SplineContainer MySpline;
    public int targetKnotIndex;
    public Transform knotTarget;

    private BezierKnot anchoredKnot;
    
    // Start is called before the first frame update
    void Start()
    {
        if (MySpline == null)
        {
            MySpline = gameObject.GetComponent<SplineContainer>();
        }
        anchoredKnot = MySpline.Spline.ToArray()[targetKnotIndex];

    }

    // Update is called once per frame
    void Update()
    {
        anchoredKnot.Position = MySpline.transform.InverseTransformPoint(knotTarget.position);
        anchoredKnot.Rotation = Quaternion.Inverse(MySpline.transform.rotation) * knotTarget.rotation;
        MySpline.Spline.SetKnot(targetKnotIndex,anchoredKnot);

    }
}
