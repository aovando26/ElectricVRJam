using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/* 
 * this script is designed to trigger haptic feedback (vibration) through an XRDirectInteractor component when the OnElectricityReady event is raised by the Player class. 
*/

public class ElectricityReady : MonoBehaviour
{
    [Range(0, 1)]
    public float intensity;
    public float duration;
    private XRDirectInteractor interactor;

    // Start is called before the first frame update
    void Start()
    {
        interactor = GetComponentInChildren<XRDirectInteractor>();

        // subscribes TriggerHaptic method to the OnElectricityReady Event
        Player.OnElectricityReady += TriggerHaptic;
    }

    void OnDestroy()
    {
        // unsubscribes TriggerHaptic method to the OnElectricityReady Event
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
