using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    public enum SoundType
    {
        BGM,
        UI,
        Effect,
        MaxCount,
    }

    private List<AudioSource> _audioSource = new ((int) SoundType.MaxCount);
    Dictionary<string, AudioClip> _clips = new ();

    public void Init()
    {
        GameObject root = GameObject.Find("--Sound");
        if (root == null)
        {
            root = new GameObject { name = "--Sound" };
            Object.DontDestroyOnLoad(root);
        }

        var soundName = System.Enum.GetNames(typeof(SoundType));

        for (int i = 0; i < soundName.Length - 1; i++)
        {
            var obj = new GameObject { name = soundName[i]};
            _audioSource.Add(obj.AddComponent<AudioSource>());
            obj.transform.parent = root.transform;
        }

        _audioSource[(int)SoundType.BGM].loop = true;
    }

    public void Play(AudioClip _audioClip, SoundType type, float pitch =1.0f)
    {
        if (_audioClip == null) return;
        if (type == SoundType.BGM)
        {
            var source = _audioSource[(int)SoundType.BGM];
            if (source.isPlaying) source.Stop();

            source.pitch = pitch;
            source.clip = _audioClip;
            source.Play();
        }
        else
        {
            AudioSource source = _audioSource[(int)SoundType.UI];
            if (type == SoundType.Effect) source = _audioSource[(int)SoundType.Effect];
            source.pitch = pitch;
            source.PlayOneShot(_audioClip);
        }
    }

    AudioClip GetOrAddAudioClip(string path, SoundType type)
    {
        AudioClip audioClip = null;

        if (!path.Contains("Assets/Sounds/")) {
            Debug.LogWarning("$[Sound Manager] path is not valid");
            return null;
         }

        if (type == SoundType.BGM) audioClip = Managers.ResourceManager.LoadAudioClip(path);
        else
        {
            if (_clips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.ResourceManager.LoadAudioClip(path);
                _clips.Add(path, audioClip);
            }
        }

        return audioClip;
    }

    public void Play(string path, SoundType type, float pitch = 1.0f)
    {
        var audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    public void StopBGM()
    {
        var source = _audioSource[(int)SoundType.BGM];
        if (source.isPlaying) source.Stop();
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSource)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

        _clips.Clear();
    }
}
