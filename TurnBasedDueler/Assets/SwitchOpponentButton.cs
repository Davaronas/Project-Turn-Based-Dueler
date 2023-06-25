using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SwitchOpponentButton : MonoBehaviour
{
    public enum SwitchToOpponent { Left, Right}

    public SwitchToOpponent side;

    private Button switchButton;

    public static Action<SwitchToOpponent> OnOpponentSwitch;

    void Start()
    {
        switchButton = GetComponent<Button>();
        switchButton.onClick.AddListener(() => Switch());

    }

    private void Switch()
    {
        OnOpponentSwitch?.Invoke(side);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
