using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Popups {
    public class PseudoMap {

        private List<String> types;
        private List<List<PseudoMapPair>> pairs;

        public PseudoMap() {
            types = new();
            pairs = new();
        }

        private int getBucket(string type) {
            int bucket = -1;
            for(int i = 0; i < types.Count; i++) {
                if(type == types[i]) {
                    bucket = i;
                }
            }
            return bucket;
        }

        public int size() {
            return types.Count;
        }

        public void insert(string id, int value) {
            int bucket = getBucket(id);
            if (bucket == -1) {
                types.Add(id);
                pairs.Add(new List<PseudoMapPair> {
                    new PseudoMapPair(id, value)
                });
            } else {
                pairs[bucket].Add(new PseudoMapPair(id, value));
            }
        }

        public void remove(string id, int value) {
            int toRemove = -1;
            int bucket = getBucket(id);
            for(int i = 0; i < pairs[bucket].Count; i++) {
                if (pairs[bucket][i].getValue() == value) {
                    toRemove = i;
                }
            }
            if (toRemove != -1) {
                pairs[bucket].RemoveAt(toRemove);
            }
        }

        public List<PseudoMapPair> listForType(string id) {
            int bucket = getBucket(id);
            return pairs[bucket];
        }
    }
}
