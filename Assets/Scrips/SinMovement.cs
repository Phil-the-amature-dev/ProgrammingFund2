using Unity.VisualScripting;
using UnityEngine;

public class SinMovement : MonoBehaviour
{
    public float range;
    public float speed;
    private Vector3 nextPosition;
    private Rigidbody rb;
    private Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //remember X and Z
        //float yLevel = range * Mathf.Sin(Time.time * speed);
        //transform.position = startPosition + new Vector3(0, yLevel, 0);

        // TODO: Move correctly! (kinematic rb - also: rb.movePosition)
    }

    private void FixedUpdate()
    {
        float yLevel = range * Mathf.Sin(Time.time * speed);
        rb.MovePosition(startPosition + new Vector3(0, yLevel, 0));
    }
}
