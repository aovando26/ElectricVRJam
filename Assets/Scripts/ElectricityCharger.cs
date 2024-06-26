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

    private AudioSource audioSource;
    public AudioClip chargingClip;
    //public GameObject chargingInterface;
    //public GameObject chargedInterface;

    private void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (player != null && isCharging)
        {
            timer += Time.deltaTime;

            if (timer >= chargingInterval)
            {
                player.ChargeElectricity(electricityAmount);
                timer = 0f;

                // accessing player class 
                // checking if electricty exceeds max charge 
                // charged interface is invoked 
                if (player.electricity >= player.maxCharge)
                {
                    isCharging = false;
                    //SwitchToChargedInterface();
                }
            }
        }
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        XRBaseInteractor interactor = args.interactor;
        player = interactor.GetComponentInParent<Player>();
        timer = 0f;
        
        // state of charging is now true, so if statement condition one line 30 is executed 
        isCharging = true;

        if (!audioSource.isPlaying)
        {

            // audio is play if it isn't already
            audioSource.clip = chargingClip;
            audioSource.loop = true;
            audioSource.Play();
        }

        //chargingInterface.SetActive(true);
        //chargedInterface.SetActive(false); // make sure the charged interface is initially hidden
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        player = null;
        isCharging = false;

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        //chargingInterface.SetActive(false);
        //chargedInterface.SetActive(false);
    }

    private void SwitchToChargedInterface()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        //chargingInterface.SetActive(false);
        //chargedInterface.SetActive(true);

        //Debug.Log("Switching to charged interface");
    }
}
