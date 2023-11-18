import 'package:flutter/material.dart';
import 'api_service.dart';

class RegisterDialog {
  static Future<void> showRegisterAccountDialog(
    BuildContext context,
    ApiService apiService,
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
                  await apiService.registerAccount(playerName, password);
                } else {
                  // Handle invalid input
                }
              },
              child: const Text('Register'),
            ),
          ],
        );
      },
    );
  }
}
