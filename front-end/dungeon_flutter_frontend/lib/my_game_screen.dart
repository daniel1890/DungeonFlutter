import 'package:flutter/material.dart';
import 'api_service.dart';
import 'memory_button.dart';
import 'register_dialog.dart';
import 'login_dialog.dart';

enum Difficulty { normal, hard }

class MyGameScreen extends StatefulWidget {
  const MyGameScreen({super.key});

  @override
  MyGameScreenState createState() => MyGameScreenState();
}

class MyGameScreenState extends State<MyGameScreen> {
  ApiService apiService = ApiService('https://localhost:7258');
  List<List<int>> board = [];
  List<List<bool>> revealed = [];
  List<int> selected = [];
  int tries = 3;
  int level = 1;
  bool gameStarted = false;
  Difficulty difficulty = Difficulty.normal;
  String loggedInPlayer = '';
  late int loggedInPlayerId;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: loggedInPlayer.isNotEmpty
            ? Text('Concentration Game - Logged in as: $loggedInPlayer')
            : const Text('Concentration Game'),
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
        const SizedBox(height: 16),
        ElevatedButton(
          onPressed: () {
            RegisterDialog.showRegisterAccountDialog(
              context,
              apiService,
              onRegisterSuccess,
            );
          },
          child: const Text('Register Account'),
        ),
        const SizedBox(height: 16),
        ElevatedButton(
          onPressed: () {
            LoginDialog.showLoginDialog(context, apiService, onLoginSuccess);
          },
          child: const Text('Login'),
        ),
        const SizedBox(height: 16),
        ElevatedButton(
          onPressed: () {
            _logout();
          },
          style: ElevatedButton.styleFrom(
            backgroundColor: Colors.red,
          ),
          child: const Text('Logout'),
        ),
        const SizedBox(height: 16),
        ElevatedButton(
          onPressed: () {
            _resetGame();
          },
          style: ElevatedButton.styleFrom(
            backgroundColor: Colors.red,
          ),
          child: const Text('Quit Game'),
        )
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
        Text('Current difficulty: $difficulty'),
        if (board.isNotEmpty) ...[
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

  void onLoginSuccess(String playerName, int playerId) {
    setState(() {
      loggedInPlayer = playerName;
      loggedInPlayerId = playerId;
    });
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Login'),
          content: Text(
            'You have succesfully logged in with the username: $playerName. And have id: $playerId',
          ),
          actions: [
            TextButton(
              onPressed: () {
                Navigator.of(context).pop();
                startGame();
                level++;
              },
              child: const Text('OK'),
            ),
          ],
        );
      },
    );
  }

  void onRegisterSuccess(String playerName, int playerId) {
    setState(() {
      loggedInPlayer = playerName;
      loggedInPlayerId = playerId;
    });
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Registration Successful'),
          content: Text(
            'You have successfully registered with the username: $playerName. And have received id: $playerId',
          ),
          actions: [
            TextButton(
              onPressed: () {
                Navigator.of(context).pop();
              },
              child: const Text('OK'),
            ),
          ],
        );
      },
    );
  }

  void _logout() {
    setState(() {
      loggedInPlayer = '';
      loggedInPlayerId = 0;
    });
  }

  Future<void> startGame() async {
    try {
      final response = level % 2 == 0
          ? await apiService.startGame(
              2 + ((level) * 2), 2 + ((level - 1) * 2))
          : await apiService.startGame(
              2 + ((level - 1) * 2), 2 + ((level) * 2));
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
    Future.delayed(const Duration(seconds: 1), () {
      setState(() {
        if (selected[0] == selected[1]) {
          selected = [];
        } else {
          if (difficulty == Difficulty.normal) {
            for (int i = 0; i < revealed.length; i++) {
              for (int j = 0; j < revealed[i].length; j++) {
                if (board[i][j] == selected[0] || board[i][j] == selected[1]) {
                  revealed[i][j] = false;
                }
              }
            }
          } else if (difficulty == Difficulty.hard) {
            revealed = List<List<bool>>.generate(
              board.length,
              (i) => List<bool>.filled(board[i].length, false),
            );
          }

          selected = [];
          tries--;

          if (tries == 0 || _allRevealed()) {
            _gameOver();
          }
        }
      });
    });

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
    int finalScore = boardScoreCasted - tries;
    if (tries == 0) {
      finalScore -= 5;
    }

    if (tries == 0) {
      _showGameOverDialog('Game Over!', 'You lose! Your score: $finalScore.');
    } else {
      _showGameOverDialog(
          'You win!', 'Congratulations! Your score: $finalScore.');
    }

    final response =
        await apiService.saveHighScore(loggedInPlayerId, finalScore);
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
            TextButton(
              onPressed: () {
                Navigator.of(context).pop();
                startGame();
                level++;
              },
              child: const Text('Next Level'),
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
