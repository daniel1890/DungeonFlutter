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
// DONE 2. Add logout functionality
// DONE 3. Add real shuffle functionality to back-end
// DONE 4. Fix normal difficulty card turning
// DONE 5. Fix score calculation
// DONE 6. Add multiple levels adding 1 to n cards on x,y of cards 2d array
// 7. Refactor front-end so multiple games
// 8. Refactor back-end so multiple games
// DONE 9. Return player ID when log in
// DONE 10. Refactor DAOs so they implement an interface which gets injected in Program.cs
// 11. Add authentication to user login using JWT token