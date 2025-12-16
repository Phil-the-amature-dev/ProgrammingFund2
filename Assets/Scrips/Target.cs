using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Target : MonoBehaviour
{
    public bool isBurnt = false;
    public Material burnMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Burn()
    {
        isBurnt = true;
        gameObject.GetComponent<Renderer>().material = burnMaterial;
    }
}
  