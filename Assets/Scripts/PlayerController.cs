using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private InputSystem_Actions controls;
    private Rigidbody rb;
    private Vector2 moveInput;
    public Transform particles;
    private ParticleSystem particlesSystem;
    private Vector3 position;
    private int counter;
    public TextMeshProUGUI textCounter;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        particlesSystem = particles.GetComponent<ParticleSystem>();
        particlesSystem.Stop();
        textCounter.text = "Counter: " + counter.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (controls.Player.Attack.WasPressedThisFrame())
        {
            Animar();
        }
    }
    void FixedUpdate()
    {
        /* Vector3 movement = new Vector3(moveInput.x, 0.0f, moveInput.y);
         rb.AddForce(movement * speed);
         */
    }
    void Awake()
    {
        anim = GetComponent<Animator>();
        controls = new InputSystem_Actions();
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.Player.Attack.ApplyBindingOverride("<Keyboard>/leftCtrl");

        
    }
    void OnEnable()
    {
        controls.Enable();
    }
    void OnDisable()
    {
        controls.Disable();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            //El objeto es recolectable

            position = other.gameObject.transform.position;
            particles.position = position;
            particlesSystem = particles.GetComponent<ParticleSystem>();
            particlesSystem.Play();
            other.gameObject.SetActive(false);
            StartCoroutine(StopParticles(particlesSystem));

            counter = counter + 1;
            textCounter.text = "Counter: " + counter.ToString();
        }
        else
        {
            //El objeto NO es recolectable

        }
    }
    public IEnumerator StopParticles(ParticleSystem particleSystem)
    {
        yield return new WaitForSecondsRealtime(5);
        particlesSystem.Stop();
    }
    void Animar()
    {
        anim.SetBool("isSendingMagic", true);
        StartCoroutine(RestartAnimation());
    }

    public IEnumerator RestartAnimation()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        anim.SetBool("isSendingMagic", false);
    }
}