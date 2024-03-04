using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SerectSong : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        _audioSource.Play();
        TextInfoMassager._iTextInfoMassager.SerectSongMessage();
        MusicManager._iMusicManager.StartTheMusicFadeOut();
        StartCoroutine(SongPlayingTimer(13));
    }
    private IEnumerator SongPlayingTimer(int _timer)
    {
        int i = 0;
        while (i < _timer)
        {
            if (i == 12)
            {
                MusicManager._iMusicManager.StartTheMusicFadeIn();
            }
            
            i++;
            yield return new WaitForSeconds(1);
        }
    }
}
