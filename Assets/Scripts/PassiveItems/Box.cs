using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : PassiveItem
{
    [Range(0, 2)]
    public int Health = 1;
    [SerializeField] private GameObject[] _levels;
    [SerializeField] private GameObject _breakEffecrtPrefab;
    [SerializeField] private Animator _animator;

    void Start()
    {
        SetHealth(Health);
    }

    public override void OnAffect()
    {
        base.OnAffect();

        Health -= 1;
        Instantiate(_breakEffecrtPrefab, transform.position, Quaternion.Euler(-90, 0, 0));
        _animator.SetTrigger("Shake");
        if (Health < 0)
        {
            Die();
        }
        else
        {
            SetHealth(Health);
        }
    }

    private void SetHealth(int value)
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i].SetActive(i <= value);
        }
    }

    private void Die()
    {
        ScoreManager.Instance.AddScore(ItemType, transform.position);
        Destroy(gameObject);
    }
}
