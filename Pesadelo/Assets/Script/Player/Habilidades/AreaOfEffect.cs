using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaOfEffect : MonoBehaviour
{
    private EffectType primaryEffect;
    private float effectStrength;
    private Vector3 moveDirection;
    private float moveSpeed;

    private List<EnemySistem> targetsInArea = new List<EnemySistem>();

    public void Initialize(EffectType effect, float strength, float lifetime, float tickRate, bool isMobile, float speed, Vector3 direction)
    {
        this.primaryEffect = effect;
        this.effectStrength = strength;

        if (isMobile)
        {
            this.moveDirection = direction;
            this.moveSpeed = speed;
        }

        Destroy(gameObject, lifetime);
        StartCoroutine(ApplyEffectsCoroutine(tickRate));
    }

    void Update()
    {
        if (moveSpeed > 0 && moveDirection != Vector3.zero)
        {
            transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);
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

    // OnTriggerEnter, OnTriggerExit, e ApplyTickToTargets permanecem exatamente iguais � vers�o anterior.

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
            if (enemy != null && targetsInArea.Contains(enemy))
            {
                if (primaryEffect == EffectType.Slow)
                {
                    enemy.RemoveContinuousSlow();
                }
                targetsInArea.Remove(enemy);
            }
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
                    target.ApplyContinuousSlow(effectStrength);
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