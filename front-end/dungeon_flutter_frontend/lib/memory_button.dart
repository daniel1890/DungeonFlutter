import 'package:flutter/material.dart';

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
