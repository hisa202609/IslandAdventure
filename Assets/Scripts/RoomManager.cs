using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    //シーンをまたぐ時、何番出口だったかのかを記憶していくstatic変数
    public static int currentDoorNumber = 0;

    void Start()
    {
        //シーン名取得
        string sceneName = SceneManager.GetActiveScene().name;
        //取得した名前に応じてBGM切り替えをする
        if(sceneName == "BossStage")
        {
            SoundManager.soundManager.PlayBgm(BGMType.InBoss);
        }
        else
        {
            SoundManager.soundManager.PlayBgm(BGMType.InGame);
        }

        if (SaveDataManager.isStartFromSavePoint)
        {
            SaveDataManager.isStartFromSavePoint = false;

            GameObject player =
                GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                player.transform.position =
                    SaveDataManager.GetSavePointPosition();
            }
        }
        else
        {

            //全Exitタグを調べて該当したGameObjectを全て配列に格納
            GameObject[] exits = GameObject.FindGameObjectsWithTag("Exit");

            //配列の中から、currentDoorNumberと同じ番号の出口を探す
            foreach (GameObject exitObj in exits)
            {
                if (currentDoorNumber == exitObj.GetComponent<Exit>().doorNumber)
                {
                    //番号が一致したExitオブジェクトのX座標とY座標
                    float x = exitObj.transform.position.x;
                    float y = exitObj.transform.position.y;

                    //プレイヤーが居るべき座標として加工する
                    switch (exitObj.GetComponent<Exit>().direction)
                    {
                        case Direction.up:
                            y += 1.0f;
                            break;
                        case Direction.down:
                            y -= 1.0f;
                            break;
                        case Direction.left:
                            x -= 1.0f;
                            break;
                        case Direction.right:
                            x += 1.0f;
                            break;
                    }

                    //シーン上に居るプレイヤーを探してくる
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (player != null)
                    {
                        //プレイヤーを加工した座標に配置
                        player.transform.position = new Vector2(x, y);
                    }
                    break; //該当するExitが見つかったのでforeachを抜ける
                }
            }
        }
    }
}
