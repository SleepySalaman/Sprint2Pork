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
        //private Dictionary<string, float> delays;

        public SoundManager()
        {
            soundEffects = new List<SoundEffect>();
            //delays = new Dictionary<string, float>();
        }
        
        public void LoadAllSounds(ContentManager content)
        {
            soundEffects.Add(content.Load<SoundEffect>("sfxSwordZap"));
            //delays.Add("sfxSwordZap", 0.67f);
        }

        public SoundEffect getSound(string sound)
        {
            return soundEffects.Find(x => x.Name == sound);
        }

        //public float GetDelay(string sound)
        //{
        //    return delays.GetValueOrDefault(sound);
        //}


    }
}
