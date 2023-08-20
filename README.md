# AIPathingTests
This project implements specific formations in unity using the built in navigation agents. The goal was to implement formations in a manner that allows for hundreds of units to shape the formations without losing a tonne of performance. I am looking into how to multithread the navigation agents to further improve performance

# Formations
The project currently supports 4 commands for the ai units.
1. Follow:
   Follow implements a constant following of the mouse pointer in the world
2. Surround:
   Sourround implements a circle formation that follows the mouse pointer and changes size based on the amount of units within the circle
3. Rectangle:
   The rectangle formation tries to deivide the amount of units between an equal amount of rows and columns in a rectangle formation, this rectangle has it's middle follow the mouse cursor as well
4. Cone:
   Cone allows the user to click in a direction after pressing the cone command, and then the units will create a cone in said direction coming from their target point, the cursor.


https://github.com/Faethryn/AIPathingTests/assets/97239542/feb38934-91ba-412f-ad66-36b27ab2a60c



https://github.com/Faethryn/AIPathingTests/assets/97239542/e0525260-8f78-4e4a-b7d9-b0eb1d7cad70

