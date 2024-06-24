using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ElectricityReady : MonoBehaviour
{
    [Range(0, 1)]
    public float intensity;
    public float duration;
    private XRDirectInteractor interactor;
    private Player player; 

    // Start is called before the first frame update
    void Start()
    {
        interactor = GetComponentInChildren<XRDirectInteractor>();
        Player.OnElectricityReady += TriggerHaptic;
    }

    void OnDestroy()
    {
        Player.OnElectricityReady -= TriggerHaptic;
    }

    void TriggerHaptic()
    {
        if (interactor != null && intensity > 0)
        {
            Debug.Log("Vibration is now acting");
            interactor.SendHapticImpulse(intensity, duration);
        }
    }
}
