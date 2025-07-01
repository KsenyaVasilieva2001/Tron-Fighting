using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FightControllerBase : MonoBehaviour
{
    [Header("Attacks")]
    public Attack heavyAttack;
    public Attack lightAttack;
    public Attack kickAttack;
    public Attack block;

    public List<Combo> combos;
    public List<int> currentCombos = new List<int>();
    public float comboGap = 0.2f;

    protected Attack currAttack;
    protected ComboInput lastInput;

    protected float timer;
    protected float gap;
    protected bool skip;

    [Header("Components")]
    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        InitCombo();
    }

    protected virtual void Update()
    {
        CheckAttack();
    }

    private void InitCombo()
    {
        for (int i = 0; i < combos.Count; i++)
        {
            Combo c = combos[i];
            c.onInput.AddListener(() =>
            {
                skip = true;
                Attack(c.comboAttack);
                ResetCombos();
            });
        }
    }

    public Attack GetAttackFromType(AttackType type)
    {
        if (type == AttackType.heavy) return heavyAttack;
        if (type == AttackType.light) return lightAttack;
        if (type == AttackType.kick) return kickAttack;
        if (type == AttackType.block) return block;
        return null;
    }

    public void Attack(Attack attack)
    {
        currAttack = attack;
        timer = attack.duration;
        animator.Play(attack.name, -1, 0);
    }

    protected void ResetCombos()
    {
        gap = 0;
        for (int i = 0; i < currentCombos.Count; i++)
        {
            Combo c = combos[currentCombos[i]];
            c.ResetCombo();
        }
        currentCombos.Clear();
    }

    protected void CheckAttack()
    {
        if (currAttack != null)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                currAttack = null;
            }
            return;
        }

        if (currentCombos.Count > 0)
        {
            gap += Time.deltaTime;
            if (gap >= comboGap)
            {
                if (lastInput != null)
                {
                    Attack(GetAttackFromType(lastInput.type));
                    lastInput = null;
                }
                ResetCombos();
            }
        }
        else
        {
            gap = 0;
        }
    }

    public bool IsBlocking()
    {
        return currAttack == block;
    }

    public bool IsAttacking()
    {
        return currAttack != null && currAttack != block;
    }
}