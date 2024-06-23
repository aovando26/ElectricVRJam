using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ElectricityCharger : MonoBehaviour
{
    public float electricityAmount = 5f;
    public float chargingInterval = 1f;

    private XRSimpleInteractable interactable;
    private Player player;
    private float timer;

    private void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnSelectEntered);
        interactable.selectExited.AddListener(OnSelectExited);
    }

    private void Update()
    {
        if (interactable.isSelected)
        {
            timer += Time.deltaTime;
            if (timer >= chargingInterval)
            {
                player.ChargeElectricity(electricityAmount);
                timer = 0f;
            }
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        XRBaseInteractor interactor = args.interactor;
        player = interactor.GetComponentInParent<Player>();
        timer = 0f;
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        player = null;
    }
}