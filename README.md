# Legends of Zelda

# Sprint 2
## Description
This project is a game that allows users to control a character, switch weapons, and interact with enemies using keyboard inputs. The game includes visual assets from **Zelda enemy sprites** and incorporates multiple states (damage, block, item, and enemy states) that the user can control in real-time.

### Zelda enemies spritesheet:
- [Sprite Sheet](https://www.spriters-resource.com/fullview/36632/?source=genre)

## Controls
- **WASD** or **Arrow Keys**: Move the character
- **1,2,3,4,5,6**: Switch between weapons
- **E**: Enter damage state
- **T, Y**: Switch block states
- **U, I**: Switch item states
- **O, P**: Switch enemy states
- **Q**: Quit game
- **R**: Rest (reset the game state)
- **F**: Toggle fullscreen mode

### Known Bugs
1. **Movement Bug While Attacking**: When the player is attacking and presses a different direction key, the attack animation may freeze or behave unexpectedly. This issue arises due to an unhandled interaction between the movement and attack state in the game engine.
   - **Workaround**: Avoid pressing movement keys during attack sequences until this bug is resolved.

2. **Occasional Lag**: Switching item states quickly can sometimes cause minor lag or delays in input responsiveness.

3. **Fullscreen Issues**: On some monitors, toggling fullscreen can cause the resolution to display incorrectly, cutting off certain UI elements.

## Tools and Processes
- **Visual Studio Code Metrics**: We used Visual Studio’s built-in tools to calculate code metrics such as coupling.
  - Metrics were tracked weekly, and we also tracked trends to ensure we were improving code quality.

## Code Reviews
Each team member participated in code reviews, with an emphasis on readability and maintainability:
- **Readability**: Ensured that variable and method names were clear, consistent, and well-documented.
- **Maintainability**: Focused on adhering to SOLID principles, reducing code duplication, and making the code adaptable to future changes.
  - Specific classes reviewed include: `PlayerController`, `EnemyManager`, and `KeyboardController`.

## Sprint Reflection
- **Team Performance**: Our team completed all key features, but timing was an issue. We waited until close to the deadline to implement some of the more complex features, which caused a last-minute rush.
- **Time Management**: Our team started late on the project due to Github issues, but we managed to catch up near the end. Next sprint, we plan to spread tasks more evenly and start working on harder features first.
- **Process Changes**: We implemented weekly code metric checks, which helped us identify potential areas for refactoring earlier in the development process. However, we need to better manage our time, especially when it comes to bug fixing.

## Future Improvements
- **Fixing Known Bugs**: In the next sprint, we will focus on fixing the attack movement bug and addressing the fullscreen display issues.
- **Optimization**: Investigating ways to reduce input lag when switching item states.
- **Code Refactoring**: More attention will be given to making the codebase modular and ready for future expansion.
