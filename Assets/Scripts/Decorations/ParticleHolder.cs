using UnityEngine;

public class ParticleHolder : MonoBehaviour
{
    [SerializeField] ParticleSystem step;
    [SerializeField] ParticleSystem tackled;
    [SerializeField] ParticleSystem groundDrag;
    public enum DefautlParticleStyle
    {
        step,
        tackled,
        groundDrag
    }

    public void SpawnParticle(DefautlParticleStyle particle)
    {
        ParticleSystem toSpawn = null;
        Vector3 relativePos = transform.position;
        switch (particle)
        {
            case DefautlParticleStyle.step:
                relativePos -= transform.forward * .5f;
                toSpawn = step;
                Instantiate<ParticleSystem>(toSpawn, relativePos, transform.rotation);
                break;
            case DefautlParticleStyle.tackled:
                toSpawn = tackled;
                Instantiate<ParticleSystem>(toSpawn, relativePos, transform.rotation);
                break;
            case DefautlParticleStyle.groundDrag:
                toSpawn = groundDrag;
                relativePos += transform.forward * 1f + Vector3.up*0.05f;
                Instantiate<ParticleSystem>(toSpawn, relativePos, transform.rotation);
                break;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
