
using UnityEngine;

public class PID
{
    public float proportionalWeight;
    public float integralWeight;
    public float derivativeWeight;

    private Vector3 error = Vector3.zero;
    private Vector3[] previousError;
    private int rollingErrorIndex = 0;
    private Vector3 pTerm;
    private Vector3 iTerm;
    private Vector3 dTerm;

    public PID( float proportionalWeight, float integralWeight, float derivativeWeight, int errorBufferSize ) {
        this.proportionalWeight = proportionalWeight;
        this.integralWeight = integralWeight;
        this.derivativeWeight = derivativeWeight;
        this.previousError = new Vector3[errorBufferSize];
    }

	public Vector3 Process( Vector3 desired, Vector3 actual, float deltaTime ) {

		error = desired - actual;

        Vector3 term1 = proportionalWeight * error;

        Vector3 term2 = Vector3.zero;
        for ( int i = 0; i < previousError.Length; i++ ) {
            term2 += previousError[i];
        }

        Vector3 term3 = derivativeWeight * ( error - previousError[(rollingErrorIndex - 1) % previousError.Length] ) / deltaTime;

        previousError[rollingErrorIndex] = error;
        rollingErrorIndex += 1;

        return term1 + term2 + term3;
	}
}
