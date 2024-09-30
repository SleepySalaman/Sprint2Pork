using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork
{
    public interface ILinkActionState
    {
        void BeIdle();
        void BeMoving();
        void BeAttacking();
        void TakeDamage();
        void Update();
    }
}
