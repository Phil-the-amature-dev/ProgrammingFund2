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
    // TODO: just use one array here. Why copy??
    // TODO maybe: just pick random particle effects at runtime, instead of diff prefabs
    
    private Collider[] hitTargets = new Collider[50]; // This is for efficiency: non-allocating every time


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //player.GetComponent<PlayerMovement>(); // Why Try? (will still give Exception if the component is not there
        transform.GetComponent<Rigidbody>().AddForce(player.forward, ForceMode.Force); // TODO: facing direction?
        audioClip = GameManager.instance.GetSfx();
    }

    // Update is called once per frame
    void Update()
          
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        //GameObject currentEffect = Instantiate(effectList[effectNum], transform.position, Quaternion.Euler(0, 0, 0));
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
                            Debug.Log("TARGETHIT");
                            Debug.Log(hitTargets[i]);
                            target.Burn(); 
                        }
                    }
                    
                } 
        }

        //play audio
        AudioSource.PlayClipAtPoint(audioClip, transform.position);

        //Destroy(currentEffect.gameObject, effectList[effectNum].GetComponent<ParticleSystem>().main.duration);
        Destroy(effect.gameObject, effect.GetComponent<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }
}
