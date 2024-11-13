# Code Review by Shivam Engineer

**Date**: 11/12  
**File Reviewed**: `Arrow.cs`  
**Author**: Prapul Arumilli<br> 
**Time Spent**: 15 minutes  

## Readability

The 'Arrow.cs' class is relatively readable, though the code could have more consistency, using switch cases in the constructor, but if else if statements in the Update() function. The class also groups together the one line functions which makes it easier to read.

## Quality

The 'Arrow.cs' class has multiple if else statements that could be grouped together as a switch statement. There are many magic numbers in the functions, though it might make the code more complicated if that were changed to many variables

### Suggested Improvement
- The Update() function could use a switch statement instead
- There could be a getRect() function similar to the rest of the code base, so then the collision functions from 'Collision.cs' could be reused instead
- There could also be an abstract class for ILinkItem as the other Link item collides functions are the same, so then you could reuse the one collision function 
