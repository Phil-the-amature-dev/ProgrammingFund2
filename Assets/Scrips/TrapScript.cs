using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public string playerTag;
    public PlayerMovement playerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag)){ 
            playerScript.Die();
        }
    }
}
