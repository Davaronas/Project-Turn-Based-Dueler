using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CombatWidgetButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public static Action<CombatDirection,EnemyCombat> OnCombatWidgetButtonPressedAttack;
    public static Action<CombatDirection> OnCombatWidgetButtonPressedPredict;
   

    public CombatDirection combatDirection;
    [Space]
    [SerializeField] private Transform holder;
    [Space]
    [SerializeField] private Sprite emptyAndPredictionSprite;
    [SerializeField] private Sprite adaptionSprite;
    [Space]
    [SerializeField] private Vector3 z_rotation = new Vector3(0,0,135);
    [SerializeField] private Vector2 baseSpritePosition = new Vector2(-35, 35);
    [SerializeField] private Vector2 spriteDistance = new Vector2(-10, 10);
    private Vector2 spriteSize = new Vector2(50,70);
    private Color32 emptyDefenseColor = new Color32(0, 0, 0, 100);
    private Color32 baseColor = Color.white;

    private List<Image> arrows = new List<Image>();


    private Button thisButton;

    private PlayerCombat playerCombat;
    private int maxDefense = 0;
    private int adaption = 0;
    private int prediction = 0;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnCombatWidgetButtonPressedPredict?.Invoke(combatDirection);
            DisplaySideDefense();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnCombatWidgetButtonPressedAttack?.Invoke(combatDirection, playerCombat.currentTarget);
        }
    }

    private void Awake()
    {
        playerCombat = FindObjectOfType<PlayerCombat>();
        maxDefense = 0;
        adaption = 0;
        prediction = 0;

    }


    private void OnEnable()
    {
        DisplaySideDefense();
    }


    private void DisplaySideDefense()
    {

        for (int i = 0; i < arrows.Count; i++)
        {
            Destroy(arrows[i].gameObject);
        }
        arrows.Clear();


        maxDefense = playerCombat.playerAgent.maxSideDefensePoints;
        switch(combatDirection)
        {
            case CombatDirection.BottomLeft:
                adaption = playerCombat.playerAgent.BL.adaptionPoints;
                prediction = playerCombat.playerAgent.BL.predictionPoints;
                break;
            case CombatDirection.BottomRight:
                adaption = playerCombat.playerAgent.BR.adaptionPoints;
                prediction = playerCombat.playerAgent.BR.predictionPoints;
                break;
            case CombatDirection.TopLeft:
                adaption = playerCombat.playerAgent.TL.adaptionPoints;
                prediction = playerCombat.playerAgent.TL.predictionPoints;
                break;
            case CombatDirection.TopRight:
                adaption = playerCombat.playerAgent.TR.adaptionPoints;
                prediction = playerCombat.playerAgent.TR.predictionPoints;
                break;
        }


        for (int i = 0; i < maxDefense; i++)
        {
            if(prediction > 0)
            {
                CreatePredictArrow(i);
                prediction--;

                continue;
            }

            if(adaption > 0)
            {
                CreateAdaptArrow(i);
                adaption--;

                continue;
            }


            CreateEmptyArrow(i);
        }


        
    }



    private void CreatePredictArrow(int i)
    {
        Image _newArrow = CreateArrow(i);

        _newArrow.sprite = emptyAndPredictionSprite;
        _newArrow.color = baseColor;
    }

    private void CreateAdaptArrow(int i)
    {
        Image _newArrow = CreateArrow(i);

        _newArrow.sprite = adaptionSprite;
        _newArrow.color = baseColor;
    }

    private void CreateEmptyArrow(int i)
    {
        Image _newArrow = CreateArrow(i);

        _newArrow.sprite = emptyAndPredictionSprite;
        _newArrow.color = emptyDefenseColor;
    }

    private Image CreateArrow(int i)
    {
        Image _newArrow = new GameObject("Arrow", typeof(Image)).GetComponent<Image>();
        //_newArrow.transform.parent = holder;
        _newArrow.transform.SetParent(holder,false);
        _newArrow.raycastTarget = false;
        _newArrow.rectTransform.sizeDelta = spriteSize;
        _newArrow.rectTransform.anchoredPosition = baseSpritePosition + (spriteDistance * i);
        _newArrow.rectTransform.rotation = Quaternion.Euler(z_rotation);

        arrows.Add(_newArrow);

        return _newArrow;
    }


}
