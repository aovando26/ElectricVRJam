using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.VFX;
using UnityEngine.InputSystem;

public class ElectricityAttack : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip electricZapClip;
    public VisualEffect vfxGraphDirectionalElectricity;
    public float electricityConsumptionRate = 1f;
    public Transform handTransform;
    public float heightOffset = 1f;

    private ActionBasedController controller;
    private Player player;
    private float elapsedTime;

    private void Start()
    {
        controller = GetComponent<ActionBasedController>();
        player = GetComponentInParent<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (controller.activateAction.action.phase == InputActionPhase.Performed && player.electricity > 0)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 1f)
            {
                player.UseElectricity(electricityConsumptionRate);
                elapsedTime = 0f;
            }
            vfxGraphDirectionalElectricity.SetFloat("Lifetime", 1f);

            if (!audioSource.isPlaying)
            {
                audioSource.clip = electricZapClip;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            elapsedTime = 0f;
            vfxGraphDirectionalElectricity.SetFloat("Lifetime", 0f);

            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}