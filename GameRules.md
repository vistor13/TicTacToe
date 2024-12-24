# **Rules of Tic-Tac-Toe**

## **Game Overview**
Tic-Tac-Toe is a two-player game played on a 3x3 grid. One player marks their moves with "X," and the other marks theirs with "O." Players take turns making moves, aiming to align three of their marks horizontally, vertically, or diagonally to win.

---

## **Rules for Valid Moves**
1. **Turn Order**: Players alternate turns. Player 1 always starts with "X."
2. **Marking a Cell**:
   - A move is valid if the chosen cell is empty.
   - Players cannot overwrite any already marked cells.
3. **Grid Boundaries**: A move is valid only if it is within the grid's boundaries (cell indices must range from `(0, 0)` to `(2, 2)`).

---

## **Win Conditions**
A player wins if they create a line of three of their marks in one of the following configurations:

### **1. Horizontal Lines**
- Top row: `(0, 0)`, `(0, 1)`, `(0, 2)`
- Middle row: `(1, 0)`, `(1, 1)`, `(1, 2)`
- Bottom row: `(2, 0)`, `(2, 1)`, `(2, 2)`

### **2. Vertical Lines**
- Left column: `(0, 0)`, `(1, 0)`, `(2, 0)`
- Middle column: `(0, 1)`, `(1, 1)`, `(2, 1)`
- Right column: `(0, 2)`, `(1, 2)`, `(2, 2)`

### **3. Diagonal Lines**
- Main diagonal: `(0, 0)`, `(1, 1)`, `(2, 2)`
- Secondary diagonal: `(0, 2)`, `(1, 1)`, `(2, 0)`

---

## **Draw Conditions**
1. The game ends in a draw if all cells are filled and no player has met any win conditions.
2. The draw is declared after the 9th move if there is no winner.

---

## **Additional Notes**
- After each move, the game must check if any win or draw conditions have been met.
- Invalid moves (out of bounds or in already occupied cells) should prompt the player to try again.
