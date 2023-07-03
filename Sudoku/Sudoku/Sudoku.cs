using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Towel;

namespace Sudoku
{
    public static class Sudoku
    {
        // Cria um tabuleiro sudoku 
        // A variável blank representa quantos campos em branco o tabuleiro deve ter
        public static int?[,] Generate(int blanks)
        {
            Random random = new();
            int?[,] board = new int?[9, 9];

            // Este array vai armazenaras valores válidos para cada célula no Values
            // e quantos valores válidos a célula possue no Count
            // Values inicia como um array de 9 posições e o Count como -1
            (int[] Values, int Count)[,] valids = new (int[] Values, int Count)[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    valids[i, j] = (new int[9], -1);
                }
            }

            #region GetValidValues

            // Popula o array Valids com valores válidos para a linha e coluna informada
            void GetValidValues(int row, int column)
            {
                // Verifica se o valor é válido dentro do Quadrado
                bool SquareValid(int value, int row, int column)
                {
                    for (int i = row - row % 3; i <= row; i++)
                    {
                        for (int j = column - column % 3; j <= column - column % 3 + 2 && !(i == row && j == column); j++)
                        {
                            if (board[i, j] == value)
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }

                // Verifica se o valor é válido na linha
                bool RowValid(int value, int row, int column)
                {
                    for (int i = 0; i < column; i++)
                    {
                        if (board[row, i] == value)
                        {
                            return false;
                        }
                    }
                    return true;
                }

                // Verifica se o valor é válido na coluna
                bool ColumnValid(int value, int row, int column)
                {
                    for (int i = 0; i < row; i++)
                    {
                        if (board[i, column] == value)
                        {
                            return false;
                        }
                    }
                    return true;
                }

                // Define o contador como 0 para esta linha e coluna
                valids[row, column].Count = 0;

                // Passa por todos os valores de 1 a 9
                // E os que são válidos para a celula são adicionados no array
                // E quando adiciona incrementa o Count
                for (int i = 1; i <= 9; i++)
                {
                    if (SquareValid(i, row, column) &&
                        RowValid(i, row, column) &&
                        ColumnValid(i, row, column))
                    {
                        valids[row, column].Values[valids[row, column].Count++] = i;
                    }
                }
            }

            #endregion


            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    GetValidValues(i, j);
                    
                    // Se o Count for 0 significa que não há valores válidos para esta célula
                    // Então deixa essa celula como null e volta o loop para a célula anterior
                    while (valids[i, j].Count == 0)
                    {
                        board[i, j] = null;
                        i = j == 0 ? i - 1 : i;
                        j = j == 0 ? 8 : j - 1;
                    }
                    // Seleciona um valor aleatório do array de valores válidos para a célula
                    int index = random.Next(0, valids[i, j].Count);
                    int value = valids[i, j].Values[index];
                    
                    // Remove o valor selecionado do array de valores válidos
                    valids[i, j].Values[index] = valids[i, j].Values[valids[i, j].Count - 1];
                    valids[i, j].Count--;

                    // Adiciona o valor ao tabuleiro
                    board[i, j] = value;
                }
            }

            // Depois que o sudoku está completamente preenchido
            // Remove valores aleatórios com base na váriavel blanks que foi informada como parâmetro do método
            // o NextUnique(biblioteca Towel) garante que os números randômicos não se repitam
            foreach (int i in random.NextUnique(Math.Max(1, blanks), 0, 81))
            {
                int row = i / 9;
                int column = i % 9;
                board[row, column] = null;
            }

            return board;
        }
    }
}
