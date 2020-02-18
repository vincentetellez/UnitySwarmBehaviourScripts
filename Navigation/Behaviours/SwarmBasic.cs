using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmBasic : MonoBehaviour
{

    private VectorNavigation vn;

	private static List<List<GameObject>> swarmList;	// A list of swarms which currently exist

	public int swarmNumber = 0;				           // Which swarm in the swarm list this entity belongs to

    public bool localizedBehaviour = false;             // Controls if behaviour should be influenced by entire swarm or just nearby neighbors

    public const int NUMNEIGHBORS = 5;
    private GameObject[] neighbors;

    public float queryChance = 0.1f;                    // Percent chance per second of querying neighbors

	private Vector3 separation;
	private Vector3 alignment;
	//private Vector3 cohesion;

	public float separationWeight = 1f;
	public float alignmentWeight = 1f;
	//public float cohesionWeight = 1f;

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

        neighbors = new GameObject[NUMNEIGHBORS];
	}

	void Update() {

        if ( localizedBehaviour ) {
            if ( Random.value < queryChance * Time.deltaTime ) {
                QueryNeighbors();
            }
        }

        if ( localizedBehaviour ) {
            CalcLocalSeparation();
            CalcLocalAlignment();
            //CalcLocalCohesion();
        }
        else {
		    CalcSeparation();
            CalcAlignment();
            //CalcCohesion();
        }
		vn.SetHeading( separationWeight * separation + alignmentWeight * alignment );
	}

	void CalcSeparation() {

		// Calculates the separation between all swarm neighbors so that a proper distance can be maintained. This method accounts for separation and cohesion in a single function.

		separation = Vector3.zero;
		for ( int i = 0; i < swarmList[swarmNumber].Count; i++ ) {
			neighbor = swarmList[swarmNumber][i];
			if ( neighbor != this.gameObject ) {
				dist = neighbor.transform.position - transform.position;
				separation += (dist.sqrMagnitude - preferredSeparation) * dist.normalized;
			}
		}
        separation /= swarmList[swarmNumber].Count;
	}

    void CalcLocalSeparation() {

		// Calculates the separation between closest swarm neighbors so that a proper distance can be maintained. This method accounts for separation and cohesion in a single function.

		separation = Vector3.zero;
		for ( int i = 0; i < neighbors.Length; i++ ) {
			neighbor = neighbors[i];
			if ( neighbor != null && neighbor != this.gameObject ) {
				dist = neighbor.transform.position - transform.position;
				separation += (dist.sqrMagnitude - preferredSeparation) * dist.normalized;
			}
		}
        separation /= neighbors.Length;
	}

	void CalcAlignment() {

		// Calculates the heading of all swarm neighbors to align with the swarms average direction

		alignment = Vector3.zero;
		for ( int i = 0; i < swarmList[swarmNumber].Count; i++ ) {
			neighbor = swarmList[swarmNumber][i];
			if ( neighbor != this.gameObject ) {
				alignment += neighbor.transform.forward;
			}
		}
		alignment /= swarmList[swarmNumber].Count;
	}

    void CalcLocalAlignment() {

        // Calculates the heading of all swarm neighbors to align with the swarms average direction

		alignment = Vector3.zero;
		for ( int i = 0; i < neighbors.Length; i++ ) {
			neighbor = neighbors[i];
			if ( neighbor != null && neighbor != this.gameObject ) {
				alignment += neighbor.transform.forward;
			}
		}
		alignment /= swarmList[swarmNumber].Count;
    }

	/*void CalcLocalCohesion() {

        // Calculates the separation between closest swarm neighbors so that a proper distance can be maintained. Original method accounted for separation and cohesion, but they have been separated to allow for different weights.

		cohesion = Vector3.zero;
		for ( int i = 0; i < neighbors.Length; i++ ) {
			neighbor = neighbors[i];
			if ( neighbor != null && neighbor != this.gameObject ) {
				dist = neighbor.transform.position - transform.position;
				//separation += (dist.sqrMagnitude - preferredSeparation) * dist.normalized;
                cohesion += dist;
			}
		}
        separation /= neighbors.Length;
	}*/

    void QueryNeighbors() {
        for ( int i = 0; i < neighbors.Length; i++ ) {
            neighbors[i] = null;
        }
        for ( int i = 0; i < swarmList[swarmNumber].Count; i++ ) {
            neighbor = swarmList[swarmNumber][i];
            if ( neighbor != this.gameObject ) {
                dist = neighbor.transform.position - transform.position;
                for ( int j = 0; j < NUMNEIGHBORS; j++ ) {
                    if ( neighbors[j] == null ) {
                        neighbors[j] = neighbor;
                        break;
                    } else if ( dist.sqrMagnitude < (neighbors[j].transform.position - transform.position).sqrMagnitude ) {
                        GameObject temp = neighbors[j];
                        neighbors[j] = neighbor;
                        neighbor = temp;
                    }
                }
            }
        }
    }
}
