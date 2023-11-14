import 'package:flutter/material.dart';
import 'api_service.dart';

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
  ApiService apiService =
      ApiService('https://localhost:7258'); // Replace with your API base URL
  String gameContent = 'Your Game Content Here';

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Asteroid Game'),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Text(gameContent),
            ElevatedButton(
              onPressed: () async {
                await startGame();
              },
              child: const Text('Start Game'),
            ),
          ],
        ),
      ),
    );
  }

  Future<void> startGame() async {
    try {
      final response = await apiService.startGame(4, 4);
      setState(() {
        gameContent = 'Game started. World: ${response['board']}';
      });
      // Handle the response, update UI, etc.
    } catch (e) {
      setState(() {
        gameContent = 'Failed to start the game: $e';
      });
    }
  }
}
