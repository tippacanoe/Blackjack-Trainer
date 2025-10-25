using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private Button play;
    [SerializeField] private Button quit;
    [SerializeField] private Button tutorial;
    [SerializeField] private Button standard;
    [SerializeField] private Button strategy;
    [SerializeField] private Button cardCounting;
    [SerializeField] private Button back;
    [SerializeField] private TextMeshProUGUI tutorialText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        play.onClick.AddListener(() => showPlayMenu());
        quit.onClick.AddListener(() => Application.Quit());
        tutorial.onClick.AddListener(() => showTutorial());
        standard.onClick.AddListener(() => startStandardMode());
        strategy.onClick.AddListener(() => startStrategyMode());
        cardCounting.onClick.AddListener(() => startCardCountingMode());
        back.onClick.AddListener(() => showMainMenu());

        showMainMenu();
    } // Start

    private void showMainMenu()
    {
        play.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
        tutorial.gameObject.SetActive(true);
        standard.gameObject.SetActive(false);
        strategy.gameObject.SetActive(false);
        cardCounting.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
        tutorialText.gameObject.SetActive(false);
    } // showMainMenu

    private void showPlayMenu()
    {
        play.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        tutorial.gameObject.SetActive(false);
        standard.gameObject.SetActive(true);
        strategy.gameObject.SetActive(true);
        cardCounting.gameObject.SetActive(true);
        back.gameObject.SetActive(true);
        tutorialText.gameObject.SetActive(false);
    } // showPlayMenu

    private void showTutorial()
    {
        play.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        tutorial.gameObject.SetActive(false);
        standard.gameObject.SetActive(false);
        strategy.gameObject.SetActive(false);
        cardCounting.gameObject.SetActive(false);
        back.gameObject.SetActive(true);
        tutorialText.gameObject.SetActive(true);
    } // showTutorial

    // set standard mode then load the game
    private void startStandardMode()
    {
        ModeTracker.setStandardMode();
        SceneManager.LoadScene("BlackjackScene");
    } // startStandardMode

    // set strategy mode then load the game
    private void startStrategyMode()
    {
        ModeTracker.setStrategyMode();
        SceneManager.LoadScene("BlackjackScene");
    } // startStrategyMode

    // set card counting mode then load the game
    private void startCardCountingMode()
    {
        ModeTracker.setCountingMode();
        SceneManager.LoadScene("BlackjackScene");
    } // startCardCountingMode

} // MenuManager
