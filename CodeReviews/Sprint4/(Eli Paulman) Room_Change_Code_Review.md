# Code Review by Elijah Paulman

**Date**: 11/12  
**File Reviewed**: `RoomChange.cs`  
**Author**: Shivam Engineer<br> 
**Time Spent**: 15 minutes  

## Readability

The `RoomChange.cs` class demonstrates good readability with descriptive method names and a logical code structure. The purpose of each method is clear from their names: `SwitchToNextRoom`, `SwitchToPreviousRoom`, and `SwitchRoom`. The use of `ref` parameters for updating the current room and related lists is appropriate, though the method signatures are somewhat lengthy.

### Suggested Improvement
- Group `blocks`, `groundItems`, `enemies`, and `fireballManagers` into a custom data structure or object (e.g., `RoomState`). This refactor would reduce redundancy in the method signatures and make parameter management easier. Overall, the code is clear but could be more readable with this minor refactor.

## Quality

The `RoomChange.cs` class is well-structured and accomplishes its intended function of managing room transitions. The `SwitchRoom` method effectively updates room-related entities by fetching values from a `Dictionary`. Using `ref` parameters here is suitable, ensuring changes reflect back to the caller.

### Suggested Improvement
- The method signatures include multiple `ref` parameters for each entity list. Introducing a `RoomState` class that encapsulates `blocks`, `groundItems`, `enemies`, and `fireballManagers` would simplify these signatures, enhancing maintainability and readability.
- Consider adding error handling for cases where `currentRoom` is not found in `rooms` to avoid potential null references and improve robustness.
