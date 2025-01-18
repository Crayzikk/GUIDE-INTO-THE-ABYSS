using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    private Button playButton;
    private Button settingsButton;
    private Button exitButton;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        playButton = root.Q<Button>("Play");
        settingsButton = root.Q<Button>("settings");
        exitButton = root.Q<Button>("exit");

        playButton.clicked += OnPlayClicked;
        settingsButton.clicked += OnSettingsClicked;
        exitButton.clicked += OnExitClicked;

        // Плануємо зміну UI після завершення поточного циклу макету
        root.schedule.Execute(() => 
        {
            Debug.Log("UI elements are now ready for modification.");
        }).ExecuteLater(10); // Виконується через кілька мілісекунд
    }

    private void OnDisable()
    {
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
