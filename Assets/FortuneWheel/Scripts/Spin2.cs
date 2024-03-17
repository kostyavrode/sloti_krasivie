using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Spin2 : MonoBehaviour
{
    public int controlledSide;
    public Button eagleButton;
    public Button tailsButton;
    [SerializeField] private SideController sideController;
    private bool _isStarted;
    private float[] _sectorsAngles;
    private float _finalAngle;
    private float _startAngle = 0;
    private float _currentLerpRotationTime;
    public Button TurnButton;
    public GameObject Circle; 			// Rotatable Object with rewards
    public Text CoinsDeltaText; 		// Pop-up text with wasted or rewarded coins amount
    public Text CurrentCoinsText; 		// Pop-up text with wasted or rewarded coins amount
    public int TurnCost = 100;			// How much coins user waste when turn whe wheel
    public int CurrentCoinsAmount;	// Started coins amount. In your project it can be set up from CoinsManager or from PlayerPrefs and so on
    public int PreviousCoinsAmount;		// For wasted coins animation

    private void Awake()
    {
        CurrentCoinsAmount = PlayerPrefs.GetInt("score");
        Debug.Log(CurrentCoinsAmount);
        PreviousCoinsAmount = CurrentCoinsAmount;
        CurrentCoinsText.text = CurrentCoinsAmount.ToString();
    }

    public void TurnWheel()
    {
        // Player has enough money to turn the wheel
        //if (CurrentCoinsAmount >= TurnCost)
        {
            _currentLerpRotationTime = 0f;

            // Fill the necessary angles (for example if you want to have 12 sectors you need to fill the angles with 30 degrees step)
            _sectorsAngles = new float[] { 180,360 };

            int fullCircles = 5;
            float randomFinalAngle = _sectorsAngles[UnityEngine.Random.Range(0, _sectorsAngles.Length)];

            // Here we set up how many circles our wheel should rotate before stop
            _finalAngle = -(fullCircles * 360 + randomFinalAngle);
            _isStarted = true;

            PreviousCoinsAmount = CurrentCoinsAmount;
            CurrentCoinsAmount -= TurnCost;
            StartCoroutine(HideCoinsDelta());
            StartCoroutine(UpdateCoinsAmount());
        }
    }
    void Update()
    {
        Debug.Log(_isStarted || CurrentCoinsAmount < TurnCost);
        // Make turn button non interactable if user has not enough money for the turn
        if (_isStarted )
        {
            TurnButton.interactable = false;
            TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
        }
        else
        {
            TurnButton.interactable = true;
            TurnButton.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        if (!_isStarted)
            return;

        float maxLerpRotationTime = 4f;

        // increment timer once per frame
        _currentLerpRotationTime += Time.deltaTime;
        if (_currentLerpRotationTime > maxLerpRotationTime || Circle.transform.eulerAngles.z == _finalAngle)
        {
            _currentLerpRotationTime = maxLerpRotationTime;
            _isStarted = false;
            _startAngle = _finalAngle % 360;

            //GiveAwardByAngle();
            StartCoroutine(HideCoinsDelta());
            if (controlledSide == sideController.CheckSide())
            {
                RewardCoins(TurnCost * 2);
            }
        }

        // Calculate current position using linear interpolation
        float t = _currentLerpRotationTime / maxLerpRotationTime;

        // This formulae allows to speed up at start and speed down at the end of rotation.
        // Try to change this values to customize the speed
        t = t * t * t * (t * (6f * t - 15f) + 10f);

        float angle = Mathf.Lerp(_startAngle, _finalAngle, t);
        Circle.transform.eulerAngles = new Vector3(0, angle, 0);
    }

    public void RewardCoins(int awardCoins)
    {
        CurrentCoinsAmount += awardCoins;
        CoinsDeltaText.text = "+" + awardCoins;
        CoinsDeltaText.gameObject.SetActive(true);
        SaveCoins();
        StartCoroutine(UpdateCoinsAmount());
    }
    private void SaveCoins()
    {
        PlayerPrefs.SetInt("score", CurrentCoinsAmount);
        PlayerPrefs.Save();
    }
    public void SetTails()
    {
        controlledSide = 1;
        tailsButton.gameObject.SetActive(false);
        eagleButton.gameObject.SetActive(true);
    }
    public void SetEagle()
    {
        controlledSide = 0;
        tailsButton.gameObject.SetActive(true);
        eagleButton.gameObject.SetActive(false);
    }

    private IEnumerator HideCoinsDelta()
    {
        yield return new WaitForSeconds(1f);
        CoinsDeltaText.gameObject.SetActive(false);
    }

    private IEnumerator UpdateCoinsAmount()
    {
        // Animation for increasing and decreasing of coins amount
        const float seconds = 0.5f;
        float elapsedTime = 0;

        while (elapsedTime < seconds)
        {
            CurrentCoinsText.text = Mathf.Floor(Mathf.Lerp(PreviousCoinsAmount, CurrentCoinsAmount, (elapsedTime / seconds))).ToString();
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        PreviousCoinsAmount = CurrentCoinsAmount;
        CurrentCoinsText.text = CurrentCoinsAmount.ToString();
    }
}
