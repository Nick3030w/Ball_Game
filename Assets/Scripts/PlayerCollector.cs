using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerCollector : MonoBehaviour
{
    [SerializeField] private ParticleSystem energyExplosion;
    [SerializeField] private ParticleSystem bigExplosion;
    [SerializeField] private float duracionEnergy = 1.5f;
    [SerializeField] private float duracionBig = 1.5f;
    public TextMeshProUGUI textCounter;

    private int counter;

    void Start()
    {
        
        energyExplosion.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        bigExplosion.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        textCounter.text = "Counter: " + counter;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Collectable")) return;

       
        if (other.GetComponent<CuboParpadeante>() != null)
            StartCoroutine(ReproducirEfecto(bigExplosion, other.transform.position, duracionBig));
        else
            StartCoroutine(ReproducirEfecto(energyExplosion, other.transform.position, duracionEnergy));

        other.gameObject.SetActive(false);

        counter++;
        textCounter.text = "Counter: " + counter;
    }

    private IEnumerator ReproducirEfecto(ParticleSystem efecto, Vector3 posicion, float duracion)
    {
        efecto.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        efecto.transform.position = posicion;
        efecto.Play(true);

        yield return new WaitForSeconds(duracion);

        efecto.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}