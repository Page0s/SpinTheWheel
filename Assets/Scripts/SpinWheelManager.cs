using UnityEngine;
using UnityEngine.UI;

public class SpinWheelManager : MonoBehaviour
{
    // This integer will be shown as a slider,
    // with the range of 1 to 6 in the Inspector
    [Range(3, 4)]
    [SerializeField] float speed = 3f;

    GameObject circle;
    Text _scoreText;
    bool _isStarted;
    float[] _sectorsAngles;
    float _currentLerpRotationTime;
    float _startAngle = 0;
    float _finalAngle;
    float _clockwiseResult = -1f;
    int _score = 0;
    int fullCircles;
    string _spinWheelName = "SpinWheel";
    string _scoreTextName = "ScoreText";
    string _currency = "$";

    private void Awake()
    {
        circle = GameObject.Find(_spinWheelName);
        _scoreText = GameObject.Find(_scoreTextName).GetComponent<Text>();
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isStarted)
        {
            float maxLerpRotationTime = 4f;

            // increment timer once per frame
            _currentLerpRotationTime += Time.deltaTime;
            if (_currentLerpRotationTime > maxLerpRotationTime || circle.transform.eulerAngles.z == _finalAngle)
            {
                _currentLerpRotationTime = maxLerpRotationTime;
                _isStarted = false;
                _startAngle = _finalAngle % 360;

                GiveAwardByAngle();
            }

            // Calculate current position using linear interpolation
            double step = _currentLerpRotationTime / maxLerpRotationTime;
            // This formulae allows to speed up at start and speed down at the end of rotation.
            step = step * step * step * (step * (6f * step - 15f) + 10f);

            float angle = Mathf.Lerp(_startAngle, _finalAngle, (float) step);
            circle.transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    public void TurnWheel()
    {
        _currentLerpRotationTime = 0f;

        // Necessary angles (for example: 12 sectors are angles with 30 degrees step)
        _sectorsAngles = new float[] { 15, 30, 45, 60, 75, 90, 105, 120, 135, 150, 165, 180, 195, 210, 225, 240, 255, 270, 285, 300, 315, 330, 345, 360 };

        // Here we set up how many circles our wheel should rotate before stop, based on speed
        if (speed < 3.2f)
        {
            fullCircles = 1;
        }
        else if (speed >= 3.2f && speed < 3.4f)
        {
            fullCircles = 2;
        }
        else if (speed >= 3.4f && speed < 3.6f)
        {
            fullCircles = 3;
        }
        else if (speed >= 3.6f && speed < 3.8f)
        {
            fullCircles = 4;
        }
        else if (speed >= 3.8f)
        {
            fullCircles = 5;
        }

            float randomFinalAngle = _sectorsAngles[Random.Range(0, _sectorsAngles.Length)];
        // Here we set up how many circles + random angle wheel should rotate before stop
        // spin clockwise -(fullCircles * 360 + randomFinalAngle);
        // spin counder-clockwise +(fullCircles * 360 + randomFinalAngle);
        _finalAngle = -(fullCircles * 360 + randomFinalAngle);
        _isStarted = true;
    }

    private void GiveAwardByAngle()
    {
        // Here you can set up rewards for every sector of wheel
        switch ((int)_startAngle * _clockwiseResult)
        {
            // “Lose a turn”
            case 0:
                RewardPlayer(0);
                break;
            // “Lose a turn”
            case 360:
                RewardPlayer(0);
                break;
            case 345:
                RewardPlayer(800);
                break;
            case 330:
                RewardPlayer(500);
                break;
            case 315:
                RewardPlayer(650);
                break;
            case 300:
                RewardPlayer(500);
                break;
            case 285:
                RewardPlayer(900);
                break;
            // Bankrupt
            case 270:
                RewardPlayer(true);
                break;
            case 255:
                RewardPlayer(5000);
                break;
            case 240:
                RewardPlayer(500);
                break;
            case 225:
                RewardPlayer(600);
                break;
            case 210:
                RewardPlayer(700);
                break;
            case 195:
                RewardPlayer(600);
                break;
            case 180:
                RewardPlayer(650);
                break;
            case 165:
                RewardPlayer(500);
                break;
            case 150:
                RewardPlayer(700);
                break;
            case 135:
                RewardPlayer(500);
                break;
            case 120:
                RewardPlayer(600);
                break;
            case 105:
                RewardPlayer(550);
                break;
            case 90:
                RewardPlayer(500);
                break;
            case 75:
                RewardPlayer(600);
                break;
            // Bankrupt
            case 60:
                RewardPlayer(true);
                break;
            case 45:
                RewardPlayer(650);
                break;
            // “Free Play”
            case 30:
                RewardPlayer(0);
                break;
            case 15:
                RewardPlayer(700);
                break;
            default:
                RewardPlayer(10);
                break;
        }
    }

    void RewardPlayer(int amount)
    {
        _score += amount;
        UpdateText();
        Debug.Log($"Here you go pal! +{amount}");
        Debug.Log($"You have = {_score}$");
    }

    void RewardPlayer(bool isBanckrupt)
    {
        if(isBanckrupt)
            _score = 0;
        UpdateText();
        Debug.Log($"Bankrupt!");
        Debug.Log($"You have = {_score}$");
    }

    void UpdateText()
    {
        _scoreText.text = _score.ToString() + _currency;
    }
}
