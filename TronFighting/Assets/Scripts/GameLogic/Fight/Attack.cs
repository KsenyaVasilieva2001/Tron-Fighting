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
public class Attack
{
    public string name;
    public float duration;
    public float weight;
}

[System.Serializable]
public class Combo
{
    public string name;
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
        if (currInput >= inputs.Count) return null;
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
