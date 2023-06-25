using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EventDisplay : MonoBehaviour
{
    [SerializeField] private GameObject eventTextPrefab;
    [SerializeField] private GameObject numbersTextPrefab;




    // Start is called before the first frame update
    void Start()
    {
        CombatManager.OnEventTextCreation += TextCreation;
        CombatManager.OnNumbersEventTextCreation += NumbersTextCreation;
        CombatManager.OnClearEventDisplay += Clear;
    }

    private void OnDestroy()
    {
        CombatManager.OnEventTextCreation -= TextCreation;
        CombatManager.OnNumbersEventTextCreation -= NumbersTextCreation;
        CombatManager.OnClearEventDisplay -= Clear;
    }



    private void TextCreation(string _text)
    {
       Text _newText = Instantiate(eventTextPrefab, transform.position, Quaternion.identity, transform).GetComponent<Text>();
        _newText.text = _text;
    }

    private void NumbersTextCreation(string _agentName, int _hp, int _armor)
    {
        if(_hp == 0 && _armor == 0) { return; }

        NumbersEventDisplay _ned = Instantiate(numbersTextPrefab, transform.position, Quaternion.identity, transform).GetComponent<NumbersEventDisplay>();
        _ned.agentName.text = _agentName + ":";
        _ned.hp_loss.text = _hp != 0 ? (-_hp).ToString() : "";
        _ned.armor_loss.text = _armor != 0 ? (-_armor).ToString() : "";
    }

    private void Clear()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }




}
