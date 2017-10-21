using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour {

    public enum BottleState
    {
        EMPTY,
        FULL_WATER,
        FILLING,
        FULL
    }

    public float yOffset;
    public GameObject hydrogenObject;
    public GameObject waterObject;
    public Transform emitterPos;

    private Vector3 emptyAndFullPos;

    private BottleState currState;

    public float hydrogenObjStartYPos;
    public float hydrogenObjEndYPos;

    public float waterObjStartYPos;
    public float waterObjEndYPos;

    public int capacity;

    private int size;

    private Vector3 hydrogenObjStartPos;
    private Vector3 hydrogenObjEndPos;

    private Vector3 waterObjStartPos;
    private Vector3 waterObjEndPos;
	// Use this for initialization
	void Start () {
        emptyAndFullPos = gameObject.transform.position;
        size = 0;
        hydrogenObjStartPos = new Vector3(0, hydrogenObjStartYPos, 0);
        hydrogenObjEndPos = new Vector3(0, hydrogenObjEndYPos, 0);
        waterObjStartPos = new Vector3(0, waterObjStartYPos, 0);
        waterObjEndPos = new Vector3(0, waterObjEndYPos, 0);
        SetState(BottleState.EMPTY);
    }

    // Update is called once per frame
    void Update () {
        switch (currState)
        {
            case BottleState.EMPTY:
                gameObject.transform.position = emptyAndFullPos;
                if (hydrogenObject != null)
                {
                    hydrogenObject.transform.localPosition = hydrogenObjStartPos;
                }
                if (waterObject != null)
                {
                    waterObject.transform.localPosition = waterObjEndPos;
                }
                break;
            case BottleState.FULL_WATER:
                gameObject.transform.position = emptyAndFullPos;
                if (hydrogenObject != null)
                {
                    hydrogenObject.transform.localPosition = hydrogenObjStartPos;
                }
                if (waterObject != null)
                {
                    waterObject.transform.localPosition = waterObjStartPos;
                }
                break;
            case BottleState.FILLING:
                Vector3 tmp = emitterPos.position;
                tmp.y += yOffset;
                gameObject.transform.position = tmp;

                if (hydrogenObject != null)
                {
                    hydrogenObject.transform.localPosition = Vector3.Lerp(hydrogenObjStartPos, hydrogenObjEndPos, (float)size / (float)capacity);
                }
                if (waterObject != null)
                {
                    waterObject.transform.localPosition = Vector3.Lerp(waterObjStartPos, waterObjEndPos, (float)size / (float)capacity);
                }
                break;
            case BottleState.FULL:
                gameObject.transform.position = emptyAndFullPos;
                if (hydrogenObject != null)
                {
                    hydrogenObject.transform.localPosition = hydrogenObjEndPos;
                }
                if (waterObject != null)
                {
                    waterObject.transform.localPosition = waterObjEndPos;
                }
                break;
            default:
                throw new System.Exception("Unknown bottle state!");
                break;
        }
        if (hydrogenObject != null)
        {
            hydrogenObject.GetComponent<ClippableObject>().plane1Position.y = transform.position.y;
        }

        if (waterObject != null)
        {
            waterObject.GetComponent<ClippableObject>().plane1Position.y = transform.position.y - 0.8f;
        }
    }

    public bool isFull()
    {
        return size >= capacity || currState == BottleState.FULL;
    }

    public void AddParticles(int amount)
    {
        size += amount;
    }

    public void SetState(BottleState state)
    {
        currState = state;
    }
}
