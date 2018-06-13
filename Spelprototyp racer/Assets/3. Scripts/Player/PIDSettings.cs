//This PIDScript calculates a value based on the PID algorithm
//Check PID for reference.
using UnityEngine;

[System.Serializable]
public class PIDSettings {
    //Coefficients for PID controller tuning
    public float pCoeff = .8f;
    public float iCoeff = .0002f;
    public float dCoeff = .2f;
    public float minimum = -1;
    public float maximum = 1;

    //Vars to store values between calculations
    float integral;
    float lastProportional;

    //Pass int current value, the code returns a number that moves player to the dessired goal
    public float Seek(float seekValue, float currentValue)
    {
        float proportional = seekValue - currentValue;

        float derivative = (proportional - lastProportional) / Time.fixedDeltaTime;
        integral += proportional * Time.fixedDeltaTime;
        lastProportional = proportional;

        //PID formula
        float value = pCoeff * proportional + iCoeff * integral + dCoeff * derivative;
        value = Mathf.Clamp(value, minimum, maximum);

        return value;
    }
}
