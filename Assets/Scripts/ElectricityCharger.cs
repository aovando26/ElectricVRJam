using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ElectricityCharger : MonoBehaviour  
{
    public float electricityAmount = 5f;
    public float chargingInterval = 1f;

    private XRSimpleInteractable interactable;
    private Player player;
    private float timer;
    private bool isCharging = false;
    //private Animator rightHandAnimation;

    private AudioSource audioSource;
    public AudioClip chargingClip;
    public GameObject chargingInterface;
    //public GameObject animatedRHand;


    private void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);

        audioSource = GetComponent<AudioSource>();
        //rightHandAnimation = animatedRHand.GetComponent<Animator>(); 
        
        //if (rightHandAnimation == null)
        //{
        //    Debug.LogError("Animator component is missing from animatedRHand.");
        //}
        //else
        //{
        //    Debug.Log("Animator component found.");
        //}
    }

    private void Update()
    {
        if (player != null && isCharging)
        {
            timer += Time.deltaTime;
            //audioSource.PlayOneShot(chargingClip);
            //chargingInterface.SetActive(true);

            // chargingInterval is 0.2f;
            //Debug.Log("Charging Inteval is: " + chargingInterval);
            //Debug.Log(" TIme is: " + timer);

            if (timer >= chargingInterval)
            {
                player.ChargeElectricity(electricityAmount);
                timer = 0f;
            }
        }
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        XRBaseInteractor interactor = args.interactor;
        player = interactor.GetComponentInParent<Player>();
        timer = 0f;
        isCharging = true;
        if (chargingClip != null && !audioSource.isPlaying)
        {
            audioSource.clip = chargingClip;
            audioSource.loop = true;
            audioSource.Play();
        }

        chargingInterface.SetActive(true);
        //if (rightHandAnimation != null)
        //{
        //    Debug.Log("Setting playingAnimation to true.");
        //    rightHandAnimation.SetLayerWeight(1, 1);
        //    rightHandAnimation.SetBool("playingAnimation", true);
        //}
        //else
        //{
        //    Debug.Log("Right Hand Animator is not found");
        //}
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        player = null;
        isCharging = false;

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (chargingInterface != null)
        {
            chargingInterface.SetActive(false);
        }

        //rightHandAnimation.SetBool("playingAnimation", false);
        //rightHandAnimation.SetLayerWeight(1, 0);
    }
}
