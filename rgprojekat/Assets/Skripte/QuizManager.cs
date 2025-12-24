using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers; // 4 odgovora
        public int correctAnswer; // index 0-3
    }

    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public TextMeshProUGUI[] answerTexts;

    public Question[] questions;

    // DODANO: klik zvuk za dugmad u kvizu
    [SerializeField] private AudioClip clickClip;

    private int currentQuestion = 0;
    private int score = 0;
    private bool answered = false;

    void Start()
    {
        ShowQuestion();
    }

    void ShowQuestion()
    {
        answered = false;

        Question q = questions[currentQuestion];
        questionText.text = q.questionText;

        for (int i = 0; i < 4; i++)
        {
            int index = i; // VAŽNO zbog closure-a

            // reset teksta
            answerTexts[i].text = q.answers[i];

            // reset boje i stanja
            answerButtons[i].image.color = new Color(1f, 0.302f, 0.302f);
            answerButtons[i].interactable = true;

            // ?? OVO JE BITNO
            answerButtons[i].onClick.RemoveAllListeners();

            // DODANO: pusti click zvuk pa onda obradi odgovor
            answerButtons[i].onClick.AddListener(() =>
            {
                if (SfxPlayer.Instance != null && clickClip != null)
                    SfxPlayer.Instance.Play(clickClip);

                Answer(index);
            });
        }
    }

    public void Answer(int index)
    {
        if (answered) return;
        answered = true;

        // zaklju?aj dugmad
        foreach (var btn in answerButtons)
            btn.interactable = false;

        // ta?an odgovor ? zeleno
        answerButtons[questions[currentQuestion].correctAnswer]
            .image.color = Color.green;

        // ako je pogrešan ? crveno
        if (index != questions[currentQuestion].correctAnswer)
        {
            answerButtons[index].image.color = Color.red;
        }
        else
        {
            score++;
        }

        StartCoroutine(NextQuestionDelay());
    }

    IEnumerator NextQuestionDelay()
    {
        yield return new WaitForSeconds(0.8f);

        currentQuestion++;

        if (currentQuestion < questions.Length)
        {
            ShowQuestion();
        }
        else
        {
            string poruka = "";

            if (score == 3)
            {
                poruka = "Čestitamo!\nOdlično poznaješ biljku Ciniju.";
            }
            else if (score == 2)
            {
                poruka = "Super rezultat!\nMože još malo bolje.";
            }
            else if (score == 1)
            {
                poruka = "Pokušaj opet.\nObrati pažnju na ključne pojmove.";
            }
            else
            {
                poruka = "Prođi ponovo kroz edukativni sadržaj.\nZnanje dolazi učenjem.";
            }

            questionText.text =
                "Rezultat: " + score + "/" + questions.Length + "\n\n" + poruka;

            foreach (var btn in answerButtons)
                btn.gameObject.SetActive(false);
        }
    }
}
