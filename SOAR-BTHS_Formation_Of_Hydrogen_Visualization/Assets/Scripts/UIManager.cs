using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Image splashTextContainer;
    public Image stepTextContainer;

    public Text splashText;
    public Text stepText;
    public Text stepNumIndicator;

    private int stepNum;
    private bool splashTextInitialized = false;

    private bool showSplashText = true;
	// Use this for initialization
	void Start () {

        stepTextContainer.gameObject.SetActive(false);
        splashTextContainer.gameObject.SetActive(false);
        if (!VisualizationManager.Instance.SplashTextInitialized)
        {
            splashTextInitialized = false;
        }
        else
        {
            splashText.text = VisualizationManager.Instance.SplashText;
            splashTextInitialized = true;
        }
        stepNum = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (!splashTextInitialized)
        {
            if (VisualizationManager.Instance.SplashTextInitialized)
            {
                splashText.text = VisualizationManager.Instance.SplashText;
                splashTextInitialized = true;
            }
        }
        splashTextContainer.gameObject.SetActive(showSplashText && splashTextInitialized);

        if (VisualizationManager.Instance.StepTextLoaded)
        {
            stepText.text = VisualizationManager.Instance.StepText(stepNum);
        }

        stepNumIndicator.text = "Step " + (stepNum + 1).ToString();
	}

    public void NextStep()
    {
        if (stepNum < VisualizationManager.Instance.stepCount - 1)
        {
            stepNum++;
        }
    }

    public void PrevStep()
    {
        if (stepNum > 0)
        {
            stepNum--;
        }
    }

    public void CloseSplashText()
    {
        showSplashText = false;
    }
}
