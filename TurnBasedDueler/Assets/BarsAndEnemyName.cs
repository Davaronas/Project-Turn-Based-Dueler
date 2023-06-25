using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarsAndEnemyName : MonoBehaviour
{
    [SerializeField] private Text enemyNameText;
    [Space]
    [SerializeField] private Text e_hp_text;
    [SerializeField] private Image e_hp_left;
    [SerializeField] private Image e_hp_right;
    [Space]
    [SerializeField] private Text e_armor_text;
    [SerializeField] private Image e_armor_left;
    [SerializeField] private Image e_armor_right;
    [Space]
    [SerializeField] private Text p_hp_text;
    [SerializeField] private Image p_hp_left;
    [SerializeField] private Image p_hp_right;
    [Space]
    [SerializeField] private Text p_armor_text;
    [SerializeField] private Image p_armor_left;
    [SerializeField] private Image p_armor_right;

    void Awake()
    {
        CombatManager.OnBarAndEnemyNameRefresh += RefreshDisplay;
    }

    private void OnDestroy()
    {
        CombatManager.OnBarAndEnemyNameRefresh -= RefreshDisplay;
    }

    private void RefreshDisplay(CombatAgent _player, CombatAgent _enemy)
    {
        enemyNameText.text = _enemy.agentName;

        e_hp_text.text = _enemy.currentHealth + " / " + _enemy.health;
        e_armor_text.text = _enemy.currentArmorPoints + " / " + _enemy.armorPoints;

        e_hp_left.fillAmount = (float)_enemy.currentHealth / _enemy.health;
        e_hp_right.fillAmount = (float)_enemy.currentHealth / _enemy.health;

        e_armor_left.fillAmount = (float)_enemy.currentArmorPoints / _enemy.armorPoints;
        e_armor_right.fillAmount = (float)_enemy.currentArmorPoints / _enemy.armorPoints;



        p_hp_text.text = _player.currentHealth + " / " + _player.health;
        p_armor_text.text = _player.currentArmorPoints + " / " + _player.armorPoints;

        p_hp_left.fillAmount = (float)_player.currentHealth / _player.health;
        p_hp_right.fillAmount = (float)_player.currentHealth / _player.health;

        p_armor_left.fillAmount = (float)_player.currentArmorPoints / _player.armorPoints;
        p_armor_right.fillAmount = (float)_player.currentArmorPoints / _player.armorPoints;
    }

}
