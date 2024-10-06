using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;

public class PlayableVideoBehaviour : PlayableBehaviour {
    
    public VideoClip Clip;
    public float Offset;

    private VideoPlayer _player;

    private double GetVideoSeekTime(Playable playable)
    {
        return Math.Max(playable.GetTime() + Offset, 0.0);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        base.OnBehaviourPause(playable, info);

        if (_player == null)
            return;

        _player.Pause();
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        base.ProcessFrame(playable, info, playerData);

        _player = (VideoPlayer)playerData;

        if (_player == null)
            return;

        if(_player.clip != Clip)
            _player.clip = Clip;

        if (_player.timeReference != VideoTimeReference.InternalTime)
            _player.timeReference = VideoTimeReference.InternalTime;

        double videoSeekTime = GetVideoSeekTime(playable);

        bool playing = Math.Abs(Time.unscaledDeltaTime - (playable.GetTime() - playable.GetPreviousTime())) < 0.002f;
        //if(!playing)
        //    Debug.Log("Paused");

        double videoSeekDifference = videoSeekTime - _player.time;

        if (playing)
        {
            if (Math.Abs(videoSeekDifference) > 0.1f)
            {
                if (Math.Floor(Time.timeSinceLevelLoad) != Math.Floor(Time.timeSinceLevelLoad - Time.deltaTime))
                {
                    _player.time = videoSeekTime + 0.05f;
                }
            }

            if(!_player.isPlaying)
            {
                _player.Play();
            }
        }
        else
        {
            _player.time = videoSeekTime;

            _player.Pause();
        }

        if (Math.Floor(Time.timeSinceLevelLoad) != Math.Floor(Time.timeSinceLevelLoad - Time.deltaTime))
        {
            Debug.Log("Video seek difference: " + videoSeekDifference + " Playback Speed: " + _player.playbackSpeed);
        }
    }
}