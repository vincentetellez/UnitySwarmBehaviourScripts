using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : AIBehaviour
{

    private VectorNavigation vn;

    public float weight = 1f;

    public GameObject objective;

    public float radius = 5f;

    private Vector3 dist;
    private Vector3 orbitalPlane;
    private float hiddenWeight;

    // Start is called before the first frame update
    void Start()
    {
        vn = GetComponentInParent<VectorNavigation>();

        orbitalPlane = Random.onUnitSphere;
    }

    // Update is called once per frame
    void Update()
    {
        dist = objective.transform.position - transform.position;
        hiddenWeight = dist.magnitude - radius;
        vn.AddHeading( 0.5f * weight * hiddenWeight * dist.normalized );
        vn.AddHeading( 0.5f * weight * Vector3.Cross( dist.normalized, orbitalPlane ) );
    }

    Vector3 GetOrbitalPlane() {
        return orbitalPlane;
    }

    void SetOrbitalPlane( Vector3 newPlane ) {
        orbitalPlane = newPlane;
    }
}
