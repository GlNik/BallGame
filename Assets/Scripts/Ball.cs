//using System;

using UnityEngine;
using UnityEngine.Events;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Ball : ActiveItem
{
    [SerializeField] private BallSettings _ballSettings;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Transform _visualTransform;
    [SerializeField] private AudioSource _connectClip;

    private int _counter;

    public UnityEvent ÑonnectingBall;

    public override void SetLevel(int level)
    {
        base.SetLevel(level);

        _renderer.material = _ballSettings.BallMaterials[level];

        Radius = Mathf.Lerp(0.4f, 0.7f, level / 10f);
        Vector3 ballScale = Vector3.one * Radius * 2f;
        _visualTransform.localScale = ballScale;
        Collider.radius = Radius;
        Trigger.radius = Radius + 0.1f;

        Projection.Setup(_ballSettings.BallProjectionMaterials[level], LevelText.text, Radius);

        if (ScoreManager.Instance.AddScore(ItemType, transform.position, level))
        {
            Die();
        }

    }

    public override void DoEffect()
    {
        base.DoEffect();

        ÑonnectingBall.Invoke();
        IncreaseLevel();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_counter <= 1)
        {
            _counter++;
            _connectClip.pitch = Random.Range(0.8f, 1.2f);
            _connectClip.Play();
        }
    }
}
