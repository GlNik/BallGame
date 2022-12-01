using UnityEngine;
using UnityEngine.EventSystems;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _sencentivity = 25f;
    [SerializeField] private float _maxXPosition = 2.5f;
    private float _xPosition;
    private float _oldMouseXPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() )
        {
            _oldMouseXPosition = Input.mousePosition.x;
        }

        if(Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            float delta = Input.mousePosition.x - _oldMouseXPosition;
            _oldMouseXPosition = Input.mousePosition.x;
            _xPosition += delta * _sencentivity / Screen.width;
            _xPosition = Mathf.Clamp(_xPosition,-_maxXPosition,_maxXPosition);
            transform.position = new Vector3(_xPosition, transform.position.y, transform.position.z);
        }
    }

}
