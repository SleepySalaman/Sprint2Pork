using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public interface ILinkDirectionState
    {
        void LookLeft();
        void LookRight();
        void LookUp();
        void LookDown();
        void Update();

        //void BeIdle();
        //void BeMoving();

        //void AttackSword();

        //void TakeDamage();

        //void UseItem();

        //void Update();
    }
}
