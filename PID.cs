
using UnityEngine;

[System.Serializable]
public class PID
{
    public float proportionalWeight;
    public float integralWeight;
    public float derivativeWeight;

    /*
    private Vector3 error = Vector3.zero;
    private Vector3[] previousError;
    private int rollingErrorIndex = 0;
    private Vector3 pTerm;
    private Vector3 iTerm;
    private Vector3 dTerm;
    */

    private float error = 0f;
    private float[] previousError;
    private int rollingErrorIndex = 0;
    private float pTerm;
    private float iTerm;
    private float dTerm;

    public PID( float proportionalWeight, float integralWeight, float derivativeWeight, int errorBufferSize ) {
        this.proportionalWeight = proportionalWeight;
        this.integralWeight = integralWeight;
        this.derivativeWeight = derivativeWeight;
        this.previousError = new float[errorBufferSize];
    }

    public PID( int errorBufferSize ) {
        this.previousError = new float[errorBufferSize];
    }

	/*public Vector3 ProcessDirection( Vector3 desired, Vector3 actual, float deltaTime ) {

		error = Quaternion.FromToRotation( actual, desired ).eulerAngles;

        Vector3 term1 = proportionalWeight * error;

        Vector3 term2 = Vector3.zero;
        for ( int i = 0; i < previousError.Length; i++ ) {
            term2 += previousError[i];
        }

        Vector3 term3 = derivativeWeight * ( error - previousError[rollingErrorIndex % previousError.Length] ) / deltaTime;

        previousError[rollingErrorIndex] = error;
        rollingErrorIndex = (rollingErrorIndex + 1) % previousError.Length;

        return term1 + term2 + term3;
	}*/

    public float Process( float desired, float actual, float deltaTime ) {
        error = desired - actual;

        pTerm = proportionalWeight * error;

        iTerm = 0f;
        for ( int i = 0; i < previousError.Length; i++ ) {
            iTerm += previousError[i];
        }
        iTerm *= integralWeight;

        dTerm = derivativeWeight * ( error - previousError[rollingErrorIndex] ) / deltaTime;

        rollingErrorIndex = (rollingErrorIndex + 1) % previousError.Length;
        previousError[ rollingErrorIndex ] = error;

        return pTerm + iTerm + dTerm;
    }
}
