using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2Pork.Popups {
    public class Graph {
        private int[,] graph;
        private int w, h;

        public Graph(int width, int height) {
            graph = new int[width, height];
            w = width;
            h = height;
            for(int i = 0; i < width; i++) {
                for(int j = 0; j < height; j++) {
                    graph[i,j] = 1;
                }
            }
        }

        public void insert(int value, int x, int y, int width, int height) {
            for(int i = x; i < width; i++) {
                for(int j = y; j < height; j++) {
                    graph[i, j] = value;
                }
            }
        }

        public int[] findShortestPath(int x, int y) {
            int[] nearest = new int[2];
            int radius = 1;
            bool found = false;
            while (!found) {
                for(int i = x - radius; !found && i <= x + radius; i++) {
                    for(int j = y - radius; !found && j <= y + radius; j++) {
                        if(i >= 0 && j >= 0 && i < w && j < h && graph[i,j] != 1 && graph[i,j] != 4) {
                            found = true;
                            nearest[0] = i;
                            nearest[1] = j;
                        }
                    }
                }
            }
            return nearest;
        }

    }
}
