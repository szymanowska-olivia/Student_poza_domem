# Student poza domem / Freshman on their own
.NET Text Adventure Game where you become a newly arrived student. You stand before your dormitory wondering whether to go in when 3 fascinating persons come up to you, choose one main quest to complete through the game...

# About the game
It's designed to be kind of like a text labyrinth game where you decide where to go and what to do by writing commands in the terminal. Student poza domem focuses mostly on mechanics and serves as a learning project for OOP in C#. There's quests, dialouges and some plot (tho it's not really deep). The player can encounter enemies and defeat them, interact with objects while completing their main objective assigned at the very begining.

# Technologies
* C#
* OOP
* .NET
* Unit Testing - NUnit

# Features
* Designed and implemented object-oriented game architecture (Player, NPCs, Creatures, Items)

* Developed room navigation system with interactive doors and locations

* Implemented item management, inventory system, and usable objects

* Created quest system with NPC dialogues and task progression

* Unit testing to validate core game mechanics and ensure code reliability

# Testing Overview

The project includes a robust unit testing suite built with **NUnit**, designed to verify the core mechanics of the text-based adventure. Below is a breakdown of the covered test classes:

*   **CommandHandlerTests**  
    Verifies the user input parsing and execution mechanisms.
    *   Ensures the `help` command correctly outputs the list of available instructions.
    *   Checks that unknown commands display an appropriate error message gracefully.
    *   Validates that input commands are case-insensitive (e.g., `Help`, `hElP`, and `HELP` are treated identically).
    *   Confirms proper error handling when multi-word commands (like `podnieś` / pick up) are executed without their required arguments.

*   **ItemTests**  
    Focuses on the data and behavior representation of in-game items.
    *   Verifies that the constructor correctly assigns the item's name and description, and defaults specific flags (like `IsAd`) to false.
    *   Validates that examining an item via the `Inspect()` method returns properly formatted text (`[Name]: [Description]`).
    *   Ensures configurable properties (like the `IsAd` flag) can be successfully modified.

*   **QuestTests**  
    Tests the logic for tracking and completing player objectives.
    *   Verifies the `CheckCompletion` logic by using conditional delegates.
    *   Ensures that a quest's status correctly changes to complete when the required condition is met.
    *   Confirms that the quest remains incomplete when the conditions are not satisfied.

*   **RoomTests**  
    Tests the generation of area descriptions and environmental elements.
    *   Ensures `Describe()` returns accurate baseline information (room name and atmospheric description) when a room is empty.
    *   Validates that if items are present in the room, they are properly appended to the room's description text (e.g., listing items residing on the floor).

# Running the project
Go into the main directory and run `dotnet run` in your console
or
open .sln file in vs code and run it

# Potential room for expanding
More advanced combat system, moving the game out of the console
