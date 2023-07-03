#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <locale.h>
#define MAX_VERTICES 7

typedef struct {
    bool adjMatrix[MAX_VERTICES][MAX_VERTICES];
    int degree[MAX_VERTICES];
} Graph;

void initGraph(Graph* graph) {
    int i, j;
    for (i = 0; i < MAX_VERTICES; i++) {
        for (j = 0; j < MAX_VERTICES; j++) {
            graph->adjMatrix[i][j] = false;
        }
        graph->degree[i] = 0;
    }
}

void addEdge(Graph* graph, int v1, int v2) {
    graph->adjMatrix[v1][v2] = true;
    graph->adjMatrix[v2][v1] = true;
    graph->degree[v1]++;
    graph->degree[v2]++;
}

int getNextVertex(int* colors, bool* colored, int numVertices) {
    int i;
    for (i = 0; i < numVertices; i++) {
        if (!colored[i]) {
            return i;
        }
    }
    return -1;
}

bool canColorVertex(Graph* graph, int vertex, int* colors, int color) {
    int i;
    for (i = 0; i < MAX_VERTICES; i++) {
        if (graph->adjMatrix[vertex][i] && colors[i] == color) {
            return false;
        }
    }
    return true;
}

void welshPowell(Graph* graph, int* colors) {
    bool colored[MAX_VERTICES] = { false };
    int numVertices = MAX_VERTICES;
    int color = 1;
    int i;

    while (true) {
        int vertex = getNextVertex(colors, colored, numVertices);
        if (vertex == -1) {
            break;
        }

        colors[vertex] = color;
        colored[vertex] = true;

        for (i = 0; i < numVertices; i++) {
            if (graph->adjMatrix[vertex][i] && !colored[i] && canColorVertex(graph, i, colors, color)) {
                colors[i] = color;
                colored[i] = true;
            }
        }

        color++;
    }
}

int main() {
    Graph graph;
    initGraph(&graph);
    int i;
    setlocale(LC_ALL, "Portuguese");

    addEdge(&graph, 0, 1); // a - b
    addEdge(&graph, 1, 2); // b - c
    addEdge(&graph, 2, 3); // c - d
    addEdge(&graph, 1, 4); // b - e
    addEdge(&graph, 4, 5); // e - f
    addEdge(&graph, 4, 6); // e - g
    addEdge(&graph, 5, 6); // f - g

    int colors[MAX_VERTICES] = { 0 };
    welshPowell(&graph, colors);

    for (i = 0; i < MAX_VERTICES; i++) {
        printf("Vértice %c - Cor %d\n", 'a' + i, colors[i] % 3 + 1);
    }

    return 0;
}

