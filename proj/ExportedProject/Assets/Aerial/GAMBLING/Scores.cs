using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class Scores : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI t1;
    [SerializeField] private TextMeshProUGUI t2;
    [SerializeField] private TextMeshProUGUI t3;
    [SerializeField] private TextMeshProUGUI wonText;
    [SerializeField] private TextMeshProUGUI totalText;
    [SerializeField] private TextMeshProUGUI betText;

    [SerializeField] private Button increaseBetButton;
    [SerializeField] private Button decreaseBetButton;

    [SerializeField] private Button rollButton;

    private int num1, num2, num3;
    private int pairmult = 10;
    private int trimult = 30;
    private int units = 0;
    private int bet = 10;

    private string folderPath;
    private string filePath;

    void Awake()
    {
        folderPath = Path.Combine(Application.dataPath, "..", "Aerial");
        filePath = Path.Combine(folderPath, "Gcore.var");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "100");
            units = 100;
        }
        else
        {
            string content = File.ReadAllText(filePath);
            int.TryParse(content, out units);
        }
        bet = Mathf.Clamp(bet, 5, units);

        // Assign button events
        if (increaseBetButton != null)
            increaseBetButton.onClick.AddListener(IncreaseBet);
        if (decreaseBetButton != null)
            decreaseBetButton.onClick.AddListener(DecreaseBet);
        if (rollButton != null)
            rollButton.onClick.AddListener(Roll);

        UpdateUI();
    }

    public void AddChips(float amount)
    {
        folderPath = Path.Combine(Application.dataPath, "..", "Aerial");
        filePath = Path.Combine(folderPath, "Gcore.var");

        int addAmount = Mathf.RoundToInt(amount);
        if (addAmount > 0)
        {
            string unitsString = File.ReadAllText(filePath);
            int units = int.TryParse(unitsString, out int parsedUnits) ? parsedUnits : 0;
            units += addAmount;
            File.WriteAllText(filePath, units.ToString());
        }
        else
        {
            Debug.LogWarning("AddChips called with non-positive amount: " + amount);
        }

    }

    public void IncreaseBet()
    {
        bet += 5;
        bet = Mathf.Clamp(bet, 5, units);
        UpdateUI();
    }

    public void DecreaseBet()
    {
        bet -= 5;
        bet = Mathf.Clamp(bet, 5, units);
        UpdateUI();
    }

    public static void Sleep(MonoBehaviour runner, float seconds)
    {
        IEnumerator DelayedAction(float delay)
        {
            yield return new WaitForSeconds(delay);
        }

        if (seconds < 0)
        {
            Debug.LogWarning("Sleep called with negative seconds: " + seconds);
            return;
        }
        runner.StartCoroutine(DelayedAction(seconds));
    }

    public void Roll()
    {
        if (File.Exists(filePath))
        {
            string content = File.ReadAllText(filePath);
            int.TryParse(content, out units);
        }

        if (units < bet)
        {
            wonText.text = "Not enough units to bet!";
            return;
        }

        units -= bet;

        for (int i = 0; i < 10; i++)
        {
            num1 = UnityEngine.Random.Range(0, 9);
            num2 = UnityEngine.Random.Range(0, 9);
            num3 = UnityEngine.Random.Range(0, 9);
            t1.text = num1.ToString();
            t2.text = num2.ToString();
            t3.text = num3.ToString();
            StartCoroutine(DelayedAction(3f));
        }

        IEnumerator DelayedAction(float delay)
        {
            yield return new WaitForSeconds(delay);
        }

        int addscore = 0;

        if (num1 == 7 && num2 == 2 && num3 == 7)
        {
            addscore = 1000;
            wonText.text = "Won:  1000, Jackpot!";
        }
        else if (num1 == num2 && num1 == num3)
        {
            int tri = num1 * trimult + bet + 100;
            addscore += tri;
            wonText.text = "Won:  " + tri + ", Three of a kind!";
        }
        else if (num1 == num3)
        {
            int n1n3 = num1 * pairmult + bet;
            addscore += n1n3;
            wonText.text = "Won:  " + n1n3 + ", Pair!";
        }
        else if (num2 == num3)
        {
            int n2n3 = num2 * pairmult + bet;
            addscore += n2n3;
            wonText.text = "Won:  " + n2n3 + ", Pair!";
        }
        else if (num1 == num2)
        {
            int n1n2 = num1 * pairmult + bet;
            addscore += n1n2;
            wonText.text = "Won:  " + n1n2 + ", Pair!";
        }
        else
        {
            addscore = 0;
            wonText.text = "Won:  0, No match!";
        }

        units += addscore;
        File.WriteAllText(filePath, units.ToString());
        bet = Mathf.Clamp(bet, 5, units);
        UpdateUI();
    }

    private void UpdateUI()
    {
        totalText.text = "Units:  " + units;
        betText.text = "Bet: " + bet;
    }
}
