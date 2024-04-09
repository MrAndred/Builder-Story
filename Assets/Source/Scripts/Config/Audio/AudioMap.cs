using System.Collections.Generic;
using UnityEngine;

namespace BuilderStory.Config.Audio
{
    [CreateAssetMenu(fileName = "AudioMap", menuName = "BuilderStory/AudioMap", order = 1)]
    public class AudioMap : ScriptableObject
    {
        [SerializeField] private AudioMapStructure[] _audioMap;

        private Dictionary<string, AudioClip> _audioMapDictionary;

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

        public void Init()
        {
            _audioMapDictionary = new Dictionary<string, AudioClip>();

            foreach (AudioMapStructure audioMapStructure in _audioMap)
            {
                _audioMapDictionary.Add(audioMapStructure.Key, audioMapStructure.Clip);
            }
        }
    }
}
