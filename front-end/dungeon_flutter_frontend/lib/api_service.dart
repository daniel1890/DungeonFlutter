import 'dart:convert';
import 'package:http/http.dart' as http;

class ApiService {
  final String baseUrl;

  ApiService(this.baseUrl);

  Future<Map<String, dynamic>> startGame(int rows, int columns) async {
    final response = await http.post(
      Uri.parse('$baseUrl/api/game/start/$rows/$columns'),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    } else {
      throw Exception(
          'Failed to start the game. Status code: ${response.statusCode}');
    }
  }

  Future<Map<String, dynamic>> saveHighScore(String player, int score) async {
    final response = await http.post(
      Uri.parse('$baseUrl/api/game/savehighscore/$player?$score'),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    } else {
      throw Exception(
          'Failed to save highscore. Status code: ${response.statusCode}');
    }
  }
}
