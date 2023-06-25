using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private GameObject combatWidgets;
    private PlayerCombat player;

    private GameObject switchToRightOpponentButton;
    private GameObject switchToLeftOpponentButton;

    private GameObject enemybarsAndNameDisplay;
    private GameObject playerBars;

    private Text predictPointText;



    void Awake()
    {
        combatWidgets = FindObjectOfType<CombatWidgets>().gameObject;
        player = FindObjectOfType<PlayerCombat>();
        playerBars = FindObjectOfType<BarsAndEnemyName>().transform.GetChild(0).gameObject;
        enemybarsAndNameDisplay = FindObjectOfType<BarsAndEnemyName>().transform.GetChild(1).gameObject;

        SwitchOpponentButton[] _switchOpponentButtons = FindObjectsOfType<SwitchOpponentButton>();

        for (int i = 0; i < _switchOpponentButtons.Length; i++)
        {
            if(_switchOpponentButtons[i].side == SwitchOpponentButton.SwitchToOpponent.Left)
            {
                switchToLeftOpponentButton = _switchOpponentButtons[i].gameObject;
            }
            else
            {
                switchToRightOpponentButton = _switchOpponentButtons[i].gameObject;
            }
        }
    }


    public void SetWidgetState(bool _state)
    {
        combatWidgets.SetActive(_state);
    }

    public void SetBarState(bool _playerState,bool _enemyState)
    {
        enemybarsAndNameDisplay.SetActive(_enemyState);
        playerBars.SetActive(_playerState);
    }

    public void SetSwitchButtonsState(bool _state)
    {
        if(_state == false)
        {
            switchToLeftOpponentButton.SetActive(false);
            switchToRightOpponentButton.SetActive(false);
        }
        else
        {

            if (player.TargetLeftNeighbourExists())
            {
                switchToRightOpponentButton.SetActive(true);
            }
            else
            {
                switchToRightOpponentButton.SetActive(false);
            }

            if (player.TargetRightNeighbourExists())
            {
                switchToLeftOpponentButton.SetActive(true);
            }
            else
            {
                switchToLeftOpponentButton.SetActive(false);
            }
        }


    }
}
