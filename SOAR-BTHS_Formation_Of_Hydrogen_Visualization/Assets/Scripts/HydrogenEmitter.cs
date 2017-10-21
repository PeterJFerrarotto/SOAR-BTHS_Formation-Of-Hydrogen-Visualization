using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydrogenEmitter : MonoBehaviour {
    private ParticleSystem hydrogen;

    private List<ParticleCollisionEvent> hydrogenCollisions;
    private ParticleSystem.Particle[] particles;
	// Use this for initialization
	void Start () {
        hydrogen = GetComponent<ParticleSystem>();
        hydrogen.Stop();
        hydrogenCollisions = new List<ParticleCollisionEvent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<Bottle>())
        {
            int collisionCount = hydrogen.GetCollisionEvents(other, hydrogenCollisions);
            other.GetComponent<Bottle>().AddParticles(collisionCount);
        }
    }

    public void EmitHydrogen(int toEmit)
    {
        hydrogen.Emit(toEmit);
    }
}
