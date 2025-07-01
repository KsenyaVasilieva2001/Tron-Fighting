using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFightController : FightControllerBase
{
    [Header("Key inputs")]
    public KeyCode heavyKey = KeyCode.I;
    public KeyCode lightKey = KeyCode.J;
    public KeyCode kickKey = KeyCode.L;
    public KeyCode blockKey = KeyCode.K;

    protected override void Update()
    {
        CheckAttack();
        InputAttack();
    }

    private void InputAttack()
    {
        ComboInput input = null;
        if (Input.GetKeyDown(heavyKey))
        {
            input = new ComboInput(AttackType.heavy);
        }
        if (Input.GetKeyDown(lightKey))
        {
            input = new ComboInput(AttackType.light);
        }
        if (Input.GetKeyDown(kickKey))
        {
            input = new ComboInput(AttackType.kick);
        }
        if (Input.GetKeyDown(blockKey))
        {
            input = new ComboInput(AttackType.block);
        }
        if (input == null) return;
        lastInput = input;

        List<int> remove = new List<int>();
        for(int i = 0; i < currentCombos.Count; i++)
        {
            Combo c = combos[currentCombos[i]];
            if (c.ContinueCombo(input))
            {
                gap = 0;
            }
            else
            {
                remove.Add(i);
            }
        }

        if (skip)
        {
            skip = false;
            return;
        }

        for (int i = 0; i < combos.Count; i++)
        {
            if (currentCombos.Contains(i)) continue;
            if (combos[i].ContinueCombo(input)){
                currentCombos.Add(i);
                gap = 0;
            }
        }


        foreach(int i in remove)
        {
            currentCombos.RemoveAt(i);
        }
        

        if(currentCombos.Count <= 0)
        {
            Debug.Log("Call attack");
            Debug.Log(input.type);
            Attack(GetAttackFromType(input.type));
        }
    }  
}




