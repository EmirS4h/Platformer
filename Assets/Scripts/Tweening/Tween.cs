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

    public bool twen = true;

    public int id;

    private void Awake()
    {
        if (twen)
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
                    Rotate(gameObject, to, rotDegrees, duration);
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
    public void Rotate(GameObject objectToRotate, Vector3 to, float rotDegrees, float duration)
    {
        _tweenObject = LeanTween.rotateAround(objectToRotate, to, rotDegrees, duration);
        _tweenObject.setLoopClamp();
    }
    public void MoveToLocation(GameObject objectToMove, Vector3 location, float duration)
    {
        _tweenObject = LeanTween.move(objectToMove, location, duration);
    }
    public void MoveUpDown(GameObject objectToMove, float to, float duration)
    {
        _tweenObject = LeanTween.moveY(objectToMove, to, duration);
        _tweenObject.setLoopPingPong();
    }
    public void MoveLeftRight()
    {
        if (local)
        {
            _tweenObject = LeanTween.moveX(gameObject, gameObject.transform.position.x +  to.x, duration);
            _tweenObject.setLoopPingPong();
            id = _tweenObject.id;
        }
        else
        {
            _tweenObject = LeanTween.moveX(gameObject, to.x, duration);
            _tweenObject.setLoopPingPong();
            id = _tweenObject.id;
        }
    }
    public void MoveUpDown()
    {
        if (local)
        {
            _tweenObject = LeanTween.moveY(gameObject, gameObject.transform.position.y +  to.y, duration);
            _tweenObject.setLoopPingPong();
            id = _tweenObject.id;
        }
        else
        {
            _tweenObject = LeanTween.moveY(gameObject, to.y, duration);
            _tweenObject.setLoopPingPong();
            id = _tweenObject.id;
        }
    }
    public void PauseTween()
    {
        LeanTween.pause(id);
    }
    public void CancelTween()
    {
        LeanTween.cancel(id);
    }
    public void ResumeTween()
    {
        LeanTween.resume(id);
    }
}
