# Code Review by Prapul Arumilli

**Date**: 12/4  
**File Reviewed**: `ClashOfClans.cs`  
**Author**: Shivam Engineer<br> 
**Time Spent**: 15 minutes  

## Readability

The `ClashOfClans` class is moderately readable, with clear naming conventions for variables and methods. However, the readability is hindered by the following issues:

- **Lengthy Methods**: Methods like `UpdateBarbs` and `UpdateCannonBall` are too long and contain nested logic, which makes it harder to follow.
- **Magic Numbers**: The frequent use of unexplained numeric literals (e.g., `7`, `5`, `280`) reduces clarity and makes the code harder to modify.
- **Minimal Comments**: There are no comments to explain the purpose of complex blocks of code, particularly in movement and collision detection logic.
- **Lack of Modularity**: Certain repeated logic, such as movement calculations and damage handling, is not abstracted into separate helper methods.

### Suggested Improvements for Readability
- Break down long methods into smaller, self-contained helper methods to simplify understanding and reduce nesting.
- Replace magic numbers with named constants or enumerations to improve clarity.
- Add comments or XML documentation to explain the purpose and flow of critical methods and variables.

---

## Quality

The class is functional and effectively manages its core responsibilities, but its implementation has several areas that could be improved:

- **Repetitive Logic**: Movement calculations and damage-handling logic are repeated in multiple places.
- **Coupling with `gamePopup`**: The class heavily depends on `gamePopup` for all interactions, reducing flexibility and testability.
- **Inefficient Design**: The `UpdateBarbs` and `UpdateCannonBall` methods process every barb and cannonball individually with nested logic, which could lead to performance issues as the number of entities increases.
- **Hard-Coded Values**: Image paths and positional offsets are hard-coded, making the class less adaptable to changes.

### Suggested Improvements for Quality
- Extract reusable logic, such as movement or health updates, into dedicated methods or utility classes.
- Use configuration files or constants for image paths and hard-coded values.
- Refactor entity-specific logic into distinct classes or components (e.g., `Cannon`, `Barbarian`, `TownHall`) to adhere to single responsibility principles.
- Consider using events or a messaging system to decouple game logic from `gamePopup`.
