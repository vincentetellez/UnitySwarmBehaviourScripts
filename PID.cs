public class PID
{
    public float proportionalWeight;
    public float integralWeight;
    public float derivativeWeight;
		
    private Vector3 error;
	private float integral;
	private float lastError;	
	
    public PID( proportionalWeight, integralWeight, derivativeWeight ) {
        this.proportionalWeight = proportionalWeight;
        this.integralWeight = integralWeight;
        this.derivativeWeight = derivativeWeight;
    }
    
	public float Process( Vector3 desired, Vector3 actual, float time ) {
		error = desired - actual;
		integral += present * timeFrame;
		float deriv = (present - lastError) / timeFrame;
		lastError = present;
		return present * pFactor + integral * iFactor + deriv * dFactor;
	}
}
