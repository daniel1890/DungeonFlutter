import 'package:flutter/material.dart';
import 'api_service.dart';
import 'dart:developer'; //(auto import will do this even)

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      home: MyGameScreen(),
    );
  }
}

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

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Concentration Game'),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Text('Tries left: $tries'),
            ElevatedButton(
              onPressed: gameStarted
                  ? null
                  : () async {
                      await startGame();
                    },
              child: const Text('Start Game'),
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
        ),
      ),
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
      // No match, hide the cards
      setState(() {
        revealed = List<List<bool>>.generate(
          board.length,
          (i) => List<bool>.filled(board[i].length, false),
        );
        selected = [];
        tries--;
      });

      if (tries == 0) {
        // Game over logic
        _gameOver();
      }
    }
  }

  void _gameOver() {
    // Add game over logic here
    print('Game over!');
  }
}

class MemoryButton extends StatelessWidget {
  final int value;
  final bool revealed;
  final VoidCallback onPressed;

  const MemoryButton({
    Key? key,
    required this.value,
    required this.revealed,
    required this.onPressed,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(8.0),
      child: ElevatedButton(
        onPressed: revealed ? null : onPressed,
        style: ElevatedButton.styleFrom(
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(10.0),
          ),
          fixedSize: const Size(75, 75), // Set your desired width and height
          padding: const EdgeInsets.all(16),
          backgroundColor:
              revealed ? Colors.grey : Colors.blue.withOpacity(0.8),
        ),
        child: Center(
          child: Text(
            revealed ? value.toString() : '♠️',
            style: const TextStyle(fontSize: 36, fontFamily: 'CardFont'),
          ),
        ),
      ),
    );
  }
}
