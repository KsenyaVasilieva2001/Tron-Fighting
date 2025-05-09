using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AttackType
{
    heavy = 0,
    light = 1, 
    kick = 2,
    block = 3
}


[System.Serializable]
public class Attack : IControllable
{
    public string name;
    public float duration; // продолжительность анимации

    public void Execute()
    {

    }
}

[System.Serializable]
public class Combo
{
    public List<ComboInput> inputs;
    public Attack comboAttack;
    public UnityEvent onInput;
    int currInput = 0;

    public bool ContinueCombo(ComboInput comboInput)
    {
        if (inputs[currInput].IsEquals(comboInput))
        {
            currInput++;
            if(currInput >= inputs.Count)
            {
                onInput.Invoke();
                ResetCombo();
            }
            return true;
        }
        else
        {
            ResetCombo();
            return false;
        }
    }

    public ComboInput GetCurrentComboInput()
    {
        return inputs[currInput];
    }

    public void ResetCombo()
    {
        currInput = 0;
    }
}

[System.Serializable]
public class ComboInput
{
    public AttackType type;

    public ComboInput(AttackType type)
    {
        this.type = type;
    }

    public bool IsEquals(ComboInput comboInput)
    {
        return type == comboInput.type;
    }
}
