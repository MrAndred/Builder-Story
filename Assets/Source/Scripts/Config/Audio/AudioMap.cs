using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderStory
{
    [CreateAssetMenu(fileName = "AudioMap", menuName = "BuilderStory/AudioMap", order = 1)]
    public class AudioMap : ScriptableObject
    {
        public static AudioMap Instance { get; private set; }

        [SerializeField] private AudioMapStructure[] _audioMap;

        private Dictionary<string, AudioClip> _audioMapDictionary;

        private void OnEnable()
        {
            Init();
        }

        private void OnDisable()
        {
            _audioMapDictionary.Clear();
        }

        private void Init()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            _audioMapDictionary = new Dictionary<string, AudioClip>();

            foreach (AudioMapStructure audioMapStructure in _audioMap)
            {
                _audioMapDictionary.Add(audioMapStructure.Key, audioMapStructure.Clip);
            }
        }

        public AudioClip GetAudioClip(string key)
        {
            foreach (AudioMapStructure audioMapStructure in _audioMap)
            {
                if (audioMapStructure.Key == key)
                {
                    return audioMapStructure.Clip;
                }
            }

            return null;
        }
    }
}
