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

  Future<Map<String, dynamic>> saveHighScore(int playerId, int score) async {
    final response = await http.post(
      Uri.parse('$baseUrl/api/game/savehighscore/$playerId?$score'),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    } else {
      throw Exception(
          'Failed to save highscore. Status code: ${response.statusCode}');
    }
  }

  Future<Map<String, dynamic>> registerAccount(
      String playerName, String password) async {
    final response = await http.post(
      Uri.parse('$baseUrl/api/account/register'),
      body: jsonEncode({
        "playerName": playerName,
        "password": password,
      }),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    } else {
      throw Exception(
          'Failed to register new account. Status code: ${response.statusCode}');
    }
  }

  Future<Map<String, dynamic>> login(
      String playerName, String password) async {
    final response = await http.post(
      Uri.parse('$baseUrl/api/account/login'),
      body: jsonEncode({
        "playerName": playerName,
        "password": password,
      }),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    } else {
      throw Exception(
          'Failed to login. Status code: ${response.statusCode}');
    }
  }
}
