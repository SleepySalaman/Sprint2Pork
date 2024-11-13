Code Review by Eli Click

Readability:
 - Date: 11/12
 - File: Minimap.cs
 - Author: Shashank Raghuraj
 - Time Spent: 15 minutes
 - Comments: The Minimap.cs class is set up conveniently and logically so that it is easy to read and follow. Despite not having comments, the code is organized in a way so that a reader can see each part of the minimap being drawn by each section of the code: background, blocks, items, enemies, and Link. Furthermore, the Draw method follows the same order of objects as the initialization method, so it is easy to tell where certain aspects of the code are being generated/drawn. The variables are appropriately named, making it easy to follow the order and logic throughout the file. While this file doesn't have any comments, its layout and design are essentially self-explanatory, and comments are not really necessary to gauge the file's behavior. Overall, this file has great readability and is one of the standouts in our code for designing with readability in mind.

Quality:
 - Date: 11/12
 - File: Paused.cs
 - Author: Shashank Raghuraj
 - Time Spent: 15 minutes
 - Comments: This class is written efficiently and effectively, but could be a target for some potential minor refactoring later. It uses the inventory object from Game1 to create a count of each of the user's items in their inventory when the game is set to "pause" during the playing state. The initialization is quick, and while the rectangle generation does have some magic numbers, it doesn't make sense to create a variable for each number when most of them are only used once. The main part of the code, under the DrawPausedScreen() method, accomplishes its task quickly and simply with only a few draw methods and a short loop. Furthermore, it is easy to tell which part of the code is doing what, so errors and issues with the sprites can easily be traced back to their respective parts. The only suggestion I would have is to possibly decrease or move the "magic numbers"; for example, declaring them as a part of the class versus declaring them each time the method is called. However, this is a relatively simple change, and doesn't impact the functionality of the code. So, while small tweaks can be made, this code is ultimately written very robustly and will likely only require small attention during the refactoring process (unless there were to be significant changes made to the pause menu).
