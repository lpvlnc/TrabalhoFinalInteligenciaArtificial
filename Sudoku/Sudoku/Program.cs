using System;
using Towel;
using static Towel.DataStructures.Omnitree;

bool endGame = false;
while (!endGame)
{
NewGame:
    // Sempre que um jogo for iniciado o console é resetado
    Console.Clear();

    // A seleção da dificuldade retorna o número de campos a serem preenchidos no sudoku
    int blanks = SelectDifficulty();

    // Cria o tabuleiro com o número de campos a serem preenchidos 
    int?[,] board = Sudoku.Sudoku.Generate(blanks);
    int?[,] activeBoard = (int?[,])board.Clone();

    int x = 0;
    int y = 0;

    Console.Clear();

    // Enquanto o jogo não for finalizado e houver campos não preenchidos no tabuleiro
    while (!endGame && ContainsNulls(activeBoard))
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Sudoku");
        Console.WriteLine();
        ConsoleWrite(activeBoard, board);
        Console.WriteLine();
        Console.WriteLine("Selecione as células usando as setas do teclado.");
        Console.WriteLine("Insira valores de 1 à 9 nas células vazias.");
        Console.WriteLine("H para obter uma ajuda (o primeiro campo em branco será preenchido com um valor válido).");
        Console.WriteLine("DELETE ou BACKSPACE para apagar o valor de uma célula.");
        Console.WriteLine("ESCAPE para sair");
        Console.WriteLine("END para gerar um novo jogo.");

        // Cálcula a posição correta do cursor
        int cursorLeft = y * 2 + 2 + (y / 3 * 2);
        int cursorTop = x + 3 + +(x / 3);
        Console.SetCursorPosition(cursorLeft, cursorTop);

        // Faz a leitura das teclas
        switch (Console.ReadKey(true).Key)
        {
            // Movimentação através das células
            case ConsoleKey.UpArrow: 
                x = x <= 0 ? 8 : x - 1; 
                break;
            case ConsoleKey.DownArrow: 
                x = x >= 8 ? 0 : x + 1; 
                break;
            case ConsoleKey.LeftArrow: 
                y = y <= 0 ? 8 : y - 1; 
                break;
            case ConsoleKey.RightArrow:
                y = y >= 8 ? 0 : y + 1; 
                break;

            // Inserção dos números usando as teclas numéricas do teclado
            case ConsoleKey.D1: 
            case ConsoleKey.NumPad1: 
                activeBoard[x, y] = IsValidMove(activeBoard, board, 1, x, y) ? 1 : activeBoard[x, y]; 
                break;
            case ConsoleKey.D2: 
            case ConsoleKey.NumPad2: 
                activeBoard[x, y] = IsValidMove(activeBoard, board, 2, x, y) ? 2 : activeBoard[x, y];
                break;
            case ConsoleKey.D3: 
            case ConsoleKey.NumPad3: 
                activeBoard[x, y] = IsValidMove(activeBoard, board, 3, x, y) ? 3 : activeBoard[x, y]; 
                break;
            case ConsoleKey.D4:
            case ConsoleKey.NumPad4: 
                activeBoard[x, y] = IsValidMove(activeBoard, board, 4, x, y) ? 4 : activeBoard[x, y]; 
                break;
            case ConsoleKey.D5: 
            case ConsoleKey.NumPad5: 
                activeBoard[x, y] = IsValidMove(activeBoard, board, 5, x, y) ? 5 : activeBoard[x, y]; 
                break;
            case ConsoleKey.D6: 
            case ConsoleKey.NumPad6: 
                activeBoard[x, y] = IsValidMove(activeBoard, board, 6, x, y) ? 6 : activeBoard[x, y]; 
                break;
            case ConsoleKey.D7: 
            case ConsoleKey.NumPad7: 
                activeBoard[x, y] = IsValidMove(activeBoard, board, 7, x, y) ? 7 : activeBoard[x, y]; 
                break;
            case ConsoleKey.D8: 
            case ConsoleKey.NumPad8: 
                activeBoard[x, y] = IsValidMove(activeBoard, board, 8, x, y) ? 8 : activeBoard[x, y]; 
                break;
            case ConsoleKey.D9: 
            case ConsoleKey.NumPad9: 
                activeBoard[x, y] = IsValidMove(activeBoard, board, 9, x, y) ? 9 : activeBoard[x, y]; 
                break;

            // Resolve o próximo campo em branco automaticamente
            case ConsoleKey.H:
                SolveNext(activeBoard, board);
                break;

            // Reinicia o jogo
            case ConsoleKey.End: goto NewGame;

            // Remove o número selecionado
            case ConsoleKey.Backspace: 
            case ConsoleKey.Delete: activeBoard[x, y] = board[x, y] ?? null; 
                break;

            // Encerra o jogo
            case ConsoleKey.Escape: endGame = true; break;
        }
    }

    // Se chegou neste ponto e o jogo não foi encerrado através do menu
    // Significa que o jogador venceu
    if (!endGame)
    {
        Console.Clear();
        Console.WriteLine("Sudoku");
        Console.WriteLine();
        ConsoleWrite(activeBoard, board);
        Console.WriteLine();
        Console.WriteLine("Você venceu!");
        Console.WriteLine();
        Console.WriteLine("Para jogar novamente pressione ENTER.\nPara sair do jogo pressione ESCAPE.");
        // Mantem o console em loop enquanto o jogador não pressionar enter ou escape
    GetInput:
        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.Enter: break;
            case ConsoleKey.Escape:
                endGame = true;
                Console.Clear();
                break;
            default: goto GetInput;
        }
    }
}
Console.Clear();
Console.Write("Jogo finalizado.");

