using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public class SoundManager
    {
        private List<SoundEffect> soundEffects;
        private SoundEffectInstance soundInstance;
        private bool playingFlag;
        //private Dictionary<string, float> delays;

        public SoundManager()
        {
            soundEffects = new List<SoundEffect>();
            playingFlag = false;
            //delays = new Dictionary<string, float>();
        }
        
        public void LoadAllSounds(ContentManager content)
        {
            soundEffects.Add(content.Load<SoundEffect>("sfxSwordZap"));
            soundEffects.Add(content.Load<SoundEffect>("sfxPlayerHurt"));
            soundEffects.Add(content.Load<SoundEffect>("sfxFlamesShot"));
            //delays.Add("sfxSwordZap", 0.67f);
        }

        public SoundEffect getSound(string sound)
        {
            return soundEffects.Find(x => x.Name == sound);
        }

        public void PlaySound(string soundName)
        {
            if (!playingFlag)
            {
                SoundEffect sound = this.getSound(soundName);
                soundInstance = sound.CreateInstance();
                soundInstance.Volume = 0.1f;
                soundInstance.Play();
                playingFlag = true;
            }
            else
            {
                if (soundInstance.State != SoundState.Playing)
                {
                    playingFlag = false;
                }
            }


        }
        //public float GetDelay(string sound)
        //{
        //    return delays.GetValueOrDefault(sound);
        //}


    }
}
