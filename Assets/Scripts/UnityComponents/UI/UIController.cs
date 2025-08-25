using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Singleton;

    [SerializeField]
    private Image _healthBar;

    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private Button _retryButton;

    private void Awake()
    {
        _retryButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        EnableGameOverPanel(false);

        Singleton = this;
    }

    public void SetHealthRatio(float ratio)
    {
        _healthBar.fillAmount = ratio;
    }

    public void EnableGameOverPanel(bool enabled)
    {
        _gameOverPanel.SetActive(enabled);
    }


}
