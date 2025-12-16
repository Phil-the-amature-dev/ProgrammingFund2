using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public TagHandle playerTag;
    public PlayerMovement playerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTag = playerScript.gameObject.GetComponent<TagHandle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag)){ // use tags instead?
            playerScript.Die();
        }
    }
}
