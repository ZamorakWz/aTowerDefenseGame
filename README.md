**_Portfolio Project:_ A Simple Tower Defense**

This is a simple tower defense game I developed to enhance my skills and expand my portfolio. The project is still ongoing. I strive to adhere to SOLID principles, OOP methodologies, and clean code practices as much as possible. I also focus on optimization and write code that allows Game Designers to work more comfortably, enabling easier level and wave editing from the inspector.

In the architecture of this project, I utilized the following technologies and design patterns: _Scriptable Objects, Object Pooling, DoTween, UniTask, Factory Design Pattern, Singleton Design Pattern, Strategy Design Pattern, and Observer Design Pattern (Events)._

_Enemy:_
- Data management using Scriptable Objects
- Various enemies with different Health, Damage and Speed variables
- UI health bar showing the health of each enemy
- Object pooling for enemies

_Tower:_
- Managing tower data with Scriptable Objects
- Three different tower types (Standard, AOE, Tesla)
- Management of all towers using Abstract class
- Freedom to place a tower anywhere on the map (except on the enemy path)
- Panel showing the properties of the selected tower
- Damage, fire rate and range upgrades from the pop-up panel
- Separate target selection system for each tower (Most health target, the fastest target, most progressed target, etc....)
- Object pooling for bullets used by towers

_Resource Management:_
- Monetization system by killing enemies
- Tower building and upgrading economics

_Visual Effects:_
- Use of Particle System
- Explosion effect for AOE towers
- Lightning effect for Tesla towers

_Camera:_
- Isometric camera system that the player can move

_Wave System:_
- Easily determine how many enemies will be in each wave from the inspector
- A certain waiting time after each wave

_In this project I also used the following:_
- Zenject (Dependency injection for better architecture)
- DoTween (Efficient simple animation system)
- UniTask (Optimized asynchronous operations)
