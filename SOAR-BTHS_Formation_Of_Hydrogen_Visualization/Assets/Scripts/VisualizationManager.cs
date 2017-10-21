using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationManager : MonoBehaviour {
    private List<string> stepText;
    public int stepCount;
    private string splashText;
    public float volumePerParticle;
    public HydrogenGenerator hG;
    public HydrogenEmitter hE;

    public Bottle[] bottles;
    private int bottleNum;

    private bool splashTextInitialized = false;
    public bool SplashTextInitialized
    {
        get
        {
            return splashTextInitialized;
        }
    }

    private bool stepTextLoaded = false;
    public bool StepTextLoaded
    {
        get
        {
            return stepTextLoaded;
        }
    }

    public string SplashText
    {
        get
        {
            return splashText;
        }
    }

    public string StepText(int index)
    {
        return stepText[index];
    }

    private static VisualizationManager _instance;

    public static VisualizationManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<VisualizationManager>();
            }

            if (_instance == null)
            {
                GameObject gO = new GameObject();
                _instance = gO.AddComponent<VisualizationManager>();
            }

            return _instance;
        }
    }
	// Use this for initialization
	void Start () {
        stepText = new List<string>();
        _instance = this;

        if (bottles == null || bottles.Length == 0)
        {
            bottles = GameObject.FindObjectsOfType<Bottle>();
        }

        if (hG == null)
        {
            hG = GameObject.FindObjectOfType<HydrogenGenerator>();
        }

        if (hE == null)
        {
            hE = GameObject.FindObjectOfType<HydrogenEmitter>();
        }

        bottleNum = 0;
        foreach (Bottle bottle in bottles)
        {
            bottle.SetState(Bottle.BottleState.EMPTY);
        }

        //splashText = File.ReadAllText("Assets/Resources/Text/splash.txt");
        splashText = Resources.Load<TextAsset>("Text/splash").text;
        splashTextInitialized = true;

        for (int i = 1; i <= stepCount; i++)
        {
            string stpTxt = Resources.Load<TextAsset>("Text/step_" + i.ToString()).text;
            stepText.Add(stpTxt);
        }
        stepTextLoaded = true;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateBottles();
	}

    public void EmitHydrogen(int hydrogen)
    {
        hE.EmitHydrogen(hydrogen);
    }

    private void FinishLab()
    {
        hG.StopPouringAcid();
    }

    public void FillBottlesWithWater()
    {
        foreach (Bottle bottle in bottles)
        {
            bottle.SetState(Bottle.BottleState.FULL_WATER);
        }
    }

    public void BeginFillingBottles()
    {
        bottles[0].SetState(Bottle.BottleState.FILLING);
    }

    private void UpdateBottles()
    {
        if (bottles[bottleNum].isFull())
        {
            if (bottleNum < bottles.Length - 1)
            {
                bottles[bottleNum++].SetState(Bottle.BottleState.FULL);
                bottles[bottleNum].SetState(Bottle.BottleState.FILLING);
            }
            else
            {
                bottles[bottleNum].SetState(Bottle.BottleState.FULL);
                FinishLab();
            }
        }
    }
}
