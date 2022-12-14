using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Creator : MonoBehaviour
{
    [SerializeField] private Transform _tube;
    [SerializeField] private Transform _spawner;
    [SerializeField] private ActiveItem _ballPrefab;
    [SerializeField] private Transform _rayTransform;
    [SerializeField] private Text _numberOfBallsText;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _LoseWindow;


    [Space(5)]
    [Header("Balls Level")]
    [Range(1, 5)]
    [SerializeField] private int _itemLevel;

    private ActiveItem _itemInTube;
    private ActiveItem _itemInSpawner;
    private Coroutine _waitForLose;

    private int _ballsLeft;

    private void Start()
    {
        _ballsLeft = Level.Instance.NumberOfBalls;
        UpdateBallsLeftText();

        CreateItemInTube();
        StartCoroutine(MoveToSpawner());
    }

    public void UpdateBallsLeftText()
    {
        _numberOfBallsText.text = _ballsLeft.ToString();
    }

    private void CreateItemInTube()
    {
        if (_ballsLeft == 0)
        {
            Debug.Log("Balls Ended");
            return;
        }
        // ????????? ???? ????????? ???????
        int itemLevel = Random.Range(0, _itemLevel);
        _itemInTube = Instantiate(_ballPrefab, _tube.position, Quaternion.identity);
        _itemInTube.SetLevel(itemLevel);
        _itemInTube.SetupToTube();

        _ballsLeft--;
        UpdateBallsLeftText();
    }

    private IEnumerator MoveToSpawner()
    {
        _itemInTube.transform.parent = _spawner;
        for (float t = 0; t < 1f; t += Time.deltaTime / 0.4f)
        {
            _itemInTube.transform.position = Vector3.Lerp(_tube.position, _spawner.position, t);
            yield return null;
        }
        _itemInTube.transform.localPosition = Vector3.zero;
        _itemInSpawner = _itemInTube;
        _rayTransform.gameObject.SetActive(true);
        _itemInSpawner.Projection.Show();
        _itemInTube = null;
        CreateItemInTube();
    }

    public void AddBallToSpawner()
    {
        _LoseWindow.SetActive(false);
        _ballsLeft += 5;
        //StopWaitForLose();
        UpdateBallsLeftText();
        CreateItemInTube();
        StartCoroutine(MoveToSpawner());

    }

    private void LateUpdate()
    {
        if (_itemInSpawner)
        {
            Ray ray = new Ray(_spawner.position, Vector3.down);
            RaycastHit hit;
            if (Physics.SphereCast(ray, _itemInSpawner.Radius, out hit, 100, _layerMask, QueryTriggerInteraction.Ignore))
            {
                _rayTransform.localScale = new Vector3(_itemInSpawner.Radius * 2f, hit.distance, 1f);
                _itemInSpawner.Projection.SetPosition(_spawner.position + Vector3.down * hit.distance);
            }

            if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Drop();
            }
        }
    }

    private void Drop()
    {
        _itemInSpawner.Drop();
        _itemInSpawner.Projection.Hide();
        _itemInSpawner = null;
        _rayTransform.gameObject.SetActive(false);

        if (_itemInTube)
        {
            StartCoroutine(MoveToSpawner());
        }
        else
        {
            Debug.Log("startlose");
            _waitForLose = StartCoroutine(WaitForLose());
            CollapseManager.Instance.OnCollapse.AddListener(ResetLoseTimer);
            GameManager.Instance.OnWin.AddListener(StopWaitForLose);
        }
    }

    private void ResetLoseTimer()
    {
        if (_waitForLose != null)
        {
            Debug.Log("ResetLoseTimer");
            StopCoroutine(_waitForLose);
            _waitForLose = StartCoroutine(WaitForLose());
        }
    }

    private void StopWaitForLose()
    {
        if (_waitForLose != null)
        {
            Debug.Log("StopWaitForLose");
            StopCoroutine(_waitForLose);
        }
    }

    private IEnumerator WaitForLose()
    {
        for (float t = 0; t < 5f; t += Time.deltaTime)
        {
            yield return null;
        }
        Debug.Log("Lose");
        CollapseManager.Instance.OnCollapse?.RemoveListener(ResetLoseTimer);
        GameManager.Instance.Lose();
    }

}
