using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekConstant : MonoBehaviour
{

    private VectorNavigation vn;

    public float weight = 1f;

    public GameObject objective;

    private Vector3 dist;

    // Start is called before the first frame update
    void Start()
    {
        vn = GetComponentInParent<VectorNavigation>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = objective.transform.position - transform.position;
        vn.AddHeading( weight * dist.normalized );
    }
}
