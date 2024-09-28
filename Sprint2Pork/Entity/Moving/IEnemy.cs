using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Entity.Moving {
    public interface IEnemy : IEntity {

        void Move();
        void Attack();

        int getX();

    }
}
