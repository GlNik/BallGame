using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseManager : MonoBehaviour
{

    public static CollapseManager Instance { get; private set; }

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
        StartCoroutine(CollapseProcess(itemA, itemB));
    }

    public IEnumerator CollapseProcess(ActiveItem itemA, ActiveItem itemB)
    {
        itemA.Disable();
        Vector3 startPosition = itemA.transform.position;
        for (float t = 0; t < 1f; t += Time.deltaTime / 0.08f)
        {
            itemA.transform.position = Vector3.Lerp(startPosition, itemB.transform.position, t);
            yield return null;
        }
        itemA.transform.position = itemB.transform.position;
        itemA.Die();
        itemB.IncreaseLevel();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

}
