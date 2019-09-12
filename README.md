******* Introduction *******  


This is a game inspired by the Bejewelled. 
The initial goal was to create a clone of Bejewelled using .Net WPF, however due to limited time many of the game mechanical features were left out.

The development time for the game was approximately 3 days. Through out the development period many complications were encountered, mostly while working 
on the algorithm for removing and adding of jewels.

In the current state the game is named the Fake Jewelled due to its resemblance to bejewelled and lack of complete mechanics.



******* Game Play *********


The Game is played in an 8 by 8 square grid. Similar to Bejewelled the goal is to match atleast 3 jewels either in a row or column. 
On matching a chain of alteast 3 jewels, all jewels in the chain will disappear and new jewels will take their place.

Matching of Jewels is done by swaping two neighbouring jewels in each turn. Jewels that are not next to each other cannot be swapped.

Unlike the original Bejewelled, jewels in the upper rows wont drop when jewels in the lower rows are removed. As stated above this mechanic was 
intentionally left out due to time constraint. Merging of 4 or more jewels in a chain to create special types of jewel was also left out.

The scoring system uses the equation: 

score = N ^ 2 , where N is the number of Jewels in a chain 

Game runs untill a limited number of turn is completed.



******** Game Design *********


The game is developed purely in .Net WPF framework and doesn't utilize any sort of game engine.

The Grid is made in WPF .XAML file through creation of multiple rows and column

The Jewel images are assigned using the .XAML style template feature. The created template were binded with the individual Jewel classes

Classes: 

The Game consists of mainly 6 classes

MainWindow : This is the main window for the game. All initial game objects are initiated here.

Jewellery Assembly 

JewelManager: This class is used to generate initial jewel configuration, remove and add new jewels.

JewelSwitcher: This class is used in switching two jewels. 

Jewel: The Jewel class is a subclass of WPF Button. The clicking of individual jewel is handled in similar manner to how the WPF button handles
       onClick events. Jewel is an abstract class with sub classes Red Jewel, Blue Jewel and Purple Jewel.

ScoreTracker: This class is used to track score and turn count.
 
Container: This class is what encapsulates all the above assembly class objects. This class is also responsible for ending the game when turn count
	   reaches 0.This class was created to essentially detach the other assembly classes from MainWindow class.

Additional: There is a UserControl class called the FinalMessage, which is essentially only used to produce the final message when game ends.
	    FinalMessage class is mostly empty as most of the design was done using the .XAML.
	    FinalViewModel class is another empty class that is only used for linking DataContent in the MainWindow .XAML with the FinalMessage
	    template.

Exporting: The project contains all the necessary resource files(jewel image files), and referencing within the .XAML is done using relative 
	   path as such, the project can be easily moved to a different machine.

  

******** Difficulties *********


The major difficulty was in development of the algorithm for detection and removal of valid jewel chains.

For the purpose of efficiency, decision to develop a selective recursive function was made instead of a bruteforce function.

The idea behind the function is as follows:

- When a switch between two jewels is made, run recursive search starting for the two swap jewels invidually
- The recursive search essentially searches for the neighbouring jewels of the same type and returns an array of matching jewels
- The recursive search is conducted in 4 directions, LEFT, RIGHT, UP and DOWN
- For each maching jewel, run the recursive function again but instead of moving in all matching jewel's direction, only move in the direction
  that was taken to reach it in the first place, ie if the jewel was initially LEFT of the first jewel then keep moving LEFT and if there is no
  matching jewel in the direction then return.
- Same recursive search is also conducted in the opposite direction, ie if first the search was conducted towards LEFT of the first jewel then 
  another search towards RIGHT of the first jewel is also conducted
- A count is also kept while recursing, so when the count is greater than 3 or if there is a possibility of count become 3 then the jewel is added 
  to a set of jewels that will removed.

Due to the complexity of the algorithm, it was difficult to implement it properly. And due to the lack of time, a lot of bugs were left in the game 
such as jewels will be removed even when the chain only has 2 elements and sometimes chain of 3 or more than 3 will be left untouched.

The overall of design of the game also lacks severly. Classes and methods are poorly implemented and haven't been refactored.
No test classes were written.

********* Conclusion ************
Even though Bejewelled is a small and simple game by the standard of many other big titles, the complexity involved in it and the work required to 
build a functionaly Bejewelled title should not be underestimated. 
Like most 2D games, Bejewelled is often seen as an easy project to complete but as experienced throuh out the 3 days project, despite the simple looks,
the game has some very complex mechanics and as such very difficult to properly produce without a properly working on the design first.
For the 3 days project, no design documents were produced due to lack of time which resulted in lots of complications and bad coding. It can be concluded 
that to produce a well made game title, it is important to take sufficient time and work on the design of the game before starting to write the code. 
