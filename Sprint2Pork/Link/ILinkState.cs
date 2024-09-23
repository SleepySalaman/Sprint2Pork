using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public interface ILinkState
    {
        void ChangeDirectionLeft();
        void ChangeDirectionRight();
        void ChangeDirectionUp();
        void ChangeDirectionDown();

        void AttackSword();

        void TakeDamage();

        void UseItem();

        void Update();
    }
}
