using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject Player;
    public float TimeBetweenShooting = 1f;
    public float range = 100f;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;
    Light gunLight;
    float effectsDisplayTime = 1.2f;

    private InputSystem_Actions controls;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        controls = new InputSystem_Actions();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= TimeBetweenShooting * effectsDisplayTime)
        {
            DisableEffects();
        }

        if (controls.Player.Attack.WasPressedThisFrame())
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 location = new Vector3(
            Player.transform.position.x,
            Player.transform.position.y + 1.1f,
            Player.transform.position.z
        );

        timer = 0f;
        gunLine.enabled = true;
        gunLight.enabled = true;
        shootRay.origin = location;
        shootRay.direction = transform.forward;
        gunLine.SetPosition(0, location);

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            Destroy(shootHit.collider.gameObject);
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            Debug.Log("No se impacto con ningun objeto");
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
}
