using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MovePlayableBehaviour : PlayableBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float duration;

    private Vector3 initialPosition;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        // Store the initial position of the GameObject when the Timeline starts
        if (Application.isPlaying)
        {
            initialPosition = startPosition;
        }
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        // Ensure the object is valid
        Transform transform = playerData as Transform;
        if (transform == null)
            return;

        // Get the current time on the timeline
        float time = (float)(playable.GetTime() / playable.GetDuration());

        // Linearly interpolate between the start and end position
        transform.position = Vector3.Lerp(startPosition, endPosition, time);
    }
}
