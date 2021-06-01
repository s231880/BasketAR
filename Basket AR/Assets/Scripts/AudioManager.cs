using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBAR
{
    //There are two audio source because different sounds can be played at the same time
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]private AudioSource[] m_Sources;
        private const int SOURCES_COUNT = 3;

        private AudioClip m_CountDown;
        private AudioClip m_Horn;
        private AudioClip m_Confetti;
        private AudioClip m_Whoosh;
        private AudioClip m_Cheering;
        private AudioClip[] m_Booing;

        public void Initialise()
        {
            //Audio sources initialisation
            m_Sources = new AudioSource[SOURCES_COUNT];
            m_Sources = gameObject.GetComponents<AudioSource>();

            //Audio clip initialisation
            m_Horn = Resources.Load<AudioClip>("Sounds/Horn");
            m_CountDown = Resources.Load<AudioClip>("Sounds/Countodown");
            m_Confetti = Resources.Load<AudioClip>("Sounds/Confetti");
            m_Cheering = Resources.Load<AudioClip>("Sounds/Cheering");
            m_Whoosh = Resources.Load<AudioClip>("Sounds/Whoosh");
            m_Booing = new AudioClip[2];
            for (int i = 1; i <= 2; ++i)
            {
                m_Booing[i - 1] = Resources.Load<AudioClip>($"Sounds/Boo_{i}");
            }

        }

        public void PlayCountdown()
        {
            m_Sources[2].clip = m_CountDown;
            m_Sources[2].pitch = 0.7f;
            m_Sources[2].Play();
        }

        public void PlayCheering()
        {
            m_Sources[0].clip = m_Cheering;
            m_Sources[0].Play();
        }

        public void PlayConfettiPop()
        {
            m_Sources[1].clip = m_Confetti;
            m_Sources[1].pitch = 1f;
            m_Sources[1].Play();
        }

        public void PlayBooing()
        {
            int sound = Random.Range(0, 2);
            m_Sources[0].clip = m_Booing[sound];
            m_Sources[0].Play();
        }

        public void PlayHorn()
        {
            m_Sources[0].clip = m_Horn;
            //m_Sources[0].pitch = 1f;
            m_Sources[0].Play();
        }

        public void PlayWhoosh()
        {
            m_Sources[2].clip = m_Whoosh;
            m_Sources[2].pitch = 1f;
            m_Sources[2].Play();
        }
    }
}

