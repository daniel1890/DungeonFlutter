import 'package:flutter/material.dart';
import 'api_service.dart';

class RegisterDialog {
  static Future<void> showRegisterAccountDialog(
    BuildContext context,
    ApiService apiService,
    Function(String, int) onRegisterSuccess,
  ) async {
    String playerName = '';
    String password = '';

    await showDialog(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Register Account'),
          content: Container(
            height: 150,
            child: Column(
              children: [
                TextField(
                  onChanged: (value) {
                    playerName = value;
                  },
                  decoration: const InputDecoration(labelText: 'Player Name'),
                ),
                TextField(
                  onChanged: (value) {
                    password = value;
                  },
                  obscureText: true,
                  decoration: const InputDecoration(labelText: 'Password'),
                ),
              ],
            ),
          ),
          actions: [
            TextButton(
              onPressed: () {
                Navigator.of(context).pop();
              },
              child: const Text('Cancel'),
            ),
            TextButton(
              onPressed: () async {
                if (playerName.isNotEmpty && password.isNotEmpty) {
                  Navigator.of(context).pop();
                  await _register(
                    context,
                    apiService,
                    playerName,
                    password,
                    onRegisterSuccess,
                  );
                } else {
                  throw Exception('Failed to register.');
                }
              },
              child: const Text('Register'),
            ),
          ],
        );
      },
    );
  }

  static Future<void> _register(
    BuildContext context,
    ApiService apiService,
    String playerName,
    String password,
    Function(String, int) onRegisterSuccess,
  ) async {
    try {
      final response = await apiService.registerAccount(playerName, password);
      final playerNameFromResponse = response['playerName'].toString();
      final playerIdFromResponse = response['id'].toInt();

      if (playerNameFromResponse != null) {
        onRegisterSuccess(playerNameFromResponse, playerIdFromResponse);
      } else {
        print('Failed handling username.');
      }
    } catch (e) {
      print('Error registering account: $e');
    }
  }
}
