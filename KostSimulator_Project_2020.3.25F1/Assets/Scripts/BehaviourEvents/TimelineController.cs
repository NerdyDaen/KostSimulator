using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class TimelineController : MonoBehaviour
{
    [Header("Timelines")]
    public List<PlayableDirector> playableDirectors;
    public List<TimelineAsset> timelines;

    public void PauseTimeline(int flag)
    {
        foreach (PlayableDirector director in playableDirectors)
        {
            director.playableGraph.GetRootPlayable(0).SetSpeed(flag);
        }
    }

    public void PlayFormTimeLines(int index)
    {
        TimelineAsset selectedAsset;

        if (timelines.Count <= index)
        {
            selectedAsset = timelines[timelines.Count - 1];
        }
        else
        {
            selectedAsset = timelines[index];
        }

        playableDirectors[0].Play(selectedAsset);
    }
}
