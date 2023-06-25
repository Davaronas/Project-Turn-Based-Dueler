using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PredictPointText : MonoBehaviour
{
    private Text predictPointText;

    void Awake()
    {
        predictPointText = GetComponentInChildren<Text>();

        CombatManager.OnPredictPointsChange += UpdateText;
    }

    private void OnDestroy()
    {
        CombatManager.OnPredictPointsChange -= UpdateText;
    }


    private void UpdateText(int _new)
    {
        predictPointText.text = _new.ToString();
    }
}
