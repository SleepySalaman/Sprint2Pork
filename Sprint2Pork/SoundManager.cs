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

        public SoundManager()
        {
            soundEffects = new List<SoundEffect>();
        }
        
        public void LoadAllSounds(ContentManager content)
        {
            soundEffects.Add(content.Load<SoundEffect>("sfxSwordZap"));
        }

        public SoundEffect getSound(string sound)
        {
            return soundEffects.Find(x => x.Name == sound);
        }
    }
}
