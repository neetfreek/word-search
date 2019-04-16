# Word Search Generator
This project a word search game written in C# using MonoGame (a C# framework implementing Microsoft XNA 4 API). The back end uses my word-search-generator project, replacing the console interface with a window interface. It is very much a work in progress!


## The Game
- User selects the size of grid; Small (6 words), Medium (12 words), Large (18 words)
- User selects a category of words to find (Instruments, Mammals, Occupations)
- User is presented with the words to find and the word search grid
 - Grid size is relative to the number of words in the words list. 
 - Words randomly placed in the grid diagonally, horizontally and vertically, both backwards and forwards.
- User can choose to regenerate a new word grid of a different size with a different list

## So Far
- FRONT END:
	- Screen size independent scaling, placement for 16:9 (not resolution independent!) 
	- Draw grid of words to find, random letters, in center of screen
	- Cursor to move about, detects mousing over different letters, menu buttons
	- Draw list of words to find (only from Mammals category, 16 words
	- Draw headings
	- Draw menu buttons