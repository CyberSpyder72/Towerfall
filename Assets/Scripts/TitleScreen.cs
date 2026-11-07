using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public GameObject title;
    public GameObject howToPlay; 
    public GameObject first;
    public GameObject second;

    public GameObject startGame;
    public GameObject next;
    public GameObject back;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("Main");
    }
    public void HowToPlay()
    {
        title.SetActive(false);
        howToPlay.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(next);
    }
    public void Quit()
    {
        Debug.Log("quitting");
        Application.Quit();
    }
    public void Done()
    {
        title.SetActive(true);
        howToPlay.SetActive(false);
        first.SetActive(true);
        second.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(startGame);
    }
    public void Back()
    {
        first.SetActive(true);
        second.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(next);
    }
    public void Next()
    {
        first.SetActive(false);
        second.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(back);
    }
}
