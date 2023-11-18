import 'package:flutter/material.dart';
import 'api_service.dart';
import 'memory_button.dart';
import 'register_dialog.dart';

enum Difficulty { normal, hard }

class MyGameScreen extends StatefulWidget {
  const MyGameScreen({super.key});

  @override
  _MyGameScreenState createState() => _MyGameScreenState();
}

class _MyGameScreenState extends State<MyGameScreen> {
  ApiService apiService = ApiService('https://localhost:7258');
  List<List<int>> board = [];
  List<List<bool>> revealed = [];
  List<int> selected = [];
  int tries = 3;
  bool gameStarted = false;
  Difficulty difficulty = Difficulty.normal; // Default difficulty

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Concentration Game'),
      ),
      body: Center(
        child: gameStarted ? _buildGameScreen() : _buildMainMenu(),
      ),
    );
  }

  Widget _buildMainMenu() {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: <Widget>[
        ElevatedButton(
          onPressed: () async {
            await startGame();
          },
          child: const Text('Start Game'),
        ),
        const SizedBox(height: 16), // Add vertical padding between buttons
        ElevatedButton(
          onPressed: () {
            // Add your logic for quitting the game
            _resetGame();
          },
          child: const Text('Quit Game'),
        ),
        const SizedBox(height: 16), // Add vertical padding between buttons
        ElevatedButton(
          onPressed: () {
            RegisterDialog.showRegisterAccountDialog(context, apiService);
          },
          child: const Text('Register Account'),
        ),
      ],
    );
  }

  Widget _buildGameScreen() {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: <Widget>[
        Text('Tries left: $tries'),
        ElevatedButton(
          onPressed: () {
            setState(() {
              difficulty = difficulty == Difficulty.normal
                  ? Difficulty.hard
                  : Difficulty.normal;
            });
          },
          child: const Text('Switch Difficulty'),
        ),
        if (board.isNotEmpty) ...[
          // Display the game grid
          for (int i = 0; i < board.length; i++)
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                for (int j = 0; j < board[i].length; j++)
                  MemoryButton(
                    value: board[i][j],
                    revealed: revealed[i][j],
                    onPressed: () => _onCardPressed(i, j),
                  ),
              ],
            ),
        ],
      ],
    );
  }

  Future<void> startGame() async {
    try {
      final response = await apiService.startGame(4, 4);
      print(response['board']);
      var extractedBoard = List<List<int>>.from(
        (response['board'] as List<dynamic>).map(
          (row) => List<int>.from(row),
        ),
      );

      setState(() {
        board = extractedBoard;
        revealed = List<List<bool>>.generate(
          board.length,
          (i) => List<bool>.filled(board[i].length, false),
        );
        selected = [];
        tries = 3;
        gameStarted = true;
      });
    } catch (e) {
      print('Error starting the game: $e');
      setState(() {
        board = [];
        revealed = [];
        selected = [];
        tries = 3;
        gameStarted = true;
      });
    }
  }

  void _onCardPressed(int row, int column) {
    if (!revealed[row][column] && selected.length < 2) {
      setState(() {
        revealed[row][column] = true;
        selected.add(board[row][column]);
      });

      if (selected.length == 2) {
        _checkForMatch();
      }
    }
  }

  void _checkForMatch() {
    if (selected[0] == selected[1]) {
      // Match found
      setState(() {
        selected = [];
      });
    } else {
      setState(() {
        if (difficulty == Difficulty.normal) {
          // For normal difficulty, only hide the selected cards
          revealed[selected[0] ~/ board.length][selected[0] % board.length] =
              false;
          revealed[selected[1] ~/ board.length][selected[1] % board.length] =
              false;
        } else if (difficulty == Difficulty.hard) {
          // For hard difficulty, hide all cards
          revealed = List<List<bool>>.generate(
            board.length,
            (i) => List<bool>.filled(board[i].length, false),
          );
        }

        selected = [];
        tries--;
      });
    }

    if (tries == 0 || _allRevealed()) {
      _gameOver();
    }
  }

  bool _allRevealed() {
    for (int i = 0; i < revealed.length; i++) {
      for (int j = 0; j < revealed[i].length; j++) {
        if (!revealed[i][j]) {
          return false;
        }
      }
    }

    return true;
  }

  void _gameOver() async {
    double boardScore =
        (((board.length.toDouble()) * (board.length.toDouble())) / 2.0);
    int boardScoreCasted = boardScore.toInt();

    if (tries == 0) {
      _showGameOverDialog(
          'Game Over!', 'You lose! Your score: $boardScoreCasted.');
    } else {
      _showGameOverDialog(
          'You win!', 'Congratulations! Your score: $boardScoreCasted.');
    }

    final response =
        await apiService.saveHighScore(1, boardScoreCasted + tries);
    print(response['highscore']);
  }

  void _showGameOverDialog(String title, String message) {
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: Text(
            title,
            style: const TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
          ),
          content: Text(
            message,
            style: const TextStyle(fontSize: 18),
          ),
          actions: [
            TextButton(
              onPressed: () {
                Navigator.of(context).pop();
                _resetGame();
              },
              child: const Text('Return to Main Menu'),
            ),
          ],
        );
      },
    );
  }

  void _resetGame() {
    setState(() {
      board = [];
      revealed = [];
      selected = [];
      tries = 3;
      gameStarted = false;
    });
  }
}
