using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerAndThirst : MonoBehaviour
{
    [Header("Hunger & Thirst Settings")]
    [SerializeField] float characterMaxHealth = 100f;
    [SerializeField] float maxHunger = 100f;
    [SerializeField] float maxThirst = 100f;
    [SerializeField] float hungerDecrase = 1f;
    [SerializeField] float thirstDecrase = 1f;
    [Header("Hunger & Thirst Damage")]
    [SerializeField] float hungerDamage = 1f;
    [SerializeField] float thirstDamage = 1f;
    [SerializeField] Image splatterImage;
    [Header("Hunger & Thirst Regenerate")]
    [SerializeField] float regenerationRate= 1f;

    public bool isHunger = false;
    public bool isThirst = false;

    private float currentHealth;
    private float currentHunger;
    private float currentThirst;
    void Start()
    {
        currentHealth = characterMaxHealth;
        currentHunger = maxHunger;
        currentThirst = maxThirst;
    }
    void Update()
    {
        DecraseHunger();
        DecraseThirst();
        CheckHungerAndThirst();
        CheckHealth();
   /*   Regenerate();
        RestoreHunger();
        RestoreThirst(); */
    }
    private void DecraseHunger()
    {
        currentHunger -= hungerDecrase * Time.deltaTime;
        currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger);
    }
    private void DecraseThirst()
    {
        currentThirst -= thirstDecrase * Time.deltaTime;
        currentThirst = Mathf.Clamp(currentThirst, 0f, maxThirst);
    }
    private void CheckHungerAndThirst()
    {
        if (currentHunger <= 0f)
        {
            ApplyHungerEffect();
        }
        else if (currentHunger > 0f)
        {
            isHunger = false;
        }
        if (currentThirst <= 0f)
        {
            ApplyThirstEffect();
        }
        else if (currentThirst > 0f)
        {
            isThirst = false;
        }
    }
    private void CheckHealth()
    {
        Color splatterAlpha = splatterImage.color;
        splatterAlpha.a = 1 - (currentHealth / characterMaxHealth);
        splatterImage.color = splatterAlpha;
        if (currentHealth <= 0f)
        {
            Debug.LogWarning("Dead");
        }
    }
    private void ApplyHungerEffect()
    {
        isHunger = true;
        currentHealth -= hungerDamage * Time.deltaTime;
        currentHealth = Mathf.Clamp(currentHealth, 0f, characterMaxHealth);
    }
    private void ApplyThirstEffect()
    {
        isThirst = true;
        currentHealth -= thirstDamage * Time.deltaTime;
        currentHealth = Mathf.Clamp(currentHealth, 0f, characterMaxHealth);
    }
    private void RestoreHunger()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            currentHunger += 20;
            currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger);
        }
    }
    private void RestoreThirst()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentThirst += 20;
            currentThirst = Mathf.Clamp(currentThirst, 0f, maxThirst);
        }
    }
    private void Regenerate()
    {
        if ((!isThirst && !isHunger) && currentHealth < characterMaxHealth)
        {
            currentHealth += regenerationRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0f, characterMaxHealth);
        }
    }
}