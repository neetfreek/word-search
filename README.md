# Word Search Generator
This project a word search game written in C# using MonoGame (a C# framework implementing Microsoft XNA 4 API). The back end uses my word-search-generator project, replacing the console interface with a window interface.

## The Game
- User selects a category of words to find (Instruments, Mammals, Occupations).
- User selects the size of grid; Small (4 words), Medium (8 words), Large (12 words).
- User is presented with a list of words to find, the word search grid, Quit and Menu buttons.
 - Each game selects different words for the player to find.
 - Grid size is relative to the number of words in the words list. 
 - Words randomly placed in the grid diagonally, horizontally and vertically, both backwards and forwards.
- LEFT-click or drag to select tile(s).
 - Currently selected tile highlighted in green.
 - Currently selected tiles have green lines drawn over them.
 - Only adjacent tiles can be selected.
- RIGHT-click or drag to deselect tile(s).
 - Lines are erased.
- On selecting a row of tiles which make up a word:
 - All tiles in word momentarily flash green.
 - Line over words become permanent (grey).
 - Word found is removed from list of words to find.
- On finding all words, user is shown a message.