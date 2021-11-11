using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// TODO investigate AudioSource.PlayOneShot() since AudioManager is cring and garbag at playing lots of short sounds.

public class AudioManager : MonoBehaviour
{
    int maxConcurrentAudioSources = 256;

    private Queue<int> availableIDs;
    private Dictionary<int, AudioSource> audioSources; // Stores (ID, AudioSource) pairs for easy access
    private Dictionary<string, AudioClip> audioClips;

    // Start is called before the first frame update
    void Start()
    {
        availableIDs = new Queue<int>();
        audioSources = new Dictionary<int, AudioSource>();
        audioClips = new Dictionary<string, AudioClip>();

        // Load AudioClips
        foreach (string audioFile in Directory.GetFiles(Application.dataPath + "/Resources/Audio/", "*.mp3")) {
            string filename = Path.GetFileName(audioFile);
            audioClips.Add(filename, Resources.Load<AudioClip>("Audio/" + Path.GetFileNameWithoutExtension(filename)));
        }

        // Fill availableIDs
        for (int id = 0; id < maxConcurrentAudioSources; ++id) {
            availableIDs.Enqueue(id);
        }

        Play("simian_segue.mp3", true);
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
        }

        // Deletes stopped AudioSources and adds id to availableIDs
        foreach (int id in stoppedIDs) {
            Destroy(audioSources[id]);
            audioSources.Remove(id);
            availableIDs.Enqueue(id);
        }
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

    // TODO add pausing functionality?

    public void Stop(int audioSourceID)
    {
        // Stops an AudioSource. It will get deleted in the next call to Update()
        audioSources[audioSourceID].Stop();
    }

    public void StopAll() {
        // Stops all AudioSources
        foreach (KeyValuePair<int, AudioSource> audioSourceEntry in audioSources) {
            Stop(audioSourceEntry.Key);
        }
    }
}
