using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private InputSystem_Actions controls;
    private Rigidbody rb;
    private Vector2 moveInput;
    public Transform particles;
    private ParticleSystem particlesSystem;
    private Vector3 position;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody>(); 
        particlesSystem = particles.GetComponent<ParticleSystem>();
        particlesSystem.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveInput.x, 0.0f, moveInput.y);
        rb.AddForce(movement * speed);
    }
    void Awake()
    {
        controls = new InputSystem_Actions();
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }
    void OnEnable(){
        controls.Enable();
    }
    void OnDisable(){
        controls.Disable();
    }
    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Collectable"))
        {
            //El objeto es recolectable
            
            position = other.gameObject.transform.position;
            particles.position = position;
            particlesSystem = particles.GetComponent<ParticleSystem>();
            particlesSystem.Play();
            other.gameObject.SetActive(false);
        }
        else
        {
            //El objeto NO es recolectable

        }
    }
}