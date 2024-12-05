# Code Review by Eli Click

## Readability
Date: 12/4

File Reviewed: Drawing.cs

Author: Eli Paulman

Time Spent: 10 minutes

While Drawing.cs is a complex file, its structure and organization make it relatively easy to look through. After refactoring, this is the method where most of the Draw() methods are housed, with each method corresponding to a system within the game. The methods are clearly named, making it easy to tell which method accounts for each particular part of the game. Furthermore, most of the magic numbers have been removed and are replaced with constants that are both easy to track the value of and make changes to as needed. For some of the methods, it is easy to tell what specific element in that system is being drawn from the surrounding variables and values. However, for some of the methods lower in the order, it is a little difficult to understand what each spriteBatch.Draw() method is actually responsible before. While the method names help, I believe that a few of the methods could benefit from additional comments to increase the readability. As a whole, though, the code is easy to understand and refactor as needed, which is the result of a lot of good work on Eli Paulman's end.

## Quality

Date: 12/4

File Reviewed: Drawing.cs

Author: Eli Paulman

Time Spent: 10 minutes

This file is unique in that it accounts for most of the drawing for the entire game, with a lot of methods being housed here following refactoring. Despite its complexity, the code inside the class itself is polished and robust. Each method is well-formatted and avoids using global variables, instead having most of the necessary assets passed in with descriptive names via the method call. This makes it easy to trace back and identify any particular pain points in later refactoring. The same applies to variables, as there is a minimal amount of magic numbers present inside the code. In terms of the code itself, it is relatively simple, utilizing loops for parts of the game that need it, such as drawing the blocks and enemies, while most of the other drawing is done via simple method calls. While it could be shortened in some cases, minimizing the complexity is actually helpful in improving the game's performance. This isn't necessarily an issue with the scope of our project, but would be crucial if we were to fully flesh out the game and implement significantly more functionality. Overall, the code is complex and simple in appropriate parts, making for a smooth running and easily refactorable class.
