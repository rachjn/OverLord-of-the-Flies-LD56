using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class MovePlayableAsset : PlayableAsset
{
    public ExposedReference<Transform> targetTransform;
    public Vector3 startPosition;
    public Vector3 endPosition;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<MovePlayableBehaviour>.Create(graph);

        MovePlayableBehaviour behaviour = playable.GetBehaviour();
        behaviour.startPosition = startPosition;
        behaviour.endPosition = endPosition;

        return playable;
    }
}
