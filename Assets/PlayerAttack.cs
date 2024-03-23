using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private ScriptableStats _stats;
    [SerializeField] private InputActionReference attack,move;

    private Transform attackPos;
    public Transform rightPos;
    public Transform leftPos;
    public LayerMask enemies;

    private Vector2 moveVec = Vector2.zero;


    private void OnEnable()
    {
        attack.action.performed += PerformAttack;
        move.action.performed += PerformedMoved;
    }

    private void PerformedMoved(InputAction.CallbackContext obj)
    {
        moveVec = obj.ReadValue<Vector2>();
        if (moveVec.x == 1.00)
        {
            attackPos = rightPos;
        }
        else
        {
            attackPos = leftPos;
        }
    }

    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
        move.action.performed -= PerformedMoved;
    }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        
        
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, _stats.AttackRange, enemies);

        for (int i =0; i< enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyMovement>().TakeDamage(_stats.AttackDamage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, _stats.AttackRange);
    }





}
