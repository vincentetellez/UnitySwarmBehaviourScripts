using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidExponential : MonoBehaviour
{

    private VectorNavigation vn;

    public float weight = 1f;

    public GameObject objective;

    private Vector3 dist;
    private float hiddenWeight;

    // Start is called before the first frame update
    void Start()
    {
        vn = GetComponentInParent<VectorNavigation>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = objective.transform.position - transform.position;
        hiddenWeight = -1 / Mathf.Max( dist.sqrMagnitude, 0.001f );
        vn.AddHeading( weight * hiddenWeight * dist.normalized );
    }
}
