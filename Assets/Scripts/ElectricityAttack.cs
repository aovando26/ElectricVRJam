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
    public LayerMask enemyLayer;

    private ActionBasedController controller;
    private Player player;
    private float elapsedTime;
    private bool isElectricityReady = false;

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

        if (spawnManager == null)
        {
            Debug.LogError("SpawnManager is not assigned to ElectricityAttack!");
        }
    }

    private void HandleElectricityReady()
    {
        isElectricityReady = true;
        Debug.Log("Electricity is ready for attack!");
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
                Debug.Log($"Used {electricityConsumptionRate} electricity. Remaining: {player.electricity}");
            }

            vfxGraphDirectionalElectricity.SetFloat("Lifetime", 1f);

            if (!audioSource.isPlaying)
            {
                audioSource.clip = electricZapClip;
                audioSource.loop = true;
                audioSource.Play();
            }

            PerformElectricityAttack();
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

    private void PerformElectricityAttack()
    {
        Ray ray = new Ray(handTransform.position, handTransform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.blue, 0.1f);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayer))
        {
            Debug.Log($"Hit object: {hit.collider.gameObject.name} at distance {hit.distance}");

            ReactiveTarget target = hit.collider.GetComponent<ReactiveTarget>();
            if (target != null)
            {
                target.ReactToHit();
                Debug.Log("ReactiveTarget hit and reacted");
            }

            EnemyWrapper enemyWrapper = hit.collider.GetComponent<EnemyWrapper>();
            if (enemyWrapper != null)
            {
                if (spawnManager != null)
                {
                    spawnManager.DamageEnemy(enemyWrapper, damageAmount);
                    Debug.Log($"Damaging enemy for {damageAmount}");
                }
                else
                {
                    Debug.LogError("SpawnManager is null in ElectricityAttack!");
                }
            }
            else
            {
                Debug.LogWarning("Hit object does not have an EnemyWrapper component");
            }

            int scorePerPrefab = 1;
            ScoreManager.instance.AddScore(scorePerPrefab);
            Debug.Log($"Score added. Current score: {ScoreManager.instance.GetScore()}");
        }
        else
        {
            Debug.Log("Electricity ray did not hit any enemy");
        }
    }
}