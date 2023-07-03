class Graph:
    def __init__(self):
        self.adjacency_list = {}
        self.degree = {}

    def add_edge(self, v1, v2):
        self.adjacency_list.setdefault(v1, []).append(v2)
        self.adjacency_list.setdefault(v2, []).append(v1)
        self.degree[v1] = self.degree.get(v1, 0) + 1
        self.degree[v2] = self.degree.get(v2, 0) + 1

def welsh_powell(graph):
    # Ordenar os vértices em ordem decrescente de grau
    vertices = sorted(graph.degree, key=lambda x: graph.degree[x], reverse=True)

    # Atribuir cores aos vértices
    coloring = {}
    color = 1

    for vertex in vertices:
        # Verificar as cores utilizadas pelos vizinhos
        neighbor_colors = set(coloring.get(neighbor) for neighbor in graph.adjacency_list[vertex] if neighbor in coloring)

        # Encontrar a menor cor disponível para o vértice
        available_color = 1
        while available_color in neighbor_colors:
            available_color += 1

        coloring[vertex] = available_color
        color = max(color, available_color)

    return coloring

# Criar o grafo
graph = Graph()
graph.add_edge('a', 'b')
graph.add_edge('b', 'c')
graph.add_edge('c', 'd')
graph.add_edge('b', 'e')
graph.add_edge('e', 'f')
graph.add_edge('e', 'g')
graph.add_edge('f', 'g')

# Chamar o algoritmo de Welsh-Powell para colorir o grafo
coloring = welsh_powell(graph)

# Exibir a coloração
for vertex, color in coloring.items():
    print(f"Vértice {vertex} - Cor {color}")

G = [[0, 1, 1, 0, 1, 0, 0],
     [1, 0, 1, 1, 0, 1, 0],
     [1, 1, 0, 1, 1, 0, 0],
     [0, 1, 1, 0, 0, 1, 0],
     [1, 0, 1, 0, 0, 1, 0],
     [0, 1, 0, 1, 1, 0, 0],
     [0, 0, 0, 0, 0, 0, 0]]

# Iniciar o nome do nó.
node = "abcdefg"
t_ = {}
for i in range(len(G)):
    t_[node[i]] = i

# Contar o grau de todos os nós.
degree = []
for i in range(len(G)):
    degree.append(sum(G[i]))

# Iniciar as cores possíveis.
colorDict = {}
for i in range(len(G)):
    colorDict[node[i]] = ["Blue", "Red", "Green"]

# Ordenar os nós de acordo com o grau.
sortedNode = []
indeks = []

# Usar o algoritmo de ordenação por seleção (selection sort).
for i in range(len(degree)):
    _max = 0
    j = 0
    for j in range(len(degree)):
        if j not in indeks:
            if degree[j] > _max:
                _max = degree[j]
                idx = j
    indeks.append(idx)
    sortedNode.append(node[idx])

# O processo principal.
theSolution = {}
for n in sortedNode:
    setTheColor = colorDict[n]
    theSolution[n] = setTheColor[0]
    adjacentNode = G[t_[n]]
    for j in range(len(adjacentNode)):
        if adjacentNode[j] == 1 and (setTheColor[0] in colorDict[node[j]]):
            colorDict[node[j]].remove(setTheColor[0])

# Imprimir a solução.
for t, w in sorted(theSolution.items()):
    print("Node", t, "=", w)
