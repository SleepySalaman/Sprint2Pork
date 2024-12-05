# Code Review by Shivam Engineer

**Date**: 12/4  
**File Reviewed**: `GameStateManager.cs`  
**Author**: Shashank Raghuraj<br> 
**Time Spent**: 15 minutes  

## Readability

The 'GameStateManager.cs' class is quite readable, with only the constructor as well as one other method having more than a single line of code. 
There also is not any nesting, which makes the code easier to follow.

## Quality

The constructor is as simple as it can be, just setting the values of private variables. And the ResetGame function has quite a few number of lines, 
though it resets everything in the game, so it makes sense that there isn't less code. 

### Suggested Improvement
- There could be methods called in ResetGame, such as having a ResetSounds method, RestRooms method, and ResetLink method or something like that.
- The methods in this class could potentially be static methods, as there isn't really the necessity for a GameStateManager object for the game to be reset
