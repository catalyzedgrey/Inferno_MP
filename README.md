
# Game Description:

The game features two players racing to reach the end of the level, running through obstacles and traps. Each time a player dies, they’re sent to their original spawning position. Whomever reach the end first wins. The level will be maze-like and unforgiving for player errors.

The game works on LAN network only.

# Game network concept:

An object, of class "State", is sent between the two players. This class has many members; string, bool, int, etc.. At first, when the two players are connecting through the lobby, no confirmation is needed When they both connect to each other, the game starts automatically, the name of player one is sent to player 2 and vice versa. The location of the player is sent with every tick of the game (100 ticks per second). Then, two listener threads (UDP and TCP) open up and run in a loop. Threads are only aborted in case of the match ended, or if the other player disconnected. When player X disconnects, no message is sent indicating that. Because a power cut is a possible situation, there won't be enough time to send. Instead, player Y game will detect the disconnection when it fails to send its own packets by TCP too many times.

TCP Messages:

    Player name. Once, when game starts.
    A message when the player dies, including the death reason (needed for the death particle effect).
    A message if the player reached the finish line.

UDP Messages:

    Player location
    Player facing orientation

Screenshot:
![Alt text](GameForm/resources/Screenshot.png?raw=true "Title")
