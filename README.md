**_Portfolio Project:_ A Simple Tower Defense**

This is a simple tower defense game I developed to enhance my skills and expand my portfolio. The project is still ongoing. As much as possible, I strive to adhere to SOLID principles, OOP methodologies, and clean code practices. I also focus on optimization and write code in a way that allows Game Designers to work more comfortably, enabling easier level and wave editing from the inspector. Here are the mechanics and features I have added to the game so far:

Base:

-Implemented basic base logic. When enemies reach the base, they reduce its health, and the game ends when health reaches zero.

Camera:

-Developed a free isometric camera angle, aiming to mimic the typical camera used in tower defense games.

Enemy:

-Added 4 different enemy types, storing their data with Scriptable Objects.
-Used Object Pooling for efficient management of these enemy types. 
-Added enums to facilitate easier adjustments by Game Designers or Level Designers from the inspector.
-Created a manager to spawn enemies from the Object Pool and place them on the stage. Included necessary Headers and Configuration Class to simplify inspector work.
-Designed a simple pathing system for enemies to follow predetermined waypoints.
-Developed a controller class for the life bar displayed above enemies.

Level and Wave:

-Introduced a separate manager class to simplify level and wave control.

Tower:

-Added 4 tower types, with their data stored using Scriptable Objects.
-Managed general Shoot and Upgrade logic of towers with an abstract class.
-Created two validator classes to ensure towers are placed correctly. One controls overlapping, and the other checks the required layer.
-Utilized switch-case to select the required prefab object for the chosen tower.
-Controlled the placement of the selected tower with the mouse using validator classes and the placement manager.

In-game UI:

-Managed essential game values such as Base Health and Current Alive Enemy with a manager through events.

This project has been a valuable learning experience, and I continue to refine and expand its features.
