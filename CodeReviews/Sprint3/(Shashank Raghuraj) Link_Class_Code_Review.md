# Code Review by Shashank Raghuraj

## Readability
Date: 10/17

File Reviewed: GroundItems.cs

Author: Eli Paulman

Time Spent: 10 minutes

The GroundItem.cs class is extremely readable, with methods that are easy to understand, however, some improvements could be naming conventions and potentially creating variables to get rid of magic numbers. One area of the code that's incredibly readable is the Update method. This update method is clearn and showcases how the item is animated. One area of the code that could be made more readable are some of the variables in the code, for instance, the magic number 30 could created into a variable called Timing. Overall, there isn't much that isn't readble, however, some of the magic numbers could be created into variables

## Quality

Date: 10/17

File Reviewed: Collision.cs

Author Shivam Engineer

Time Spent: 10 minutes

The Collision.cs class has excellent code quality. The three methods clearly hit on every use case of collision including item and blocks. It follows the math that was talked about in class, and the in-game implemention of these methods work perfectly. A potential change that's related to this game is when link gets damaged and moves backwards. This is checked through the regular collision method and works perfectly, and makes sure link isn't glitching through the wall. Overall, the code quality is amazing and identifies each type of collision through the methods
