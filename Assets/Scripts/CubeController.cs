using UnityEngine;

public class CubeController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RotateCube()
    {
        transform.Rotate(new Vector3(45, 45, 45));
    }
    public void ScaleCube(float value)
    {
        transform.localScale = new Vector3(value,value,value);
    }
}
