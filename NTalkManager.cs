using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NTalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;//실질적인 데이ㅣ터베이스..?
    Dictionary<int, string> nameData;
    Dictionary<int, Sprite> portraitData;
    Dictionary<int, Choice> choiceData;

    public Sprite[] portaritArr;

    private NTalkManager instance;
    //const string URL = "https://docs.google.com/spreadsheets/d/1ezu6SwtRKRhPwIq2cW92QUN2HOQGTbQKvg13g80Deyc/export?format=tsv";
    const string URL = "https://docs.google.com/spreadsheets/d/1ezu6SwtRKRhPwIq2cW92QUN2HOQGTbQKvg13g80Deyc/export?format=tsv&gid=1201976522&rangeA1:C5";

    int lineSize, rowSize;
    string[,] sentence;
    public bool kor;

    private void Awake()
    {
        if (instance == null)
            DontDestroyOnLoad(this.gameObject);
        else
            Destroy(this.gameObject);

        talkData = new Dictionary<int, string[]>();
        nameData = new Dictionary<int, string>();
        portraitData = new Dictionary<int, Sprite>();
        choiceData = new Dictionary<int, Choice>();
        if (kor)
            GenerateKorData();
        else
        GenerateEngData();
        //StartCoroutine(loadData());
    }


    IEnumerator loadData()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();
    
        string data = www.downloadHandler.text;
        //Debug.Log(data);
        string[] line = data.Split('\n');
        lineSize = line.Length;
        rowSize = line[0].Split('\t').Length;
        sentence = new string[lineSize, rowSize];

        for (int i = 0; i <lineSize; i++)
        {
            string[] row = line[i].Split('\t');
            for (int j = 0; j < rowSize; j++)
            {
                sentence[i, j] = row[j];
            }
        }
