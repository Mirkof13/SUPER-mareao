using UnityEngine;
using TMPro;

public class Title : MonoBehaviour
{
    private void Start()
    {
        var controlsText = GameObject.Find("ControlsText");
        if (controlsText != null)
        {
            var tmp = controlsText.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
                tmp.text = "Evaluacion Continua:\n\nPROGRAMACION GRAFICA\nY MULTIMEDIA I\n\nMirkof Guzman";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.NewGame();
        }
    }
}