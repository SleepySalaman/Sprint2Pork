# Sprint 3
## Eli Paulman's Code Review (Link.cs written by Eli Click) Date: 10/20

Readability
The Link class is generally well-structured with clear naming conventions and 
good use of encapsulation through interfaces like ILinkDirectionState and 
ILinkActionState. However, readability can be improved by adding comments to 
explain non-trivial logic, especially in more complex methods such as Move() 
and BeDamaged(). Replacing "magic numbers" like 40 in the Attack() method 
with named constants will make the code easier to understand. Additionally, 
simplifying boolean logic in methods like BeDamaged() and reducing repetitive 
logic across methods like LookLeft and LookRight would enhance clarity.

Maintainability
To improve maintainability, several public fields (such as X, Y, OffsetX, and OffsetY) 
should be changed to private and exposed through properties to better encapsulate data. 
The repeated code in direction handling (e.g., LookLeft, LookRight) could be refactored 
to reduce duplication. To support future expansion, refactoring the UseItem() 
method using a design pattern like Factory would make it more scalable. 
Finally, splitting the responsibilities in methods like BeDamaged() into 
smaller, focused methods would make the code easier to maintain and extend.
