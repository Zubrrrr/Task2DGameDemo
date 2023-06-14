using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementAndDamage : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _stoppingDistance = 1f;
    [SerializeField] private float _attackDistance = 2f;
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] private int _minDamage = 5;
    [SerializeField] private int _maxDamage = 10;
    [SerializeField] private float _attackInterval = 1f;

    private Transform playerTransform;
    private bool isPlayerInRange;
    private bool isAttacking;
    private float startY;
    private float lastAttackTime;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(_playerTag).transform;
        startY = transform.position.y;
        lastAttackTime = -_attackInterval;
    }

    private void Update()
    {
        if (playerTransform == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= _detectionRadius)
        {
            if (distanceToPlayer <= _attackDistance && CanAttack())
            {
                isPlayerInRange = true;
                isAttacking = true;
                StartCoroutine(AttackPlayer());
            }
            else if (distanceToPlayer <= _stoppingDistance)
            {
                isPlayerInRange = true;
                isAttacking = false;
            }
            else
            {
                isPlayerInRange = true;
                isAttacking = false;
                MoveTowardsPlayer();
            }
        }
        else
        {
            isPlayerInRange = false;
            isAttacking = false;
        }
    }

    private IEnumerator AttackPlayer()
    {
        int damage = Random.Range(_minDamage, _maxDamage + 1);
        playerTransform.GetComponent<HealthPlayer>().Damage(damage);
        lastAttackTime = Time.time;
        yield return new WaitForSeconds(_attackInterval);
        isAttacking = false;
    }

    private bool CanAttack()
    {
        return Time.time - lastAttackTime >= _attackInterval;
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = playerTransform.position - transform.position;
        direction.y = 0f;
        direction.Normalize();

        Vector2 movement = new Vector2(direction.x * _moveSpeed * Time.deltaTime, 0f);
        transform.Translate(movement);
    }
}
