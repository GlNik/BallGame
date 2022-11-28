using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActiveItem : Item
{
    public int Level;
    public float Radius;
    [SerializeField] protected TextMeshProUGUI LevelText;  
    [SerializeField] protected SphereCollider Collider;
    [SerializeField] protected SphereCollider Trigger;
    [SerializeField] protected Animator AnimatorItem;

    public Projection Projection;
    public Rigidbody Rigidbody;
    public bool IsDead;

    [ContextMenu("IncreaseLevel")]

    protected virtual void Start()
    {
        Projection.Hide();
    }

    public void IncreaseLevel()
    {
        Level++;
        SetLevel(Level);
        AnimatorItem.SetTrigger("IncreaseLevel");

        Trigger.enabled = false;
        Invoke(nameof(EnabelTrigger), 0.08f);
    }

    public virtual void SetLevel(int level)
    {
        Level = level;

        int number = (int)Mathf.Pow(2, level + 1);
        string numberString = number.ToString();
        LevelText.text = numberString;

        //Radius = Mathf.Lerp(0.4f, 0.7f, level / 10f);
        //Vector3 ballScale = Vector3.one * Radius * 2f;
        //_visualTransform.localScale = ballScale;
        //_collider.radius = Radius;
        //_trigger.radius = Radius + 0.1f;
    }

    private void EnabelTrigger()
    {
        Trigger.enabled = true;
    }

    public void SetupToTube()
    {
        Trigger.enabled = false;
        Collider.enabled = false;
        Rigidbody.isKinematic = true;
        Rigidbody.interpolation = RigidbodyInterpolation.None;
    }

    public void Drop()
    {
        Trigger.enabled = true;
        Collider.enabled = true;
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

    public virtual void DoEffect()
    {

    }

    public void Disable()
    {
        Trigger.enabled = false;
        Rigidbody.isKinematic = true;
        Collider.enabled = false;
        IsDead = true;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
