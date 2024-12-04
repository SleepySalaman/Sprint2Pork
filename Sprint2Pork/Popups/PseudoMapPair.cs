using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Popups {
    public class PseudoMapPair {

        private string pairId;
        private int pairValue;
        private int pairHealth;

        public PseudoMapPair(string id, int value) {
            pairId = id;
            pairValue = value;
            pairHealth = 3;
        }

        public int getValue() {
            return pairValue;
        }

        public string getID() {
            return pairId;
        }

        public int getHealth() {
            return pairHealth;
        }

        public void setValue(int value) {
            pairValue = value;
        }

        public void incrementValue(int value) {
            pairValue++;
        }

        public bool takeDamage() {
            pairHealth--;
            return pairHealth <= 0;
        }

    }
}
