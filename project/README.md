# Programming Test

We have prepared for you a small prototype :video_game:

It's a simple game, where the user controls a character that can move left and right on the screen, is able to shoot bullets and kill enemies that are spawned in front of the character.
The game is infinite and goes on until the player loses.

The game is supposed to run in landscape mode.

### Game mechanics 🔫
The game has already implemented basic flow and UI to get you started.

The main game has basic implementation ready:
- Splash screen to start the main game
- Player can move left and right with arrows or A,D keys
- Player can shoot bullets on space
- Enemies are spawning based on basic config
```
public class SpawnerConfig
{
    public GameObject Prefab;
    public float SpawnDelay;
    public Vector3 SpawnPosition;
}
```
- Killing enemies by bullets
- Player is receiving damage when hit by enemies
- Basic configuration  for player and enemy parametrization
```
public class EnemyConfig
{
    public int Hitpoints;
    public float Speed;
    public float Damage;
}
```
```
public class PlayerConfig
{
    public int Hitpoints;
    public float Speed;
    public float BulletDamage;
    public float BulletSpeed;
    public float BulletCooldown;
}
```
- End Popup when player dies
- Top bar is displaying current score and player hp 

### How to run the proto 🏃
To run the game launch scene *Splash* in *Assets/Scenes* and click Play button.

### Assignment 🔨
- Make sure that the game is mobile friendly, and take into consideration mobile platform restrictions.
- Implement simple highscore system with scrollable list in endPopup that will display the data
    - Game should remember and display player's highest score from the session
    - Leaderboard scroll should use an instance of LeaderboardModel as the source of data to display
    - LeaderboardModel has simple logic to generate some mock data
    - Make sure that the list is displayed efficiently.
    - Use LeaderboardItem prefabs as the view for models
    - Display the current score in the end popup
- Add support for touch input
    - Touching the left part of the screen, the player moves left (same as using the left arrow key)
    - Touching the right part of the screen, the player moves right (same as using the right arrow key)
    - Touching the central part of the screen, the player shoots a bullet
- Extend gameplay mechanic, pick one of the tasks below:
    - Add additional enemy behaviour
        - Examples:
            - Enemy that can shoot
            - Enemy that can change direction when going towards player
    - Add additional player shooting behaviour
        - Examples:
           - Different shooting patterns (ie. cone, sequence)
        - Bullets with damage range


### Deliveries 📦
- [ ] The Unity project folder (with the subfolders Assets, Packages and Project Settings)
- [ ] Android apk with the build working on mobile android devices (or an ipa with the build working on iOs devices, if you prefer)


### Additional information 🤞
- You may take up to 3 days to complete your game.
- You should use Unity 2020 or later and C# for scripting.
- It is possible to include Unity built-in packages and packages of the Unity Registry, 
but it is not possible to use 3rd party packages or assets coming from the Asset Store or other sources, except for animations (e.g. DOTween), and already included in manifest.json
- You can modify any code in the project if you see any places which can be improved in example: optimizations, architecture.
- You are expected to fulfill the scope but are not limited to it, feel free to be as creative as you
like!

### Review 🦄
- We will look at the code structure and implementation, as well as Unity integration. 
- Graphical quality of the elements will not be evaluated, but the layouts and structure will.
- Remember  that the game is supposed to run on mobile.
- It's early prototype, but it's good to follow good practices - show use your skills
- Make sure to give proper control to non developers and make changes to game without need of code changes
- Try to write clean, extensive code with clear separation between layers.
- It's always a plus, if you add any something extra that would improve execution, readability and fluidity of the gameplay :sparkles: 

## GOOD LUCK AND HAVE FUN! :trophy:
