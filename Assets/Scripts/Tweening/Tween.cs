using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween : MonoBehaviour
{
    private LTDescr _tweenObject;
    public enum AnimationType
    {
        ScaleInAndOut,
        MoveAlongAxis,
        Rotate,
    }
    public AnimationType animationType;

    public enum MoveAxis
    {
        None,
        X,
        Y,
    }
    public MoveAxis moveAxis;

    public enum EaseType
    {
        None,
        EaseInBack,
        EaseInBounce,
        EaseInOutBack,
        EaseInOutBounce,
        EaseOutBack,
        EaseOutBounce,
        EaseInOutElastic,
        Linear,
    }
    public EaseType easeType;

    public float duration;
    public float rotDegrees;

    public bool loop = false;
    public bool pingpong = false;
    public bool local = false;
    public bool tweenAfterATime = false;

    public float delay = 0.0f;

    public Vector3 to;

    private void Start()
    {
        switch (animationType)
        {
            case AnimationType.ScaleInAndOut:
                ScaleInAndOut();
                break;
            case AnimationType.MoveAlongAxis:
                MoveAlongAxis();
                break;
            case AnimationType.Rotate:
                Rotate();
                break;
        }
        if (loop)
        {
            _tweenObject.setLoopClamp();
        }
        if (pingpong)
        {
            _tweenObject.setLoopPingPong();
        }
        if (tweenAfterATime)
        {
            _tweenObject.setDelay(delay);
        }
        switch (easeType)
        {
            case EaseType.None:
                break;
            case EaseType.EaseInBack:
                _tweenObject.setEase(LeanTweenType.easeInBack);
                break;
            case EaseType.EaseInBounce:
                _tweenObject.setEase(LeanTweenType.easeInBounce);
                break;
            case EaseType.EaseInOutBack:
                _tweenObject.setEase(LeanTweenType.easeInOutBack);
                break;
            case EaseType.EaseInOutBounce:
                _tweenObject.setEase(LeanTweenType.easeInOutBounce);
                break;
            case EaseType.EaseOutBack:
                _tweenObject.setEase(LeanTweenType.easeOutBack);
                break;
            case EaseType.EaseOutBounce:
                _tweenObject.setEase(LeanTweenType.easeOutBounce);
                break;
            case EaseType.EaseInOutElastic:
                _tweenObject.setEase(LeanTweenType.easeInOutElastic);
                break;
            case EaseType.Linear:
                _tweenObject.setEase(LeanTweenType.linear);
                break;
        }
    }

    public void ScaleInAndOut()
    {
        _tweenObject = LeanTween.scale(gameObject, to, duration);
    }

    public void MoveAlongAxis()
    {
        switch (moveAxis)
        {
            case MoveAxis.None:
                break;
            case MoveAxis.X:
                if (local)
                {
                    _tweenObject = LeanTween.moveX(gameObject, gameObject.transform.position.x +  to.x, duration);
                }
                else
                {
                    _tweenObject = LeanTween.moveX(gameObject, to.x, duration);
                }
                break;
            case MoveAxis.Y:
                if (local)
                {
                    _tweenObject = LeanTween.moveY(gameObject, gameObject.transform.position.y +  to.y, duration);
                }
                else
                {
                    _tweenObject = LeanTween.moveY(gameObject, to.y, duration);
                }
                break;
        }
    }
    public void Rotate()
    {
        LeanTween.rotateAround(gameObject, to, rotDegrees, duration);
    }
}
