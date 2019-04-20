/*==================================================================================*
*  Manage and hold word list, grid                                                  *
*===================================================================================*
* 1. Contain words to find and GridGame properties, set via fields                      *
* 2. Handle setup of words to find array, ManagerData called to return words array  *
* 3. ROUTINE: Handle setup of grid:                                                 *
*   a. Handle setup empty grid                                                      *
*       i. Size based on 1) total number characters in words 2) longest word        *
*       ii. Create empty grid                                                       *
*   b. Handle populate grid with words to find                                      *
*   c. Populate remaining empty grid elements with random characters                *
* 4. Check words have space to fit in grid with different directions, orders        *
* 5. Place words in grid in different directions, orders                            *
*===================================================================================*/
using Microsoft.Xna.Framework;
using System;
using WordSearch.Common;

namespace WordSearch
{
    public class Grid
    {
        /*==============================================================================*
        *  1. Fields for setting and properties for safe access of word array and grid  *
        *   a. Fields set in HandleSetupWords(), HandleSetupGrid()                      *
        *   b. Properties used by this and other classes                                *
        *===============================================================================*/
        private string[] wordsGame;
        private char[,] gridGame;
        // properties for access
        public string[] WordsGame
        {
            get
            {
                return wordsGame;
            }
        }
        public char[,] GridGame
        {
            get
            {
                return gridGame;
            }
        }

        public void SetupGridGame(string listSelected, int listSize)
        {
            HandleSetupWords(listSelected, listSize);
            HandleSetupGrid();
        }

        /*==================================*
        *  2. Handle setup words to find    *
        *===================================*/
        private void HandleSetupWords(string listSelected, int listSize)
        {
            wordsGame = ManagerData.HandleListLoad(listSelected, listSize);
        }

