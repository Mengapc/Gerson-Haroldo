using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaOfEffect : MonoBehaviour
{
    private EffectType primaryEffect;
    private float effectStrength;
    private float effectDuration;
    private Vector3 moveDirection;
    private float moveSpeed;

    private List<EnemySistem> targetsInArea = new List<EnemySistem>();

    public void Initialize(skills skillData, float finalStrength, float finalLifetime, float finalDuration, Vector3 initialDirection)
    {
        this.primaryEffect = skillData.effectType;
        this.effectStrength = finalStrength;
        this.effectDuration = finalDuration;

        if (skillData.isMobile)
        {
            this.moveDirection = initialDirection;
            this.moveSpeed = skillData.moveSpeed;
        }

        Destroy(gameObject, finalLifetime);
        StartCoroutine(ApplyEffectsCoroutine(skillData.effectTickRate));
    }

    void Update()
    {
        if (moveSpeed > 0 && moveDirection != Vector3.zero)
        {
            transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("basic_enemy"))
        {
            EnemySistem enemy = other.GetComponent<EnemySistem>();
            if (enemy != null && !targetsInArea.Contains(enemy))
            {
                targetsInArea.Add(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("basic_enemy"))
        {
            EnemySistem enemy = other.GetComponent<EnemySistem>();
            if (enemy != null)
            {
                targetsInArea.Remove(enemy);
            }
        }
    }

    private IEnumerator ApplyEffectsCoroutine(float tickRate)
    {
        while (true)
        {
            yield return new WaitForSeconds(tickRate);
            ApplyTickToTargets();
        }
    }

    private void ApplyTickToTargets()
    {
        for (int i = targetsInArea.Count - 1; i >= 0; i--)
        {
            EnemySistem target = targetsInArea[i];
            if (target == null || !target.gameObject.activeInHierarchy)
            {
                targetsInArea.RemoveAt(i);
                continue;
            }

            switch (primaryEffect)
            {
                case EffectType.Slow:
                    target.SlowEnemy(effectStrength, effectDuration);
                    break;
                case EffectType.Push:
                    target.PushFrom(transform.position, effectStrength);
                    break;
                case EffectType.Pull:
                    target.PullTo(transform.position, effectStrength);
                    break;
            }
        }
    }
}