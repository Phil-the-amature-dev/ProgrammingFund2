using UnityEngine;

public class SpinningTrap : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    

    private void FixedUpdate()
    {
        rb.AddTorque(Vector3.up * speed, ForceMode.Force);
    }
}
