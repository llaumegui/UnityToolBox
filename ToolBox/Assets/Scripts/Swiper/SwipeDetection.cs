using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    //Works with InputSystem
    [SerializeField]
    InputManager _inputManager;

    float _minimumDistance = .2f;   //
    float _maximumTime = 1;         // requirements to be considered a swipe

    float _directionThreshold = .9f;

    [SerializeField]
    Transform _trail;

    Vector2 _startPosition;
    float _startTime;
    Vector2 _endPosition;
    float _endTime;

    Coroutine _trailCoroutine;

    private void OnEnable()
    {
        _inputManager.OnStartTouch += SwipeStart;
        _inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        _inputManager.OnStartTouch -= SwipeStart;
        _inputManager.OnEndTouch -= SwipeEnd;
    }

    void SwipeStart(Vector2 position, float time)
    {
        _startPosition = position;
        _startTime = time;

        if(_trail!=null)
        {
            _trail.gameObject.SetActive(true);
            _trail.position = position;
            _trailCoroutine = StartCoroutine(TrailCoroutine());
        }
    }

    void SwipeEnd(Vector2 position, float time)
    {
        if (_trail!=null)
        {
            _trail.gameObject.SetActive(false);
            StopCoroutine(_trailCoroutine);
        }

        _endPosition = position;
        _endTime = time;
        DetectSwipe();
    }

    IEnumerator TrailCoroutine()
    {
        while(true)
        {
            _trail.position = _inputManager.PrimaryPosition();
            yield return null;
        }
    }

    void DetectSwipe()
    {
        if ((_startPosition == Vector2.zero) || (_endPosition == Vector2.zero))
            return;

        if (Vector3.Distance(_startPosition,_endPosition)>=_minimumDistance && (_endTime-_startTime)<=_maximumTime)
        {
            Debug.Log($"Distance : {Vector3.Distance(_startPosition, _endPosition)} | Time : {_endTime - _startTime}");
            Debug.DrawLine(_startPosition, _endPosition, Color.red, .5f);

            Vector3 direction = _endPosition - _startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;

            SwipeDirection(direction2D);
        }
    }

    void SwipeDirection(Vector2 direction)
    {
        //Horizontal Or Vertical

        if(Vector2.Dot(Vector2.up,direction)>_directionThreshold)
        {
            Debug.Log("Swipe Up");
        }
        else if (Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
            Debug.Log("Swipe Down");
        }
        else if(Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            Debug.Log("Swipe Left");
        }
        else if(Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
            Debug.Log("Swipe Right");
        }
        else
        {
            //Diagonal

            if(Vector2.Dot(Vector2.one,direction)>_directionThreshold)
            {
                Debug.Log("Swipe UpRight");
            }
            else if(Vector2.Dot(Vector2.one*-1,direction)>_directionThreshold)
            {
                Debug.Log("Swipe DownLeft");
            }
            else if(Vector2.Dot(new Vector2(1,-1),direction)>_directionThreshold)
            {
                Debug.Log("Swipe DownRight");
            }
            else if(Vector2.Dot(new Vector2(-1,1),direction)>_directionThreshold)
            {
                Debug.Log("Swipe UpLeft");
            }
        }
    }
}
