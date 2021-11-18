using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// TODO investigate AudioSource.PlayOneShot() since AudioManager is cring and garbag at playing lots of short sounds.

public class AudioManager : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float volume;

    [SerializeField]
    public int maxConcurrentAudioSources;

    [SerializeField]
    public List<string> sceneLoadBGMList; // Audio files to play on Start()

    private string[] clickSounds = {"clack", "click01", "click02", "click03", "click04", "coarse_click", "funny_click", "ping", "spacey_click", "tiny_click"};
    private Queue<int> availableIDs;
    private Dictionary<int, AudioSource> audioSources; // Stores (ID, AudioSource) pairs for easy access
    private Dictionary<string, AudioClip> audioClips;

    // Start is called before the first frame update
    void Start()
    {
        availableIDs = new Queue<int>();
        audioSources = new Dictionary<int, AudioSource>();
        audioClips = new Dictionary<string, AudioClip>();

        // Get audio file names
        string[] validExtensions = {".mp3", ".wav"};
        List<string> audioFiles = new List<string>();
        foreach (string audioFile in Directory.EnumerateFiles(Application.dataPath + "/Resources/Audio/")) {
            foreach (string ext in validExtensions) {
                if (audioFile.EndsWith(ext)) {
                    audioFiles.Add(audioFile);
                    break;
                }
            }
        }

        // Load AudioClips
        foreach (string audioFile in audioFiles) {
            string filename = Path.GetFileName(audioFile);
            audioClips.Add(filename.Substring(0, filename.LastIndexOf(".")), Resources.Load<AudioClip>("Audio/" + Path.GetFileNameWithoutExtension(filename)));
        }

        // Fill availableIDs
        for (int id = 0; id < maxConcurrentAudioSources; ++id) {
            availableIDs.Enqueue(id);
        }

        // Play sceneLoadBGMs
        foreach (string audioClip in sceneLoadBGMList) {
            Play(audioClip, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Iterate through audioSources and delete stopped sources
        List<int> stoppedIDs = new List<int>(); // Stores keys of stopped audio sources

        // Find stopped AudioSources
        foreach (KeyValuePair<int, AudioSource> audioSourceEntry in audioSources) {
            if (!audioSourceEntry.Value.isPlaying) {
                stoppedIDs.Add(audioSourceEntry.Key);
            }
            audioSourceEntry.Value.volume = volume;
        }

        // Deletes stopped AudioSources and adds id to availableIDs
        foreach (int id in stoppedIDs) {
            Destroy(audioSources[id]);
            audioSources.Remove(id);
            availableIDs.Enqueue(id);
        }

        Debug.Log(audioSources.Count);
    }

    // AUDIO CONTROL

    /*public ref AudioSource GetAudioSource(int audioSourceID)
    {
        // TODO fix: can't have ref to object in collection
        return ref audioSources[audioSourceID];
    }*/

    public int Play(string audioFile, bool loop)
    {
        // Instantiates new AudioSource from an audio file and returns the AudioSource's ID
        if (availableIDs.Count == 0 || !audioClips.ContainsKey(audioFile)) {
            // Return -1 if max concurrent AudioSources reached or audioFile does not exist
            return -1;
        }

        // Instantiate AudioSource
        int id = availableIDs.Dequeue();
        audioSources.Add(id, gameObject.AddComponent<AudioSource>());

        // Configure AudioSource
        audioSources[id].clip = audioClips[audioFile];
        audioSources[id].loop = loop;
        audioSources[id].Play();

        return id;
    }

    public void PlayLooped(string audioFile) {
        // Workaround for button UI OnClick() requiring up to one parameter
        Play(audioFile, true);
    }

    public void PlayOnce(string audioFile) {
        // Workaround for button UI OnClick() requiring up to one parameter
        Play(audioFile, false);
    }

    public void PlayRandomClickSound() {
        // Chooses a random click audio clip and plays it once
        System.Random random = new System.Random();
        int idx = random.Next(0, clickSounds.Length);
        Play(clickSounds[idx], false); 
    }

    public void Stop(int audioSourceID)
    {
        // Stops an AudioSource. It will get deleted in the next call to Update()
        if (audioSources.ContainsKey(audioSourceID)) {
            audioSources[audioSourceID].Stop();
        }
    }

    public void StopAll() {
        // Stops all AudioSources
        foreach (KeyValuePair<int, AudioSource> audioSourceEntry in audioSources) {
            Stop(audioSourceEntry.Key);
        }
    }

    // TODO adjust to logarithmic scale of volume
    public void SetVolume(float volume) {
        this.volume = Mathf.Clamp(volume, 0.0f, 1.0f);
    }
}
