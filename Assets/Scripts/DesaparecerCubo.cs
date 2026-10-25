using System.Collections;
using UnityEngine;

public class DesaparecerCubo : MonoBehaviour
{
    [SerializeField] private float tiempoParaDesaparecer = 10f;
    [SerializeField] private ParticleSystem bigExplosion;
    [SerializeField] private float duracionExplosion = 1.5f;

    void Start()
    {
        StartCoroutine(DesaparecerDespuesDeTiempo());
    }

    private IEnumerator DesaparecerDespuesDeTiempo()
    {
        yield return new WaitForSeconds(tiempoParaDesaparecer);

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        if (bigExplosion != null)
        {
            bigExplosion.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            bigExplosion.transform.position = transform.position;
            bigExplosion.Play(true);

            yield return new WaitForSeconds(duracionExplosion);

            bigExplosion.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        gameObject.SetActive(false);
    }
}