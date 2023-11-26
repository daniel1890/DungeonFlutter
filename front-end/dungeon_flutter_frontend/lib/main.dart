import 'package:flutter/material.dart';
import 'my_game_screen.dart';

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

// TODOS: 
// DONE 1. Add DAO layer to back-end
// 2. Add logout functionality
// 3. Add real shuffle functionality to back-end
// 4. Fix normal difficulty card turning
// 5. Fix score calculation
// 6. Add multiple levels adding 1 to n cards on x,y of cards 2d array
// 7. Refactor front-end so multiple games
// 8. Refactor back-end so multiple games