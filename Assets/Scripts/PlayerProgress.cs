using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    public List<PlayerProgressLavel> Levels;

    public RectTransform ExperienceValueRectTransform;
    public TextMeshProUGUI LavelValueTMP;

    private int _levelValue = 1;

    private float _experienceCurrentValue = 0;
    private float _experienceTargetValue = 100;

    private void Start()
    {
        SetLevel(_levelValue);
        DrawUI();
    }

    public void AddExperience(float value)
    {
        _experienceCurrentValue += value;

        if (_experienceCurrentValue >= _experienceTargetValue)
        {
            SetLevel(_levelValue + 1);
            _experienceCurrentValue = 0;
        }
        DrawUI();
    }

    private void SetLevel(int value)
    {
        _levelValue = value;

        var currentLevel = Levels[_levelValue - 1];
        _experienceTargetValue = currentLevel.ExperienceForTheNextLavel;
        GetComponent<FireballCaster>().Damage = currentLevel.FireballDamage;

        var greanadeCaster = GetComponent<GrenadeCaster>();
        greanadeCaster.Damage = currentLevel.GrenadelDamage;

        if (currentLevel.GrenadelDamage < 0)
            greanadeCaster.enabled = false;
        else
            greanadeCaster.enabled = true;
            
    }

    private void DrawUI()
    {
        ExperienceValueRectTransform.anchorMax = new Vector2(_experienceCurrentValue / _experienceTargetValue, 1);
        LavelValueTMP.text = _levelValue.ToString();
    }
}
