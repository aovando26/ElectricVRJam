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

            // Debug.Log(elapsedTime);

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

            // raycasting to detect hits
            // from player's hand / electricity attack origin - returning z - axis 
            Ray ray = new Ray(handTransform.position, handTransform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                ReactiveTarget target = hit.collider.GetComponent<ReactiveTarget>();
                int scorePerPrefab = 1; 
                if (target != null)
                {
                    target.ReactToHit();
                    ScoreManager.instance.AddScore(scorePerPrefab);
                }
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