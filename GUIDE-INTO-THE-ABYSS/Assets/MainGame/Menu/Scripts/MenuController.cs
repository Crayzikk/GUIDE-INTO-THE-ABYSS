using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    private Button playButton;
    private Button settingsButton;
    private Button exitButton;

    private void OnEnable()
    {
        // Отримуємо UI документ
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Зв'язуємо кнопки
        playButton = root.Q<Button>("Play");
        settingsButton = root.Q<Button>("settings");
        exitButton = root.Q<Button>("exit");

        // Прив'язуємо обробники подій
        playButton.clicked += OnPlayClicked;
        settingsButton.clicked += OnSettingsClicked;
        exitButton.clicked += OnExitClicked;
    }

    private void OnDisable()
    {
        // Відписуємо обробники подій
        playButton.clicked -= OnPlayClicked;
        settingsButton.clicked -= OnSettingsClicked;
        exitButton.clicked -= OnExitClicked;
    }

    private void OnPlayClicked()
    {
        Debug.Log("Play button clicked!");
        // Додайте логіку для початку гри
    }

    private void OnSettingsClicked()
    {
        Debug.Log("Settings button clicked!");
        // Відкрийте меню налаштувань
    }

    private void OnExitClicked()
    {
        Debug.Log("Exit button clicked!");
        Application.Quit(); // Завершення програми
    }
}
