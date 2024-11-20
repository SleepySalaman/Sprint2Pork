using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Sprint2Pork
{
    public class SoundManager
    {
        private List<SoundEffect> soundEffects;
        private SoundEffectInstance soundInstance;
        //private Dictionary<string, float> delays;

        public SoundManager()
        {
            soundEffects = new List<SoundEffect>();
            //delays = new Dictionary<string, float>();
        }

        public void LoadAllSounds(ContentManager content)
        {
            soundEffects.Add(content.Load<SoundEffect>("sfxSwordZap"));
            soundEffects.Add(content.Load<SoundEffect>("sfxPlayerHurt"));
            soundEffects.Add(content.Load<SoundEffect>("sfxFlamesShot"));
            soundEffects.Add(content.Load<SoundEffect>("sfxKeyAppears"));
            soundEffects.Add(content.Load<SoundEffect>("sfxItemObtained"));
            soundEffects.Add(content.Load<SoundEffect>("sfxItemReceived"));
            //delays.Add("sfxSwordZap", 0.67f);
        }

        public SoundEffect GetSound(string sound)
        {
            return soundEffects.Find(x => x.Name == sound);
        }

        public void PlaySound(string soundName)
        {
            SoundEffect sound = GetSound(soundName);
            soundInstance = sound.CreateInstance();
            soundInstance.Volume = 0.25f;
            soundInstance.Play();
            //playingFlag = true;
        }

        public void ToggleBackgroundMusic()
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause();
            }
            else
            {
                MediaPlayer.Resume();
            }
        }
    }
}
