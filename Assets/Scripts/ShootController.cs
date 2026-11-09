using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject Player; // Guarda una referencia del jugador
    public float TimeBetweenShooting = 1f; // sirve para que no sea posible disparar mas de una vez por segundo. <-- mecanicas del juego

    public float range = 100f; //a que distancia viaja el rayo

    float timer; //cada cuanto se puede disparar
    Ray shootRay;  // son clases que se utilizan para determinar el disparo, si el rayo impacta algun objeto
    RaycastHit shootHit;//--los videojuegos tipo shooter, utilizan la estrategia de fisica para saber que y hacia donde se dirige y en donde impacta.
    int shootableMask;//internamente unity le asocia un entero a la capa que se crea
    LineRenderer gunLine; //los componentes de shooter, esta es la linea que dibuja el rayo y muestra su trayectoria
    Light gunLight;//destello de luz cuando se lanza el rayo
    float effectsDisplayTime = 1.2f;//cuanto tiempo dura el rayo dibujado en pantalla

    private InputSystem_Actions controls; //determinar cuando se presiona la tecla ctrl

    void Awake() 
    {
        shootableMask = LayerMask.GetMask("Shootable");//se obtiene una capa de referencia numerica de la capa Shootable
        gunLine = GetComponent<LineRenderer>(); //se tienen que obtener de la variable shoot
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
        timer += Time.deltaTime; //variable flotante se incrementa de acuerdo a time.deltatime

        if (timer >= TimeBetweenShooting * effectsDisplayTime) //se hace la comparacion de si timer es >= a timpo en disparo y apaga el rayo
        {
            DisableEffects();
        }

        if (controls.Player.Attack.WasPressedThisFrame()) //si se presiona la tecla attack se llama al metodo shoot
        {
            Shoot();
        }
    }

    void Shoot()//el método que se debe modificar para hacer cambios en la seccion de disparos
    {
        Vector3 location = new Vector3(  //guarda la pocision del jugador
            Player.transform.position.x, //
            Player.transform.position.y + 1.1f, // para que el rayo salga a la altura del jugador
            Player.transform.position.z
        );

        timer = 0f; //cuando se dispara el temporizador se reinicia
        gunLine.enabled = true; //se activa el linerenderer que dibuja el rayo
        gunLight.enabled = true;//se activa la iluminacion
        shootRay.origin = location; // define el origen del rayo que es igual a location
        shootRay.direction = transform.forward; //para que el rayo salga hacia la direccion delantera del jugador
        gunLine.SetPosition(0, location); //para comenzar a dibujar el rayo, es decir comenzando desde ese punto

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask)) //el metodo que permite verificar si el rayo impacta algun objeto en un rango de (range) que pertenezca a la capa de shootable
        {
            Destroy(shootHit.collider.gameObject);//shoothit tiene la informacion de cual es el objeto que se impacta, luego se pregunta sobre el colider de ese objeto para saber con que se impacta
            gunLine.SetPosition(1, shootHit.point);//el punto exacto donde el rayo impacta con el cubo, es una coordenada exacta. se usa ej: saber si un disparo fue en el cuerpo o cabeza.

        }
        else  //si el rayo no impacta con ningun objeto
        {
            Debug.Log("No se impacto con ningun objeto");
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range); //dibujar la linea desde su punto de origen, en la direccion correspondiente y con el rango correspondiente
            
        }
    }

    void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
}
