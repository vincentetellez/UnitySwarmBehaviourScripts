using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmBasic : MonoBehaviour
{

    private VectorNavigation vn;

	private static List<List<GameObject>> swarmList;	// A list of swarms which currently exist

	public int swarmNumber = 0;				           // Which swarm in the swarm list this entity belongs to

	private Vector3 separation;
	private Vector3 alignment;
	private Vector3 cohesion;

	public float separationWeight = 1f;
	public float alignmentWeight = 1f;
	public float cohesionWeight = 1f;

	public float preferredSeparation = 25f;			   // Calculations are done using square magnitudes, not magnitudes

	// Buffer variables to avoid having to constantly allocate space
	private GameObject neighbor;
	private Vector3 dist;

	void Start() {

        // Grab a reference to the script which performs movement
		vn = GetComponentInParent<VectorNavigation>();

        // During startup, initialize the list of swarms and add this object to the appropriate swarm
        if ( swarmList == null ) {
            swarmList = new List<List<GameObject>>();
        }
        while ( swarmList.Count - 1 < swarmNumber ) {
            swarmList.Add( new List<GameObject>() );
        }
        swarmList[swarmNumber].Add( this.gameObject );

	}

	void Update() {

		CalcSeparation();
		CalcAlignment();
		vn.SetHeading( separationWeight * separation + alignmentWeight * alignment );
	}

	void CalcSeparation() {

		// Calculates the separation between all swarm neighbors so that a proper distance can be maintained. This method takes care of separation and cohesion at the same time

		separation = Vector3.zero;
		for ( int i = 0; i < swarmList[swarmNumber].Count; i++ ) {
			neighbor = swarmList[swarmNumber][i];
			if ( neighbor != this ) {
				dist = neighbor.transform.position - transform.position;
				separation += (dist.sqrMagnitude - preferredSeparation) * dist.normalized;
			}
		}
	}

	void CalcAlignment() {

		// Calculates the heading of all swarm neighbors to align with the swarms average direction

		alignment = Vector3.zero;
		for ( int i = 0; i < swarmList[swarmNumber].Count; i++ ) {
			neighbor = swarmList[swarmNumber][i];
			if ( neighbor != this ) {
				alignment += neighbor.transform.forward;
			}
		}
		alignment /= swarmList[swarmNumber].Count;
	}

	void CalcCohesion() {

		cohesion = Vector3.zero;
	}
}
