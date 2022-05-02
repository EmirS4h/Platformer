using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween : MonoBehaviour
{
    public enum AnimationType
    {
        ScaleInAndOut,
        MoveAlongAxis,
        Rotate,
    }
    public AnimationType animationType;

    public enum MoveAxis
    {
        X,
        Y,
    }
    public MoveAxis moveAxis;

    public float duration;
    public float rotDegrees;

    public bool loop = false;
    public bool pingpong = false;
    public bool local = false;

    public Vector3 from;
    public Vector3 to;

    private void Start()
    {
        switch (animationType)
        {
            case AnimationType.ScaleInAndOut:
                ScaleInAndOut(gameObject, to, duration, loop, pingpong);
                break;
            case AnimationType.MoveAlongAxis:
                MoveAlongAxis(gameObject, moveAxis, to, duration, loop, pingpong, local);
                break;
            case AnimationType.Rotate:
                Rotate(gameObject, to, rotDegrees, duration, loop, pingpong);
                break;
        }

    }

    public static void ScaleInAndOut(GameObject gameObject, Vector3 vector3, float time, bool loop, bool pingpong)
    {
        if (loop)
        {
            LeanTween.scale(gameObject, vector3, time).setLoopClamp();
        }
        else if (pingpong)
        {
            LeanTween.scale(gameObject, vector3, time).setLoopPingPong();
        }
        else
        {
            LeanTween.scale(gameObject, vector3, time);
        }
    }

    public static void MoveAlongAxis(GameObject gameObject, MoveAxis moveAxis, Vector3 to, float time, bool loop, bool pingpong, bool local)
    {
        switch (moveAxis)
        {
            case MoveAxis.X:
                if (loop)
                {
                    if (local)
                    {
                        LeanTween.moveX(gameObject, gameObject.transform.position.x +  to.x, time).setLoopClamp();
                    }
                    else
                    {
                        LeanTween.moveX(gameObject, to.x, time).setLoopClamp();
                    }
                }
                else if (pingpong)
                {
                    if (local)
                    {
                        LeanTween.moveX(gameObject, gameObject.transform.position.x +  to.x, time).setLoopPingPong();
                    }
                    else
                    {
                        LeanTween.moveX(gameObject, to.x, time).setLoopPingPong();
                    }
                }
                else
                {
                    if (local)
                    {
                        LeanTween.moveX(gameObject, gameObject.transform.position.x +  to.x, time);
                    }
                    else
                    {
                        LeanTween.moveX(gameObject, to.x, time);
                    }
                }
                break;
            case MoveAxis.Y:
                if (loop)
                {
                    if (local)
                    {
                        LeanTween.moveY(gameObject, gameObject.transform.position.y +  to.y, time).setLoopClamp();
                    }
                    else
                    {
                        LeanTween.moveY(gameObject, to.y, time).setLoopClamp();
                    }
                }
                else if (pingpong)
                {
                    if (local)
                    {
                        LeanTween.moveY(gameObject, gameObject.transform.position.y +  to.y, time).setLoopPingPong();
                    }
                    else
                    {
                        LeanTween.moveY(gameObject, to.y, time).setLoopPingPong();
                    }
                }
                else
                {
                    if (local)
                    {
                        LeanTween.moveY(gameObject, gameObject.transform.position.y +  to.y, time);
                    }
                    else
                    {
                        LeanTween.moveY(gameObject, to.y, time);
                    }
                }
                break;
        }
    }
    public static void Rotate(GameObject gameObject, Vector3 vector3, float deg, float time, bool loop, bool pingpong)
    {
        if (loop)
        {
            LeanTween.rotateAround(gameObject, vector3, deg, time).setLoopClamp();
        }
        else if (pingpong)
        {
            LeanTween.rotateAround(gameObject, vector3, deg, time).setLoopPingPong();
        }
        else
        {
            LeanTween.rotateAround(gameObject, vector3, deg, time);
        }
    }
}
