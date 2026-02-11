using UnityEngine;

public class TrapScript : MonoBehaviour
{
    [SerializeField] private string playerTag;
    PlayerMovement playerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // find the player... using tag?        
        playerScript = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int TestMethod(float input) { return 0; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag)){ 
            playerScript.Die();
        }
    }
}