        /*======================================*
        *  3. Handle setup grid size, populate  *
        *=======================================*/
        private void HandleSetupGrid()
        {
            HandleSetupEmptyGrid();
            PopulateGridWords(WordsGame, GridGame);
            PopulateEmptyElements(GridGame);
        }
        /*==============================*
        *  3.a. Handle setup empty grid *
        *===============================*/
        private void HandleSetupEmptyGrid()
        {
            // Declare, initialise with null () values, grids
            int numCharsInWords = Helper.CountWordsCharactersAll(WordsGame);
            int lengthLongestWord = Helper.LongestWord(WordsGame).Length;
            int numGridRowsCols = SetGridSize(numCharsInWords, lengthLongestWord);
            SetupEmptyGrid(numGridRowsCols);
        }
        private int SetGridSize(int numCharsInWords, int lengthLongestWord)
        {
            // minimum GridGame dimensions to fit longest wordCurrent
            int sizeMinGrid = lengthLongestWord * lengthLongestWord;

            // add extra GridGame elements to ensure enough space for non-wordCurrent characters
            int totalElementsGrid = (int)(numCharsInWords * 4f);

            int totalElementsGridSquare = (int)Math.Sqrt(totalElementsGrid);

            // increase current number of GridGame elements until reaches next root of square (e.g. 5, 6, 7)
            while (Math.Sqrt(sizeMinGrid) != totalElementsGridSquare + 1)
            {
                sizeMinGrid++;
            }

            // get number of rows/cols
            int numRowsCols = (int)Math.Sqrt(sizeMinGrid);

            return numRowsCols;
        }
        private void SetupEmptyGrid(int numGridElements)
        {
            gridGame = new char[numGridElements, numGridElements];
        }
        /*======================================*
        *  3.b. Handle populate grid with words *
        *=======================================*/
        private void PopulateGridWords(string[] words, char[,] grid)
        {
            bool wordPlaced = false;
            int numberWordsToPlace = Helper.CountElements(words);

            // iterate Words to place
            for (int wordCurrent = 0; wordCurrent < numberWordsToPlace; wordCurrent++)
            {
                wordPlaced = false;
                while (!wordPlaced)
                {
                    // Get random starting point for word
                    Vector2 coord = new Vector2(Helper.Random(0, grid.GetLength(0) - 1), Helper.Random(0, grid.GetLength(1) - 1));
                    if (PlaceWordInGrid(coord, words[wordCurrent], grid))
                    {
                        wordPlaced = true;
                    }
                }
            }
        }
        private bool PlaceWordInGrid(Vector2 pos, string word, char[,] grid)
        {
            int col = (int)pos.X;
            int row = (int)pos.Y;

            // elements represent placements options, 0 == left->right, 1 = right->left, etc. (in order presented below)
            int[] placementOptions = new int[8] { 9, 9, 9, 9, 9, 9, 9, 9 };
            int placementOption = 9;
            bool haveOptions = false;

            for (int counter = 0; counter < word.Length; counter++)
            {
                // If point empty or point contains same letter word's current character
                if (grid[col, row] == '\0' | grid[col, row] == word[0])
                {
                    if (SpaceRight(word, pos, grid))
                    {
                        placementOptions[0] = 1;
                        haveOptions = true;
                    }
                    if (SpaceLeft(word, pos, grid))
                    {
                        placementOptions[1] = 2;
                        haveOptions = true;
                    }
                    if (SpaceDown(word, pos, grid))
                    {
                        placementOptions[2] = 3;
                        haveOptions = true;
                    }
                    if (SpaceUp(word, pos, grid))
                    {
                        placementOptions[3] = 4;
                        haveOptions = true;
                    }
                    if (SpaceUpRight(word, pos, grid))
                    {
                        placementOptions[4] = 5;
                        haveOptions = true;
                    }
                    if (SpaceDownRight(word, pos, grid))
                    {
                        placementOptions[5] = 6;
                        haveOptions = true;
                    }
                    if (SpaceUpLeft(word, pos, grid))
                    {
                        placementOptions[6] = 7;
                        haveOptions = true;
                    }
                    if (SpaceDownLeft(word, pos, grid))
                    {
                        placementOptions[7] = 8;
                        haveOptions = true;
                    }

                    if (haveOptions)
                    {
                        while (placementOption == 9)
                        {
                            placementOption = placementOptions[Helper.Random(0, placementOptions.Length - 1)];
                        }

                        switch (placementOption)
                        {
                            case 1:
                                PlaceWordRight(word, pos, grid);
                                break;
                            case 2:
                                PlaceWordLeft(word, pos, grid);
                                break;
                            case 3:
                                PlaceWordDown(word, pos, grid);
                                break;
                            case 4:
                                PlaceWordUp(word, pos, grid);
                                break;
                            case 5:
                                PlaceWordUpRight(word, pos, grid);
                                break;
                            case 6:
                                PlaceWordDownRight(word, pos, grid);
                                break;
                            case 7:
                                PlaceWordUpLeft(word, pos, grid);
                                break;
                            case 8:
                                PlaceWordDownLeft(word, pos, grid);
                                break;
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        /*======================================*
        *  3.c. Populate empty grid elements    *
        *=======================================*/
        private void PopulateEmptyElements(char[,] grid)
        {
            for (int counterRow = 0; counterRow < grid.GetLength(0); counterRow++)
            {
                for (int counterCol = 0; counterCol < grid.GetLength(1); counterCol++)
                {
                    if (grid[counterRow, counterCol] == '\0')
                    {
                        grid[counterRow, counterCol] = Helper.Random('a', 'z');
                    }
                }
            }
        }

        /*==============================*
        *  4. Check words fit in grid   *
        *===============================*/
        private bool SpaceRight(string word, Vector2 pos, char[,] grid)
        {
            int col = (int)pos.X;
            int row = (int)pos.Y;

            if ((grid.GetLength(0)) - col >= word.Length)
            {
                // iterate right in row, checking each successive element empty or same as current char
                for (int counter = 0; counter < word.Length; counter++)
                {
                    if (grid[row, col + counter] != '\0' && grid[row, col + counter] != word[counter])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        } // check space left -> right
        private bool SpaceLeft(string word, Vector2 pos, char[,] grid)
        {
            int col = (int)pos.X;
            int row = (int)pos.Y;

            if (col >= word.Length - 1)
            {
                // iterate left in row, checking each successive element empty or same as current char
                for (int counter = 0; counter < word.Length; counter++)
                {
                    if (grid[row, col - counter] != '\0' && grid[row, col - counter] != word[counter])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        } // check space right -> left
        private bool SpaceDown(string word, Vector2 pos, char[,] grid)
        {
            int col = (int)pos.X;
            int row = (int)pos.Y;

            if ((grid.GetLength(0)) - row >= word.Length)
            {
                // iterate right in row, checking each successive element empty or same as current char
                for (int counter = 0; counter < word.Length; counter++)
                {
                    if (grid[row + counter, col] != '\0' && grid[row + counter, col] != word[counter])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        } // check space up -> down
        private bool SpaceUp(string word, Vector2 pos, char[,] grid)
        {
            int col = (int)pos.X;
            int row = (int)pos.Y;

            if (row >= word.Length - 1)
            {
                // iterate left in row, checking each successive element empty or same as current char
                for (int counter = 0; counter < word.Length; counter++)
                {
                    if (grid[row - counter, col] != '\0' && grid[row - counter, col] != word[counter])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        } // check space down -> up
        private bool SpaceUpRight(string word, Vector2 pos, char[,] grid)
        {
            int col = (int)pos.X;
            int row = (int)pos.Y;

            if ((grid.GetLength(0)) - col >= word.Length && // if space right
                (row >= word.Length - 1)) // if space up
            {
                // iterate right in row, checking each successive element empty or same as current char
                for (int counter = 0; counter < word.Length; counter++)
                {
                    if (grid[row - counter, col + counter] != '\0' && grid[row - counter, col + counter] != word[counter])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        } // check space diagonal left -> up right
        private bool SpaceDownRight(string word, Vector2 pos, char[,] grid)
        {
            int col = (int)pos.X;
            int row = (int)pos.Y;

            if ((grid.GetLength(0)) - col >= word.Length && // if space right
                (grid.GetLength(1)) - row >= word.Length) // if space down
            {
                // iterate right in row, checking each successive element empty or same as current char
                for (int counter = 0; counter < word.Length; counter++)
                {
                    if (grid[row + counter, col + counter] != '\0' && grid[row + counter, col + counter] != word[counter])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        } // check space diagonal left -> down right
        private bool SpaceUpLeft(string word, Vector2 pos, char[,] grid)
        {
            int col = (int)pos.X;
            int row = (int)pos.Y;

            if (row >= word.Length - 1 && // if space up
                col >= word.Length - 1) // if space left
            {
                // iterate right in row, checking each successive element empty or same as current char
                for (int counter = 0; counter < word.Length; counter++)
                {
                    if (grid[row - counter, col - counter] != '\0' && grid[row - counter, col - counter] != word[counter])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        } // check space diagonal left -> up right
        private bool SpaceDownLeft(string word, Vector2 pos, char[,] grid)
        {
            int col = (int)pos.X;
            int row = (int)pos.Y;

            if ((grid.GetLength(0)) - row >= word.Length && // if space down
                col >= word.Length - 1) // if space left
            {
                // iterate right in row, checking each successive element empty or same as current char
                for (int counter = 0; counter < word.Length; counter++)
                {
                    if (grid[row + counter, col - counter] != '\0' && grid[row + counter, col - counter] != word[counter])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        } // check space diagonal left -> up right

        /*==============================*
        *  5. Word placement in grid    *
        *===============================*/
        private void PlaceWordRight(string word, Vector2 pos, char[,] grid)
        {
            int col = (int)pos.X;
            int row = (int)pos.Y;

            for (int counter = 0; counter < word.Length; counter++)
            {
                grid[row, col + counter] = word[counter];
            }
        } // place word left -> right
        private void PlaceWordLeft(string word, Vector2 pos, char[,] grid)
        {
            int col = (int)pos.X;
            int row = (int)pos.Y;

            for (int counter = 0; counter < word.Length; counter++)
            {
                grid[row, col - counter] = word[counter];
            }
        } // place word right -> left
        private void PlaceWordDown(string word, Vector2 pos, char[,] grid)
        {
            int col = (int)pos.X;
            int row = (int)pos.Y;

            for (int counter = 0; counter < word.Length; counter++)
            {
                grid[row + counter, col] = word[counter];
            }
        } // place word up -> down
        private void PlaceWordUp(string word, Vector2 pos, char[,] grid)
        {
            int col = (int)pos.X;
            int row = (int)pos.Y;

            for (int counter = 0; counter < word.Length; counter++)
            {
                grid[row - counter, col] = word[counter];
            }
        } // place word down -> up
        private void PlaceWordUpRight(string word, Vector2 pos, char[,] grid)
        {
            int col = (int)pos.X;
            int row = (int)pos.Y;

            for (int counter = 0; counter < word.Length; counter++)
            {
                grid[row - counter, col + counter] = word[counter];
            }
        } // place word diagonal left -> up right
        private void PlaceWordDownRight(string word, Vector2 pos, char[,] grid)
        {
            int col = (int)pos.X;
            int row = (int)pos.Y;

            for (int counter = 0; counter < word.Length; counter++)
            {
                grid[row + counter, col + counter] = word[counter];
            }
        } // place word diagonal left -> down right
        private void PlaceWordUpLeft(string word, Vector2 pos, char[,] grid)
        {
            int col = (int)pos.X;
            int row = (int)pos.Y;

            for (int counter = 0; counter < word.Length; counter++)
            {
                grid[row - counter, col - counter] = word[counter];
            }
        } // place word diagonal left -> up left
        private void PlaceWordDownLeft(string word, Vector2 pos, char[,] grid)
        {
            int col = (int)pos.X;
            int row = (int)pos.Y;

            for (int counter = 0; counter < word.Length; counter++)
            {
                grid[row + counter, col - counter] = word[counter];
            }
        } // place word diagonal left -> down left
    }
}