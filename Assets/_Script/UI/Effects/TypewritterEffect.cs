using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypewritterEffect : MonoBehaviour
{
    [SerializeField] float typingSpeed = 0.1f; // Speed of typing effect
    [SerializeField] List<string> messages; // List of messages to display
    [SerializeField] TextMeshPro textMesh;
    
    string _currentMessages;
    int _currentMessagesIndex = 0;
    int _currentCharIndex = 0;
    float timer = 1f;
    float timerPerMessage = 2f;
    bool _isTyping = false;

    // Start is called before the first frame update
    void Start()
    {
        _currentMessages = messages[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTyping)
        {
            timer -= Time.deltaTime;

            if (_currentMessagesIndex >= messages.Count)
            {
                _isTyping = false;
                GameManager.Instance.SetGameState("Playing");
                return;
            }

            if (_currentCharIndex < _currentMessages.Length)
            {
                if (timer <= 0)
                {
                    timer += typingSpeed;
                    _currentCharIndex++;
                    textMesh.text = _currentMessages.Substring(0, _currentCharIndex);
                }
            }
            else
            {
                timerPerMessage -= Time.deltaTime;
                if (timerPerMessage <= 0)
                {
                    _currentMessagesIndex++;
                    if (_currentMessagesIndex < messages.Count)
                    {
                        _currentMessages = messages[_currentMessagesIndex];
                        _currentCharIndex = 0;
                        timerPerMessage = 2f; // Reset timer for the next message
                    }
                }
            }
        }
    }

    public void StartTyping()
    {
        _isTyping = true;
    }
}
