# Legends of Zelda

## Sprint 5

## Description
This project is a game that allows users to control a character, switch weapons, and interact with enemies using keyboard inputs. The game includes visual assets from **Zelda enemy sprites** and incorporates multiple states (damage, block, item, and enemy states) that the user can control in real-time.

Rooms are loaded from room csv files where 0 represents empty space and positive integers represent unique game objects such as ground items, blocks, and enemies. Rooms are populated based on the positions of these ints within the csv file (matrix represents room).

### Zelda enemies spritesheet:
- [Sprite Sheet](https://www.spriters-resource.com/fullview/36632/?source=genre)

## Controls
- **WASD** or **Arrow Keys**: Move the character
- **J, K**: Cycle weapon forward/backward in B slot
- **Z & N**: Use weapon in A slot (sword)
- **X**: Use weapon in B slot
- **1,2,3,4,5,6**: Use secondary weapons (debug mode)
- **O**: Toggle end game screen (can reset with r or press o again to start over)
- **P/I**: Toggle pause/inventory screen
- **M**: Toggle background music
- **Left/Right Mouse**: Switch room states (debug mode)
- **Q**: Quit game
- **R**: Reset (reset the game state)
- **F**: Toggle fullscreen mode
- **H**: Toggle hitboxes

### Known Bugs
1. **Trapped on Room Switch (Debug Mode Only)**: When manually switching rooms, it is possible for Link to be stuck in a wall in the new room. This is due to Link's position being constant on a manual room change. This is a small bug that only occurs when debugging as manual room switching will not be incorporated into the final game.

## **Tools and Processes**

### **Visual Studio Code Metrics**  
We used Visual Studio’s built-in tools to calculate and monitor code metrics, such as coupling and complexity. Metrics were tracked weekly, and trends were analyzed to ensure continuous improvement in code quality. Our code compiles with no errors or warnings.

#### **Current Metrics**
- **Project**: Sprint2Pork  
- **Configuration**: Debug  
- **Scope**: Assembly
  
| **Metric**                  | **Value** |
|-----------------------------|-----------|
| Maintainability Index        | 79        |
| Cyclomatic Complexity        | 1,073     |
| Depth of Inheritance         | 2         |
| Class Coupling               | 172       |
| Lines of Source Code         | 6,394     |
| Lines of Executable Code     | 1,774     |


## Code Reviews
Each team member participated in code reviews, with an emphasis on readability and maintainability:
- **Readability**: Ensured that variable and method names were clear, consistent, and well-documented.
- **Maintainability**: Focused on adhering to SOLID principles, reducing code duplication, and making the code adaptable to future changes.
  - Specific classes reviewed during this sprint can be found in the `CodeReviews` `Sprint 5` folder.
  - Additional code reviews from past sprints can be found in the `CodeReviews` folder of the repo

## **Sprint Reflection**

### **Team Performance**  
Our team completed all key features on time and met the sprint objectives. We maintained a consistent pace throughout the sprint, focusing on both refactoring existing code and implementing new features. Towards the end of the sprint, team members increased their contributions to ensure all features were fully implemented and all known bugs were resolved. Collaboration and communication were strong, which helped us achieve our goals efficiently.

### **Time Management**  
Our team started the project shortly after Sprint 5 began and maintained steady progress.  
- We scheduled regular check-ins to assess progress, address blockers, and adjust priorities as needed.  
- Despite initial steady progress, we experienced a noticeable increase in workload towards the end of the sprint, as several tasks required final adjustments and debugging.  
- To mitigate last-minute rushes in future sprints, we plan to allocate more time for testing and bug fixes earlier in the sprint and prioritize critical features first.

### **Process Changes**: 
We implemented weekly code metric checks, which helped us identify potential areas for refactoring earlier in the development process. However, we need to better manage our time, especially when it comes to bug fixing and refactoring.

### **Challenges Faced**  
1. **Integration Issues**: Some components did not integrate seamlessly, requiring additional debugging time.  
   - *Solution*: We implemented regular integration tests and reviewed code dependencies earlier in the sprint.  

2. **Unexpected Bugs**: A few critical bugs were discovered during final testing, which required immediate attention and delayed other tasks.  
   - *Solution*: Moving forward, we will allocate buffer time for testing and code review to catch bugs earlier.  

### **Lessons Learned**  
- **Early Testing is Key**: Conducting more frequent tests and code reviews throughout the sprint would have minimized last-minute issues.  
- **Improved Communication**: Regular communication and updates between team members helped identify and resolve blockers faster.  
- **Task Prioritization**: Prioritizing critical tasks earlier in the sprint helped us avoid delays and ensured we met the sprint goals.

### **Future Improvements**  
- Implement automated testing to reduce manual debugging efforts.  
- Allocate specific time for code integration and system-level testing.  
- Improve task estimation to better allocate resources and avoid last-minute rushes.
- Expand on code refactoring with a focus on future maintainability and expansion.

### **Trello Board Links from Past Sprints**  
We utilized Trello to track our progress and manage tasks across all sprints. Below are the links to Trello boards from previous sprints, which provide a detailed history of our task management, progress, and team contributions:

- **Sprint 2 Trello Board**: [Sprint 2 Board](https://trello.com/b/BaB2vWmP/john-pork-studios) 
- **Sprint 3 Trello Board**: [Sprint 3 Board](https://trello.com/b/0C31UwxK/sprint-3)  
- **Sprint 4 Trello Board**: [Sprint 4 Board](https://trello.com/b/IYsFpeVh/sprint-4)  
- **Sprint 5 Trello Board**: [Sprint 5 Board](https://trello.com/b/4VCNe9ln/sprint5)

These boards document:  
- **Task Assignments**: Each task was assigned to a team member along with deadlines.  
- **Progress Tracking**: Tasks moved across `To Do`, `In Progress`, `In Review`, and `Completed` columns to reflect their status.  
- **Sprint Retrospectives**: Key insights, challenges, and improvements were noted in each sprint's retrospective section.  
- **Backlog Management**: Outstanding tasks were tracked in the backlog and carried over to subsequent sprints if necessary.  

These links provide a comprehensive view of how our project evolved over time and how we managed our workflow to achieve project milestones.
