using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitpoints = 5;

    [Tooltip("Adds amount to maxHitpoints when enemy dies for next respawn")]
    [SerializeField] int difficultyRamp = 1;

    [SerializeField] GameObject displayLevel;

    int currentHitpoints = 0;

    Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        displayLevel = GameObject.FindGameObjectWithTag("LevelScale");
    }

    void OnEnable()
    {
        currentHitpoints = maxHitpoints;
        UpdateDisplay();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void UpdateDisplay()
    {
        TMPro.TextMeshProUGUI UI;
        UI = displayLevel.GetComponent<TMPro.TextMeshProUGUI>();
        UI.text = "Enemy Level: " + maxHitpoints;
    }

    void ProcessHit()
    {
        currentHitpoints--;
        if (currentHitpoints <= 0)
        {
            enemy.RewardGold();
            maxHitpoints += difficultyRamp;
            gameObject.SetActive(false);
        }
    }
}