//Debug.Log(sentence);    
    }
    void GenerateEngData()
    {
        //QuestTalk (퀘스트 번호, 오브젝트 번호)
        // 인트로
        talkData.Add(1000 + 10, new string[] {
            "It's been a year since I lost contact with my sister.:11",
            "After asking around, I found this mansion. Hope she's here....:11",
            "Please, come on in. I'll take you to the second-floor lobby.:20",
            "(That's weird. Everything seems familiar to him. Like he knows who I am...):11"
        });
        //시나리오1
        talkData.Add(2000 + 20, new string[] {
            "Nice to meet you. I'm Pandora. :10",
            "I know it's sudden, but have you happen to see my sister?:10",
            "Her name is Mollie. She looks very much like me.:10",
            "Well...stayed here for a while a few years ago.:20",
            "But, I had no contact with her ever since then.:20",
            "(Lying. She's been missing for only half a year. Something's suspicious.):10",
            "(This is the only place where I can trace her. It's my only hope.):10",
            "(Should I ask him if I could stay here for a while?):10"
        });
        //시나리오1 질문
        choiceData.Add(20 + 2000, new Choice("(Should I ask him if I could stay here for a while?)", new string[] { "Ask him.", "Don't ask" }));
        //시나리오1 응답1
        talkData.Add(20 + 2000 + 100, new string[] {
            "Is it okay for me to stay here for a while?:10",
            "I know it's a nuisance, but I'm desperate to find my sister. :10",
            "....:20",
            "Sure yes. There are plenty of guest rooms. :20",
            "The more flowers, the better it is. Don't you agree?:20",
            "Sebasitian will guide you through the mansion. :20",
            "Make yourself at home.:20"
        });
        //시나리오1 응답2
        talkData.Add(20 + 2000 + 200, new string[] {
            "(What now...I can't give up on her like this.):11",
            "If you don't mind, you can stay here.:20",
            "We'll do our best to help you, my lady. Whatever that you need, just tell us.:20",
            "Thanks. That's so nice of you.:10",
            "It's my pleasure. The more flowers, the better it is.                              :20",
            "Have a seat. Sebatiain will be here soon.:20",
        });

        //시나리오 2
        talkData.Add(30 + 3000, new string[] {
            "Hi! You must be the one looking for...:30",
            "I'm Pandora. Good to see you.:10",
            "Hey, anyway,:30",
            "........?:11",
            "Isn't this mansion so nice? as well as the house owner.  :30"
        });
        choiceData.Add(30 + 3000, new Choice("Isn't this mansion so nice? as well as the house owner.  ",
            new string[] { "Well...", "Indeed." }));
        talkData.Add(100 + 30 + 3000, new string[] {
            "You don't know anything, huh? :30",
            "When will we ever have a chance to stay in this such magnificent house without his generosity?:30",
            "You'll understand what I mean when you get older.:30",
            "Why don't you thank him, now ey?:30",
            "...........:10",
            "크흠... 궁금한 게 있으면 물어봐도 좋아.:30"

        });

        talkData.Add(200 + 30 + 3000, new string[] {
            "You're right. I would do anything to live in a mansion like this.:30",
            "Have you seen through the mansion yet?:30",
            "You'll be using the room in the middle on the 1st floor. 한량 stayed in the next room.:30",
            "There is a lobby, a dining room, and a gallery on the 2nd floor.:30",
            "The house-owner uses the third floor alone. There should be a library and a bedroom.:30",
            "명심하자. 집주인이 사용하는 3층은 각별히 조심해야겠어. :10",
            "So, what is this house-owner like? :10",
            "OMG. I can't believe I left out the most important part. :30",
            " He is the most important being for the mansion and even the world.:30",
            "There are rumors that he was a royal or a member of a mafia. :30",
            "Well, I think, it’s more that he was a German spy during the war. :30",
            "(쓸모있는 대답은 아니군.):10",
            "하하. 농담이야. :30",
            "가끔 무섭지만 대체로 자상하고, 규칙을 중요하게 여기지.:30",
            "너도 그를 사랑하게 될거야. :30",
            "자! 한 번 둘러봐봐. 궁금한 게 있다면 언제든지 물어봐도 좋아! :30"
        });

        talkData.Add(3000, new string[] { "저 벽에 액자 보이지? 읽어보는게 좋을걸.:30" }); //라코스테 기본(고유)대사

        talkData.Add(40 + 30000, new string[] { "'첫번째, 지하실에 들어가지 않는다.'", "'두번째, 밤 늦게 돌아다니지 않는다.'라고 적혀있다." });

        talkData.Add(40 + 3000, new string[] {
            "벽에 규칙이 적혀있던데, 규칙을 어기면 어떻게 되는거야?:10",
            "규칙을 어겨본 적이 없어서 잘 모르겠네. :30",
            "설마 집주인을 배신할 생각은 아니지?:30",
            "푸른수염은 생각보다 더 무서운 사람이거든.:30",
            "벽에 적힌 규칙을 명심해!:30",
            "1층의 네 방으로 가봐. 엘레베이터는 나가서 왼쪽에 있어. :30",

        });



        portraitData.Add(10 + 0, portaritArr[0]);
        portraitData.Add(10 + 1, portaritArr[1]);
        portraitData.Add(10 + 2, portaritArr[2]);
        portraitData.Add(20 + 0, portaritArr[3]);
        portraitData.Add(20 + 1, portaritArr[4]);
        portraitData.Add(20 + 2, portaritArr[5]);
        portraitData.Add(30 + 0, portaritArr[6]);

        nameData.Add(10, "한도아");
        nameData.Add(20, "푸른수염");
        nameData.Add(30, "라코스테");
        nameData.Add(40, "오필리어");
    }
    
    void GenerateKorData()
    {


        //일반대사
        talkData.Add(1000, new string[] { "..... :10" }); //없으면 오류나서 넣어둔 것..
        talkData.Add(2000, new string[] { "오늘따라 꽃향기가 좋군요:20" });

        //물건 조사
        talkData.Add(100, new string[] { "오 캡틴 마이 캡틴?" });
        talkData.Add(10000, new string[] { "너무 멀리 온 것 같다","...","되돌아가자" });
        talkData.Add(20000, new string[] { "고급지고 푹신해보이는 소파다.","음...", "앉기엔 너무 하얗다." });
        talkData.Add(20001, new string[] { "음...", "앉기엔 너무 하얗다." });
        talkData.Add(25000, new string[] { "빛이 너무 강해서 밖이 보이지 않는다","..?", "그게 가능한가?" });
        talkData.Add(30000, new string[] { "'첫번째, 지하실에 들어가지 않는다.'", "'두번째, 밤 늦게 돌아다니지 않는다.'라고 적혀있다." });

        //QuestTalk (퀘스트 번호, 오브젝트 번호)
        // 인트로
        talkData.Add(1000 + 10, new string[] { 
            "동생과 연락이 끊긴 지 벌써 1년이 지났다.:11",
            "수소문해서 알게 된 이 저택. 부디 이곳에서는 동생을 찾을 수 있기를.....:11",
            "들어오시죠. 2층으로 안내하겠습니다.:20",
            "(내가 누군지 말하지도 않았는데...? 모든 일이 익숙한 듯 보인다.):11" 
        });
        //시나리오1
        talkData.Add(2000 + 20, new string[] {
            "안녕하세요. 저는 한도아라고 합니다.:10",
            "갑작스러운 걸 알지만.. 혹시 제 동생을 보신 적이 있으신가요?.:10",
            "이름은 한량이고, 저랑 무척 닮았어요.:10",
            "음...글쎄요..몇 년 전에 저택에서 잠깐 지낸 적은 있지만..:20",
            "그 이후에는 저조차 연락이 닿지 않아서요.:20",
            "(거짓말이다. 동생이 없어진지는 반년밖에 되지 않았어. 뭔가 수상해):10",
            "(동생의 흔적이 남은 곳. 이곳이 동생을 찾을 유일한 희망이야.):10",
            "(이곳에서 며칠 지낼 수 있냐고 물어볼까?):10"
        });
        //시나리오1 질문
        choiceData.Add(20 + 2000, new Choice("(이곳에서 며칠 지낼 수 있냐고 물어볼까?)", new string[] { "부탁한다", "부탁하지 않는다"}));
        //시나리오1 응답1
        talkData.Add(20 + 2000 + 100, new string[] {
            "혹시 이곳에서 며칠 지내며 동생을 찾을 수 있을까요?:10",
            "실례인걸 알지만..정말로 절박하거든요.:10",
            "....:20",
            "물론입니다. 손님방도 여러 개 있고..:20",
            "꽃은 많을 수록 좋으니까요.:20",
            "저택 안내는 라코스테가 해줄 겁니다.:20",
            "부디 편하게 계세요.:20"
        });
        //시나리오1 응답2
        talkData.Add(20 + 2000 + 200, new string[] {
            "(이제 어쩌지...이대로 동생을 포기할 수는 없어.):11",
            "혹시 괜찮으시다면, 저택에서 지내셔도 괜찮습니다.:20",
            "저희도 최대한 도울테니까요. 숙녀분이 찾는 게 무엇이든.:20",
            "감사합니다. 정말 친절하시네요.:10",
            "뭘요. 꽃은 많을 수록 좋으니까요.:20",
            "앉아 계세요. 라코스테가 곧 내려올 겁니다.:20",
        });

        //시나리오 2 - 라코스테 시나리오(규칙 볼 때까지 반복)
        talkData.Add(30 + 3000, new string[] { 
            "안녕! 네가 그 누굴 찾으러 왔다던...:30",
            "한도아야. 만나서 반가워.:10",
            "그래 그건 그렇고 말이야:30",
            "........?:11",
            "이 저택 너무 멋있지 않아? 집주인도 마찬가지고. :30"
        });
        choiceData.Add(30 + 3000, new Choice("이 저택 너무 멋있지 않아? 집주인도 마찬가지고. ",
            new string[] { "글쎄", "그러게" }));
        talkData.Add(100 + 30 + 3000, new string[] {
            "네가 세상을 아직 몰라서 그래. :30",
            "푸른 수염이 아니였다면 우리가 어떻게 이런 으리으리한 집에서 살 수 있겠어?:30",
            "네가 조금만 더 나이를 먹으면 날 이해하게 될거야.:30",
            "푸른 수염에게 감사하는 마음을 좀 가져보면 어때?:30",
            "...........:10",
            "크흠... 궁금한 게 있으면 물어봐도 좋아.:30"

        });

        talkData.Add(200 + 30 + 3000, new string[] {
            "그치. 난 이런 집을 가지기 위해서라면 뭐든 할거야.:30",
            "저택은 조금 둘러봤니?:30",
            "넌 아마 1층 가운데 방을 쓰게 되겠지. 네방 옆에 한량이 쓰던 방도 있어.:30",
            "2층은 로비와 다이닝룸, 갤러리가 있어.:30",
            "3층은 푸른수염이 단독으로 사용해. 서재랑 침실이 있을거야.:30",
            "명심하자. 집주인이 사용하는 3층은 각별히 조심해야겠어. :10",
            "집주인은 어떤 사람이야? :10",
            "맙소사. 가장 중요한 걸 잊어버리다니. :30",
            " 그는 이 저택에서,  그리고 이 세계에서 제일 중요한 사람이지.:30",
            "그가 왕족이었다는 소문도 있고, 누구는 그가 마피아라고도 해. :30",
            "(쓸모있는 대답은 아니군.):10",
            "하하. 농담이야. :30",
            "가끔 무섭지만 대체로 자상하고, 규칙을 중요하게 여기지.:30",
            "너도 그를 사랑하게 될거야. :30",
            "자! 한 번 둘러봐봐. 궁금한 게 있다면 언제든지 물어봐도 좋아! :30"
        });

        talkData.Add(3000, new string[] { "저 벽에 액자 보이지? 읽어보는게 좋을걸.:30" }); //라코스테 기본(고유)대사

        talkData.Add(40 + 30000, new string[] { "'첫번째, 지하실에 들어가지 않는다.'","'두번째, 밤 늦게 돌아다니지 않는다.'라고 적혀있다."});

        talkData.Add(40 + 3000, new string[] {
            "벽에 규칙이 적혀있던데, 규칙을 어기면 어떻게 되는거야?:10",
            "규칙을 어겨본 적이 없어서 잘 모르겠네. :30",
            "설마 집주인을 배신할 생각은 아니지?:30",
            "푸른수염은 생각보다 더 무서운 사람이거든.:30",
            "벽에 적힌 규칙을 명심해!:30",
            "1층의 네 방으로 가봐. 엘레베이터는 나가서 왼쪽에 있어. :30",

        });

        //시나리오 끗

        //초상화 이미지
        portraitData.Add(10+0,portaritArr[0]);
        portraitData.Add(10+1, portaritArr[1]);
        portraitData.Add(10+2, portaritArr[2]);
        portraitData.Add(20+0, portaritArr[3]);
        portraitData.Add(20+1, portaritArr[4]);
        portraitData.Add(20+2, portaritArr[5]);
        portraitData.Add(30+0, portaritArr[6]);

        nameData.Add(10, "한도아");
        nameData.Add(20, "푸른수염");
        nameData.Add(30, "라코스테");
        nameData.Add(40, "오필리어");

        
    }


     public string GetTalk(int id, int talkIndex)
    {
        //예외처리
        if (!talkData.ContainsKey(id))
        {
            Debug.Log(id - id % 10);
            //응급조치
            //return GetTalk(id - id % 100, talkIndex);//기본대사 출력

            if (!talkData.ContainsKey(id - id % 10))//퀘스트 맨 처음 대사가 없다.
            {
                return GetTalk(id - id % 100, talkIndex);//기본대사 출력
            }
            else
            { //다음 퀘스트 안내 출력해야함.
                return GetTalk(id - id % 10, talkIndex);
            }
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int portraitIndex)
    {
        return portraitData[portraitIndex];
    }
    
    public string GetName(int id)
    {
        return nameData[id];
    }

    public Choice GetChoice(int ChoiceNumber)
    {
        if (!choiceData.ContainsKey(ChoiceNumber))
        {
            //Debug.Log(ChoiceNumber + ":  선택지 없음");
            return null;
        }   
        else
            return choiceData[ChoiceNumber];
    }

    public void SkipTalking(int answerNum)
    {

    }
}
