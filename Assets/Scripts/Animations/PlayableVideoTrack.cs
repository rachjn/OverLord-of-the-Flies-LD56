using UnityEngine.Video;
using UnityEngine.Timeline;

[TrackClipType(typeof(PlayableVideoAsset), false)]
[TrackBindingType(typeof(VideoPlayer))]
public class PlayableVideoTrack : TrackAsset {
    
}