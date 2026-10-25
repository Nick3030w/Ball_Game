using System.Collections;
using UnityEngine;

public class CuboParpadeante : MonoBehaviour
{
    [SerializeField] private float tiempoOculto = 5f;
    [SerializeField] private float tiempoVisible = 3f;

    private Renderer rend;
    private Collider col;
    private Color[] colores = { Color.green, Color.black, Color.red, Color.white, Color.blue };
    private int indiceColor = 0;

    void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
        StartCoroutine(CicloParpadeo());
    }

    private IEnumerator CicloParpadeo()
    {
        while (true)
        {
            rend.enabled = false;
            col.enabled = false;
            yield return new WaitForSeconds(tiempoOculto);

            rend.material.color = colores[indiceColor];
            indiceColor = (indiceColor + 1) % colores.Length;
            rend.enabled = true;
            col.enabled = true;
            yield return new WaitForSeconds(tiempoVisible);
        }
    }
}