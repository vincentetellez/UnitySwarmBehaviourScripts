using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekConstant : MonoBehaviour
{

    private VectorNavigation vn;

    public float weight = 1f;

    public bool senseObjectives = false;

    public string seekTag;

    public float senseFrequency = 0.1f;

    public GameObject[] objectives;

    private Vector3 dist;
    private float hiddenWeight;
    private float divZeroAccuracy = 0.001f;

    // Start is called before the first frame update

    void Start()
    {
        vn = GetComponentInParent<VectorNavigation>();
    }

    // Update is called once per frame

    void Update()
    {
        if ( senseObjectives ) {
            if ( Random.value < senseFrequency * Time.deltaTime ) {
                QueryEnvironment();
            }
        }

        for ( int i = 0; i < objectives.Length; i++ ) {
            dist = objectives[i].transform.position - transform.position;
            hiddenWeight = 1 / Mathf.Max( dist.sqrMagnitude, divZeroAccuracy );
            vn.AddHeading( weight * hiddenWeight * dist.normalized / objectives.Length );
        }
    }
    
    void QueryEnvironment() {
        try {
            objectives = GameObject.FindObjectsWithTag( seekTag );
        }
        catch ( UnityException e ) {
            Debug.Log( e );
            Debug.Log( "Use a valid seek tag for this script." );
        }
    }
}
