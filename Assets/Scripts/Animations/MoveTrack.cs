using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackBindingType(typeof(Transform))]
[TrackClipType(typeof(MovePlayableAsset))]
public class MoveTrack : TrackAsset
{
    // This ensures the Timeline can bind to Transform and work with MovePlayableAsset
}
