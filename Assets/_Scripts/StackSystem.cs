using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;


public class StackSystem : MonoBehaviour
{
    public static StackSystem instance;
    public float movementDelay = 0.25f, finishDelay, m_scaleY;

    public int moneyValue = 0, totalMoney, level, newMoneyValue;
    public TextMeshProUGUI moneyValueText;

    public List<GameObject> moneys = new List<GameObject>();
    public ParticleSystem cashParticle;
    public enum GameState { playing, finished }
    public GameState gameState;
    private bool finish = true;
    Animator playerAnimator;
    public GameObject playerPrefab, cashPrefab;
    [SerializeField]
    private Text totalMoneyText, totalMoneyTextGame, totalMoneyTextFinish, finishButtonText;
    BoxCollider m_collider;

    private void Awake()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        if (instance == null)
        {
            instance = this;
            moneyValue = 0;
            gameState = GameState.playing;
        }
        totalMoney = FileManager.LoadScore();
        totalMoneyText.text = totalMoney.ToString();
        totalMoneyTextGame.text = totalMoney.ToString();
        totalMoneyTextFinish.text = totalMoney.ToString();
    }

    private void Update()
    {
        if (gameState == GameState.playing)
        {
            MoveListElements();
        }
        if (gameState == GameState.finished && finish)
        {
            //print("update");
            finish = false;
            Invoke("Finished", 0.5f);
        }
        totalMoney = FileManager.LoadScore();
        totalMoneyText.text = totalMoney.ToString();
        totalMoneyTextGame.text = totalMoney.ToString();
        totalMoneyTextFinish.text = totalMoney.ToString();
        finishButtonText.text = newMoneyValue.ToString();
    }

    public void Finished()
    {
        playerAnimator.enabled = true;
        Camera.main.transform.parent = null;
        playerAnimator.SetTrigger("finish");
        playerPrefab.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        StartCoroutine(GameFinished());
        totalMoneyText.text = totalMoney.ToString();
        totalMoneyTextGame.text = totalMoney.ToString();
        totalMoneyTextFinish.text = totalMoney.ToString();
    }
    public void StackMoney(GameObject other, int index)
    {
        moneys.Add(other);
        StartCoroutine(DoBigger());
    }

    private IEnumerator DoBigger()
    {
        for (int i = moneys.Count - 1; i > 0; i--)
        {
            int index = i;
            Vector3 scale = new Vector3(1, 1, 1);
            scale *= 1.5f;

            moneys[index].transform.DOScale(scale, 0.1f).OnComplete(() =>
            moneys[index].transform.DOScale(new Vector3(1, 1, 1), 0.1f));
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void MoveListElements()
    {
        for (int i = 1; i < moneys.Count; i++)
        {
            Vector3 position = Vector3.zero;
            if (i == 1)
            {
                position = transform.GetChild(0).transform.position;
                position.y += 0.4f;
            }
            else
            {
                position = moneys[i - 1].transform.position;
            }

            position.z += 1.70f;
            moneys[i].transform.DOLocalMoveX(position.x, movementDelay);
            moneys[i].transform.DOLocalMoveY(position.y, movementDelay);
            moneys[i].transform.DOLocalMoveZ(position.z, movementDelay);
        }
    }
    public void DistributeCollectibles(GameObject other, int index, GameObject obstacle)
    {
        if (index == 0)
        {
            index = 1;
        }
        for (int i = moneys.Count - 1; i > index - 1; i--)
        {
            GameObject gameObject = moneys[i];
            moneys.Remove(gameObject);
            Destroy(gameObject.GetComponent<Collision>());
            Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            Vector3 target = new Vector3(Random.Range(-3.5f, 3.5f), 2.35f, obstacle.transform.position.z + Random.Range(2, 10));
            Vector3 targetUpPos = target - -new Vector3(0, 0, (target.z - gameObject.transform.position.z) / 2);
            gameObject.transform.DOMove(targetUpPos, 0.5f).OnComplete(() =>
            {
                gameObject.transform.DOMove(targetUpPos, 0.5f);
            });
            if (gameObject.tag == "Collected_Money")
            {
                moneyValue--;
                moneyValueText.text = moneyValue.ToString();
                gameObject.tag = "Money";
            }
            else if (gameObject.tag == "Collected_Gold")
            {
                moneyValue -= 2;
                moneyValueText.text = moneyValue.ToString();
                gameObject.tag = "Gold";
            }
            else if (gameObject.tag == "Collected_Diamond")
            {
                moneyValue -= 3;
                moneyValueText.text = moneyValue.ToString();
                gameObject.tag = "Diamond";
            }
        }

    }
    public void DestroyMoney(GameObject other, GameObject obstacle)
    {
        Instantiate(cashParticle, obstacle.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        if (other.gameObject.tag == ("Collected_Money"))
        {
            Destroy(other.gameObject);
            moneys.Remove(other.gameObject);
            moneyValue--;
            moneyValueText.text = moneyValue.ToString();
        }
        else if (other.gameObject.tag == ("Collected_Gold"))
        {
            Destroy(other.gameObject);
            moneys.Remove(other.gameObject);
            moneyValue -= 2;
            moneyValueText.text = moneyValue.ToString();
        }
        else if (other.gameObject.tag == ("Collected_Diamond"))
        {
            Destroy(other.gameObject);
            moneys.Remove(other.gameObject);
            moneyValue -= 3;
            moneyValueText.text = moneyValue.ToString();
        }
    }
    IEnumerator GameFinished()
    {
        //print("gamefinished");
        playerPrefab.transform.parent = null;
        m_collider = playerPrefab.GetComponent<BoxCollider>();
        m_collider.center = new Vector3(0.003674507f, 0.9f, -0.01469755f);
        for (int i = 0; i < moneyValue; i++)
        {
            GameObject go = Instantiate(cashPrefab, new Vector3(0, 2.35f + (i * 0.75f), 95f), Quaternion.Euler(new Vector3(0, 0, 0)));
            if (i == moneyValue - 1)
            {
                go.tag = "LastMoney";
            }
            else
            {
                go.tag = "Untagged";
            }
            playerPrefab.transform.position = new Vector3(0, go.transform.position.y - 0.75f, 94.5f);
            yield return new WaitForSeconds(finishDelay);
        }
        totalMoney += newMoneyValue;
        FileManager.SaveScore(totalMoney);
    }
}


