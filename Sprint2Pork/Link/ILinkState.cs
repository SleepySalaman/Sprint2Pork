using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2JohnPork
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