// Percorre todos os campos do tabuleiro até encontrar o próximo campo em branco
// Quando encontra verifica qual número é uma inserção válida e o insere no tabuleiro
void SolveNext(int?[,] activeBoard, int?[,] board)
{
    for (int x = 0; x < 9; x++)
    {
        for (int y = 0; y < 9; y++)
        {
            if (activeBoard[x, y] == null)
            {
                for (int i = 1; i <= 9; i++)
                {
                    if (IsValidMove(activeBoard, board, i, x, y))
                    {
                        activeBoard[x, y] = i;
                        return;
                    }
                }
            }
        }
    }
}

// Verifica se pode ou não inserir o valor
bool IsValidMove(int?[,] activeBoard, int?[,] board, int value, int x, int y)
{
    // A posição no tabuleiro precisa ser null, ou seja ser uma das posições das quais o jogador pode preencher
    if (board[x, y] is not null)
    {
        return false;
    }

    // Verifica se o valor já foi usado dentro do quadrado
    for (int i = x - x % 3; i <= x - x % 3 + 2; i++)
    {
        for (int j = y - y % 3; j <= y - y % 3 + 2; j++)
        {
            if (activeBoard[i, j] == value)
            {
                return false;
            }
        }
    }
    // Verifica se o valor já foi usado em outra posição na mesma linha
    for (int i = 0; i < 9; i++)
    {
        if (activeBoard[x, i] == value)
        {
            return false;
        }
    }
    // Verifica se o valor já foi usado em outra posição na mesma coluna
    for (int i = 0; i < 9; i++)
    {
        if (activeBoard[i, y] == value)
        {
            return false;
        }
    }
    return true;
}

// Verifica se há células sem valores no tabuleiro
bool ContainsNulls(int?[,] board)
{
    for (int i = 0; i < 9; i++)
    {
        for (int j = 0; j < 9; j++)
        {
            if (board[i, j] is null)
            {
                return true;
            }
        }
    }
    return false;
}

// Atualiza o tabuleiro na tela
void ConsoleWrite(int?[,] board, int?[,] lockedBoard)
{
    ConsoleColor consoleColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("╔═══════╦═══════╦═══════╗");
    for (int i = 0; i < 9; i++)
    {
        Console.Write("║ ");
        for (int j = 0; j < 9; j++)
        {
            if (lockedBoard is not null && lockedBoard[i, j] is not null)
            {
                Console.Write((lockedBoard[i, j].HasValue ? lockedBoard[i, j].ToString() : "■") + " ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write((board[i, j].HasValue ? board[i, j].ToString() : "■") + " ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            if (j == 2 || j == 5)
            {
                Console.Write("║ ");
            }
        }
        Console.WriteLine("║");
        if (i == 2 || i == 5)
        {
            Console.WriteLine("╠═══════╬═══════╬═══════╣");
        }
    }
    Console.WriteLine("╚═══════╩═══════╩═══════╝");
    Console.ForegroundColor = consoleColor;
}

// Seleção de dificuldade, este método retorna quantos espaços em brancos o tabuleiro irá ter
int SelectDifficulty()
{
    bool selected = false;
    int blankSpaces = 0;
    int blankSpacesEasy = 10;
    int blankSpacesNormal = 15;
    int blankSpacesHard = 20;
    while (!selected)
    {
        Console.Clear();
        Console.WriteLine("Selecione o nível de dificuldade:");
        Console.WriteLine($"[1] - Fácil [{blankSpacesEasy} espaços em branco]");
        Console.WriteLine($"[2] - Normal [{blankSpacesNormal} espaços em branco]");
        Console.WriteLine($"[3] - Difícil [{blankSpacesHard} espaços em branco]");
        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.D1: 
            case ConsoleKey.NumPad1: 
                blankSpaces = blankSpacesEasy;
                selected = true;
                break;
            case ConsoleKey.D2: 
            case ConsoleKey.NumPad2:
                blankSpaces = blankSpacesNormal;
                selected = true;
                break;
            case ConsoleKey.D3: 
            case ConsoleKey.NumPad3:
                blankSpaces = blankSpacesHard;
                selected = true;
                break;
        }
    }
    return blankSpaces;
}