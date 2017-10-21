using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydrogenGenerator : MonoBehaviour {
    private ParticleSystem acid;

    public float acidToHydrogenRatio;

    private List<ParticleCollisionEvent> acidCollisions;
    private float timePouring;

	// Use this for initialization
	void Start () {
        acid = GetComponent<ParticleSystem>();
        acid.Stop();
        acidCollisions = new List<ParticleCollisionEvent>();
	}
	
	// Update is called once per frame
	void Update () {	
	}

    public void PourAcid()
    {
        acid.Play();
    }

    public void StopPouringAcid()
    {
        acid.Stop();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<MetalStrip>())
        {
            int numCollisionEvents = acid.GetCollisionEvents(other, acidCollisions);
            VisualizationManager.Instance.EmitHydrogen((int)(numCollisionEvents * acidToHydrogenRatio));
        }
    }
}
