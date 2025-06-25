using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public Vector2 MouseDelta { get; private set; }

    [Header("Key inputs")]
    public KeyCode heavyKey;
    public KeyCode lightKey;
    public KeyCode kickKey;
    public KeyCode blockKey;

    [Header("Attacks")]
    public Attack heavyAttack;
    public Attack lightAttack;
    public Attack kickAttack;
    public Attack block;

    public List<Combo> combos;
    public List<int> currentCombos = new List<int>();
    public float comboGap = 0.2f;
    Attack currAttack;
    ComboInput lastInput;

    float timer;
    float gap;
    bool skip; 

    [Header("Components")]
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        InitCombo();
    }

    private void InitCombo()
    {
        for(int i = 0; i < combos.Count; i++)
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

    [SerializeField] private PlayerController playerController;
    void Update()
    {
        CheckAttack();
        InputAttack();
        MoveUpdate();
        KickEnemy();
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
            Attack(GetAttackFromType(input.type));
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
    private void Attack(Attack attack)
    {
        currAttack = attack;
        timer = attack.duration;
        animator.Play(attack.name, -1, 0);
    }

    void ResetCombos()
    {
        gap = 0;
        for(int i = 0; i < currentCombos.Count; i++)
        {
            Combo c = combos[currentCombos[i]];
            c.ResetCombo();
        }
        currentCombos.Clear();
    }

    private void CheckAttack()
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

        if(currentCombos.Count > 0)
        {
            gap += Time.deltaTime;
            if(gap >= comboGap)
            {
                if(lastInput != null)
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

    
    private void MoveUpdate()
    {
        MovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        MouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        /*
        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (inputDirection.magnitude > 0)
        {
            IControllable moveCommand = new MoveCommand(playerController.movementState, inputDirection);
            playerController.animator.SetFloat("Speed", inputDirection.magnitude);
            moveCommand.Execute();

        }
        else
        {
            IControllable moveCommand = new MoveCommand(playerController.movementState, Vector3.zero);
            playerController.animator.SetFloat("Speed", inputDirection.magnitude);
            moveCommand.Execute();
        }
        */
    }
      

    private void KickEnemy()
    {
        if (playerController.currentEnemy != null && Input.GetKeyDown(KeyCode.Space))
        {
            playerController.currentEnemy.TakeDamage(1);
            playerController.uiController.UpdateEnemyHealth(playerController.currentEnemy.currentHealth, playerController.currentEnemy.data.maxHealth);
        }
    }
}




