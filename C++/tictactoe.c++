#include <iostream>
using namespace std;

// grid[0] is just a placeholder
char grid[10] = {'?','1','2','3','4','5','6','7','8','9'};
bool random = true;

int checkStatus();
int computerMove();
void drawBoard();

int main()
{
    int player = 1, i, choice = -1;
    char mark;

    do
    {
        drawBoard();

        if(player == 1)
        {
            cout << "Player, please enter a number: ";
            cin >> choice;
        }
        else
        {
            choice = computerMove();
            cout << "Computer marked grid " << choice << "." << endl;
        }
        cin.get();

        // If player 1's turn, mark grid with an 'X', else 'O' (Computer)
        mark = (player == 1) ? 'X' : 'O';

        if (choice == 1 && grid[1] == '1')
            grid[1] = mark;
        else if (choice == 2 && grid[2] == '2')
            grid[2] = mark;
        else if (choice == 3 && grid[3] == '3')
            grid[3] = mark;
        else if (choice == 4 && grid[4] == '4')
            grid[4] = mark;
        else if (choice == 5 && grid[5] == '5')
            grid[5] = mark;
        else if (choice == 6 && grid[6] == '6')
            grid[6] = mark;
        else if (choice == 7 && grid[7] == '7')
            grid[7] = mark;
        else if (choice == 8 && grid[8] == '8')
            grid[8] = mark;
        else if (choice == 9 && grid[9] == '9')
            grid[9] = mark;
        else
        {
            if(!(choice >= 1 && choice <= 9))
                break;

            cout << "Invalid move";

            player--;
            cin.ignore();
        }
        i = checkStatus();

        player = ((player+1)%2) ? 1:2;

    } while(i==-1);

    drawBoard();

    if(i == 1)
    {
        if(player == 2)
            cout << "==>\aPlayer wins!";
        else
            cout << "==>\aComputer wins!";
    }
    else if(i == 0)
        cout << "==>\aGame draw";
    else
        cout << "Please restart the game!"<< endl;

    cin.ignore();
    return 0;
}

void drawBoard()
{
    system("cls");
    cout << "\n\tTic Tac Toe\n\n";

    cout << "Player 1 (X)  ---  Computer (O)" << endl << endl;
    cout << endl;

    cout << "     |     |     " << endl;
    cout << "  " << grid[1] << "  |  " << grid[2] << "  |  " << grid[3] << endl;

    cout << "_____|_____|_____" << endl;
    cout << "     |     |     " << endl;

    cout << "  " << grid[4] << "  |  " << grid[5] << "  |  " << grid[6] << endl;

    cout << "_____|_____|_____" << endl;
    cout << "     |     |     " << endl;

    cout << "  " << grid[7] << "  |  " << grid[8] << "  |  " << grid[9] << endl;

    cout << "     |     |     " << endl << endl;
}

int checkStatus()
{
    // Win Conditions - Straight
    if (grid[1] == grid[2] && grid[2] == grid[3])
        return 1;
    else if (grid[4] == grid[5] && grid[5] == grid[6])
        return 1;
    else if (grid[7] == grid[8] && grid[8] == grid[9])
        return 1;

    // Win Conditions - Diagonals
    else if (grid[1] == grid[4] && grid[4] == grid[7])
        return 1;
    else if (grid[2] == grid[5] && grid[5] == grid[8])
        return 1;
    else if (grid[3] == grid[6] && grid[6] == grid[9])
        return 1;
    else if (grid[1] == grid[5] && grid[5] == grid[9])
        return 1;
    else if (grid[3] == grid[5] && grid[5] == grid[7])
        return 1;

    // Draw Condition
    else if (grid[1] != '1' && grid[2] != '2' && grid[3] != '3' 
            && grid[4] != '4' && grid[5] != '5' && grid[6] != '6' 
            && grid[7] != '7' && grid[8] != '8' && grid[9] != '9')
        return 0;

    // Continue game
    else
        return -1;
}

int computerMove()
{
    /*
    int i = -1;

    if(random)
    {
        i = 1 + rand() % 9;
        if(grid[i] != 'X')
        {
            random = false;
            return i;
        }
        return computerMove();
    }
    */

    int i = 1 + rand() % 9;
    if(grid[i] != 'X')
        return i;
    return computerMove();
}
