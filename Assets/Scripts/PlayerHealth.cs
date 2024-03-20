using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float Value = 100;
    public RectTransform ValueRectTransform;

    public GameObject GameplayUI;
    public GameObject GameOverScreen;
    public Animator AnimatorPlayer;

    private float _maxValue;

    private void Start()
    {
        _maxValue = Value;
    }

    public bool IsAlive()
    {
        return Value >= 0;
    }

    public void DealDamage(float damage)
    {
        Value -= damage; 
        if (Value <= 0)
        {
            PlayerIsDead();
        }

        DrawHealthBar();
    }

    private void DrawHealthBar()
    {
        ValueRectTransform.anchorMax = new Vector2(Value / _maxValue, 1);
    }
    private void PlayerIsDead()
    {
        GameOverScreen.SetActive(true);
        GameOverScreen.GetComponent<Animator>().SetTrigger("show");
        GameplayUI.SetActive(false);

        GetComponent<PlayerController>().enabled = false;
        GetComponent<FireballCaster>().enabled = false;
        GetComponent<CameraRotation>().enabled = false;

        AnimatorPlayer.SetTrigger("death");
    }
    public void AddHealth(float amount)
    {
        Value += amount;
        DrawHealthBar();
    }
}
