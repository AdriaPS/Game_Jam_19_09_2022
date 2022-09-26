using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    public UnityEvent onWalk;

    public UnityEvent onJump;

    public void OnWalk()
    {
        onWalk?.Invoke();
    }

    public void OnJump()
    {
        onJump?.Invoke();
    }
}