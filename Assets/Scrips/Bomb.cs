using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Bomb : MonoBehaviour
{
    
    public Transform player; // ? -> set by player
    public float explosionRadius;
    public float explosionStrength;
    public float upwardsModifier;
    private AudioClip audioClip;
    private Camera playerCamera;
    [SerializeField] private float explosionVolume;
    
    private Collider[] hitTargets = new Collider[50]; // This is for efficiency: non-allocating every time


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameManager.instance.player;
        playerCamera = player.GetComponentInChildren<Camera>();
        audioClip = GameManager.instance.GetSfx();
        
        transform.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward, ForceMode.Force); 
    }


    private void OnCollisionEnter(Collision collision)
    {

        GameObject effect = Instantiate (GameManager.instance.GetEffect(), transform.position, Quaternion.Euler(0, 0, 0));

        //Explosion Guide:
        //https://gamedevbeginner.com/how-to-make-an-explosion-in-unity/
        int targetNum = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, hitTargets);
        
        

        if (targetNum > 0)
        for (int i = 0; i < targetNum; i++)
        {
                
                if (hitTargets[i].TryGetComponent(out Rigidbody rb) && hitTargets[i].gameObject.layer != 3 && hitTargets[i].gameObject.layer != 7) // TODO maybe: no hard coded layers?
                {
                    rb.AddExplosionForce(explosionStrength, transform.position, explosionRadius, upwardsModifier);
                    if (hitTargets[i].TryGetComponent(out Target target))
                    {
                        if (!target.isBurnt)
                        {
                            GameManager.instance.addScore(1);
                            Debug.Log(hitTargets[i]);
                            target.Burn(); 
                        }
                    }
                    
                } 
        }

        //play audio
        AudioSource.PlayClipAtPoint(audioClip, transform.position, explosionVolume);

        Destroy(effect.gameObject, effect.GetComponent<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }
}
