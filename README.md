# Word Search Generator
This project a word search game written in C# using MonoGame (a C# framework implementing Microsoft XNA 4 API). The back end uses my word-search-generator project, replacing the console interface with a window interface. It is very much a work in progress!


## The Game
- User selects the size of grid; Small (6 words), Medium (12 words), Large (16 words)
- User selects a category of words to find (Instruments, Mammals, Occupations)
- User is presented with the words to find and the word search grid
 - Grid size is relative to the number of words in the words list. 
 - Words randomly placed in the grid diagonally, horizontally and vertically, both backwards and forwards.
- User can choose to regenerate a new word grid of a different size with a different list

## So Far
- FRONT END:
	- Screen size independent scaling, placement for 16:9 (not resolution independent!) 
	- Menu: buttons to select
		- Start to begin
			- Category of words to find
			- Size of grid to play on
			- Menu to return to Start
		- Quit to quit game
	- On selecting category, size game begins
		- Draw chosen list including chosen number of words to find
		- Draw heading, list of words to find, buttons to return to menu, quit
	- Selection 
		- Left-click to select menu buttons Menu, Quit
		- Left-click(/drag) to select letter tiles in grid
			- Highlight selected tile
			- Draw line through tile
		- Right-click to unselect letter tiles
	- On find words
		- Remove word from list
		- Briefly highlight word found
		- Draw letter tile lines permanently
		
## Bugs
- Grid sometimes to small to accommodate all words (most recreatable in Occupations, Small)
- Word highlight too brief

## ToDo
- On find all words display completion message
- Fix bugs
- Make prettier	