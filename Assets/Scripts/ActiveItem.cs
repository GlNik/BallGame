using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActiveItem : MonoBehaviour
{
    public int Level;
    public float Radius;
    [SerializeField] protected TextMeshProUGUI LevelText;

    [SerializeField] private Transform _visualTransform;
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private SphereCollider _trigger;
    [SerializeField] private Animator _animator;

    public Projection Projection;
    public Rigidbody Rigidbody;
    public bool IsDead;

    [ContextMenu("IncreaseLevel")]

    private void Start()
    {
        Projection.Hide();
    }

    public void IncreaseLevel()
    {
        Level++;
        SetLevel(Level);
        _animator.SetTrigger("IncreaseLevel");

        _trigger.enabled = false;
        Invoke(nameof(EnabelTrigger), 0.08f);
    }

    public virtual void SetLevel(int level)
    {
        Level = level;

        int number = (int)Mathf.Pow(2, level + 1);
        string numberString = number.ToString();
        LevelText.text = numberString;

        Radius = Mathf.Lerp(0.4f, 0.7f, level / 10f);
        Vector3 ballScale = Vector3.one * Radius * 2f;
        _visualTransform.localScale = ballScale;
        _collider.radius = Radius;
        _trigger.radius = Radius + 0.1f;
    }

    private void EnabelTrigger()
    {
        _trigger.enabled = true;
    }

    public void SetupToTube()
    {
        _trigger.enabled = false;
        _collider.enabled = false;
        Rigidbody.isKinematic = true;
        Rigidbody.interpolation = RigidbodyInterpolation.None;
    }

    public void Drop()
    {
        _trigger.enabled = true;
        _collider.enabled = true;
        Rigidbody.isKinematic = false;
        Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

        transform.parent = null;
        Rigidbody.velocity = Vector3.down * 1.2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsDead) return;
        if (other.attachedRigidbody)
        {
            if (other.attachedRigidbody.GetComponent<ActiveItem>() is ActiveItem otherItem)
            {
                if (Level == otherItem.Level && otherItem.IsDead == false)
                {
                    CollapseManager.Instance.Collapse(this, otherItem);
                }
            }
        }
    }

    public void Disable()
    {
        _trigger.enabled = false;
        Rigidbody.isKinematic = true;
        _collider.enabled = false;
        IsDead = true;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
