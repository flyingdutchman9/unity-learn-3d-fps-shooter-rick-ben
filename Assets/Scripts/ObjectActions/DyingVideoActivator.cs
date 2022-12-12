using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DyingVideoActivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var videoPlayer = GetComponentInChildren<VideoPlayer>();
            videoPlayer.Play();
        }
    }
}
