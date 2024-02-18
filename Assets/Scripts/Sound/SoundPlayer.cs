using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : SingletonBase<SoundPlayer>
    {
        [SerializeField] private Sounds m_Sounds;
        [SerializeField] private AudioClip[] m_AudioClips = new AudioClip[0];
        [SerializeField] private AudioClip m_BGM;
        private AudioSource m_AudioSourse;

        private new void Awake()
        {
            base.Awake();
            m_AudioSourse = GetComponent<AudioSource>();
            Instance.m_AudioSourse.clip = m_BGM;
            Instance.m_AudioSourse.Play();
        }
        public void Play(Sound sound)
        {
            m_AudioSourse.PlayOneShot(m_Sounds[sound]);
        }
    }
}