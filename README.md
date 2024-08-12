**_Portfolio Project:_ A Simple Tower Defense**

This is a simple tower defense game I developed to enhance my skills and expand my portfolio. The project is still ongoing. I strive to adhere to SOLID principles, OOP methodologies, and clean code practices as much as possible. I also focus on optimization and write code that allows Game Designers to work more comfortably, enabling easier level and wave editing from the inspector.

In the architecture of this project, I utilized the following technologies and design patterns: _Scriptable Objects, Object Pooling, DoTween, UniTask, Factory Design Pattern, Singleton Design Pattern, Strategy Design Pattern, and Observer Design Pattern (Events)._

Mechanics and Features Added:

_Base:_
- Implemented basic base logic. When enemies reach the base, they reduce its health, and the game ends when health reaches zero.

_Camera:_
- Developed a free isometric camera angle, aiming to mimic the typical camera used in tower defense games.

_Enemy:_
- Added 4 different enemy types, storing their data with Scriptable Objects.

- Used Object Pooling for efficient management of these enemy types.
- Added enums to facilitate easier adjustments by Game Designers or Level Designers from the inspector.

- Created a manager to spawn enemies from the Object Pool and place them on the stage. Included necessary Headers and Configuration Class to simplify inspector work.

- Designed a simple pathing system for enemies to follow predetermined waypoints.
- Developed a controller class for the life bar displayed above enemies.

_Level and Wave:_
- Introduced a separate manager class to simplify level and wave control.

_Tower:_
- Added 4 tower types, with their data stored using Scriptable Objects.

- Managed general Shoot and Upgrade logic of towers with an abstract class.

- Created two validator classes to ensure towers are placed correctly. One controls overlapping, and the other checks the required layer.

- Utilized switch-case to select the required prefab object for the chosen tower.

- Controlled the placement of the selected tower with the mouse using validator classes and the placement manager.

_Bullet System:_

Implemented a bullet system based on tower type. Currently, there are two types of attacks:

- A straightforward bullet that directly targets the enemy.

- An AOE bullet that follows an arc from above and strikes the target area.

_Target Detection and Selection:_
- Used the Line Renderer component to define individual ranges for each tower based on their range radius values.

- Currently, towers target and attack the nearest enemy within their range, but I plan to enhance this system in the future.

_In-game UI:_
- Managed essential game values such as Base Health and Current Alive Enemy with a manager through events.

This project has been a valuable learning experience, and I continue to refine and expand its features.
