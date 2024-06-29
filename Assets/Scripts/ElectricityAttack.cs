using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.VFX;
using UnityEngine.InputSystem;

public class ElectricityAttack : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip electricZapClip;
    public VisualEffect vfxGraphDirectionalElectricity;
    public float electricityConsumptionRate = 10.0f;
    public Transform handTransform;
    public float heightOffset = 1f;

    public SpawnManager spawnManager;
    public int damageAmount = 20;

    private ActionBasedController controller;
    private Player player;
    private float elapsedTime;

    //  indicator to check if the player's electricity is ready - initally set to false
    private bool isElectricityReady = false;

    // subscribes and unsubscribes to the OnElectricityReady event from the Player class
    private void OnEnable()
    {
        Player.OnElectricityReady += HandleElectricityReady;
    }

    private void OnDisable()
    {
        Player.OnElectricityReady -= HandleElectricityReady;
    }

    private void Start()
    {
        controller = GetComponent<ActionBasedController>();
        player = GetComponentInParent<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    // this event is triggered when the player's electricity is ready for attack 
    private void HandleElectricityReady()
    {
        isElectricityReady = true;
    }

    private void Update()
    {
        // check if electricity is ready, action performed, and if player has enough electricity (greater than 0)
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

            // raycasting to detect hits
            Ray ray = new Ray(handTransform.position, handTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                ReactiveTarget target = hit.collider.GetComponent<ReactiveTarget>();
                GameObject enemy = hit.collider.gameObject;
                int scorePerPrefab = 1;
                Debug.Log($"Current score is {scorePerPrefab}");
                if (target != null)
                {
                    target.ReactToHit();
                    ScoreManager.instance.AddScore(scorePerPrefab);
                    spawnManager.DamageEnemy(enemy, damageAmount);
                }
            }
        }
        else
        {
            // VFX settings are reset
            elapsedTime = 0f;
            vfxGraphDirectionalElectricity.SetFloat("Lifetime", 0f);

            // stops audio playback
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
