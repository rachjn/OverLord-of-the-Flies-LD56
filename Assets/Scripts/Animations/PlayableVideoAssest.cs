using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;

public class PlayableVideoAsset : PlayableAsset
{
    public ExposedReference<VideoClip> Clip = new ExposedReference<VideoClip>();
    public float Offset;

    private double _duration;

    public override double duration
    {
        get
        {
            return _duration > 0.0f ? _duration : base.duration;
        }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        ScriptPlayable<PlayableVideoBehaviour> playable = ScriptPlayable<PlayableVideoBehaviour>.Create(graph);

        var director = owner.GetComponent<PlayableDirector>();
        var ps = director.GetGenericBinding(this) as VideoClip;

        playable.GetBehaviour().Clip = Clip.Resolve(graph.GetResolver());
        playable.GetBehaviour().Offset = Offset;
        if(playable.GetBehaviour().Clip != null)
        {
            _duration = playable.GetBehaviour().Clip.length;
        }
        else
        {
            _duration = 0.0f;
        }

        return playable;
    }
}