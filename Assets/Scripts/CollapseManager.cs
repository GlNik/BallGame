using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class CollapseManager : MonoBehaviour
{
    public static CollapseManager Instance { get; private set; }

    public UnityEvent OnCollapse;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Collapse(ActiveItem itemA, ActiveItem itemB)
    {
        ActiveItem toItem;
        ActiveItem fromItem;
        // если высота шаров по у отличаетс€ больше чем на 0.02а
        if (Mathf.Abs(itemA.transform.position.y - itemB.transform.position.y) > 0.02f)
        {
            //
            if (itemA.transform.position.y > itemB.transform.position.y)
            {
                fromItem = itemA;
                toItem = itemB;
            }
            else
            {
                fromItem = itemB;
                toItem = itemA;
            }
        }
        else
        {
            // если скорость ј больше чем скорость Ѕ
            if (itemA.Rigidbody.velocity.magnitude > itemB.Rigidbody.velocity.magnitude)
            {
                fromItem = itemA;
                toItem = itemB;
            }
            else
            {
                fromItem = itemB;
                toItem = itemA;
            }
        }

        StartCoroutine(CollapseProcess(fromItem, toItem));
    }

    public IEnumerator CollapseProcess(ActiveItem fromItem, ActiveItem toItem)
    {
        fromItem.Disable();

        if (fromItem.ItemType == ItemType.Ball || toItem.ItemType == ItemType.Ball)
        {
            Vector3 startPosition = fromItem.transform.position;
            for (float t = 0; t < 1f; t += Time.deltaTime / 0.08f)
            {
                // ругаетс€ почему то
                fromItem.transform.position = Vector3.Lerp(startPosition, toItem.transform.position, t);
                yield return null;
            }
            fromItem.transform.position = toItem.transform.position;
        }

        if (fromItem.ItemType == ItemType.Ball && toItem.ItemType == ItemType.Ball)
        {
            fromItem.Die();
            toItem.DoEffect();
            ExplodeBall(toItem.transform.position, toItem.Radius + 0.1f);
        }
        else
        {
            if (fromItem.ItemType == ItemType.Ball)
            {
                fromItem.Die();
            }
            else
            {
                fromItem.DoEffect();
            }

            if (toItem.ItemType == ItemType.Ball)
            {
                toItem.Die();
            }
            else
            {
                toItem.DoEffect();
            }
        }

        OnCollapse.Invoke();

    }

    public void ExplodeBall(Vector3 position, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(position, radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            PassiveItem passiveItem = colliders[i].GetComponent<PassiveItem>();
            if (colliders[i].attachedRigidbody)
            {
                passiveItem = colliders[i].attachedRigidbody.GetComponent<PassiveItem>();
            }
            if (passiveItem)
            {
                passiveItem.OnAffect();
            }
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

}
