using System.Collections;
using UnityEngine;

namespace SilentSpace.Audio
{
    public enum AudioType
    {
        None,
        ST_Stellaris_SpatialLullaby,
        ST_Infinity_FabricOfSpaceTime,
        SFX_Sonar_01,
        SFX_Enemy_Creature_Speak_01,
        SFX_Enemy_Creature_Roar,
        SFX_Player_Walk,
        SFX_Player_Run,
        SFX_Player_CrouchWalk,
        SFX_UI_SpaceyClick_Button,
        SFX_UI_Clack_Slider,
        SFX_UI_Button11_Button,
        SFX_UI_Button29_Button,
        //Custom audioTypes here
    }

    public class AudioController : MonoBehaviour
    {
        //members
        public static AudioController Instance; //singleton

        public bool debug;
        public AudioTrack[] tracks;

        private Hashtable _audioTable; //relationship between audio types (key) and audio tracks (value).
        private Hashtable _jobTable; //relationship between audio types (key) and jobs (value) (Coroutine, IEnumerator);

        [System.Serializable]
        public class AudioObject
        {
            public AudioType type;
            public AudioClip clip;
        }

        [System.Serializable]
        public class AudioTrack
        {
            public AudioSource source;
            public AudioObject[] audio;
        }

        private class AudioJob
        {
            public AudioAction Action;
            public AudioType Type;
            public bool Fade;
            public bool Overplay;
            public float Delay;

            public AudioJob(AudioAction action, AudioType type, bool fade, bool overplay, float delay)
            {
                Action = action;
                Type = type;
                Fade = fade;
                Overplay = overplay;
                Delay = delay;
            }
        }
            
        private enum AudioAction
        {
            Start,
            Stop,
            Restart,
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Configure();
            }
            else
            {
                Dispose();
            }
        }

        public void PlayAudio(AudioType type, bool fade = false, bool overplay = false, float delay = 0f)
        {
            AddJob(new AudioJob(AudioAction.Start, type, fade, overplay, delay));
        }
            
        public void StopAudio(AudioType type, bool fade = false, bool overplay = false, float delay = 0f)
        {
            AddJob(new AudioJob(AudioAction.Stop, type, fade, overplay, delay));
        }
            
        public void RestartAudio(AudioType type, bool fade = false, bool overplay = false, float delay = 0f)
        {
            AddJob(new AudioJob(AudioAction.Restart, type, fade, overplay, delay));
        }

        private void Configure()
        {
            Instance = this;
            _audioTable = new Hashtable();
            _jobTable = new Hashtable();
            GenerateAudioTable();
        }

        private void Dispose()
        {
            foreach(DictionaryEntry entry in _jobTable)
            {
                IEnumerator job = (IEnumerator)entry.Value;
                StopCoroutine(job);
            }
        }

        private void GenerateAudioTable()
        {
            foreach(AudioTrack track in tracks)
            {
                foreach(AudioObject obj in track.audio)
                {
                    //do not duplicate keys.
                    if (_audioTable.ContainsKey(obj.type))
                    {
                        LogWarning("You are trying to register audio [" + obj.type + "] that has already been registered.");
                    } 
                    else
                    {
                        _audioTable.Add(obj.type, track);
                        Log("Registering audio [" + obj.type + "].");
                    }
                }
            }
        }

        private IEnumerator RunAudioJob(AudioJob job)
        {
            yield return new WaitForSeconds(job.Delay);

            AudioTrack track = (AudioTrack)_audioTable[job.Type];
            track.source.clip = GetAudioClipFromAudioTrack(job.Type, track);

            switch (job.Action)
            {
                case AudioAction.Start:
                    track.source.Play();
                    break;
                case AudioAction.Stop:
                    if (!job.Fade)
                    {
                        track.source.Stop();
                    }
                    break;
                case AudioAction.Restart:
                    track.source.Stop();
                    track.source.Play();
                    break;
            }

            if (job.Fade)
            {
                float initial = job.Action == AudioAction.Start || job.Action == AudioAction.Restart ? 0.0f : 1.0f;
                float target = initial == 0 ? 1 : 0;
                float duration = 1f;
                float timer = 0.0f;

                while(timer <= duration)
                {
                    track.source.volume = Mathf.Lerp(initial, target, timer / duration);
                    timer += Time.deltaTime;
                    yield return null;
                }

                if(job.Action == AudioAction.Stop)
                {
                    track.source.Stop();
                }
            }

            _jobTable.Remove(job.Type);
            Log("Job count: "+_jobTable.Count);

            yield return null;
        }

        private void AddJob(AudioJob job)
        {
            //remove conflicting jobs
            RemoveConflictingJobs(job.Type);
        

            //start job
            IEnumerator jobRunner = RunAudioJob(job);
            _jobTable.Add(job.Type, jobRunner);
            StartCoroutine(jobRunner);
            Log("Starting the job [" + job.Type + "] with opeartion : " + job.Action);
        }

        private void RemoveJob(AudioType type)
        {
            if (!_jobTable.ContainsKey(type))
            {
                LogWarning("Trying to stop a job [" + type + "] that is not running.");
                return;
            }

            IEnumerator runningJob = (IEnumerator) _jobTable[type];
            StopCoroutine(runningJob);
            _jobTable.Remove(type);
        }

        private void RemoveConflictingJobs(AudioType type)
        {
            if (_jobTable.ContainsKey(type))
            {
                RemoveJob(type);
            }

            AudioType conflictAudio = AudioType.None;
            foreach(DictionaryEntry entry in _jobTable)
            {
                AudioType audioType = (AudioType)entry.Key;
                AudioTrack audioTrackInUse = (AudioTrack)_audioTable[audioType];
                AudioTrack audioTrackNeeded = (AudioTrack)_audioTable[type];
                if(audioTrackNeeded.source == audioTrackInUse.source)
                {
                    //conflict
                    conflictAudio = audioType;
                }
            }
            if(conflictAudio != AudioType.None)
            {
                RemoveJob(conflictAudio);
            }
        }

        public AudioClip GetAudioClipFromAudioTrack(AudioType type, AudioTrack track)
        {
            foreach(AudioObject obj in track.audio)
            {
                if(obj.type == type)
                {
                    return obj.clip;
                }
            }
            return null;
        } 

        private void Log(string msg)
        {
            if (!debug) return;
            Debug.Log("[AudioController]:" + msg);
        }
        private void LogWarning(string msg)
        {
            if (!debug) return;
            Debug.LogWarning("[AudioController]:" + msg);
        }
    }
}