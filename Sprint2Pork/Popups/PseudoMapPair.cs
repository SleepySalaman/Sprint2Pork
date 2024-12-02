using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Popups {
    public class PseudoMapPair {

        private string pairId;
        private int pairValue;

        public PseudoMapPair(string id, int value) {
            pairId = id;
            pairValue = value;
        }

        public int getValue() {
            return pairValue;
        }

        public string getID() {
            return pairId;
        }

        public void setValue(int value) {
            pairValue = value;
        }

        public void incrementValue(int value) {
            pairValue++;
        }

    }
}
